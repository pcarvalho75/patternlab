using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.MSParser
{
    public static class MS2PrecursorFinder
    {
        /// <summary>
        /// If we dont fint any precursor, we will return 0; this method will only search for 3's and 2's
        /// </summary>
        /// <param name="percentileTolerance">Suggested 0.05</param>
        /// <param name="ppmTolerance"></param>
        /// <param name="thePeaks"></param>
        /// <returns></returns>
        public static Ion FindPrecursorAndCharge(double percentileTolerance, double ppmTolerance, List<Ion> thePeaks, bool considerCharge2)
        {
            Ion precursor = new Ion(0, 0, 0, 0);
            if (thePeaks.Count <= 50) { return precursor; }

            if (percentileTolerance > 1 || percentileTolerance < 0)
            {
                throw (new Exception("Percentile tolerance out of bounds; use the recomended 0.95"));
            }

            thePeaks.Sort((a, b) => b.Intensity.CompareTo(a.Intensity));
            int chopper = (int)Math.Round( (double)thePeaks.Count * percentileTolerance);
            List<Ion> mostIntensePeaks = new List<Ion>(chopper);
            
            for (int i = 0; i < chopper; i++)
            {
                mostIntensePeaks.Add(thePeaks[i]);
            }

            if (mostIntensePeaks.Count < 10) { return precursor; }

            mostIntensePeaks.Sort((a, b) => a.MZ.CompareTo(b.MZ));

            Ion chosenPeak = mostIntensePeaks[0];

            //and just a tweak lets find if there is a higher peak within a 5 da range
            for (int i = 0; i < mostIntensePeaks.Count; i++)
            {
                if (mostIntensePeaks[i].Intensity > chosenPeak.Intensity && Math.Abs(chosenPeak.MZ - mostIntensePeaks[i].MZ) < 7)
                {
                    chosenPeak = mostIntensePeaks[i];
                }
            }


            

            //Now lets verify if there is signal for a charge 2, 3, 4 for this peak; the signal must be at least 10% of the precursor

            //For the 4 charge, we will search for 3 and 2, if we dont find nothing, we try the 3, then the two, since the signal is more limited.
            //We shal not try higher charges since they are less likey to happen so we could be introducing false positives
            double CRP3to2 = (chosenPeak.MZ * 3 - 1) / 2;
            double CRP2to1 = (chosenPeak.MZ * 2) - 1;
            double CRP3to2Acumulator = 0;
            double CRP2to1Acumulator = 0;


            for (int i = 0; i < mostIntensePeaks.Count; i++)
            {


                if (PatternTools.pTools.PPM(CRP3to2, mostIntensePeaks[i].MZ) < ppmTolerance)
                {
                    CRP3to2Acumulator += mostIntensePeaks[i].Intensity;
                }
                else if (PatternTools.pTools.PPM(CRP2to1, mostIntensePeaks[i].MZ) < ppmTolerance)
                {
                    CRP2to1Acumulator += mostIntensePeaks[i].Intensity;
                }
            }

            if (CRP3to2Acumulator > chosenPeak.Intensity * 0.15)
            {
                precursor = new Ion(chosenPeak.MZ, chosenPeak.Intensity, chosenPeak.RetentionTime, 0);
                precursor.Charge = 3;
            }
            else if (CRP2to1Acumulator > mostIntensePeaks[0].Intensity * 0.2)
            {
                precursor = new Ion(chosenPeak.MZ, chosenPeak.Intensity, chosenPeak.RetentionTime, 0);
                precursor.Charge = 2;
            }

            //Make sure we get the peaks back in order by mz
            thePeaks.Sort((a, b) => a.MZ.CompareTo(b.MZ));

            if ((precursor.Charge == 2 && considerCharge2) || precursor.Charge > 2)
            {
                return precursor;
            }
            else
            {
                return (new Ion(0, 0, 0, 0));
            }
        }
    }
}
