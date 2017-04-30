using PatternTools.MSParser;
using PatternTools.MSParserLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternTools.RawReader
{
    public class Reader
    {
        //"ITMS + c ESI d sa Full ms2 443.77@etd100.00 [50.00-900.00]"
        Regex isMS1 = new Regex("Full ms ", RegexOptions.Compiled);
        Regex isMS2 = new Regex("Full ms2", RegexOptions.Compiled);
        Regex isMS3 = new Regex("Full ms3", RegexOptions.Compiled);
        Regex capturePrecursor = new Regex("ms[.|0-9] ([0-9|.]+)@", RegexOptions.Compiled);
        Regex captureInstrumentType = new Regex("^([A-Z]+) ", RegexOptions.Compiled);
        Regex captureActivationType = new Regex("@([A-Z|a-z]+)", RegexOptions.Compiled);

        int scanIntensityCutoffType = 1;
        int scanIntensityCutoffValue = 0;
        int maxNoPeaks = 15000;
        int scanCentroidResult = 1;

        RawReaderParams MyParams;

        public Reader(RawReaderParams myParams)
        {
            MyParams = myParams;
        }

        internal List<Ion> GetSpectrum(string rawFile, int scanNo)
        {
            List<Ion> returnIons = new List<Ion>();
            MSFileReaderLib.MSFileReader_XRawfile m2 = new MSFileReaderLib.MSFileReader_XRawfile();

            m2.Open(rawFile);
            m2.SetCurrentController(0, 1);

            string filter = null;
            m2.GetFilterForScanNum(scanNo, ref filter);



            double scanCentroidPeakWidth = 0.005;
            int arraySize = maxNoPeaks;
            object massList = null;
            object peakFlags = null;

            m2.GetMassListFromScanNum(
                ref scanNo,
                filter,
                scanIntensityCutoffType,
                scanIntensityCutoffValue,
                maxNoPeaks,
                scanCentroidResult,
                ref scanCentroidPeakWidth,
                ref massList,
                ref peakFlags,
                ref arraySize);

            double[,] thisMassList = (double[,])massList;

            int l = thisMassList.GetLength(1);

            for (int counter = 0; counter < thisMassList.GetLength(1); counter++)
            {
                double intensity = Math.Round(thisMassList[1, counter], 1);

                if (intensity > 0)
                {
                    Ion ion = new Ion(Math.Round(thisMassList[0, counter], 5), intensity, 0, 0);
                    returnIons.Add(ion);
                }
            }

            return returnIons;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawFile">The Thermo RAW File</param>
        /// <param name="scanNumbers">If no scan numbers are in the array the filter will search for all scan numbers</param>
        /// <returns></returns>
        public List<MSLight> GetSpectra(string rawFile, List<int> scanNumbers, bool onlyMS1)
        {
            Console.WriteLine("Parsing " + rawFile);
            MSFileReaderLib.MSFileReader_XRawfile m2 = new MSFileReaderLib.MSFileReader_XRawfile();
            
            m2.Open(rawFile);
            m2.SetCurrentController(0, 1);

            //string filter2 = "";
            //m2.GetFilterForScanNum(50, ref filter2);

            int noSpectra = 0;
            m2.GetNumSpectra(ref noSpectra);
            
            MSLight lastMS1 = new MSLight();
            List<MSLight> mySpectra = new List<MSLight>(noSpectra);

            for (int i = 1; i <= noSpectra; i++)
            {
               
                string filter = null;
                if (scanNumbers.Count == 0 || scanNumbers.Contains(i))
                {
                    m2.GetFilterForScanNum(i, ref filter);


                    MSLight thisMS = new MSLight();

                    Ion precursor = new Ion(-1, -1, -1, -1);
                    if (isMS1.IsMatch(filter))
                    {
                        lastMS1 = thisMS;
                    }
                    else
                    {
                        if (onlyMS1)
                        {
                            continue;
                        }
                        if (!MyParams.UseThermoMonoIsotopicPrediction)
                        {
                            precursor = GetPrecursor(filter, lastMS1);
                        }
                    }




                    //-------------

                    double retentionTime = 0;
                    m2.RTFromScanNum(i, ref retentionTime);


                    string thisFilter = null;
                    m2.GetFilterForScanNum(i, ref thisFilter);

                    string flags = null;
                    m2.GetFlags(ref flags);


                    //------------

                    double scanCentroidPeakWidth = 0.005;
                    int arraySize = maxNoPeaks;
                    object massList = null;
                    object peakFlags = null;

                    m2.GetMassListFromScanNum(
                        ref i,
                        filter,
                        scanIntensityCutoffType,
                        scanIntensityCutoffValue,
                        maxNoPeaks,
                        scanCentroidResult,
                        ref scanCentroidPeakWidth,
                        ref massList,
                        ref peakFlags,
                        ref arraySize);


                    thisMS.ScanNumber = i;

                    object trailerLabels = null;
                    object trailerValues = null;
                    int cc = 0;

                    m2.GetTrailerExtraForScanNum(i, ref trailerLabels, ref trailerValues, ref cc);

                    object chargeStateObject = null;
                    m2.GetTrailerExtraValueForScanNum(i, "Charge State:", ref chargeStateObject);
                    double chargeState = double.Parse(chargeStateObject.ToString());


                    string[] theStringValues = (string[])trailerValues;

                    if (theStringValues[1].Contains("="))
                    {
                        string [] split = Regex.Split(theStringValues[1], "=");
                        string [] correction = Regex.Split(split[1], ";");
                        thisMS.IonInjectionTime = double.Parse(correction[0]);
                        
                    }
                    else
                    {
                        thisMS.IonInjectionTime = double.Parse(theStringValues[2]);
                    }



                    Match instrument = captureInstrumentType.Match(filter);
                    thisMS.InstrumentType = instrument.Groups[1].ToString();
                    thisMS.ILines.Add("I\tFilter\t" + filter);
                    Match activationType = captureActivationType.Match(filter);
                    thisMS.ActivationType = activationType.Groups[1].ToString().ToUpper();


                    if (!thisMS.ActivationType.Equals(""))
                    {
                        if (MyParams.UseThermoMonoIsotopicPrediction)
                        {
                            object monoisotopicObject = null;
                            m2.GetTrailerExtraValueForScanNum(i, "Monoisotopic M/Z:", ref monoisotopicObject);
                            double objectValue = Math.Round(double.Parse(monoisotopicObject.ToString()), 4);

                            if (objectValue == 0)
                            {
                                //Thermo was unable to determine precursor so we will try to get it.
                                precursor = GetPrecursor(filter, lastMS1);
                                thisMS.ChargedPrecursor = precursor.MZ;
                            }
                            else
                            {
                                precursor = new Ion(objectValue, 1, 1, i);
                                //Console.WriteLine("Monoisotopic Thermo: " + precursor.MZ);
                                thisMS.ChargedPrecursor = precursor.MZ;
                            }

                        }
                        else
                        {
                            thisMS.ChargedPrecursor = precursor.MZ;
                        }




                        if (chargeState == 0)
                        {
                            thisMS.ZLines.Add("Z\t2\t" + Math.Round(PatternTools.pTools.DechargeMSPeakToPlus1(precursor.MZ, 2), 4));
                            thisMS.ZLines.Add("Z\t3\t" + Math.Round(PatternTools.pTools.DechargeMSPeakToPlus1(precursor.MZ, 3), 4));
                        }
                        else
                        {
                            thisMS.ZLines.Add("Z\t" + int.Parse(chargeState.ToString()).ToString() + "\t" + Math.Round(PatternTools.pTools.DechargeMSPeakToPlus1(precursor.MZ, chargeState), 4));
                        }

                        thisMS.ILines.Add("I\tPrecursorScan\t" + lastMS1.ScanNumber);
                        thisMS.ILines.Add("I\tPrecursorInt\t" + precursor.Intensity);
                    }


                    double[,] thisMassList = (double[,])massList;

                    int l = thisMassList.GetLength(1);

                    double[] mz = new double[thisMassList.GetLength(1)];
                    double[] intensity = new double[thisMassList.GetLength(1)];


                    for (int counter = 0; counter < thisMassList.GetLength(1); counter++)
                    {

                        if (thisMassList[1, counter] > 0.5) //check if we have a minimum intensity
                        {
                            mz[counter] = Math.Round(thisMassList[0, counter], 5);
                            intensity[counter] = Math.Round(thisMassList[1, counter], 1);

                            //thisMS.MZ.Add(Math.Round(thisMassList[0, counter], 5));
                            //thisMS.Intensity.Add(Math.Round(thisMassList[1, counter], 1));
                        }
                    }

                    if (mz.Length == 0) //There is no use in saving an empty MS
                    {
                        continue;
                    }

                    thisMS.MZ = mz.ToList();
                    thisMS.Intensity = intensity.ToList();

                    thisMS.CromatographyRetentionTime = Math.Round(retentionTime, 3);

                    //----------------------------

                    mySpectra.Add(thisMS);
                }

            }
            Console.WriteLine(".");

            m2.Close();

            //Some final cleanup
            mySpectra.RemoveAll(a => a.MZ.Count < 5);

            if (MyParams.ActivateSpectraCleaner)
            {
                CleanSpectra(mySpectra);
            }

            return mySpectra;
            
        }

        private void CleanSpectra(List<MSLight> mySpectra)
        {

                foreach (MSLight ms in mySpectra)
                {
                    if (ms.ZLines.Count == 0)
                    {
                        continue;
                    }

                    List<Ion> theIons = GetIonsFromSpectrum(ms);
                    List<Ion> simplifiedIonList = new List<Ion>();
                    for (double window = 0; window < ms.MZ[ms.MZ.Count - 1] + 1; window += MyParams.SpectraCleanerWindowSize)
                    {
                        List<Ion> capturedIons = theIons.FindAll(a => a.MZ >= window && a.MZ < window + MyParams.SpectraCleanerWindowSize);

                        if (capturedIons.Count <= MyParams.SpectraCleanerMaxPeaksPerWindow)
                        {
                            simplifiedIonList.AddRange(capturedIons);
                        }
                        else
                        {
                            capturedIons.Sort((a, b) => b.Intensity.CompareTo(a.Intensity));
                            capturedIons.RemoveRange(MyParams.SpectraCleanerMaxPeaksPerWindow - 1, capturedIons.Count - MyParams.SpectraCleanerMaxPeaksPerWindow);
                            simplifiedIonList.AddRange(capturedIons);
                        }

                        simplifiedIonList.Sort((a, b) => a.MZ.CompareTo(b.MZ));

                    }

                    List<double> newMZ = new List<double>();
                    List<double> newIntensity = new List<double>();

                    for (int i = 0; i < simplifiedIonList.Count; i++)
                    {
                        newMZ.Add(simplifiedIonList[i].MZ);
                        newIntensity.Add(simplifiedIonList[i].Intensity);
                    }

                    ms.MZ = newMZ;
                    ms.Intensity = newIntensity;

                }

                return;

        }

        private Ion GetPrecursor(string filter, MSLight lastMS1)
        {
            Match precursorM = capturePrecursor.Match(filter);
            double chargedPrecursor = double.Parse(precursorM.Groups[1].ToString());

            double intensity = -1;
            double mz = -1;

            //now we need to get the closest peak to the MS1 peaks
            for (int i = 0; i < lastMS1.MZ.Count; i++)
            {
                if (Math.Abs(PatternTools.pTools.PPM(lastMS1.MZ[i], chargedPrecursor)) <= MyParams.PPMToleranceForInferingPrecursorMZ)
                {
                    if (lastMS1.Intensity[i] > intensity)
                    {
                        intensity = lastMS1.Intensity[i];
                        mz = lastMS1.MZ[i];
                    }
                }
            }

            if (mz == -1)
            {
                mz = chargedPrecursor;
                intensity = 0;
            }

            return new Ion(mz, intensity, lastMS1.CromatographyRetentionTime, lastMS1.ScanNumber);
        }

        public enum MSType {
            ms1,
            ms2,
            ms3,
            all
        }

        double ToNone(double mz, double charge)
        {
            //return ((mz * charge) - (((charge) * 1.00782503214)));
            return ((mz * charge) - (((charge) * 1.007276466)));
            
        }

        private List<Ion> GetIonsFromSpectrum(MSLight ms)
        {
            List<Ion> theIons = new List<Ion>(ms.MZ.Count);
            for (int i = 0; i < ms.MZ.Count; i++)
            {
                theIons.Add(new Ion(ms.MZ[i], ms.Intensity[i], 0, 0));
            }

            return theIons;
        }

    }
}
