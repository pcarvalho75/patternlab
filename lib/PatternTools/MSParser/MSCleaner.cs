using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.MSParser
{
    /// <summary>
    /// This class is to be used when demultiplexing mass spectra
    /// you can remove CRPs of other ions, remove unexplained complementary ions and neutral losses
    /// 
    /// </summary>
    public static class MSCleaner
    {

        /// <summary>
        /// Only submit CID spectra to this method!
        /// 
        /// </summary>
        /// <param name="theSpectra"></param>
        /// <param name="windowWidth"></param>
        /// <returns></returns>
        public static List<Ion> CIDCleanPrecursorWindow(List<Ion> theSpectra, double centerOfFragmentation, double windowWidth)
        {
            List<Ion> cleanedMS = new List<Ion>(theSpectra.Count);
            
            double halfWindow = windowWidth / 2;
            double lowerBound = centerOfFragmentation - halfWindow + 0.1; //The 0.1 is just a security factor.
            double upperBound = centerOfFragmentation + halfWindow - 0.1;

            foreach (Ion spectralIon in theSpectra)
            {
                if ( (spectralIon.MZ < lowerBound) || (spectralIon.MZ > upperBound) )
                {
                    cleanedMS.Add(spectralIon);
                }

            }

            return (cleanedMS);
        }


        /// <summary>
        /// Removes 25 da before precursor and 2 da ahead.
        /// </summary>
        /// <param name="theSpectra"></param>
        /// <param name="acceptablePPM"></param>
        /// <param name="precursors"></param>
        /// <param name="removeNL"></param>
        /// <returns></returns>
        public static List<Ion> ETDRemoveCRPsAndNeutralLosses(List<Ion> theSpectra, double acceptablePPM, List<Ion> precursors)
        {

            List<Ion> cleanedMS = new List<Ion>(theSpectra.Count);
            List<Ion> CRPs = new List<Ion>();

            //Here we obtain all the CRP's a precursor can generate
            foreach (Ion precursor in precursors)
            {
                double precursorCharge1 = PatternTools.pTools.DechargeMSPeakToPlus1(precursor.MZ, precursor.Charge);
                CRPs.Add(new Ion(precursorCharge1, 1, 0, 0));
                
                for (double pn = 2; pn <= precursor.Charge; pn++)
                {
                    double crp = (precursorCharge1 / (pn)) + ((pn-1) * 1.008) / pn;
                    CRPs.Add(new Ion(crp, pn, 0, 0));
                }

            }

            foreach (Ion spectralIon in theSpectra)
            {
                bool includeIon = true;

                foreach (Ion crp in CRPs)
                {
                    //The intensity information here was purposely used to store the charge info
                    if (PatternTools.pTools.PPM(spectralIon.MZ, (crp.MZ - (18/ (double)crp.Intensity) ))  < acceptablePPM * 1.5)
                    {
                        includeIon = false;
                        break;
                    }
                    else if (PatternTools.pTools.PPM(spectralIon.MZ, (crp.MZ - (17 / (double)crp.Intensity))) < acceptablePPM * 1.5)
                    {
                        includeIon = false;
                        break;
                    }
                    else if (PatternTools.pTools.PPM(spectralIon.MZ, (crp.MZ)) < acceptablePPM * 1.3)
                    {
                        includeIon = false;
                        break;
                    }
                }

                if (includeIon)
                {
                    cleanedMS.Add(spectralIon);
                }

            }


            return (cleanedMS);
        }



        /// <summary>
        /// Careful! Works only for ions of charge 2
        /// </summary>
        public static List<Ion> ETDComplementaryIons3(List<Ion> theSpectra, double ppm, Ion precursor, double minimumComplementaryIntensity)
        {


            List<Ion> cleanedMS = new List<Ion>();

            //if the ion is a charge two, //Lets assume that both fragments have charge +1
            double predictedPrecursorSumWithProtons = (precursor.MZ * 3) - 1.008;


            for (int i = 0; i < theSpectra.Count; i++)
            {
                if (theSpectra[i].Intensity < minimumComplementaryIntensity) { continue; }
                for (int j = 0; j < theSpectra.Count; j++) //Lets associate j with list 2;
                {

                    double massSum = (theSpectra[i].MZ + theSpectra[j].MZ);
                    
                    if (massSum > (predictedPrecursorSumWithProtons + 2) ) { break; }

                    if (PatternTools.pTools.PPM(massSum, predictedPrecursorSumWithProtons) < ppm)
                    {
                        if (!cleanedMS.Contains(theSpectra[i]))
                        {
                            cleanedMS.Add(theSpectra[i]);
                        }

                        if (!cleanedMS.Contains(theSpectra[j]))
                        {
                            cleanedMS.Add(theSpectra[j]);
                        }

                    }
                }
            }

            return (cleanedMS);
        }

    }
}
