using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatternTools.FastaParser;
using PatternTools.SQTParser;
using PatternTools;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace SEPRPackage
{

    [Serializable]
    public class ProteinManager
    {
        public List<MyProtein> MyProteinList { get; set; }
        public List<PeptideResult> MyPeptideList { get; set; }
        public List<SQTScan> AllSQTScans { get; set; }
        public string ProteinDB { get; set; }
        Parameters myParams;

        public List<int> TheGroups
        {
            get
            {
                List<int> theGroups = (from p in MyProteinList
                                       select p.GroupNo).Distinct().ToList();
                return theGroups;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scans"></param>
        /// <param name="theParams"></param>
        /// <param name="allProteins">If you have a previous restriction list of proteins such as in regrouper, this will clean the scan information only to map to these proteins.  However, this result cannot be concatenated with future results so it should only be used by regrouper</param>
        public ProteinManager(List<SQTScan> scans, Parameters theParams, List<string> allProteinIDs)
        {
            myParams = theParams;

            if (allProteinIDs.Count > 0)
            {
                //Parallel.ForEach(scans, scan =>
                foreach (SQTScan scan in scans)
                {
                    //Make sure there are no illegal characters in a name                     
                    scan.IdentificationNames = scan.IdentificationNames.Intersect(allProteinIDs).ToList();
                }
                //);

            }

            StartUp(scans);

        }


        private void StartUp(List<SQTScan> scans)
        {
            AllSQTScans = scans;


            MyProteinList = (from s in scans.AsParallel()
                             from n in s.IdentificationNames
                             group s by n into identifications
                             select new MyProtein(identifications.Key, identifications.ToList())).ToList();


            MyPeptideList = (from s in AllSQTScans.AsParallel()
                             group s by s.PeptideSequenceCleaned into sequenceKey
                             select new PeptideResult(sequenceKey.Key, sequenceKey.ToList())).Distinct().ToList();
        }

        private void RebuildPeptideListFromUpdatedProteinList()
        {
            Console.WriteLine("  Building peptide list from protein list");

            List<string> cleanedSequences = AllCleanedPeptideSequences;


            //Patch pointed out by tiago balbuena to deal with peptides of same sequence but different flanking aa.

            Dictionary<string, List<PeptideResult>>
                seqCounter = (from prot in MyProteinList.AsParallel()
                              from pep in prot.PeptideResults
                              where cleanedSequences.Contains(pep.CleanedPeptideSequence)
                              group pep by pep.CleanedPeptideSequence into g
                              select new { Sequence = g.Key, Peptides = g }).ToDictionary(a => a.Sequence, a => a.Peptides.ToList());

            foreach (KeyValuePair<string, List<PeptideResult>> kvp in seqCounter)
            {
                if (kvp.Value.Count > 1)
                {
                    List<string> dSequences = (from pr in kvp.Value
                                               select pr.PeptideSequence).Distinct().ToList();

                    if (dSequences.Count > 1)
                    {
                        //Lets create a new peptide result
                        List<SQTScan> allScans = new List<SQTScan>();
                        foreach (PeptideResult pr in kvp.Value)
                        {
                            allScans.AddRange(pr.MyScans);
                        }

                        allScans = allScans.Distinct().ToList();

                        PeptideResult surrogate = new PeptideResult(kvp.Key, allScans);
                        kvp.Value.Clear();
                        kvp.Value.Add(surrogate);
                    }
                }
            }



            MyPeptideList = new List<PeptideResult>(seqCounter.Keys.Count);
            foreach (KeyValuePair<string, List<PeptideResult>> kvp in seqCounter)
            {
                MyPeptideList.Add(kvp.Value[0]);
            }

            //RebuildScansFromProteins();

            Console.WriteLine("  Done building peptide list");
        }

        public List<string> AllCleanedPeptideSequences
        {
            get
            {
                List<string> pepSeqs = (from scan in AllSQTScans
                                       select scan.PeptideSequenceCleaned).Distinct().ToList();

                pepSeqs.Sort((a, b) => b.Length.CompareTo(a.Length));

                return pepSeqs;
            }
        }

        public int RemoveDecoyProteins(string decoyTag)
        {
            int i = MyProteinList.RemoveAll(a => a.Locus.StartsWith(decoyTag));
            RebuildScansFromModifiedProteinList();
            return i;
        }

        public void CalculateProteinCoverage(List<FastaItem> fastaItems)
        {

            Parallel.ForEach(MyProteinList, myProtein =>
            //foreach (MyProtein myProtein in MyProteinList)
            {
                //Locate the corrsponding fasta item
                myProtein.Locus = myProtein.Locus.TrimEnd('\r', '\n');
                PatternTools.FastaParser.FastaItem item = fastaItems.Find(a => a.SequenceIdentifier.Equals(myProtein.Locus));

                if (item != null)
                {
                    myProtein.Coverage = Math.Round(item.Coverage(myProtein.DistinctPeptides), 4);
                    myProtein.Length = item.Sequence.Length;
                    myProtein.MolWt = Math.Round(item.MonoisotopicMass, 1);
                    myProtein.Description = item.Description;
                    myProtein.Sequence = item.Sequence;
                }
                else
                {
                    //For some bizarre reason the key was not found
                    Console.WriteLine("ERROR:: + Protein must be found in the database: " + myProtein.Locus);
                    myProtein.Coverage = 0;
                    myProtein.Length = 0;
                    myProtein.MolWt = 0;
                    myProtein.Description = "Description not found in database";
                }
            }
            );

        }

        /// <summary>
        /// Applies the Bayesian Discriminant function at protein level
        /// </summary>
        /// <param name="myParams"></param>
        public void BayesianCleaningAtProteinLevel()
        {
            int minNoExamplesPerClass = 5;

            PatternTools.GaussianDiscriminant.Discriminant gd = new PatternTools.GaussianDiscriminant.Discriminant();
            List<int> dims = new List<int> { 0, 1, 2, 3, 4, 5 };

            int negativeClassExampleCounter = 0;
            foreach (MyProtein p in MyProteinList)
            {
                //Find out what class does this belong
                double label = 1;
                if (p.Locus.StartsWith(myParams.LabeledDecoyTag))
                {
                    label = -1;
                    negativeClassExampleCounter++;
                }

                gd.MySparseMatrix.addRow(new sparseMatrixRow((int)label, dims, p.InputVector));
            }


            //We need to make sure everything is working properly in this new normalization
            //This greately degrades the classifier!!! never use!!!!
            //gd.MySparseMatrix.NormalizeAllColumnsToRangeFrom0To1New();

            //------
            Console.WriteLine("Target examples for protein model = " + gd.MySparseMatrix.theMatrixInRows.FindAll(a => a.Lable == 1).Count);
            Console.WriteLine("Decoy examples for protein model = " + gd.MySparseMatrix.theMatrixInRows.FindAll(a => a.Lable == -1).Count);
            gd.Model(false, new List<int>(), minNoExamplesPerClass, true, false);

            if (gd.ClassLableClassPackageDic.Keys.Count != 2)
            {
                throw new System.ArgumentException("Not enough examples to generate protein classification model.  No available negative datapoints: " + negativeClassExampleCounter);
            }


            Parallel.ForEach(MyProteinList, r =>
            //foreach (Scan s in p.MyScans)
            {
                //The result is ordered by class number
                var results = gd.Classify(r.InputVector.ToArray());
                double BayesianDiference = results[0].Score - results[1].Score;
                r.BayesianScore = BayesianDiference;
            }
            );

            double BayesianMin = MyProteinList.Min(a => a.BayesianScore);
            double BayesianMax = MyProteinList.Max(a => a.BayesianScore);
            double BayesianDif = BayesianMax - BayesianMin;

            MyProteinList.Sort((a, b) => b.BayesianScore.CompareTo(a.BayesianScore));
            int numberOfReverseProteins = MyProteinList.FindAll(a => a.Locus.StartsWith(myParams.LabeledDecoyTag)).Count;
            int numberOfForwardProteins = MyProteinList.FindAll(a => !a.Locus.StartsWith(myParams.LabeledDecoyTag)).Count;

            //Now lets do the filtering
            int cutOffValue = MyProteinList.Count;

            for (cutOffValue = MyProteinList.Count - 1; cutOffValue > 0; cutOffValue--)
            {
                if (MyProteinList[cutOffValue].Locus.StartsWith(myParams.LabeledDecoyTag))
                {
                    numberOfReverseProteins--;
                }
                else
                {
                    numberOfForwardProteins--;
                }
                //Calculate FDR;
                double fdr = (double)numberOfReverseProteins / ((double)numberOfForwardProteins + (double)numberOfReverseProteins);

                if (fdr <= myParams.ProteinFDR)
                {
                    break;
                }
            }

            MyProteinList.RemoveRange(cutOffValue, MyProteinList.Count - cutOffValue);

            //Must cal this method to correct for the removed proteins
            RebuildScansFromModifiedProteinList();

        }

                /// <summary>
        /// Sometimes we can apply filters to the protein object and later we may wish to obtain a fresh scan list of what is left
        /// </summary>
        /// <returns></returns>
        public void RebuildScansFromModifiedProteinList()
        {
            AllSQTScans = (from p in MyProteinList 
                           from s in p.Scans
                           select s).Distinct().ToList();

            //We should also rebuild peptide list
            RebuildPeptideListFromUpdatedProteinList();
            
        }

        //public void EliminateInProteins()
        //{

        //    Console.WriteLine("Eliminating In Proteins");

        //    Console.WriteLine("  Searching for the in proteins");

        //    DateTime beginTime = DateTime.Now;
        //    //Eliminate proteins having only not unique peptides that have all their peptides within another protein that has a unique peptide

        //    ConcurrentBag<MyProtein> toDelete = new ConcurrentBag<MyProtein>();

        //    Parallel.ForEach(MyProteinList, cPtn =>
        //    //foreach (MyProtein cPtn in MyProteinList)
        //    {
        //        var preSearch = MyProteinList.FindAll(a => cPtn.DistinctPeptides.Count < a.DistinctPeptides.Count && a.GroupNo > -1);
        //        if (preSearch.Exists(a => a.DistinctPeptides.Intersect(cPtn.DistinctPeptides).Count() == cPtn.DistinctPeptides.Count))
        //        {
        //            cPtn.GroupNo = -1;
        //            toDelete.Add(cPtn);
        //        }
        //    }
        //    );

        //    Console.WriteLine(MyProteinList.Count);


        //    Console.WriteLine("   " + toDelete.Count + " in proteins detected");

        //    MyProteinList = MyProteinList.Except(toDelete.ToList()).ToList();
        //    Console.WriteLine(MyProteinList.Count);
        //    Console.WriteLine("  In ptns eliminated: " + toDelete.Count + " Time:" + Math.Ceiling((DateTime.Now - beginTime).TotalMilliseconds).ToString() + " ms.");

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minNoPeptides"></param>
        /// <param name="eliminateInPtns"></param>
        /// <param name="displaySinglePtn">goups all epesentatives and only outputs the one with most peptides</param>
        public void GroupProteinsHavingCommonPeptides(int minNoPeptides)
        {
            DateTime beginTime = DateTime.Now;

            //---------------------------------------------------------------------
            Console.WriteLine("Establishing groups");


            List<ProteinGroup2> myProteinGroups = Strategy3(minNoPeptides);


            //Now assign the groups;
            Parallel.ForEach(myProteinGroups, pg =>
            //foreach (ProteinGroup pg in myProteinGroups)
                {
                    pg.AssignGroupTypesToProteins();
                }
            );

            //And now label the group numbers
            for (int i = 0; i < myProteinGroups.Count; i++)
            {
                foreach (MyProtein p in myProteinGroups[i].MyProteins)
                {
                    p.GroupNo = i + 1;
                }
            }       

            Console.WriteLine("Done gouping. Time for grouping:" + Math.Ceiling((DateTime.Now - beginTime).TotalMilliseconds).ToString());

        }


        //-------------------

        private List<ProteinGroup2> Strategy3 (int minNoPeptides)
        {
            List<ProteinGroup2> theGroups = new List<ProteinGroup2>(MyProteinList.Count);


            foreach (MyProtein prot in MyProteinList)
            {
                theGroups.Add(new ProteinGroup2(prot));
            }

            int leftOff = 0;
            
            
            while (true)
            {

                int startSize = theGroups.Count;

                for (int i = leftOff; i < theGroups.Count; i++)
                {
                    List<ProteinGroup2> theMatches = (from g in theGroups
                                                      where g.MyPeptides.Intersect(theGroups[i].MyPeptides).Count() >= minNoPeptides
                                                      select g).ToList();

                    if (theMatches.Count > 1)
                    {
                        List<MyProtein> theProteins = (from groups in theMatches
                                                     from prot in groups.MyProteins
                                                     select prot).ToList();

                        ProteinGroup2 ng = new ProteinGroup2(theProteins);

                        theGroups = theGroups.Except(theMatches).ToList();
                        theGroups.Add(ng);
                        leftOff = i;
                        break;
                    }

                }

                if (theGroups.Count == startSize)
                {
                    break;
                }

            }

            foreach (ProteinGroup2 pg in theGroups)
            {
                pg.AssignGroupTypesToProteins();
            }

            return theGroups;

        }


        //-----------------------------

 

        //-----------------------------------------------------------------------------------------------------

        

        public void RebuildProteinsFromScans()
        {
            //Find the proteins that have
            MyProteinList.AsParallel().ForAll(a => a.Scans = a.Scans.Intersect(AllSQTScans).ToList());
            MyProteinList.RemoveAll(a => a.Scans.Count == 0);
            RebuildPeptideListFromUpdatedProteinList();
        }

        internal void RebuildScansFromProteins()
        {
            List<SQTScan> allScansTmp = new List<SQTScan>(AllSQTScans.Count);

            foreach (MyProtein p in MyProteinList)
            {
                allScansTmp.AddRange(p.Scans);
            }

            AllSQTScans = allScansTmp.Distinct().ToList();
        }

    }
}
