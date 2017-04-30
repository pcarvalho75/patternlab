using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.PTMMods;

namespace PatternTools.SpectraPrediction
{
    public class SpectralPredictionParameters
    {
        //Ion types
        public bool A { get; set; }
        public bool B { get; set; }
        public bool C { get; set; }
        public bool X { get; set; }
        public bool Y { get; set; }
        public bool Z { get; set; }
        public bool IsMonoisotopic { get; set; }
        public List<ModificationItem> MyModifications { get; set; }

        //Neutral Losses
        public bool H20 { get; set; }
        public bool NH3 { get; set; }

        public int DaughterIonChargesMax { get; set; }

        public SpectralPredictionParameters()
        {
        }

        public SpectralPredictionParameters
            (bool A, bool B, bool C, bool X, bool Y, bool Z, bool H2O, bool NH3,
            int daughterIonChargesMax, bool isMonoisotopic, List<ModificationItem> myModifications)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.H20 = H2O;
            this.NH3 = NH3;
            this.DaughterIonChargesMax = daughterIonChargesMax;
            this.IsMonoisotopic = isMonoisotopic;

            MyModifications = myModifications;
        }

        
        
    }
}
