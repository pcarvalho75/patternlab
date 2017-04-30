using PatternTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PLP
{
    public class PatternLabProject
    {
        public SparseMatrix MySparseMatrix { get; set; }
        public SparseMatrixIndexParserV2 MyIndex { get; set; }
        public string Description { get; set; }
        public string TheFileFromWereIWasLoaded { get; set; }

        public PatternLabProject(SparseMatrix sm, SparseMatrixIndexParserV2 index, string description)
        {
            MySparseMatrix = sm;
            MyIndex = index;
            Description = description;
        }

        public PatternLabProject(string sparseMatrixFileName, string indexFileName, string description)
        {
            MySparseMatrix = new SparseMatrix(new StreamReader(sparseMatrixFileName));
            MyIndex = new SparseMatrixIndexParserV2(new StreamReader(indexFileName));
            Description = description;
        }

        public PatternLabProject(string pplFile)
        {
            Regex splitter = new Regex("###Description : |###SparseMatrix|###Index");
            Regex cleanNewLine = new Regex("[\n|\r]");

            string[] cols = splitter.Split(File.ReadAllText(pplFile));

            Description = cleanNewLine.Replace(cols[1], "");

            MySparseMatrix = new SparseMatrix(cols[2], true);

            MyIndex = new SparseMatrixIndexParserV2(cols[3], true);

            TheFileFromWereIWasLoaded = pplFile;

            Console.WriteLine("File " + pplFile + " loaded.");
        }

        public void Save (string filename)
        {
            string theSparseMatrix = MySparseMatrix.MatrixAsString().ToString();
            string theIndex = MyIndex.IndexAsString().ToString();

            StreamWriter sw = new StreamWriter(filename);

            sw.WriteLine("###Description : " + Description);
            sw.WriteLine("###SparseMatrix");
            sw.Write(theSparseMatrix);
            sw.WriteLine("###Index");
            sw.Write(theIndex);

            sw.Close();

        }




    }
}
