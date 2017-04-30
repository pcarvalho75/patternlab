using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SEPRPackage;
using PatternTools.SQTParser;
using PatternTools.FastaParser;
using PatternTools;

namespace Regrouper.GroupingElements.Parser
{
    class ProteinParser : ParserBase, IParser
    {
        List<ProteinIndexStruct> theIndex = new List<ProteinIndexStruct>();
        public List<ProteinIndexStruct> TheIndex { get { return theIndex; } }
        public bool UseNSAF { get; set; }
        public bool UseMaxParsimony { get; set; }

        public void SavePatternLabProjectFile(string fileName, string projectDescription)
        {
            SparseMatrixIndexParserV2 smip = GenerateIndex();
            SparseMatrix sm = GenerateSparseMatrix();

            PLP.PatternLabProject plp = new PLP.PatternLabProject(sm, smip, projectDescription);
            plp.Save(fileName);
        }
        
        public void SaveIndex(string fileName) {
            SparseMatrixIndexParserV2 smip = GenerateIndex();
            smip.WriteGPI(fileName);
        }

        private SparseMatrixIndexParserV2 GenerateIndex()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < theIndex.Count; i++)
            {
                sb.AppendLine(theIndex[i].Index + "\t" + theIndex[i].Locus + "\t" + theIndex[i].Description);
            }

