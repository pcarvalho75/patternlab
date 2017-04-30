using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.SpectraPrediction;
using PatternTools.MSParser;

namespace PatternTools.SpectralMatchEvaluator
{
    public static class Compare
    {

        public static SpectrumComparisonResult Do(List<TheTests> theTests, List<PredictedIon> theoretical, List<Ion> experimentalPeaks, double ppm, int cleanedPeptideSequenceLength, double relativeIntensityThreshold)
        {
            PatternTools.MSParser.MSFull ms = new MSFull();
            ms.MSData = experimentalPeaks;
            ms.TotalIonIntensity = experimentalPeaks.Sum(a => a.Intensity);

            return (Do(theTests, theoretical, ms, ppm, cleanedPeptideSequenceLength, relativeIntensityThreshold));

        }


        public static SpectrumComparisonResult Do(List<TheTests> theTests, List<PredictedIon> theoretical, MSFull experimentalTmp, double ppm, int cleanedPeptideSequenceLength, double relativeIntensityThreshold)
        {
            SpectrumComparisonResult scr = new SpectrumComparisonResult();

            //Make sure we dont screw up the original spectrum
            MSFull experimental = PatternTools.ObjectCopier.Clone(experimentalTmp);
            
            double maxIntensity = experimental.MSData.Max(a => a.Intensity);
            experimental.MSData.RemoveAll(a => a.Intensity / maxIntensity < relativeIntensityThreshold);

            bool allTests = false;
            if (theTests.Contains(TheTests.AllTests)) { allTests = true; }

            //Just to make sure
            foreach (PredictedIon p in theoretical)
            {
                p.Matched = false;
            }

            //Before all find all matched theoretical and experimental
            List<Ion> matchedIons = new List<Ion>(100);
            foreach (PredictedIon pi in theoretical)
            {
                List<Ion> theIons = experimental.MSData.FindAll(a => PatternTools.pTools.PPM(a.MZ, pi.MZ) < ppm);

                if (theIons.Count > 0)
                {
                    matchedIons.AddRange(theIons);
                    pi.Matched = true;
                }

            }

            matchedIons = matchedIons.Distinct().ToList();

            if (theTests.Contains(TheTests.SignalPercentage) || allTests)
            {
                scr.SignalPercentage = Math.Round(matchedIons.Sum(a => a.Intensity) / experimental.TotalIonIntensity, 6);
            }

            if (theTests.Contains(TheTests.ACount) || allTests)
            {
                scr.ACount = SequentialScore(theoretical, IonSeries.A);
            }
            if (theTests.Contains(TheTests.BCount) || allTests)
            {
                scr.BCount = SequentialScore(theoretical, IonSeries.B);
            }
            if (theTests.Contains(TheTests.CCount) || allTests)
            {
                scr.CCount = SequentialScore(theoretical, IonSeries.C);
            }
            if (theTests.Contains(TheTests.XCount) || allTests)
            {
                scr.XCount = SequentialScore(theoretical, IonSeries.X);
            }
            if (theTests.Contains(TheTests.YCount) || allTests)
            {
                scr.YCount = SequentialScore(theoretical, IonSeries.Y);
            }
            if (theTests.Contains(TheTests.ZCount) || allTests)
            {
                scr.ZCount = SequentialScore(theoretical, IonSeries.Z);
            }


            if (theTests.Contains(TheTests.ComplementaryPairsAX) || allTests)
            {
                scr.ComplementaryPairsAX = CountComplementaryPairs(scr.ACount, scr.XCount, cleanedPeptideSequenceLength);
            }
            if (theTests.Contains(TheTests.ComplementaryPairsBY) || allTests)
            {
                scr.ComplementaryPairsBY = CountComplementaryPairs(scr.BCount, scr.YCount, cleanedPeptideSequenceLength);
            }
            if (theTests.Contains(TheTests.ComplementaryPairsCZ) || allTests)
            {
                scr.ComplementaryPairsCZ = CountComplementaryPairs(scr.CCount, scr.ZCount, cleanedPeptideSequenceLength);
            }

            return scr;

        }

        private static int CountComplementaryPairs(List<PredictedIon> i1, List<PredictedIon> i2, int peptideLength)
        {
            int pairCounter = 0;


            foreach (PredictedIon i in i1)
            {

                int index = i2.FindIndex(a => i.Number + a.Number == peptideLength + 1);

                if (index > -1)
                {
                    pairCounter++;
                }
            }

            //var result = from i1 in theIons1
            return pairCounter;
        }





        private static List<PredictedIon> SequentialScore(List<PredictedIon> theoretical, IonSeries ionSerie)
        {
            int greatestSequence = 0;
            int sequenceCounter = 0;

            List<PredictedIon> theIons = theoretical.FindAll(a => a.Series.Equals(ionSerie));
            List<PredictedIon> theResult = new List<PredictedIon>(10);
            List<PredictedIon> partialResult = new List<PredictedIon>(10);

            if (theIons[0].Matched)
            {
                sequenceCounter++;
                partialResult.Add(theIons[0]);
                
                greatestSequence = sequenceCounter;
                theResult.Clear();
                theResult.AddRange(partialResult);
            }

            for (int i = 1; i < theIons.Count-1; i++)
            {
                if (theIons[i].Matched && theIons[i + 1].Matched)
                {
                    sequenceCounter++;
                    partialResult.Add(theIons[i+1]);
                } else {
                    if (sequenceCounter > greatestSequence)
                    {
                        greatestSequence = sequenceCounter;
                        theResult.Clear();
                        theResult.AddRange(partialResult);
                    }
                }
            }

            if (theIons[theIons.Count-2].Matched)
            {
                sequenceCounter++;
                partialResult.Add(theIons[theIons.Count-1]);

                greatestSequence = sequenceCounter;
                theResult.Clear();
                theResult.AddRange(partialResult);
            }

            return theResult;
        }


        internal static void PrintResults(SpectrumComparisonResult evaluationTests)
        {
            Console.WriteLine("Theoretical / Experimental match evaluation-----");
            Console.WriteLine("ACount {0} :: {1}", evaluationTests.ACount.Count, PrintSequence(evaluationTests.ACount));
            Console.WriteLine("BCount {0} :: {1}", evaluationTests.BCount.Count, PrintSequence(evaluationTests.BCount));
            Console.WriteLine("CCount {0} :: {1}", evaluationTests.CCount.Count, PrintSequence(evaluationTests.CCount));
            Console.WriteLine("XCount {0} :: {1}", evaluationTests.XCount.Count, PrintSequence(evaluationTests.XCount));
            Console.WriteLine("YCount {0} :: {1}", evaluationTests.YCount.Count, PrintSequence(evaluationTests.YCount));
            Console.WriteLine("ZCount {0} :: {1}", evaluationTests.ZCount.Count, PrintSequence(evaluationTests.ZCount));
            Console.WriteLine("GoldenAX {0}", evaluationTests.ComplementaryPairsAX);
            Console.WriteLine("GoldenBY {0}", evaluationTests.ComplementaryPairsBY);
            Console.WriteLine("GoldenCZ {0}", evaluationTests.ComplementaryPairsCZ);
            Console.WriteLine("SignalPercentage {0}", evaluationTests.SignalPercentage);
            Console.WriteLine("------------------------------------------------");
        }

        private static string PrintSequence(List<PredictedIon> theSequence)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < theSequence.Count; i++)
            {
                sb.Append(theSequence[i].FinalAA);
            }
            return sb.ToString();
        }
    }
}
