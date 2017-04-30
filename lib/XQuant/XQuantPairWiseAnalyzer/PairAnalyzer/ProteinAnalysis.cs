using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XQuant.Quants;

namespace XQuantPairWiseAnalyzer.PairAnalyzer
{
    [Serializable]
    public class ProteinAnalysis
    {
        public double Coverage { get; set; }
        public FastaItem MyFastaItem { get; set; }
        public List<PeptideAnalysis> MyPepAnalyses { get; set; }

        double pValC1 = -1;

        public double PValueC1
        {
            get
            {
                if (pValC1 == -1)
                {
                    List<double> pValues = new List<double>();
                    List<double> weights = new List<double>();

                    foreach (PeptideAnalysis pa in MyPepAnalyses)
                    {
                        pValues.Add(pa.BinomialC1);
                        int counter = pa.AssociationResults.Count(a => Regex.IsMatch(a, "[[1-9]|" + Regex.Escape("+") + "|" + Regex.Escape("-") + "]"));

                        foreach (string s in pa.AssociationResults)
                        {
                            try
                            {

                                double d = double.Parse(s);
                                counter++;
                            } catch
                            {

                            }
                        }

                        double w = Math.Sqrt(counter);

                        if (w == 0)
                        {
                            w = 0.05;
                        }
                        weights.Add(w);
                    }

                    if (pValues.Count == 0)
                    {
                        return 0.5;
                    }
                    else
                    {
                        pValC1 = PatternTools.pTools.StouffersMethod(pValues, weights);
                    }
                }

                return pValC1;
            }
        }

        public double GetAvgFFold()
        {
            List<double> no = MyPepAnalyses.FindAll(a => a.LogAVGFold != 0).Select(b => b.LogAVGFold).ToList();

            if (no.Count > 0)
            {
                return no.Average();
            } else
            {
                return 0;
            }
        }

        public ProteinAnalysis(FastaItem fi, double coverage, List<PeptideAnalysis> myPepAnalysis)
        {
            MyFastaItem = fi;
            Coverage = coverage;
            MyPepAnalyses = myPepAnalysis;
            myPepAnalysis.Sort((a, b) => a.Sequence.Length.CompareTo(b.Sequence.Length));
        }

        /// <summary>
        /// Used for serialization purposes
        /// </summary>
        public ProteinAnalysis()
        {

        }
    }
}