            SparseMatrixIndexParserV2 smip = new SparseMatrixIndexParserV2(sb.ToString(), true);
            return smip;
        }

        public SparseMatrix GenerateSparseMatrix()
        {
            StringBuilder sb = new StringBuilder();

            //Write the class descriptions
            foreach (DirectoryClassDescription myDir in MyDirectoryDescriptionDictionary)
            {
                sb.AppendLine("#ClassDescription\t" + myDir.ClassLabel + "\t" + myDir.Description);
            }

            foreach (ResultEntry rp in MyResultPackages)
            {
                //Print the file name
                sb.AppendLine("#" + rp.MyFileInfo.FullName);

                //Find out class label
                int label = rp.ClassLabel;
                sb.Append(label);

                //Obtain the nsaf factor
                double nsafDenominator = 0;
                if (UseNSAF)
                {
                    for (int i = 0; i < theIndex.Count; i++)
                    {
                        int proteinIndex = rp.MyResultPackage.MyProteins.MyProteinList.FindIndex(a => a.Locus.Equals(theIndex[i].Locus));

                        if (proteinIndex > -1)
                        {
                            double specCounts = 0;
                            if (MyParameters.MyProteinType == ProteinOutputType.SpecCountsOfAllPeptides)
                            {
                                specCounts = rp.MyResultPackage.MyProteins.MyProteinList[proteinIndex].Scans.Count;
                            }
                            else
                            {
                                specCounts = rp.MyResultPackage.MyProteins.MyProteinList[proteinIndex].Scans.FindAll(a => a.IsUnique).Count();
                            }
                            double nSAFFactor = specCounts / rp.MyResultPackage.MyProteins.MyProteinList[proteinIndex].Length;
                            nsafDenominator += nSAFFactor;
                        }
                    }

                }

                //Write the input vector
                for (int i = 0; i < theIndex.Count; i++)
                {
                    int proteinIndex = rp.MyResultPackage.MyProteins.MyProteinList.FindIndex(a => a.Locus.Equals(theIndex[i].Locus));

                    if (proteinIndex > -1)
                    {
                        double specCounts = 0;

                        if (MyParameters.MyProteinType == ProteinOutputType.SpecCountsOfAllPeptides)
                        {
                            specCounts = rp.MyResultPackage.MyProteins.MyProteinList[proteinIndex].Scans.Count;
                        }
                        else
                        {
                            specCounts = rp.MyResultPackage.MyProteins.MyProteinList[proteinIndex].Scans.FindAll(a => a.IsUnique).Count();
                        }

                        if (UseNSAF)
                        {
                            specCounts = (specCounts / rp.MyResultPackage.MyProteins.MyProteinList[proteinIndex].Length) / nsafDenominator;
                        }

                        int myIndex = i;
                        sb.Append(" " + myIndex + ":" + specCounts);
                    }
                }

                sb.AppendLine("");
            }

            SparseMatrix sm = new SparseMatrix(sb.ToString(), true);
            return sm;

        }


        /// <summary>
        /// This method can only be called after the data has bee processed
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveSparseMatrix (string fileName) 
        {
            SparseMatrix sm = GenerateSparseMatrix();
            sm.saveMatrix(fileName);
        }


        /// <summary>
        /// This eliminates decoys and builds the index
        /// </summary>
        public void ProcessParsedData()
        {
            //Generate a SEPro Fusion---------------------
            ResultPackage seprofusion = FusionSEPro(MyResultPackages.Select(a => a.MyResultPackage).ToList());

            ///-------------------------------------------

            //If we wish to eliminate decoy proteins
            if (MyParameters.EliminateDecoys)
            {
                seprofusion.MyProteins.RemoveDecoyProteins(MyParameters.DecoyTag);
            }

            //we do this in two steps... first we build the index, then, we build the sparse matrix rows

            List<MyProtein> myProteins = seprofusion.MyProteins.MyProteinList;

            if (UseMaxParsimony)
            {
                myProteins = seprofusion.MaxParsimonyList();
            }

            List<TMPProt> result = (from prot in myProteins
                          select new TMPProt(prot.Locus, prot.Description, prot.ContainsUniquePeptide )).ToList();


            //If we only want proteins of unique peptides
            if (MyParameters.MyProteinType == ProteinOutputType.SpecCountsOfUniquePeptides)
            {
                result = (from r in result
                         where r.UniquePeptides > 0
                         select r).ToList();
            }



            theIndex = new List<ProteinIndexStruct>(result.Count());


            for (int i = 0; i < result.Count; i++)
            {
                ProteinIndexStruct p = new ProteinIndexStruct();
                p.Locus = result[i].Locus;
                p.Description = result[i].Description;
                p.Index = i;

                if (!theIndex.Exists(a => a.Locus.Equals(result[i].Locus)))
                {
                    theIndex.Add(p);
                }
                else
                {
                    throw new Exception("Problems generating SEPro fusion file");
                }
            }

            //int counter = 0;
            //foreach (TMPProt r in result)
            //{
            //    counter++;
            //    ProteinIndexStruct p = new ProteinIndexStruct();
            //    p.Locus = r.Locus;
            //    p.Description = r.Description;
            //    p.Index = counter;

            //    if (!theIndex.Exists(a => a.Locus.Equals(r.Locus)))
            //    {
            //        theIndex.Add(p);
            //    }
            //    else
            //    {
            //        counter--;
            //    }
            //}

        }

        private class TMPProt
        {
            public string Locus { get; set; }
            public string Description { get; set; }
            public int UniquePeptides { get; set; }

            public TMPProt(string locus, string description, int uniquePeptides)
            {
                Locus = locus;
                Description = description;
                UniquePeptides = uniquePeptides;
            }
        }

        private ResultPackage FusionSEPro(List <ResultPackage> myPackages )
        {
            List<SQTScan> allScans = new List<SQTScan>();
            SEPRPackage.Parameters theparams = new SEPRPackage.Parameters();


            List<string> allProteinIDs = new List<string>();
            string database = "";
            List<FastaItem> fastaItems = new List<FastaItem>();
            foreach (ResultPackage pkg in myPackages)
            {
                allScans.AddRange(pkg.MyProteins.AllSQTScans);
                theparams = pkg.MyParameters;
                allProteinIDs.AddRange(pkg.MyProteins.MyProteinList.Select(a => a.Locus).ToList());

                foreach (MyProtein p in pkg.MyProteins.MyProteinList)
                {
                    if (!fastaItems.Exists(a => a.SequenceIdentifier.Equals(p.Locus)))
                    {
                        FastaItem fasta = new FastaItem();
                        fasta.Description = p.Description;
                        fasta.Sequence = p.Sequence;
                        fasta.SequenceIdentifier = p.Locus;
                        fastaItems.Add(fasta);
                    }
                }

                database = pkg.Database;
            }



            Console.WriteLine("Generating SEPro Fusion");
            ProteinManager pm = new ProteinManager(allScans, theparams, allProteinIDs.Distinct().ToList());

            pm.CalculateProteinCoverage(fastaItems);
            pm.GroupProteinsHavingCommonPeptides(1);
            ResultPackage rp = new ResultPackage(pm, theparams, database, theparams.SeachResultDirectoy, false);
            rp.MyProteins.RebuildProteinsFromScans();
            Console.WriteLine("Done fusioning the SEPro files.");
            return rp;
        }



        /// <summary>
        /// Eliminates dims that are not contained in the sparse matrix
        /// </summary>
        /// <param name="sm"></param>
        internal void VerifyIntegrity(PatternTools.SparseMatrix sm)
        {
            
            List<ProteinIndexStruct> toBeRemoved = new List<ProteinIndexStruct>();

            foreach (ProteinIndexStruct p in theIndex) {
                bool exists = sm.theMatrixInRows.Exists(a => a.Dims.Contains(p.Index));

                if (!exists)
                {
                    toBeRemoved.Add(p);
                }
            }

            theIndex = theIndex.Except(toBeRemoved).ToList();
        }
    }

    struct ProteinIndexStruct
    {
        public string Locus { get; set; }
        public string Description { get; set; }

        public int Index { get; set; }
    }
}
