using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PatternTools.TReXS.RegexGenerationTools;

namespace PatternTools.T_ReXS.RegexGenerationTools
{
    [Serializable()]
    public class SearchResult
    {
        double deltaCN = -1;
        public int ScanNumber { get; set; }
        public List<SearchResultCandidate> TheResults { get; set; }
        string machineName;
        double measuredPrecMZ = -1;
        double estimatedPrecCharge = -1;
        string sequence = "-1";
        
        public string Sequence {
            get
            {
                if (sequence.Equals("-1"))
                {
                    TheResults.Sort((a, b) => b.Score.CompareTo(a.Score));
                    sequence = TheResults[0].Sequence;
                }

                return sequence;
            }
         }

        public double MeasuredPrecursorMZ
        {
            get { return measuredPrecMZ; }
        }

        public double EstimatedPrecursorCharge
        {
            get { return estimatedPrecCharge; }
        }

        public String MachineName
        {
            get { return machineName; }
        }

        public double DeltaCN
        {
            get
            {
                if (deltaCN == -1)
                {
                    if (TheResults.Count == 0) { deltaCN = 0; }
                    else if (TheResults.Count == 1) { deltaCN = 1; }
                    else
                    {
                        TheResults.Sort((a, b) => b.Score.CompareTo(a.Score));
                        deltaCN = (TheResults[0].Score - TheResults[1].Score) / TheResults[0].Score;
                    }

                }
                return deltaCN;
            }
        }

        public SearchResult(int scanNumber, double measuredPrecursorMZ, double estimatedPrecursorCharge)
        {
            TheResults = new List<SearchResultCandidate>();
            machineName = System.Environment.MachineName;
            this.measuredPrecMZ = measuredPrecursorMZ;
            this.estimatedPrecCharge = estimatedPrecursorCharge;
            this.ScanNumber = scanNumber;

        }

        /// <summary>
        /// This constructor is used for serialization purposes only.  Do not use it!
        /// </summary>
        public SearchResult() { 
        
        }

        [Serializable()]
        public class SearchResultCandidate
        {
            public List<RegexAndPath> RegexAndPaths { get; set; }
            public SearchResultCandidate()
            {
                RegexAndPaths = new List<RegexAndPath>();
            }
            public string Name { get; set; }
            public string Sequence { get; set; }
            public double Score { get; set; }
            public double TheoreticalMass { get; set; }
        }

        [Serializable]
        public class RegexAndPath
        {
            public string TheRegex { get; set; }
            public List<int> NodePath { get; set; }

            /// <summary>
            /// Used only for serialization purposes
            /// </summary>
            public RegexAndPath() { }
            
            public RegexAndPath(List<int> nodePath, string regex )
            {
                this.NodePath = nodePath;
                this.TheRegex = regex;
            }
        }
    }

}
