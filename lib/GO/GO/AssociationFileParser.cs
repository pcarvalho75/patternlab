using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace GO
{
    [Serializable]
    public class AssociationFileParser
    {
        string GOAfileName;
        string IPIXRefsFilename;
        int GOColumn;
        int myColumn;
        Dictionary<string, List<string>> associationDic = new Dictionary<string, List<string>>();

        
        public AssociationFileParser(string GOAfileName, string IPIXRefsFilename, AssociationType t, int GOColumn, int myColumn)
        {
            this.GOAfileName = GOAfileName;
            this.IPIXRefsFilename = IPIXRefsFilename;
            this.GOColumn = GOColumn;
            this.myColumn = myColumn;
            Parse(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<string> Translate(string input)
        {
            List<string> theResult = new List<string>();
            if (associationDic.TryGetValue(input, out theResult))
            {
                return theResult;
            }
            else
            {
                return new List<string>();
            }
        }

        //----------------------------------------------------------

        private void Parse(AssociationType t)
        {

            if (t.Equals(AssociationType.UniProt))
            {
                string line = "";
                StreamReader sr = new StreamReader(GOAfileName);
                associationDic.Clear();

                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.StartsWith("Uni")) { continue; }
                    string[] cols = Regex.Split(line, "\t");

                    string GoID = Regex.Replace(cols[4], "GO:", "");

                    string vs = cols[10];

                    string[] UniprotIDs = Regex.Split(vs, Regex.Escape("|"));

                    foreach (string u in UniprotIDs)
                    {
                        string theString = u;
                        if (u.Contains("_"))
                        {
                            string[] cols2 = Regex.Split(u, "_");
                            theString = cols2[0];
                        }

                        if (associationDic.ContainsKey(theString))
                        {
                            if (!associationDic[theString].Contains(GoID))
                            {
                                associationDic[theString].Add(GoID);
                            }
                        }
                        else
                        {
                            associationDic.Add(theString, new List<string>() { GoID });
                        }
                    }

                }

            }
            else
            {


                string read;
                Regex tabSpliter = new Regex(@"\t");
                Regex semiColenSpliter = new Regex(@";");
                Regex PipeSpliter = new Regex(@"\|");

                StreamReader sr = new StreamReader(GOAfileName);


                int conversionIndex = 10; //The column where the IPI's are located
                int theGOColumn = 4; // The GO Column for the IPI database

                if (t.Equals(AssociationType.Generic))
                {
                    conversionIndex = myColumn;
                    theGOColumn = GOColumn;
                }

                while ((read = sr.ReadLine()) != null)
                {
                    if (read.StartsWith("!") || read.Length == 0)
                    {
                        continue;
                    }
                    string[] ids = tabSpliter.Split(read);

                    //Modification here
                    //The IPIs column 10,  //GOs column 4  //PlasmoDraft column 1

                    ids[theGOColumn] = System.Text.RegularExpressions.Regex.Replace(ids[theGOColumn], "GO:", "");


                    //First, lets extract the IPIs
                    string[] IDsInRow = PipeSpliter.Split(ids[conversionIndex]);
                    List<string> IPIs = new List<string>();
                    foreach (string s in IDsInRow)
                    {
                        if (s.Contains("IPI"))
                        {
                            IPIs.Add(s);
                        }

                        if (t.Equals(AssociationType.Generic))
                        {
                            IPIs.Add(s);
                        }
                    }

                    foreach (string IPI in IPIs)
                    {
                        if (associationDic.ContainsKey(IPI))
                        {
                            //We should only add the GO Term if it is not in the array;
                            if (!associationDic[IPI].Contains(ids[theGOColumn]))
                            {
                                associationDic[IPI].Add(ids[theGOColumn]);
                            }

                        }
                        else
                        {
                            List<string> theList = new List<string>();
                            theList.Add(ids[theGOColumn]);
                            associationDic.Add(IPI, theList);
                        }
                    }

                }

                sr.Close();
            }
        }
    }

    public enum AssociationType
    {
        IPI, UniProt, Generic, GenePeptFull
    }
}
