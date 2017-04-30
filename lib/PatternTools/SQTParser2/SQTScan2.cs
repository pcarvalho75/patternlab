using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternTools.SQTParser2
{
    [Serializable]
    public class SQTScan2
    {
        public int NoEnzymeCleavages { get; set; }
        public double ScanNumber { get; set; }
        public List<Match> Matches = new List<Match>();
        public double MeasuredMH { get; set; }
        public string FileName { get; set; }
        public List<List<double>> Quantitation { get; set; }
        public int ChargeState { get; set; }
        public double ProbabilityValue { get; set; }
        public int NumberOfCandidates { get; set; }

        //Number of spectral counts peptides with this sequence has
        public double Sibblings { get; set; }
        public MSParserLight.MSLight MSLight { get; set; }

        public SQTScan2()
        {
        }


        public string FileNameWithScanNumberAndChargeState
        {
            get
            {
                return (FileName + "." + ScanNumber + "." + ScanNumber + "." + ChargeState);
            }
        }

    }
}

