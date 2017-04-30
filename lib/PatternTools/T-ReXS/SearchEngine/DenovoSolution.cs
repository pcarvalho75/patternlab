using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.T_ReXS.SearchEngine
{
    public class DenovoSolution
    {
        List<int> TheNodes { get; set; }
        public int SequentialScore
        {
            get
            {
                return TheNodes.Count;
            }
        }

        public double TotalIntensity
        {
            get;
            set;
        }
    }
}
