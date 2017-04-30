using PatternTools.MSParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PatternTools.MSParserLight
{
    [Serializable]
    public class MSLight
    {
        public double CromatographyRetentionTime { get; set; }
        public int ScanNumber { get; set; }
        public string ActivationType { get; set; }
        public List<double> MZ {get; set;}
        public List<double> Intensity { get; set; }
        public List<string> ZLines { get; set; }
        public double ChargedPrecursor { get; set; }
        public FileInfo FileInformation { get; set; }
        public string InstrumentType { get; set; }
        public double IonInjectionTime { get; set; }
        public List<string> ILines { get; set; }
        
        /// <summary>
        /// Rendered dynamically, this is slow
        /// </summary>
        public List<Ion> Ions
        {
            get
            {
                Ion[] tmp = new Ion[MZ.Count];
                for (int i = 0; i < MZ.Count; i++)
                {
                    tmp[i] = new Ion(MZ[i], Intensity[i], CromatographyRetentionTime, ScanNumber);
                }

                return tmp.ToList();
            }
        }



        public List<double> MHPrecursor
        {
            get 
            {
                List<double> mhs = new List<double>(ZLines.Count);

                foreach (string zLine in ZLines)
                {
                    string[] cols = Regex.Split(zLine, "\t");
                    mhs.Add(double.Parse(cols[2]));
                }

                return mhs;
            }
        }

        public List<double> Zs
        {
            get
            {
                List<double> zs = new List<double>(ZLines.Count);

                foreach (string zLine in ZLines)
                {
                    string[] cols = Regex.Split(zLine, "\t");
                    zs.Add(double.Parse(cols[1]));
                }

                return zs;
            }
        }

        public MSLight()
        {
            MZ = new List<double>();
            Intensity = new List<double>();
            ZLines = new List<string>();
            ILines = new List<string>();
        }

        
    }
}
