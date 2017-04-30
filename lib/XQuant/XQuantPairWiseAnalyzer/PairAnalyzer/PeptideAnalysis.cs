using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQuantPairWiseAnalyzer.PairAnalyzer
{
    [Serializable]
    public class PeptideAnalysis
    {
        /// <summary>
        /// Used for serialization purposes
        /// </summary>
        public PeptideAnalysis()
        {

        }

        double bin = -1;
        double logavgfold = -1;

        public string Sequence { get; set; }
        public int Z { get; set; }
        public string[] AssociationResults { get; set; }

        public bool IsUnique { get; set; }

        public double Binomial
        { 
            get { return bin; }
        }

        public double BinomialC1
        {
            get
            {
                if (LogAVGFold > 0)
                {
                    return bin;
                }
                else
                {
                    return 1 - bin;
                }
            }
        }

        public double LogAVGFold
        {
            get { return logavgfold; }
        }
 
        public PeptideAnalysis(string sequence, int z, string [] associations)
        {
            Sequence = sequence;
            Z = z;
            AssociationResults = associations;

            CalculateBinAndAVG();
        }

        void CalculateBinAndAVG()
        {
            List<double> folds = new List<double>();
            int positiveCounter = 0;
            int negativeCounter = 0;

            for (int x = 0; x < AssociationResults.Length; x++)
            {

                if (AssociationResults[x].Equals("+"))
                {
                    positiveCounter++;
                }
                else if (AssociationResults[x].Equals("-"))
                {
                    negativeCounter++;
                }
                else
                {
                    double d = double.Parse(AssociationResults[x]);

                    if (d != 0)
                    {
                        double dValue = (Math.Log(d, 2));
                        folds.Add(dValue);

                        if (dValue > 0) { positiveCounter++; }
                        if (dValue < 0) { negativeCounter++; }
                    }
                }
            }


            int total = positiveCounter + negativeCounter;

            double p = -1;

            if (positiveCounter > negativeCounter)
            {
                p = alglib.binomialcdistribution(positiveCounter - 1, total, 0.5);
            } 
            else if (negativeCounter > positiveCounter) 
            {
                p = alglib.binomialcdistribution(negativeCounter - 1, total, 0.5);
            } 
            else 
            {
                p = 0.5;
            }

            
            

            bin = Math.Round(p,4);
            
            if (folds.Count == 0)
            {
                logavgfold = 0;
            }
            else
            {
                logavgfold = Math.Round(folds.Average(), 4);
            }

            if (positiveCounter > negativeCounter && logavgfold < 0)
            {
                p = 0.5;
            } 
            else if (negativeCounter > positiveCounter && logavgfold > 0)
            {
                p = 0.5;
            }

        }

    }
}
