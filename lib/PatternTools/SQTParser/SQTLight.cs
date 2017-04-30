using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternTools.SQTParser
{
    [Serializable]
    public class SQTLight
    {
        public int ScanNumber { get; set; }
        public int Z { get; set; }
        public string PeptideSequenceCleaned { get; set; }
        public double PrimaryScore { get; set; }
        public double MeasuredMH { get; set; }
        public double TheoreticalMH { get; set; }

        public SQTLight(SQTScan scn)
        {
            if (scn == null) { }
            else
            {
                ScanNumber = scn.ScanNumber;
                Z = scn.ChargeState;
                PeptideSequenceCleaned = scn.PeptideSequenceCleaned;
                PrimaryScore = scn.PrimaryScore;
                MeasuredMH = scn.MeasuredMH;
                TheoreticalMH = scn.TheoreticalMH;
            }

        }

        /// <summary>
        /// Used for serialization purposes
        /// </summary>
        public SQTLight() { }
    }
}
