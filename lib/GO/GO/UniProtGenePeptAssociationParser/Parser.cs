using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace GO.UniProtAssociationParser
{
    public static class Parser
    {
        public static List<UniProtOrGenePeptItem> ParseGenePeptFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            string line;

            List<UniProtOrGenePeptItem> myUniprotItems = new List<UniProtOrGenePeptItem>();

            UniProtOrGenePeptItem u = null;
            int counter = 0;

            Regex goCapture = new Regex("GO:([0-9]{3,})");

            while ((line = sr.ReadLine()) != null)
            {
                counter++;

                if (line.StartsWith("VERSION "))
                {
                    
                    List<string> cols = Regex.Split(line, " ").ToList();
                    List<string> terms = Regex.Split(cols.Last(), ";").ToList();

                    for (int i = 0; i < terms.Count; i++)
                    {
                        terms[i] = terms[i].Replace("GI:", "");
                    }
                    u = new UniProtOrGenePeptItem(terms);

                    myUniprotItems.Add(u);
                }

                if (line.Contains("GO:"))
                {
                    foreach (Match m in goCapture.Matches(line))
                    {
                        string mtmp = m.ToString();
                        mtmp = mtmp.Substring(3, mtmp.Length - 3);
                        myUniprotItems.Last().GOItems.Add(mtmp);
                    }
                }

            }

            myUniprotItems.Add(u);

            myUniprotItems.RemoveAll(a => a.GOItems.Count == 0);

            return myUniprotItems;
        }

        public static List<UniProtOrGenePeptItem> ParseUniprotFile(string fileName)
        {

            StreamReader sr = new StreamReader(fileName);
            string line;

            List<UniProtOrGenePeptItem> myUniprotItems = new List<UniProtOrGenePeptItem>();

            UniProtOrGenePeptItem u = new UniProtOrGenePeptItem(new List<string>());
            int counter = 0;

            while ((line = sr.ReadLine()) != null)
            {
                counter++;
                try
                {
                    if (line.StartsWith("AC   "))
                    {
                        string[] cols = Regex.Split(line, "   ");
                        string[] terms = Regex.Split(cols[1], ";");

                        myUniprotItems.Add(u);
                        u = new UniProtOrGenePeptItem(terms.ToList());
                    }

                    if (line.StartsWith("DR   "))
                    {
                        string[] cols = Regex.Split(line, ";");
                        foreach (string s in cols)
                        {
                            if (s.StartsWith(" GO:"))
                            {
                                string corrected = Regex.Replace(s, " GO:", "");
                                u.GOItems.Add(corrected);
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Error parsing line " + counter);
                }
            }

            myUniprotItems.Add(u);

            return myUniprotItems;
        }

    }
}
