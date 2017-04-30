using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CometWrapper
{
    [Serializable]
    public class Modification
    {
        public Modification()
        {
        }

        public Modification(string name, decimal massShift, string residues)
        {
            Name = name;
            MassShift = massShift;
            Residues = residues;
            isDiff = false;
        }

        public string Name { get; set; }
        public decimal MassShift { get; set; }
        public string Residues { get; set; }

        public bool isCTerm { get; set; }

        public bool isNTerm { get; set; }

        public bool isDiff { get; set; }

        public string Symbol { get; set; }
    }

}
