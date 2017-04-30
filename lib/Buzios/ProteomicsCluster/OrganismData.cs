using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Buzios
{
    [Serializable]
    public class OrganismData
    {
        public string Name { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double ElasticPotentialEnergy { get; set; }
        public int ID { get; set; }
        public double Weight { get; set; }

        public int Label { get; set; }

        public OrganismData(string name, int id, int label)
        {
            Name = name;
            PosX = -1;
            PosY = -1;
            ID = id;
            ElasticPotentialEnergy = double.NaN;
            Weight = 1;
            Label = label;
        }


    }
}
