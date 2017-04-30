using HMMVerifier;
using HMMVerifier.HMM;
using PatternTools;
using PatternTools.FastaParser;
using PepExplorer2.DeNovo;
using PepExplorer2.Result2;
using SEPRPackage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Regrouper.Dominomics
{

    public class Dominomics
    {
        static HMMVerifier.BatchDomainFetcher bdf; // for dominomics
        static Dictionary<string, string> locusFastaDict; //for dominoics
        static Dictionary<int, string> matrixClassDescriptionDictionary;
        static List<FileInfoResultPackage> myResultPackages;
        public static int Progress { get; set; }
        InputFormat MyInputFormat;

        //Stuff for output
        List<PatternTools.SparseMatrixIndexParserV2.Index> indexesWithoutValues;
        PatternTools.SparseMatrixIndexParserV2 indexParserClean;
        SparseMatrix sparseMatrixClean;

        public Dominomics(MultipleDirectorySelector mds, InputFormat inputF)
        {
            Progress = 0;
            MyInputFormat = inputF;

            //Read all SEPro Files to obtain a dictionary of ID (key) and fasta (value) and peptide (key) SeproFiles (values)

            locusFastaDict = new Dictionary<string, string>();
            myResultPackages = new List<FileInfoResultPackage>();


            matrixClassDescriptionDictionary = new Dictionary<int, string>();
            foreach (DirectoryClassDescription myDir in mds.MyDirectoryDescriptionDictionary)
            {

                if (inputF == InputFormat.SEPro)
                {
                    FileInfo[] resultFilteredFiles = new DirectoryInfo(myDir.MyDirectoryFullName).GetFiles("*.sepr", SearchOption.AllDirectories);
                    FeedLocusFastaDictSEPro(myDir, resultFilteredFiles);
                }
                else if (inputF == InputFormat.MPex)
                {
                    FileInfo[] resultFilteredFiles = new DirectoryInfo(myDir.MyDirectoryFullName).GetFiles("*.mpex", SearchOption.AllDirectories);
                    FeedLocusFastaDictPex(myDir, resultFilteredFiles);
                }
                matrixClassDescriptionDictionary.Add(myDir.ClassLabel, myDir.Description);

            }

            bdf = new HMMVerifier.BatchDomainFetcher();

        }

        public void DoWork(int DomainIEvalue, int DomainEValue)
        {
            bdf.Fetch(locusFastaDict, true, 5, DomainIEvalue, DomainIEvalue);
            while (BatchDomainFetcher.Percent < 100)
            {
                Thread.Sleep(1000);
            }
            return;
        }


        private SparseMatrix GenerateDominomicsSparseMatrix(List<FileInfoResultPackage> myResultPackages, PatternTools.SparseMatrixIndexParserV2 indexParser)
        {
            SparseMatrix sm = new SparseMatrix();


            //foreach (FileInfoResultPackage thisRP in myResultPackages)
            //{
            //    Console.WriteLine("Processing sparse matrix row for " + thisRP.MyFileInfo.FullName);

            //    List<int> dims = new List<int>();
            //    List<double> values = new List<double>();

            //    foreach (SparseMatrixIndexParserV2.Index index in indexParser.TheIndexes)
            //    {

            //        List<HMMResult> hmms = HMMVerifier.BatchDomainFetcher.Results.FindAll(a => a.TargetNamek__BackingField.Equals(index.Name));

            //        //We can transform this into a concurrent bag and paralelize the loop below
                    
            //        if (MyInputFormat == InputFormat.SEPro)
            //        {
            //            List<PeptideResult> candidatesInDomain = new List<PeptideResult>();
                        
            //            foreach (HMMResult hmmr in hmms)
            //            {
            //                string fastaSeq = locusFastaDict[hmmr.QNamek__BackingField];

            //                ResultPackage seproRP = (ResultPackage)thisRP.MyResultPackage;
            //                foreach (PeptideResult pr in seproRP.MyProteins.MyPeptideList)
            //                {
            //                    MatchCollection mc = Regex.Matches(fastaSeq, PatternTools.pTools.CleanPeptide(pr.CleanedPeptideSequence, true));

            //                    foreach (Match m in mc)
            //                    {
            //                        if (m.Index + m.Length > hmmr.AFromk__BackingField && hmmr.ATok__BackingField > m.Index)
            //                        {
            //                            candidatesInDomain.Add(pr);
            //                            break;
            //                        }
            //                    }
            //                }

            //            }

            //            candidatesInDomain = candidatesInDomain.Distinct().ToList();

            //            if (candidatesInDomain.Count > 0)
            //            {
            //                dims.Add(index.ID);
            //                values.Add(candidatesInDomain.Sum(a => a.MyScans.Count));
            //            }
            //        }
            //        else if (MyInputFormat == InputFormat.MPex)
            //        {
            //            List<AlignmentResult> candidatesInDomain = new List<AlignmentResult>();

            //            ResultPckg2 pexRP = (ResultPckg2)thisRP.MyResultPackage;

                                               
            //            foreach (AlignmentResult aln in pexRP.Alignments) //Lets cycle through the good alignments
            //            {
            //                bool secondBreak = false;
            //                bool thirdBreak = false;

            //                foreach (HMMResult hmmr in hmms)
            //                {

            //                    foreach (Alignment al in aln)
            //                    {
            //                        string pepSeq = Regex.Replace(new string(al.Sequence1), "-", "");
            //                        string fastaSeq = locusFastaDict[hmmr.QNamek__BackingField];

            //                        MatchCollection mc = Regex.Matches(fastaSeq, pepSeq);

            //                        foreach (Match m in mc)
            //                        {
            //                            if (m.Index + m.Length > hmmr.AFromk__BackingField && hmmr.ATok__BackingField > m.Index)
            //                            {
            //                                candidatesInDomain.Add(aln);
            //                                secondBreak = true;
            //                                thirdBreak = true;
            //                                break;
            //                            }
            //                        }

            //                        if (secondBreak)
            //                        {
            //                            break;
            //                        }

            //                    }

            //                    if (thirdBreak)
            //                    {
            //                        break;
            //                    }
            //                }
                            
            //            }

            //            if (candidatesInDomain.Count > 0)
            //            {
            //                dims.Add(index.ID);
            //                values.Add(candidatesInDomain.Count);
            //            }
            //        }

            //    }


            //    sparseMatrixRow smr = new sparseMatrixRow(thisRP.ClassLabel, dims, values);
            //    smr.FileName = thisRP.MyFileInfo.FullName;

            //    sm.addRow(smr);

            //}

            return sm;
        }

        public void PrepareIndexAndSparseMatrix()
        {
            var groupedHMM = from thisResult in HMMVerifier.BatchDomainFetcher.Results
                             group thisResult by thisResult.TargetNamek__BackingField into mygroup
                             select new { mygroup.Key, Results = mygroup.ToList() };


            int counter = 0;

            List<SparseMatrixIndexParserV2.Index> tmpIndexes = new List<SparseMatrixIndexParserV2.Index>();
            foreach (var groupedResult in groupedHMM)
            {
                counter++;
                SparseMatrixIndexParserV2.Index i = new SparseMatrixIndexParserV2.Index();
                i.ID = counter;
                i.Name = groupedResult.Key;
                i.Description = groupedResult.Results[0].Descriptionk__BackingField + " IDs:" + string.Join(", ", groupedResult.Results.Select(a => a.QNamek__BackingField).Distinct().ToList());
                tmpIndexes.Add(i);
            }

            PatternTools.SparseMatrixIndexParserV2 tmpIndexParser = new SparseMatrixIndexParserV2(tmpIndexes);
            tmpIndexParser.SortIndexesByID();


            Console.WriteLine("Preparing sparse matrix file");
            //First lets extract the classes


            myResultPackages.Sort((a, b) => a.ClassLabel.CompareTo(b.ClassLabel));

            SparseMatrix tmpSparseMatrix = GenerateDominomicsSparseMatrix(myResultPackages, tmpIndexParser);



            //Find out the dims with no values
            indexesWithoutValues = tmpIndexParser.TheIndexes.FindAll(a => tmpSparseMatrix.ExtractDimValues(a.ID).Count == 0);



            List<PatternTools.SparseMatrixIndexParserV2.Index> cleanIndexes = tmpIndexParser.TheIndexes.Except(indexesWithoutValues).ToList();

            for (int i = 0; i < cleanIndexes.Count; i++)
            {
                cleanIndexes[i].ID = i + 1;
            }

            indexParserClean = new SparseMatrixIndexParserV2(cleanIndexes);
            sparseMatrixClean = GenerateDominomicsSparseMatrix(myResultPackages, indexParserClean);
            sparseMatrixClean.ClassDescriptionDictionary = matrixClassDescriptionDictionary;


            Console.WriteLine("Done");
 
        }

        public void SaveIndexWithNoMappedDomains(string fileName)
        {
            Console.WriteLine("Domains with no mapped peptides: ");
            StreamWriter sw1 = new StreamWriter(fileName);
            foreach (PatternTools.SparseMatrixIndexParserV2.Index i in indexesWithoutValues)
            {
                sw1.WriteLine(i.Name + "\t" + i.Description);
            }
            sw1.Close();
        }

        public void SaveIndex(string fileName)
        {
            indexParserClean.WriteGPI(fileName);
        }

        public void SaveSparseMatrix(string fileName)
        {
            sparseMatrixClean.saveMatrix(fileName);
        }

 

        private void FeedLocusFastaDictSEPro(DirectoryClassDescription myDir, FileInfo[] resultFilteredFiles)
        {
            foreach (FileInfo fi in resultFilteredFiles)
            {
                Console.WriteLine("Parsing " + fi.FullName);

                SEPRPackage.ResultPackage thisPckg = SEPRPackage.ResultPackage.Load(fi.FullName);

                myResultPackages.Add(new FileInfoResultPackage(fi, thisPckg, myDir.ClassLabel));


                foreach (MyProtein protein in thisPckg.MyProteins.MyProteinList)
                {
                    if (!locusFastaDict.ContainsKey(protein.Locus))
                    {
                        locusFastaDict.Add(protein.Locus, protein.Sequence);
                    }
                }

            }
        }

        private void FeedLocusFastaDictPex(DirectoryClassDescription myDir, FileInfo[] resultFilteredFiles)
        {
            foreach (FileInfo fi in resultFilteredFiles)
            {
                Console.WriteLine("Parsing " + fi.FullName);

                ResultPckg2 thisPckg = ResultPckg2.DeserializeResultPackage(fi.FullName);

                myResultPackages.Add(new FileInfoResultPackage(fi, thisPckg, myDir.ClassLabel));

                foreach (FastaItem fastaItem in thisPckg.MyFasta)
                {
                    if (!locusFastaDict.ContainsKey(fastaItem.SequenceIdentifier))
                    {
                        locusFastaDict.Add(fastaItem.SequenceIdentifier, fastaItem.Sequence);
                    }
                }

            }
        }


        internal void SavePatternLabProject(string fileName, string description)
        {
            PLP.PatternLabProject plp = new PLP.PatternLabProject(sparseMatrixClean, indexParserClean, description);
            plp.Save(fileName);
        }
    }
}
