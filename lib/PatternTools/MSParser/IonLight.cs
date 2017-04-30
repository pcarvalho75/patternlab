using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternTools.MSParser
{
    [Serializable]
    public class IonLight
    {
        public double MZ { get; set; }
        public double Intensity { get; set; }
        public double RetentionTime { get; set; }
        public int ScanNumber { get; set; }

        public IonLight(double MZ, double Intensity, double Retention, int ScanNumber)
        {
            this.MZ = MZ;
            this.Intensity = Intensity;
            this.RetentionTime = Retention;
            this.ScanNumber = ScanNumber;
        }

        /// <summary>
        /// Used for serialization purposes
        /// </summary>
        public IonLight() { }
    }
}
