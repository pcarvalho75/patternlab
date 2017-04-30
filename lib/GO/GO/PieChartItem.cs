using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GO
{
    public class PieChartItem
    {
        public double Count { get; set; }
        public string TermName { get; set; }

        public PieChartItem(double count, string termName)
        {
            Count = count;
            TermName = termName;
        }
    }
}
