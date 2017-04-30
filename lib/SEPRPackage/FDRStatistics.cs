using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.SQTParser;

namespace SEPRPackage
{
    [Serializable]
    public static class FDRStatistics
    {
        public static FDRResult GenerateFDRStatistics(ProteinManager myProteins, Parameters myParams)
        {
            //Generate FDR statistics

            Console.WriteLine("Unique Proteins--->" + myProteins.MyProteinList.Count);

            PeptideAnalysisPckg p = new PeptideAnalysisPckg(myProteins.AllSQTScans, myParams);
            Console.Write(p.GoodScans);


            int labeledDecoyScans = p.BadScans.Count;   
            
            int labeledDecoyProteins = myProteins.MyProteinList.FindAll(a => a.Locus.StartsWith(myParams.LabeledDecoyTag)).Count;

            FDRResult fdrResult = new FDRResult();
            
            fdrResult.ReversedProteins = labeledDecoyProteins;
            fdrResult.TotalProteins = myProteins.MyProteinList.Count;
            fdrResult.ReversedPeptides = p.MyBadPeptideDic.Keys.Count;
            fdrResult.TotalPeptides = p.MyBadPeptideDic.Keys.Count + p.MyGoodPeptideDic.Count;
            fdrResult.ReversedSpectra = labeledDecoyScans;
            fdrResult.TotalSpectra = myProteins.AllSQTScans.Count;

            return fdrResult;
        }

        [Serializable]
        public struct FDRResult
        {
            public double ReversedProteins {get; set; }
            public double TotalProteins { get; set; }
            public double ProteinFDR 
            {
                get
                {
                    return ( (ReversedProteins / TotalProteins) * 100);
                }
            }

            public double ReversedPeptides { get; set; }
            public double TotalPeptides { get; set; }

            public double PeptideFDR
            {
                get
                {
                    return ( (ReversedPeptides / TotalPeptides) * 100);
                }
            }

            public double ReversedSpectra { get; set; }
            public double TotalSpectra { get; set; }

            public double SpectraFDR
            {
                get
                {
                    return ( (ReversedSpectra / TotalSpectra) * 100);
                }
            }

            public string SpectraFDRLabel
            {
                get
                {
                    return (ReversedSpectra + " / " + TotalSpectra + " = " + Math.Round(SpectraFDR,2) + "%");
                }
            }

            public string PeptideFDRLabel
            {
                get
                {
                    return (ReversedPeptides + " / " + TotalPeptides + " = " + Math.Round(PeptideFDR, 2) + "%");
                }
            }

            public string ProteinFDRLabel
            {
                get
                {
                    return (ReversedProteins + " / " + TotalProteins + " = " + Math.Round(ProteinFDR, 2) + "%");
                }
            }

        }
    }
}
