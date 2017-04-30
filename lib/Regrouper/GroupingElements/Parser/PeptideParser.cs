using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using PatternTools;

namespace Regrouper.GroupingElements.Parser
{
    class PeptideParser : ParserBase, IParser
    {
        List<GlobalPeptideResult> globalPeptideIndex = new List<GlobalPeptideResult>();

        public List<GlobalPeptideResult> GlobalPeptideIndex
        {
            get { return globalPeptideIndex; }
            set { globalPeptideIndex = value; }
        }

        public void SaveSparseMatrix(string fileName)
        {
            SparseMatrix sm = GenerateSparseMatrix();
            sm.saveMatrix(fileName);

        }
        public void SaveIndex(string fileName)
        {
            SparseMatrixIndexParserV2 smip = GenerateIndex();
            smip.WriteGPI(fileName);
        }

        public void SavePatternLabProjectFile(string fileName, string projectDescription)
        {
            SparseMatrixIndexParserV2 smip = GenerateIndex();
            SparseMatrix sm = GenerateSparseMatrix();

            PLP.PatternLabProject plp = new PLP.PatternLabProject(sm, smip, projectDescription);
            plp.Save(fileName);

        }

        private SparseMatrix GenerateSparseMatrix()
        {

            //Write the class descriptions
            StringBuilder sb = new StringBuilder();
            foreach (DirectoryClassDescription myDir in MyDirectoryDescriptionDictionary)
            {
                sb.AppendLine("#ClassDescription\t" + myDir.ClassLabel + "\t" + myDir.Description);
            }

            foreach (ResultEntry re in MyResultPackages)
            {
                sb.AppendLine("#" + re.MyFileInfo.FullName);

                //Write the label
                sb.Append(re.ClassLabel);
                for (int i = 0; i < globalPeptideIndex.Count; i++)
                {
                    int index = re.MyResultPackage.MyProteins.MyPeptideList.FindIndex(a => a.CleanedPeptideSequence.Equals(globalPeptideIndex[i].Sequence));
                    int indexNo = i;
                    if (index > -1)
                    {
                        sb.Append(" " + indexNo + ":" + re.MyResultPackage.MyProteins.MyPeptideList[index].MyScans.Count);
                    }
                }
                sb.AppendLine("");
            }

            SparseMatrix sm = new SparseMatrix(sb.ToString(), true);
            return sm;

        }

        private SparseMatrixIndexParserV2 GenerateIndex()
        {
            SparseMatrixIndexParserV2 ipv2 = new SparseMatrixIndexParserV2();

            for (int i = 0; i < globalPeptideIndex.Count; i++)
            {
                SparseMatrixIndexParserV2.Index index = new SparseMatrixIndexParserV2.Index();



                StringBuilder proteinDescription = new StringBuilder();
                for (int j = 0; j < globalPeptideIndex[i].MappableProteins.Count; j++)
                {
                    proteinDescription.Append(" " + globalPeptideIndex[i].MappableProteins[j]);
                }

                string proteinDescriptionReady = proteinDescription.ToString();
                proteinDescriptionReady = Regex.Replace(proteinDescriptionReady, "\n", "");
                proteinDescriptionReady = Regex.Replace(proteinDescriptionReady, "\r", "");


                index.ID = i + 1;
                index.Name = globalPeptideIndex[i].Sequence;
                index.Description = proteinDescriptionReady;

                ipv2.TheIndexes.Add(index);
            }

            return ipv2;
        }

        public void ProcessParsedData(bool considerOnlyUnique)
        {
            //If we wish to eliminate decoy proteins
            if (MyParameters.EliminateDecoys)
            {
                foreach (ResultEntry re in MyResultPackages)
                {
                    re.MyResultPackage.MyProteins.RemoveDecoyProteins(MyParameters.DecoyTag);
                }
            }

            List<string> globalPeptideList = (from re in MyResultPackages
                                              from p in re.MyResultPackage.AllPeptideSequences
                                              select p).Distinct().ToList();

            foreach (string p in globalPeptideList)
            {
                List<List<string>> mappableProteins = (from re in MyResultPackages.AsParallel()
                                                       from pepR in re.MyResultPackage.MyProteins.MyPeptideList
                                                       where pepR.CleanedPeptideSequence == p
                                                       select pepR.MyMapableProteins).ToList();

                List<string> uniqueList = new List<string>(mappableProteins.Count);

                foreach (List<string> list in mappableProteins)
                {
                    uniqueList.AddRange(list);
                }
                uniqueList = uniqueList.Distinct().ToList();

                GlobalPeptideResult gpp = new GlobalPeptideResult();
                gpp.Sequence = p;
                gpp.MappableProteins = uniqueList;

                globalPeptideIndex.Add(gpp);
            }

            if (MyParameters.PeptideParserOnlyUniquePeptides)
            {
                globalPeptideIndex.RemoveAll(a => a.MappableProteins.Count > 1);
            }


        }

        public void ProcessParsedData()
        {
            ProcessParsedData(false);
        }
    }

    struct GlobalPeptideResult
    {
        public string Sequence { get; set; }
        public List<string> MappableProteins { get; set; }

    }
}
