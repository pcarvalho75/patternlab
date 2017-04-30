using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools;
using PatternTools.SQTParser;
using System.Text.RegularExpressions;

namespace SEPRPackage
{
    [Serializable]
    public class PeptideResult
    {

        int formScore = -1;

        public double SeproQ { get; set; }

        public string PeptideSequence { get; set; }
        public string CleanedPeptideSequence {
            get
            {
                return (MyScans[0].PeptideSequenceCleaned);
            }
        }
        public List<SQTScan> MyScans { get; set; }
        
        public int NoMyScans { get { return MyScans.Count; } }
        
        public List<string> MyMapableProteins
        {
            get
            {
                List<string> mapableProteins = (from scan in MyScans
                                                from i in scan.IdentificationNames
                                                select i).Distinct().ToList();
                return mapableProteins;
            }
        }

        public int NoMyMapableProteins
        {
            get { return MyMapableProteins.Count; }
        }

        public PeptideResult(string peptideID, List<SQTScan> myScans)
        {
            PeptideSequence = peptideID;
            MyScans = myScans;
        }

        public string TheProteinLocci
        {
            get
            {
                string result = "";
                foreach (string s in MyMapableProteins)
                {
                    result += " | " + s;
                }
                return result;
            }
        }

        
    }
}
