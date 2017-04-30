using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Text;

namespace PatternTools
{
    [Serializable]
    public class SparseMatrixIndexParserV2
    {
        List<Index> theIndexes = new List<Index>();
        private string p1;
        private bool p2;

        public List<Index> TheIndexes
        {
            get { return theIndexes; }
        }

        public List<string> AllNames
        {
            get 
            {
                List<string> allNames = (from i in theIndexes
                                         select i.Name).Distinct().ToList();
                return allNames;

            }
        }

        /// <summary>
        /// Clears all the loaded indexes
        /// </summary>
        public void ClearBuffer()
        {
            theIndexes.Clear();
        }

        public List<int> allIDs()
        {
            return TheIndexes.Select(a => a.ID).ToList();
        }



        public void SortIndexesByID()
        {
            theIndexes.Sort((a, b) => a.ID.CompareTo(b.ID));
        }

        /// <summary>
        /// there is no control for automatic index atribution on this method
        /// indexes id will be attributed as provided
        /// </summary>
        /// <param name="i"></param>
        public void Add(Index i, bool verifyIfDuplicate = false)
        {
            if (verifyIfDuplicate)
            {
                if (theIndexes.Exists(a => a.Name.Equals(i.Name)))
                {
                    throw new Exception("The index that you are trying to insert is a duplicate.");
                }
            }

            theIndexes.Add(i);


        }

        /// <summary>
        /// This will only add an id if it is non existing
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void Add(string name, string description)
        {
            if (!theIndexes.Exists((a) => a.Name.Equals(name)))
            {
                Index i = new Index();
                i.Name = name;
                if (description.Length > 0)
                {
                    i.Description = description;
                }
                else
                {
                    i.Description = "";
                }

                //get latest id
                theIndexes.Sort((a, b) => b.ID.CompareTo(a.ID));
                if (theIndexes.Count == 0)
                {
                    i.ID = 1;
                }
                else
                {
                    i.ID = theIndexes[0].ID + 1;
                }

                theIndexes.Add(i);
            }
        }

        /// <summary>
        /// This method compiles all the data as a string to be eventually saved to disk
        /// </summary>
        /// <returns></returns>
        public StringBuilder IndexAsString()
        {
            StringBuilder sb = new StringBuilder();

            theIndexes.Sort((a, b) => a.ID.CompareTo(b.ID));
            foreach (var item in theIndexes)
            {
                sb.AppendLine(item.ID + "\t" + item.Name + "\t" + item.Description);
            }

            return sb;
        }

        public void WriteGPI(string fileName)
        {
            theIndexes.Sort((a, b) => a.ID.CompareTo(b.ID));
            System.IO.StreamWriter sw = new StreamWriter(fileName);
            foreach (var item in theIndexes)
            {
                sw.WriteLine("{0}\t{1}\t{2}", item.ID, item.Name, item.Description);
            }


            sw.Close();

        }

        public SparseMatrixIndexParserV2()
        {
        }

        public SparseMatrixIndexParserV2(List<Index> indexes)
        {
            theIndexes = indexes;
        }


        public SparseMatrixIndexParserV2(StreamReader fileName)
        {
            getIndexFromString(fileName.ReadToEnd());
            fileName.Close();
        }

        public SparseMatrixIndexParserV2(string indexAsString, bool blank)
        {
            getIndexFromString(indexAsString);
        }

        private void getIndexFromString(string indexAsString)
        {
            List<string> reads = Regex.Split(indexAsString, "(\n|\r|\r\n)").ToList();
            reads.RemoveAll(a => a.Length < 3);

            //Declare the PathCandidates we will use
            Regex tabSplitter = new Regex(@"\t");

            //Parse and load to Ram
            foreach (string read in reads)
            {
                //Make sure we skip any comments
                if (read.StartsWith("#") || read.Length == 0) { continue; }

                string[] line = tabSplitter.Split(read);

                Index i = new Index();
                i.ID = int.Parse(line[0]);
                i.Name = line[1];
                if (line.Length >= 3)
                {
                    i.Description = line[2];
                }
                else
                {
                    i.Description = "";
                }

                if (!theIndexes.Exists((a) => a.Name.Equals(i.Name)))
                {
                    theIndexes.Add(i);
                }

            }
        }

        public string GetDescription(int ID)
        {
            int index = theIndexes.FindIndex((a) => a.ID ==ID );

            if (index > -1)
            {
                return (theIndexes[index].Description);
            }
            else
            {
                return ("Description not found - Error!");
            }

        }

        public string GetName(int ID)
        {
            int index = theIndexes.FindIndex((a) => a.ID == ID);

            if (index > -1)
            {
                return (theIndexes[index].Name);
            }
            else
            {
                return ("Name not found in index file");
            }
        }

        [Serializable]
        public class Index
        {
            public int ID {get; set;}
            public string Name { get; set; }
            public string Description { get; set; }
        }


        /// <summary>
        /// Generaly used for tagging indexes
        /// </summary>
        /// <param name="ID"></param>
        public void TransformIndexToNegative(int ID)
        {
            int index = theIndexes.FindIndex((a) => a.ID == ID);
            if (theIndexes[index].ID > 0)
            {
                theIndexes[index].ID *= -1;
            }
        }
    }

    
}
