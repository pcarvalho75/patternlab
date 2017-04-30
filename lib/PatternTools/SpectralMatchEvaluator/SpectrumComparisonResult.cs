using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.SpectraPrediction;

namespace PatternTools.SpectralMatchEvaluator
{
    [Serializable]
    public class SpectrumComparisonResult
    {
        public SpectrumComparisonResult()
        {
            SignalPercentage = 0;
            ComplementaryPairsAX = 0;
            ComplementaryPairsBY = 0;
            ComplementaryPairsCZ = 0;

            ACount = new List<PredictedIon>();
            BCount = new List<PredictedIon>();
            CCount = new List<PredictedIon>();
            XCount = new List<PredictedIon>();
            YCount = new List<PredictedIon>();
            ZCount = new List<PredictedIon>();
            
        }

        public double SignalPercentage { get; set; }
        public List<PredictedIon> ACount { get; set; }
        public List<PredictedIon> BCount { get; set; }
        public List<PredictedIon> CCount { get; set; }
        public List<PredictedIon> XCount { get; set; }
        public List<PredictedIon> YCount { get; set; }
        public List<PredictedIon> ZCount { get; set; }

        public int ComplementaryPairsAX { get; set; }
        public int ComplementaryPairsBY { get; set; }
        public int ComplementaryPairsCZ { get; set; }
    }
}
