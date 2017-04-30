using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEProQ
{
    [Serializable]
    public class FilterSettings
    {
        public double PValue { get; set; }
        public double PeaksScore { get; set; }
        public double PPMError { get; set; }
        public int MinZ { get; set; }
    }
}
