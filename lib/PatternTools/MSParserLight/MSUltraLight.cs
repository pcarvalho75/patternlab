using PatternTools.MSParser;
using PatternTools.MSParserLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternTools.MSParserLight
{
    [Serializable]
    public class MSUltraLight
    {
        public float CromatographyRetentionTime { get; set; }
        public int ScanNumber { get; set; }
        
        /// <summary>
        /// -1 for NA, 1 for FTMS, 2 for ITMS, 3 for TOF
        /// </summary>
        public short InstrumentType { get; set; }

        /// <summary>
        /// 1 for CID, 2 for HCD, 3 for ETD
        /// </summary>
        public short ActivationType { get; set; }

        public short MSLevel { get; set; }

        public List<Tuple<float, float>> Ions { get; private set; }

        /// <summary>
        /// An index to represent the file where this spectrum was extracted from.
        /// </summary>
        public short FileNameIndex { get; set; }

        /// <summary>
        /// Greatest fragment ion intensity.
        /// </summary>
        public float MaxIntensity { get; private set; }

        private Tuple<float, float> _MostIntenseIon;

        public Tuple<float, float> MostIntenseIon
        {
            get
            {
                if (_MostIntenseIon != null)
                {
                    return _MostIntenseIon;
                }

                if (Ions.Count == 0)
                {
                    return null;
                }

                Tuple<float,float> mostIntenseIon = Ions[0];

                for (int i = 1; i < Ions.Count; i++)
                {
                    if (Ions[i].Item2 > mostIntenseIon.Item2)
                    {
                        mostIntenseIon = Ions[i];
                    }
                }

                _MostIntenseIon = mostIntenseIon;

                return mostIntenseIon;
            }
        }



        /// <summary>
        /// MZ, Z; a Z of 0 means it is unknown
        /// </summary>
        public List<Tuple<float, short>> Precursors { get; set; }

        /// <summary>
        /// Intensity of precursor ion.
        /// </summary>
        public float PrecursorIntensity { get; set; }



        public MSUltraLight(float chromatograpgyRetentionTime,
                            int scanNumber,
                            List<Tuple<float,float>> ions,
                            List<Tuple<float, short>> precursors,
                            float precursorIntensity,
                            short instrumentTye,
                            short mslevel,
                            short fileNameIndex = -1)
        {
            this.CromatographyRetentionTime = chromatograpgyRetentionTime;
            this.ScanNumber = scanNumber;
            this.Ions = ions;
            Precursors = precursors;
            this.PrecursorIntensity = precursorIntensity;
            this.FileNameIndex = fileNameIndex;

            MSLevel = mslevel;
            InstrumentType = instrumentTye;
            ActivationType = -1;

            this.UpdateMaxIntensity();
        }

        /// <summary>
        /// Normalize intensities to unit vector so that square norm equals one.
        /// </summary>
        public void NormalizeIntensities()
        {
            float denominator = (float) Math.Sqrt(Ions.Sum(a => Math.Pow(a.Item2, 2)));
            Ions = Ions.Select(a => new Tuple<float, float>(a.Item1, a.Item2 / denominator)).ToList();
        }

        public MSUltraLight(MSLight m, short fileNameIndex)
        {
            this.CromatographyRetentionTime = (float)m.CromatographyRetentionTime;
            this.ScanNumber = m.ScanNumber;
            Precursors = new List<Tuple<float, short>>() {  new Tuple<float, short>((float)m.ChargedPrecursor, (short)m.Zs[0] ) };
            
            this.FileNameIndex = fileNameIndex;

            // Get list of ion MZs and intensities
            this.Ions = new List<Tuple<float, float>>(m.MZ.Count);
            for (int i = 0; i < m.MZ.Count; i++)
            {
                float mz = (float)m.MZ[i];
                float intensity = (float)m.Intensity[i];
                this.Ions.Add(new Tuple<float, float>(mz, intensity));
            }

        }

        public void UpdateIons(List<Tuple<float, float>> intensities)
        {
            Ions = intensities;
            
            this.UpdateMaxIntensity();
        }

        private void UpdateMaxIntensity()
        {
            if (Ions.Count > 0)
            {
                this.MaxIntensity = Ions.Max(a => a.Item2);
            }
        }

        public MSUltraLight()
        {

        }

        ///// <summary>
        ///// For compatibility with ms2 format
        ///// </summary>
        ///// <returns></returns>
        //public List<string> GetZLines()
        //{
        //    if (MSLevel > 1)
        //    {
        //        List<string> zLines = new List<string>();

        //        foreach (var p in Precursors)
        //        {
        //            zLines.Add("Z\t" + p.Item2 + "\t" + PatternTools.pTools.DechargeMSPeakToPlus1(p.Item1, p.Item2));
        //        }

        //        return zLines;

        //    } else
        //    {
        //        return null;
        //    }
        //}

        public List<string> GetZLines()
        {
            if (MSLevel > 1)
            {
                StringBuilder zLinesSb = new StringBuilder();

                foreach (var p in Precursors)
                {
                    zLinesSb.Append("Z\t" + p.Item2 + "\t" + PatternTools.pTools.DechargeMSPeakToPlus1(p.Item1, p.Item2).ToString());
                }

                List<string> zLines = new List<string>();
                zLines = Regex.Split(zLinesSb.ToString(), "\r\n").ToList();
                zLines.RemoveAll(a => String.IsNullOrEmpty(a));
                return zLines;

            }
            else
            {
                return null;
            }
        }
    }
}
