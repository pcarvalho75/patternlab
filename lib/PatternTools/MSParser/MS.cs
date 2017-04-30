using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using PatternTools.MSParserLight;

namespace PatternTools.MSParser
{
    /// <summary>
    /// This object describes a mass spectrum
    /// </summary>
    [Serializable]
    public class MSFull
    {
        List<string> zLines = new List<string>();
        public double TotalIonIntensity { get; set; }
        public double CromatographyRetentionTime { get; set; }
        public int ScanNumber { get; set; }
        public List<int> Charges { get; set; }
        public bool isMS2 { get; set; }
        public string ActivationType { get; set; }
        public double IonInjectionTime { get; set; }
        public string InstrumentType { get; set; }
        public List<string> Ilines { get; set; }
        public List<Ion> MSData {get; set;}

       

        List<Identification> proteinIdentification = new List<Identification>();
        public List<Identification> ProteinIdentifications {
            get { return proteinIdentification; }
            set { proteinIdentification = value; }

        }


        private List<double> dechargedPrecursorsFromZLine = new List<double>();
        
        public List<double> DechargedPrecursorsFromZLine {

            get
            {
                if (dechargedPrecursorsFromZLine.Count == 0)
                {
                    foreach (string z in zLines)
                    {
                        string[] cols = Regex.Split(z, "\t");
                        dechargedPrecursorsFromZLine.Add(double.Parse(cols[2]));
                    }
                }
                return (dechargedPrecursorsFromZLine);
            }

            set { dechargedPrecursorsFromZLine = value; }
        }


        

        
        //-------------------------------------------

        /// <summary>
        /// Just a plain simple way to join peaks that are very close together.
        /// This method is recomended to be used after deconvolution
        /// </summary>
        /// <param name="tolerance"></param>
        public void ClusterMSIons(double tolerance)
        {
            
            MSData.Sort((a,b) => a.MZ.CompareTo(b.MZ));
            bool needsToCluster = true;
            int externalCounter = 0;

            while (needsToCluster)
            {
                needsToCluster = false;
                List<Ion> ionsToExclude = new List<Ion>(MSData.Count);
                for (int i = externalCounter; i < MSData.Count; i++)
                {
                    double upperbound = MSData[i].MZ + tolerance;
                    for (int k = i; k < MSData.Count; k++)
                    {
                        if (i == k) {
                            continue;
                        }
                        if (MSData[k].MZ > upperbound) { 
                            break; 
                        }
                        externalCounter = i;

                        if (MSData[k].MZ <= upperbound)
                        {
                            ionsToExclude.Add(MSData[k]);
                        }
                    }
                    
                }

                if (ionsToExclude.Count > 0)
                {
                    needsToCluster = true;
                    foreach (Ion i in ionsToExclude)
                    {
                        MSData.Remove(i);
                    }
                }
            }
        }


        public List<string> ZLines
        {
            get { return zLines; }
            set { zLines = value; }
        }

        public List<int> GetChargesFromZLines()
        {
            List<int> charges = new List<int>(zLines.Count);
            foreach (string zline in zLines)
            {
                string[] cols = Regex.Split(zline, "\t");
                charges.Add(int.Parse(cols[1]));
            }
            return charges;
        }

        /// <summary>
        /// This is obtained in the S line
        /// </summary>
        public double ChargedPrecursor { get; set; }

        //Used mostly for data dependant methods
        public List<double> Precursors { get; set; }      

        public MSFull()
        {
            this.MSData = new List<Ion>(100);
            this.Charges = new List<int>();
            Ilines = new List<string>();
            isMS2 = false;
        }

        public MSFull(MSUltraLight ms2)
        {
            MSData = ms2.Ions.Select(a => new Ion(a.Item1, a.Item2, ms2.CromatographyRetentionTime, ms2.ScanNumber)).ToList();

            isMS2 = true;
            ActivationType =  ms2.ActivationType.ToString();
            ChargedPrecursor = ms2.Precursors[0].Item1;

            //These lines are not working for velos data
            Charges = new List<int>() { ms2.Precursors[0].Item2 };
            //---------------------------------------------------

            CromatographyRetentionTime = ms2.CromatographyRetentionTime;
            InstrumentType = ms2.InstrumentType.ToString();
            ScanNumber = ms2.ScanNumber;
            ZLines = ms2.GetZLines();
        }


        public string Note { get; set; }
    }
}
