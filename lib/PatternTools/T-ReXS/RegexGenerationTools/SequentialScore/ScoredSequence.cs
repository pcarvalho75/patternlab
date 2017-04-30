using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.T_ReXS.RegexGenerationTools.SequentialScore
{
    public class ScoredSequence
    {
        public string Sequence { get; set; }
        public int SequentialScore { get; set; }
        public double AvgPPMError { get; set; }
        public double TotalIntensity {get; set;}

    }
}
