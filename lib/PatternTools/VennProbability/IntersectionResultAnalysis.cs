using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.VennProbability
{
    public class IntersectionResultAnalysis
    {
        public int TotalNoReplicates { get; set; }
        public double PercentageOfUniqueness { get; set; }
        public int NoReplicatesAppeared { get; set; }
        public int NoCandidatesInGroup { get; set; }
        public int ClassLabel { get; set; }

        /// <summary>
        /// >= lower bound
        /// </summary>
        public double SignalLowerBound { get; set; }

        /// <summary>
        /// less than upper bound
        /// </summary>
        public double SignalUpperBound { get; set; }

        public double BayesianProbability { get; set; }
    }
}
