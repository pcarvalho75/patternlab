using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.SpectraPrediction
{
    [Serializable]
    public class PredictedIon
    {
        public int Charge { get; set; }
        public double MZ { get; set; }

        /// <summary>
        /// The number in the series
        /// </summary>
        public int Number { get; set; }
        public IonSeries Series { get; set; }
        public string FinalAA { get; set; }

        public bool Matched { get; set; }


        public PredictedIon(int charge, double mz, int number, string finalAA, IonSeries series)
        {
            Charge = charge;
            MZ = mz;
            Number = number;
            FinalAA = finalAA;
            Series = series;
        }
    }
}
