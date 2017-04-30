using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEProQ.Acetylation
{
    [Serializable]
    public class AcetylationParams
    {
        public string MS1SEProDirectory { get; set; }
        public double MediumMarker { get; set; }
        public double HeavyMarker { get; set; }
        public int SearchSpaceSize { get; set; }
        public double DeltaHighMedium
        {
            get { return (HeavyMarker - MediumMarker); }
        }

        public AcetylationParams(
            string ms1SeproDirectory,
            double mediumMarker,
            double heavyMarker,
            int searchSpaceSize
            )
        {
            MS1SEProDirectory = ms1SeproDirectory;
            MediumMarker = mediumMarker;
            HeavyMarker = heavyMarker;
            SearchSpaceSize = searchSpaceSize;
        }
    }
}
