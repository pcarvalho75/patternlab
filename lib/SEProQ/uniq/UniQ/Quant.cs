using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniQ
{
    [Serializable]
    public class Quant
    {
        public Quant() { }

        public int Z { get; set; }
        public string FileName { get; set; }
        public int ScanNumber { get; set; }
        public List<double> MarkerIntensities { get; set; }

        public Quant(int z, string fileName, int scanNumber, List<double> markerIntensities)
        {
            Z = z;
            FileName = fileName;
            ScanNumber = scanNumber;
            MarkerIntensities = markerIntensities;
        }

    }
}
