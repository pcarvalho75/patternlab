using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.VennProbability
{

    class IntersectionResult
    {
        List<int> theSparseMatrixRows;
        List<int> intersectingDims;

        public List<int> TheSparseMatrixRows {
            get
            {
                return theSparseMatrixRows;
            }
        }

        public double PercentageUniqueness { get; set; }
        public double NoUniqueComparedToTotal { get; set; }

        public List<int> IntersectingDims
        {
            get
            {
                return intersectingDims;
            }
        }

        public IntersectionResult(List<int> theSparseMatrixRows, List<int> intersectingDims)
        {
            this.theSparseMatrixRows = theSparseMatrixRows;
            this.intersectingDims = intersectingDims;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="irs">The data belonging from 1 group</param>
        /// <param name="noOfGroups">The number of groups (i.e. {L, M, H, VH})</param>
        /// <returns></returns>
        public static List<IntersectionResultAnalysis> IntersectionResultAnalysis(List<IntersectionResult> irs, int noOfGroups)
        {

            List<IntersectionResultAnalysis> ira = new List<IntersectionResultAnalysis>();

            //Step 1, Calculate Frequencies
            Dictionary<int, double> noReplicatesFrequencyDict = new Dictionary<int, double>();
            Dictionary<int, double> noIndividuals = new Dictionary<int, double>();

            foreach (IntersectionResult ir in irs)
            {
                if (noReplicatesFrequencyDict.ContainsKey(ir.TheSparseMatrixRows.Count))
                {
                    noReplicatesFrequencyDict[ir.TheSparseMatrixRows.Count] += ir.PercentageUniqueness;
                    noIndividuals[ir.TheSparseMatrixRows.Count] += ir.NoUniqueComparedToTotal;
                }
                else
                {
                    noReplicatesFrequencyDict.Add(ir.TheSparseMatrixRows.Count, ir.PercentageUniqueness);
                    noIndividuals.Add(ir.TheSparseMatrixRows.Count, (int)ir.NoUniqueComparedToTotal);
                }
            }

            //Law of Total Probabilit used for calculating the Bayesian Probability of finding the candidate 0 times
            double totalProbabilityForNotPresent = 0;
            foreach (KeyValuePair<int, double> kvp in noReplicatesFrequencyDict)
            {
                totalProbabilityForNotPresent += alglib.poissondistr.poissondistribution(0, (double)kvp.Key) * kvp.Value;
            }

            //Law of Total Probabilit used for calculating the Bayesian Probability of finding the candidate
            int noReplicates = noReplicatesFrequencyDict.Keys.Count;
            List<double> partialProbabilities = new List<double>();

            for (int x = 1; x <= noReplicates; x++)
            {
                double probSum = 0;

                for (int y = 1; y <= noReplicates; y++) {
                    double poissProb = Poisson((double)x, (double)y);
                    double prob = noReplicatesFrequencyDict[y] * poissProb;
                    probSum += prob;
                }

                partialProbabilities.Add(probSum);

            }


            //Here we calculate and finish up the probability for the Bayesian approach
            foreach (KeyValuePair<int, double> kvp in noReplicatesFrequencyDict)
            {
                IntersectionResultAnalysis i = new IntersectionResultAnalysis();
                i.TotalNoReplicates = noReplicatesFrequencyDict.Count;
                i.NoReplicatesAppeared = kvp.Key;
                i.PercentageOfUniqueness = kvp.Value;

                //Now for the final Bayesian Probability
                double ph = alglib.poissondistr.poissondistribution(0, (double)kvp.Key);

                double pdgh = totalProbabilityForNotPresent;
                double pdgnh = partialProbabilities.Sum();

                i.BayesianProbability = Bayes(ph, pdgh, pdgnh);
                i.NoCandidatesInGroup = (int)noIndividuals[kvp.Key];

                ira.Add(i);
            }

            return (ira);

        }

        static double Poisson(double m, double n0)
        {
            int factorial = 1;

            for (int i = 1; i <= n0; i++) {
                factorial *= i;
            }

            return (Math.Exp(-m) * Math.Pow(m,n0) / (double)factorial );
        }

        static double Bayes(double ph, double pdgh, double pdgnh)
        {
            //Poisson:http://www.changbioscience.com/stat/prob.html
            //Bayes:  http://psych.fullerton.edu/mbirnbaum/bayes/BayesCalc.htm#top
            //PHGD= (PDGH*PH)/(PDGH*PH + PDGNH*(1-PH))

            return ((pdgh * ph) / (pdgh * ph + pdgnh * (1 - ph)));
        }
    }

    
}
