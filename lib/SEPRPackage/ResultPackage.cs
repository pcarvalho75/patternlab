using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.FastaParser;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.ComponentModel;

namespace SEPRPackage
{
    [Serializable]
    public class ResultPackage : INotifyPropertyChanged

    {
        //Implement the interface
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        //Add the properties
        public ProteinManager MyProteins { get; set; }
        public Parameters MyParameters { get; set; }
        public string Database {get; set;}
        public string SQTDirectory {get; set;}
        public FDRStatistics.FDRResult MyFDRResult { get; set; }

        
        /// <summary>
        /// Used Machine Learning Lock Mass Correction
        /// </summary>
        public bool LockMass {
            get { return lockMass; }
        }
        private bool lockMass;


        /// <summary>
        /// This is obtained dynamically so don't call this oftenly
        /// </summary>
        public List<string> AllPeptideSequences
        {
            get
            {
                return (MyProteins.AllCleanedPeptideSequences);
            }

        }

        public ResultPackage()
        {
        }


        public ResultPackage
            (ProteinManager myProteins,
            Parameters parameters,
            string databaseUsed,
            string sqtDirectory,
            bool lockMass
            )
        {
            this.lockMass = lockMass;
            this.MyProteins = myProteins;
            this.MyParameters = parameters;
            this.Database = databaseUsed;
            this.SQTDirectory = sqtDirectory;

            this.MyFDRResult = FDRStatistics.GenerateFDRStatistics(myProteins, MyParameters);
        }

        public void Save(string fileName)
        {
            //Refresh statistics in case anything has changed
            MyFDRResult = FDRStatistics.GenerateFDRStatistics(MyProteins, MyParameters);

            //Serialize it!
            
            System.IO.FileStream flStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            bf.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesAlways;
            bf.Serialize(flStream, this);
            flStream.Close();
        }

        public static ResultPackage Load(string fileName)
        {
            //De-serialize it!
            
            System.IO.FileStream flStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            ResultPackage newRP = (ResultPackage)bf.Deserialize(flStream);
            flStream.Close();
            return newRP;
        }


