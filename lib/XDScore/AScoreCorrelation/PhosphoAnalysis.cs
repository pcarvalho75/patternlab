using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AScoreCorrelation
{
    public class PhosphoAnalysis
    {
        public string Confidence {get; set;}
        public int ScanNo { get; set; }
        public string Sequence { get; set; }
        public string FosfoScores { get; set; }

        public PhosphoAnalysis(string sequence, int scanNo, string confidence, string fosfoscores)
        {
            Sequence = sequence;
            ScanNo = scanNo;
            Confidence = confidence;
            FosfoScores = fosfoscores;
        }
        
    }
}
