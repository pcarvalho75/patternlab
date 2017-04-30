using System.Collections.Generic;
using System;

namespace PatternTools.MSParser
{
    [Serializable]
    public class Ion
    {
        public double MZ { get; set; }
        public double Intensity { get; set; }
        public double RetentionTime { get; set; }
        public int Charge { get; set; }
        public double ChargeScore { get; set; }
        public int ScanNumber { get; set; }
        
        /// <summary>
        /// If we want to add any comments
        /// </summary>
        public string Note { get; set; }

        public Ion(double MZ, double Intensity, double Retention,  int ScanNumber)
        {
            this.MZ = MZ;
            this.Intensity = Intensity;
            this.RetentionTime = Retention;
            this.ScanNumber = ScanNumber;
            //clusterBuffer = new List<double>();
            this.Note = "";
        }

        /// <summary>
        /// Used for serialization purposes
        /// </summary>
        public Ion()
        {

        }
    }
}