        public List<MyProtein> MaxParsimonyList()
        {
            //int proteinCounter = 0;

            //Lets generate an array that represents a list of proteins according to the MaxParsimony
            List<MyProtein> MaxParsimony = new List<MyProtein>(MyProteins.TheGroups.Count);
            List<MyProtein> ProteinsTMP = PatternTools.ObjectCopier.Clone(MyProteins.MyProteinList);


            foreach (int groupNumber in MyProteins.TheGroups)
            {
                List<MyProtein> proteins = ProteinsTMP.FindAll(a => a.GroupNo == groupNumber);
                List<MyProtein> minumumCoverSet = new List<MyProtein>();

                proteins.Sort((a, b) => b.Length.CompareTo(a.Length));
                if (proteins[0].MyGroupType == ProteinGroupType.All || proteins[0].MyGroupType == ProteinGroupType.Unique || proteins[0].MyGroupType == ProteinGroupType.Single)
                {
                    //proteinCounter++;
                    minumumCoverSet.Add(proteins[0]);
                    proteins.RemoveAt(0);
                    

                    //Add for remaining proteins
                    if (proteins.Count > 0)
                    {
                        string add = "; Additional IDs concatenated into MaxParsimony group: ";
                        add += string.Join(", ", proteins.Select(a => a.Locus).ToList());
                        minumumCoverSet[0].Description += add;
                    }

                    MaxParsimony.AddRange(minumumCoverSet);

                    continue;
                }


                //We are dealing with a "some group"; lets generate the bipartite graph

                List<string> peptides = (from p in proteins
                                         from s in p.DistinctPeptides
                                         select s).Distinct().ToList();

                proteins = (from p in proteins
                            select p).OrderByDescending(p => p.DistinctPeptides.Count).ThenByDescending(p => p.Length).ToList();

                //proteins.Sort((a,b) => b.DistinctPeptides.Count.CompareTo(a.DistinctPeptides.Count));

                //At each step, we will chose the item that contains the largest number of uncovered results
                // http://en.wikipedia.org/wiki/Set_cover_problem

                
                //minumumCoverSet.Add(proteins[0]);
                //MaxParsimony.Add(proteins[0]);
                //proteins.RemoveAt(0);


                //Now, lets cycle until we have the full set complete
                try
                {
                    while (true)
                    {
                        //Get the covered peptides
                        List<string> coveredPeptides = (from p in minumumCoverSet
                                                        from pep in p.DistinctPeptides
                                                        select pep).Distinct().ToList();


                        
                        
                        //Find the maximum coverage possible
                        int maxCoverage = proteins.Max(a => a.DistinctPeptides.Intersect(peptides).Distinct().Count());

                        //find the item from the proteinNodes that will provide the greatest additional cover
                        MyProtein nextProtein = proteins.Find(a => a.DistinctPeptides.Intersect(peptides).Distinct().Count() == maxCoverage);

                        //add it to the minimum cover set
                        //MaxParsimony.Add(nextProtein);
                        minumumCoverSet.Add(nextProtein);

                        //remove it from the protein nodes
                        proteins.Remove(nextProtein);

                        //check if we still need to converge
                        //Get the covered peptides
                        coveredPeptides = (from p in minumumCoverSet
                                           from pep in p.DistinctPeptides
                                           select pep).Distinct().ToList();

                        peptides = peptides.Except(coveredPeptides).ToList();

                        if (proteins.Count == 0 || coveredPeptides.Count == peptides.Count || peptides.Count == 0)
                        {
                            break;
                        }
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine("Max parsimony problem on group number: " + groupNumber + "\n" + e.Message);
                }
                
                //Add for remaining proteins
                if (proteins.Count> 0) 
                {
                    string add = "; Additional IDs concatenated into MaxParsimony group: ";
                    add += string.Join(", ", proteins.Select(a => a.Locus).ToList());
                    minumumCoverSet[0].Description += add;
                }

               
                MaxParsimony.AddRange(minumumCoverSet);

            }

            return MaxParsimony;

            //return proteinCounter;

        }


        public List<MyProtein> MaxParsimonyList_Old()
        {
            //int proteinCounter = 0;

            //Lets generate an array that represents a list of proteins according to the MaxParsimony
            List<MyProtein> MaxParsimony = new List<MyProtein>(MyProteins.TheGroups.Count);
            
            foreach (int groupNumber in MyProteins.TheGroups)
            {
                List<MyProtein> proteins = MyProteins.MyProteinList.FindAll(a => a.GroupNo == groupNumber);


                if (proteins[0].MyGroupType == ProteinGroupType.All || proteins[0].MyGroupType == ProteinGroupType.Unique || proteins[0].MyGroupType == ProteinGroupType.Single)
                {
                    //proteinCounter++;
                    MaxParsimony.Add(proteins[0]);
                    continue;
                }

                if (proteins[0].MyGroupType == ProteinGroupType.Some && proteins.Count == 2)
                {
                    //proteinCounter += 2;
                    MaxParsimony.AddRange(proteins);
                    continue;
                }

                //We are dealing with a "some group"; lets generate the bipartite graph

                List<string> peptides = (from p in proteins
                                         from s in p.DistinctPeptides
                                         select s).Distinct().ToList();

                proteins = (from p in proteins
                            select p).OrderByDescending(p => p.DistinctPeptides).ThenByDescending(c=> c.Length).ToList();
                
                //proteins.Sort((a,b) => b.DistinctPeptides.Count.CompareTo(a.DistinctPeptides.Count));

                //At each step, we will chose the item that contains the largest number of uncovered results
                // http://en.wikipedia.org/wiki/Set_cover_problem

                List<MyProtein> minumumCoverSet = new List<MyProtein>();
                //minumumCoverSet.Add(proteins[0]);
                MaxParsimony.Add(proteins[0]);
                proteins.RemoveAt(0);


                //Now, lets cycle until we have the full set complete
               try
               {
                while (true)
                {
                    //Get the covered peptides
                    List<string> coveredPeptides = (from p in minumumCoverSet
                                                    from pep in p.DistinctPeptides
                                                    select pep).Distinct().ToList();

                    //Find the maximum coverage possible

                        int maxCoverage = proteins.Max(a => a.DistinctPeptides.Union(coveredPeptides).Distinct().Count());

                        //find the item from the proteinNodes that will provide the greatest additional cover
                        MyProtein nextProtein = proteins.Find(a => a.DistinctPeptides.Union(coveredPeptides).Distinct().Count() == maxCoverage);

                        //add it to the minimum cover set
                        MaxParsimony.Add(nextProtein);
                        //minumumCoverSet.Add(nextProtein);

                        //remove it from the protein nodes
                        proteins.Remove(nextProtein);

                        //check if we still need to converge

                        if (proteins.Count == 0 || coveredPeptides.Count == peptides.Count)
                        {
                            break;
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Max parsimony problem on group number: " + groupNumber + "\n" + e.Message);
                }

                //proteinCounter += minumumCoverSet.Count;

            }

            return MaxParsimony;

            //return proteinCounter;

        }


        //Properly handles removing a protein and updating FDR's, peptide lists, scan lists
        public void RemoveProtein(List<MyProtein> proteins)
        {
            //For each of this protein's peptide lets see if this peptide still maps to some other protein, or else we should remove it.

            foreach (MyProtein p in proteins)
            {
                MyProteins.MyProteinList.Remove(p);
                List<string> remainingProteins = MyProteins.MyProteinList.Select(a => a.Locus).ToList();


                List<PeptideResult> peptidesToRemove = new List<PeptideResult>();
                foreach (PeptideResult pr in p.PeptideResults)
                {
                    if (pr.MyMapableProteins.Intersect(remainingProteins).ToList().Count == 0)
                    {
                        peptidesToRemove.Add(pr);
                    }
                }

                foreach (PeptideResult pr in peptidesToRemove)
                {
                    PeptideResult pr2 = MyProteins.MyPeptideList.Find(a => a.PeptideSequence.Equals(pr.CleanedPeptideSequence));
                    bool removed = MyProteins.MyPeptideList.Remove(pr2);
                }
            }

            MyProteins.RebuildScansFromProteins();
            MyFDRResult = FDRStatistics.GenerateFDRStatistics(MyProteins, MyParameters);
            NotifyPropertyChanged("MyProteinList");




        }



    }
}
