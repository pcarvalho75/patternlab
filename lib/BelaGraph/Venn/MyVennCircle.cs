using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BelaGraph
{
    class MyVennCircle
    {
        public string Name { get; set; }
        public double Radius { get; set; }
        public PointCav CenterOfCircle { get; set; }
        public PointCav MappedCircleCenter { get; set; }
        public List<double> OverlapArea { get; set; }
        public DataVector MyDataVector { get; set; }
    }

}
