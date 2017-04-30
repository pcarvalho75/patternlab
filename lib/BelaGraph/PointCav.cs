using System;
using System.Collections.Generic;
using System.Text;

namespace BelaGraph
{
    public class PointCav
    {

        double x;
        double y;
        public double ExtraParamNumeric { get; set; }
        public string ExtraParam { get; set; }
        public string ExtraParam2 { get; set; }

        public PointCav(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public PointCav() { }
        
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y {
            get { return y; }
            set { y = value; }
        }
        public string MouseOverTip { get; set; }
    }
}
