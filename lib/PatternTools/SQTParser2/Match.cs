using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternTools.SQTParser2
{
    [Serializable]
    public class Match
    {
        public int PrimaryRank { get; set; }
        public int SecondaryRank { get; set; }
        public double TheoreticalMH { get; set; }
        public double DeltaCN { get; set; }
        public double PrimaryScore { get; set; }
        public double SecondaryScore { get; set; }
        public int PeaksMatched { get; set; }
        public int PeaksConsidered { get; set; }
        public string PeptideSequence { get; set; }
        public List<ID> IDs { get; set; }

        public Match()
        {
            IDs = new List<ID>();
        }


    }
}
