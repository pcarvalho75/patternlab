using System;
using System.Collections.Generic;
using System.Text;

namespace PatternTools
{
    [Serializable]
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Tip { get; set; }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point (double[] xy)
        {
            this.X = xy[0];
            this.Y = xy[1];
        }
    }
}
