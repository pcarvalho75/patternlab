using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Linq;
using PLP;

namespace GO
{
    public class ResultParser
    {

        //Used to store the Identificator followed by its points
        Dictionary<string, double> theResults = new Dictionary<string, double>();
        Dictionary<string, List<string>> translatedResults = new Dictionary<string, List<string>>();
        List<ProteinInfo> myProteins = new List<ProteinInfo>();

        public PatternLabProject MyPatternLabProject { get; private set; }

        public List<ProteinInfo> MyProteins
        {
            get { return myProteins; }
        }

        public Dictionary<string, double> TheResults
        {
            get { return theResults; }
        }

        public Dictionary<string, List<string>> TranslatedResults
        {
            get { return translatedResults; }
        }
        
        public ResultParser()
        {

        }

        public void ParseTrendQuestACFoldOrIndex(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            if (fi.Extension.Equals(".plp"))
            {
                MyPatternLabProject = new PatternLabProject(fileName);

                foreach (var i in MyPatternLabProject.MyIndex.TheIndexes)
                {
                    theResults.Add(i.Name, 1);
                    AddProtein(i.Name, i.Description);
                }
                
            }
            else
            {

                StreamReader sr = new StreamReader(fileName);
                List<string> duplicateKeys = new List<string>(); //Used for debugging
                string read;
                Regex tabSpliter = new Regex(@"\t");
                theResults.Clear();

                //We first need to know if we are dealing with an ACFold Report or Index file
                //We believe that the indexFile contains no "#"
                bool indexFile = true;
                bool skip = false;
                int lineCounter = 0;
                while ((read = sr.ReadLine()) != null)
                {
                    lineCounter++;

                    if (read.StartsWith("#") || read.Length == 0)
                    {
                        //This is a comment

                        if (!skip)
                        {
                            indexFile = false;
                        }
                        if (read.StartsWith("#TrendQuest"))
                        {
                            indexFile = true; //It is a trendquest file
                            skip = true;

                        }

                    }
                    else
                    {
                        string[] theLine = tabSpliter.Split(read);
                        if (indexFile)
                        {
                            //Make sure we are not duplicating keys!
                            if (theResults.ContainsKey(theLine[1]))
                            {
                                duplicateKeys.Add(theLine[1]);
                            }
                            else
                            {
                                theResults.Add(theLine[1], 1);
                                AddProtein(theLine[1], theLine[2]);
                            }
                        }
                        else
                        {
                            //We are dealing with an ACFold report
                            if (read.StartsWith("#") || read.Length == 0 || read.StartsWith("\t") || read.StartsWith("\""))
                            {
                                continue;
                            }
                            theResults.Add(theLine[0], double.Parse(theLine[1]));
                            AddProtein(theLine[0], theLine[6]);
                        }
                    }
                }
                sr.Close();
            }
        }

        private void AddProtein (string ID, string Description) {
            
            if (!myProteins.Exists((a) => a.ID.Equals(ID)))
            {
                try
                {
                    ProteinInfo pt = new ProteinInfo();
                    if (ID.StartsWith("IPI"))
                    {
                        //remove the decimal at the end
                        ID = Regex.Replace(ID, @"\..*", "");
                    }
                    pt.ID = ID;
                    pt.Description = Description;
                    myProteins.Add(pt);
                }
                catch {
                    throw (new Exception("Failed parsing result report"));
                
                }
            }
        }

        public void PrepareTranslatedDictionary (ref GOTerms gt) {
            
            translatedResults.Clear();
            foreach (KeyValuePair<string, double> kvp in theResults)
            {
                translatedResults.Add(kvp.Key, gt.AssociationTranslator.Translate(kvp.Key));
            }

        }

        public class ProteinInfo
        {
            public string ID { get; set; }
            public string Description { get; set; }
        }
    }


}
