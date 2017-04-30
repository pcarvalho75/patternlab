using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PatternTools;
using PatternTools.SQTParser;
using SEPRPackage;

namespace SEProcessor
{
    public class Path
    {
        public string Name { get; set; }
        public List<SQTScan> MyScans { get; set; }
        Regex trypticTermini = new Regex("[-|K|R]" + Regex.Escape(".") + "[^P]", RegexOptions.Compiled);
        EnzymeEvaluator enzymeEvaluator = new EnzymeEvaluator();

        public void CalculateScoresForGeneralClassification(Parameters myParams)
        {


            //------------------------------------------

            double deltaCNMin = MyScans.Min(a => a.DeltaCN);
            double deltaCNDenominator = MyScans.Max(a => a.DeltaCN) - deltaCNMin;

            double probabilityMin = MyScans.Min(a => a.ProbabilityValue);
            double probabilityDenominator = MyScans.Max(a => a.ProbabilityValue) - probabilityMin;
            
            double secondaryRankMin = MyScans.Min(a => a.SecondaryRank);
            double secondaryRankDenominator = MyScans.Max(a => a.SecondaryRank) - secondaryRankMin;

            //This operation slightly improves classification quality
            foreach (SQTScan s in MyScans)
            {
                //We will use the tail score to temporarely store the primary score
                s.TailScore = s.PrimaryScore; // We will store the primary score here and unnormaize it later
                s.PrimaryScore = Math.Log(s.PrimaryScore) / Math.Log( s.LengthOfPeptideSequenceCleanedNoParenthesis);
                s.SecondaryRank = Math.Log((double)s.SecondaryRank, 10);
                
                //And this normalization should only be used by Comet
                //s.Bayes_CompBYScore = s.SecondaryScore;
                s.SecondaryScore = -1 * Math.Log(s.SecondaryScore);
            }

            double primaryScoreMin = MyScans.Min(a => a.PrimaryScore);
            double primaryScoreDenominator = MyScans.Max(a => a.PrimaryScore) - primaryScoreMin;
            
            double secondaryScoreMin = MyScans.Min(a=> a.SecondaryScore);
            double secondaryScoreDenominator = MyScans.Max(a => a.SecondaryScore) - secondaryScoreMin;

            double numPeaksMatchedMin = MyScans.Min(a => (double)a.PeaksMatched/(double)a.PeaksConsidered);
            double numPeaksMatchedDenominator = MyScans.Max(a => (double)a.PeaksMatched / (double)a.PeaksConsidered) - numPeaksMatchedMin;

            double ppmMin = MyScans.Min(a => Math.Abs(a.PPM_Orbitrap));
            double ppmDenominator = MyScans.Max(a => Math.Abs(a.PPM_Orbitrap)) - ppmMin;

            double sRankMin = MyScans.Min(a => a.SecondaryRank);
            double sRankDenominator = MyScans.Max(a => a.SecondaryRank) - sRankMin;


            //Presence score dictionary
            List<double> presenceValues = new List<double>();
            Dictionary<string, double> presenceDictionary = new Dictionary<string, double>();

            //double presenceScoreMin = 0;
            //double presenceScoreMax = 0;
            //double presenceScoreDelta = 0;

            //Now this is only done at peptide level
            //if (myParams.CompositeScorePresence)
            //{

            //    presenceDictionary = (from s in MyScans
            //                          group s by s.PeptideSequenceCleaned into g
            //                          select new { CleanedSequence = g.Key, Scans = g.Count() }).ToDictionary(a => a.CleanedSequence, a=> (double)a.Scans);


            //    //Lets define what is presence 1, 2, 3, 4, 5
            //    List<double> keyValues = presenceDictionary.Values.ToList();
            //    List<double> distinctValues = keyValues.Distinct().ToList();
            //    distinctValues.Sort();

            //    double separator = (double) distinctValues.Count / 10;
            //    double posInArray = 0;
            //    for (int i = 0; i < 5; i++)
            //    {
            //        posInArray+= separator;
            //        presenceValues.Add(distinctValues[(int)posInArray]);
            //    }

            //    presenceScoreMin = Math.Log(presenceDictionary.Values.Min());
            //    presenceScoreMax = Math.Log(presenceDictionary.Values.Max());
            //    presenceScoreDelta = presenceScoreMax - presenceScoreMin;
            //}



            //----

            Parallel.ForEach(MyScans, s =>
            //foreach (PatternTools.Scan s in MyScans)
            {

                if (myParams.CompositeScoreSecondaryRank)
                {
                    double secondaryRankScore = 1 - ((double)s.SecondaryRank - (double)secondaryRankMin) / (secondaryScoreDenominator);
                    s.Bayes_SecondaryRankScore = secondaryRankScore;
                }

                ////Presence score filter
                //if (myParams.CompositeScorePresence)
                //{
                //    double presenceScore = 0.05;

                //    for (int i = 0; i < presenceValues.Count; i++)
                //    {
                //        if (presenceDictionary[s.PeptideSequenceCleaned] > presenceValues[i])
                //        {
                //            presenceScore += 0.16;
                //        }
                //    }

                //    s.Sibblings = presenceDictionary[s.PeptideSequenceCleaned];

                //    //s.Bayes_PresenceScore = (Math.Log(s.Sibblings) - presenceScoreMin) / presenceScoreDelta;
                //    s.Bayes_PresenceScore = presenceScore;

                //}

                if (myParams.CompositeScorePrimaryScore)
                {
                    double normalizedPrimaryScore = (s.PrimaryScore - primaryScoreMin) / primaryScoreDenominator;
                    s.Bayes_PrimaryScore = normalizedPrimaryScore;
                }

                if (myParams.CompositeScoreSecondaryRank)
                {
                    double normalizedSRankScore = (double)(s.SecondaryRank - sRankMin) / (double)sRankDenominator;
                    s.Bayes_SecondaryRankScore = normalizedSRankScore;
                }

                if (myParams.CompositeScoreSecondaryScore)
                {
                    double normalizedSecondaryScore = (s.SecondaryScore - secondaryScoreMin) / secondaryScoreDenominator;
                    s.Bayes_SecondaryScore = normalizedSecondaryScore;
                }

                if (myParams.CompositeScoreDeltaCN)
                {
                    double normalizedDeltaCN = (s.DeltaCN - deltaCNMin) / deltaCNDenominator;
                    s.Bayes_DeltaCNScore = normalizedDeltaCN;
                }

                if (myParams.CompositeScorePeaksMatched)
                {
                    double thisFraction = (double)s.PeaksMatched / (double)s.PeaksConsidered;
                    double normalizedPeaksMatched = (thisFraction - numPeaksMatchedMin) / numPeaksMatchedDenominator;
                    s.Bayes_NumPeaksMatchedScore = normalizedPeaksMatched;
                }

                if (myParams.CompositeScoreDeltaMassPPM)
                {
                    double ppmScore = 1 - (Math.Abs(s.PPM_Orbitrap) - ppmMin) / ppmDenominator;
                    s.Bayes_PPMScore = ppmScore;
                }

                if (myParams.CompositScoreDigestion)
                {
                    double enzymeScore = 0;

                    int enzTermini = enzymeEvaluator.NoEnzymaticCleavages(s.PeptideSequence, Enzyme.Trypsin).Count;

                    if (enzTermini == 0)
                    {
                        enzymeScore = 0.1;
                    }
                    else if (enzTermini == 1)
                    {
                        enzymeScore = 0.35;
                    }
                    else if (enzTermini == 2)
                    {
                        enzymeScore = 1;
                    }

                    s.NoEnzymeCleavages = enzTermini;

                    s.Bayes_EnzymeEfficiencyScore = enzymeScore;

                }


            }
            );

        }
        
        public Path (string name) {
            Name = name;
            MyScans = new List<SQTScan>();
        }

        public Path(string name, List<SQTScan> myScans)
        {
            Name = name;
            MyScans = myScans;
        }
 
    }
}
