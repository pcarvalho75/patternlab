using SEPRPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEProcessor
{
    class KeepTrack
    {
        public string Name { get; set; }
        public double BadSpectra { get; set; }
        public double TotalSpectra { get; set; }
        public double BadPeptides { get; set; }
        public double TotalPeptides { get; set; }

        List<string> goodPeptides = new List<string>();
        List<string> badPeptides = new List<string>();

        public KeepTrack(string name)
        {
            BadSpectra = 0;
            TotalSpectra = 0;
            BadPeptides = 0;
            TotalSpectra = 0;
            Name = name;
            
        }

        public void AccumulateResults (PeptideAnalysisPckg anst) {

            badPeptides.AddRange(anst.MyBadPeptideDic.Keys);
            goodPeptides.AddRange(anst.MyGoodPeptideDic.Keys);

            badPeptides = badPeptides.Distinct().ToList();
            goodPeptides = goodPeptides.Distinct().ToList();

            BadSpectra += anst.NoBadSpectra;
            TotalSpectra += anst.TotalSpectra;
            BadPeptides = badPeptides.Count;
            TotalPeptides = BadPeptides + goodPeptides.Count;
        }

        public string SpectraFDR
        {
            get
            {
                string report = BadSpectra + " / " + TotalSpectra + " = " + (BadSpectra / TotalSpectra).ToString("N3");
                return (report);
            }
        }

        public string PeptideFDR
        {
            get
            {
                string report = BadPeptides + " / " + TotalPeptides + " = " + (BadPeptides / TotalPeptides).ToString("N3");
                return (report);
            }
        }

    }
}
