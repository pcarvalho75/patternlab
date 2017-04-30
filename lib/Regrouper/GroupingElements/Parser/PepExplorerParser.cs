using PatternTools;
using PatternTools.FastaParser;
using PepExplorer2.Result2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regrouper.GroupingElements.Parser
{
    class PepExplorerParser : IParser
    {
        public bool UseNSAF { get; set; }
        public bool UseMaxParsimony { get; set; }
        
        public List<DirectoryClassDescription> MyDirectoryDescriptionDictionary { get; set; }
        public Parameters MyParameters { get; set; }

        List<ThePackage> MyResultPackages = new List<ThePackage>();

        public void ParseDirs(List<DirectoryClassDescription> dirs)
        {
            MyDirectoryDescriptionDictionary = dirs;
            MyResultPackages = new List<ThePackage>();

            foreach (DirectoryClassDescription dcd in dirs)
            {
                //Get all Sepro Files in this and in deeper directories

                FileInfo[] fileInfo = new DirectoryInfo(dcd.MyDirectoryFullName).GetFiles("*.mpex", SearchOption.AllDirectories);

                foreach (FileInfo file in fileInfo)
                {
                    //First lets unserialize the object
                    Console.WriteLine("Loading .. " + file.FullName);
                    ResultPckg2 rp =  ResultPckg2.DeserializeResultPackage(file.FullName);
                    MyResultPackages.Add(new ThePackage(rp, file, dcd.ClassLabel));
                }

                Console.WriteLine("Done loading.");
            }
        }

        public void SavePatternLabProjectFile(string fileName, string description)
        {
            KeyValuePair<SparseMatrixIndexParserV2, SparseMatrix> ism = GenerateIndexAndSparseMatrix();
            PLP.PatternLabProject plp = new PLP.PatternLabProject(ism.Value, ism.Key, description);
            plp.Save(fileName);
        }

        public KeyValuePair<SparseMatrixIndexParserV2, SparseMatrix> GenerateIndexAndSparseMatrix()
        {
            StringBuilder sb = new StringBuilder();



            //Get a list of all proteins identified in all packages
            List<string> fastaIDs = (from pckg in MyResultPackages
                                    from fst in pckg.MyPackage.MyFasta
                                    select fst.SequenceIdentifier).Distinct().ToList();

            SparseMatrixIndexParserV2 smi = new SparseMatrixIndexParserV2();

            foreach (string fastaID in fastaIDs)
            {
                List<FastaItem> fi = (from pckg in MyResultPackages
                                      from fst in pckg.MyPackage.MyFasta
                                      where fst.SequenceIdentifier.Equals(fastaID)
                                      select fst).ToList();

                smi.Add(fastaID, fi[0].Description);
            }


            SparseMatrix sm = new SparseMatrix();

            foreach (DirectoryClassDescription myDir in MyDirectoryDescriptionDictionary)
            {
                sm.ClassDescriptionDictionary.Add(myDir.ClassLabel, myDir.Description);
            }

            foreach (ThePackage rp in MyResultPackages)
            {
                sparseMatrixRow smr = new sparseMatrixRow(rp.MyClassLabel);
                smr.FileName = rp.MyFileInfo.FullName;

                List<int> dims = new List<int>();
                List<double> values = new List<double>();

                for (int i = 0; i < smi.TheIndexes.Count; i++)
                {
                    int count = rp.MyPackage.Alignments.FindAll(a => a.ProtIDs.Contains(smi.TheIndexes[i].Name)).Count;

                    if (count > 0)
                    {
                        dims.Add(i + 1);
                        values.Add(count);
                    }
                }

                smr.Dims = dims;
                smr.Values = values;

                sm.addRow(smr);

            }


            return new KeyValuePair<SparseMatrixIndexParserV2, SparseMatrix>(smi, sm);
        }

        public void ProcessParsedData()
        {
            Console.WriteLine("Empty method");
        }
    }

    class ThePackage
    {
        public ResultPckg2 MyPackage { get; set; }
        public FileInfo MyFileInfo { get; set; }
        public int MyClassLabel { get; set; }

        public ThePackage(ResultPckg2 rp, FileInfo file, int classLabel) 
        {
            MyPackage = rp;
            MyFileInfo = file;
            MyClassLabel = classLabel;
        }
    }
}
