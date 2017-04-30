using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using CSMSL.Spectral;
using CSMSL.Proteomics;

namespace PatternTools.MSParserLight
{
    public static class ParserLight
    {

        /// <summary>
        /// The result is sorted by scan number
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<MSLight> ParseLightMS2(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            if (!fi.Exists)
            {
                throw new Exception("File " + fi.FullName + " not found.");
            }
            List<MSLight> theSpectra = new List<MSLight>(100);

            if (fi.Extension.Equals(".ms2") || fi.Extension.Equals(".ms3"))
            {


                //The PathCandidates
                Regex tabSeparator = new Regex(@"\t");
                Regex mzSeparator = new Regex(@"\s+", RegexOptions.Compiled);
                Regex isNumber = new Regex(@"^[0-9]", RegexOptions.Compiled);
                Regex RegExRetTime = new Regex(@"RetTime");
                Regex RegexActivationType = new Regex(@"ActivationType");
                Regex RegexInstrumentType = new Regex(@"InstrumentType");
                StreamReader sr = new StreamReader(fileName);

                string line;

                MSLight ms = new MSLight();
                while ((line = sr.ReadLine()) != null)
                {
                    if (isNumber.IsMatch(line))
                    {
                        string[] cols = mzSeparator.Split(line);
                        ms.MZ.Add(double.Parse(cols[0]));
                        ms.Intensity.Add(double.Parse(cols[1]));
                    }
                    else if (RegexActivationType.IsMatch(line))
                    {
                        string[] theStrings2 = tabSeparator.Split(line);
                        ms.ActivationType = theStrings2[2];
                    }
                    else if (line.StartsWith("I\t"))
                    {
                        ms.ILines.Add(line);
                    }
                    else if (RegExRetTime.IsMatch(line))
                    {
                        string[] theStrings2 = tabSeparator.Split(line);
                        ms.CromatographyRetentionTime = double.Parse(theStrings2[2]);
                    }
                    else if (RegexInstrumentType.IsMatch(line))
                    {
                        string[] theStrings2 = tabSeparator.Split(line);
                        ms.InstrumentType = theStrings2[2];

                    }
                    else if (line.StartsWith("Z"))
                    {
                        ms.ZLines.Add(line);
                    }
                    else if (line.StartsWith("S"))
                    {
                        //We have a new spectra // scan
                        theSpectra.Add(ms);


                        //Step 2:Get the new one ready
                        ms = new MSLight();

                        string[] cols = tabSeparator.Split(line);
                        ms.ScanNumber = int.Parse(cols[1]);

                        if (cols.Length == 4)
                        {
                            //This is an MS2 spectra
                            ms.ChargedPrecursor = double.Parse(cols[3]);
                        }
                        ms.FileInformation = fi;
                    }
                }

                theSpectra.Add(ms);

                sr.Close();

                //Just in case
                theSpectra.Sort((a, b) => a.ScanNumber.CompareTo(b.ScanNumber));
                theSpectra.RemoveAt(0);

                return theSpectra;
            } 
            else if (fi.Extension.Equals(".xxmzML"))
            {
                Console.WriteLine("CSMSL mzML parser activated");

                //Mzml mm = new Mzml(fileName);
                //mm.Open();

                //foreach (MSDataScan<MZSpectrum> scan in mm)
                //{
                //    MSLight m = new MSLight();

                //    Spectrum<MZPeak, MZSpectrum> spec =  scan.MassSpectrum;
                //    m.MZ = spec.GetMasses().Select(a => Math.Round(a, 5)).ToList();
                //    m.Intensity = spec.GetIntensities().Select(a => Math.Round(a, 5)).ToList();
                //    m.ScanNumber = scan.SpectrumNumber;
                //    m.CromatographyRetentionTime = scan.RetentionTime;
                //    m.IonInjectionTime = scan.InjectionTime;

                //    //"1000511 MS level 1";
                //    Console.WriteLine("\tMS level :" + scan.MsnOrder);


                //    int msLevel = scan.MsnOrder;
                //    m.ILines.Add("I\tms_level\t" + msLevel);

                //    if (msLevel > 1)
                //    {
                //        //Precursor info

                //        m.ChargedPrecursor = Math.Round(mm.GetPrecursorMz(scan.SpectrumNumber), 5);
                //        double z = (double)mm.GetPrecusorCharge(scan.SpectrumNumber);
                //        double mh = Math.Round(PatternTools.pTools.DechargeMSPeakToPlus1(m.ChargedPrecursor, z), 5);
                //        m.ZLines.Add("Z\t" + z + "\t" + mh);
                //        m.MHPrecursor.Add(mh);

                //        //Not working
                //        //m.ILines.Add("I\tPrecursorScan\t" + mm.GetParentSpectrumNumber(ds.SpectrumNumber));

                //        DissociationType dt = mm.GetDissociationType(scan.SpectrumNumber);
                //        if (dt == DissociationType.CID)
                //            m.ActivationType = "CID";
                //        else if (dt == CSMSL.Proteomics.DissociationType.ETD)
                //            m.ActivationType = "ETD";
                //        else if (dt == CSMSL.Proteomics.DissociationType.ECD)
                //            m.ActivationType = "ECD";
                //        else if (dt == CSMSL.Proteomics.DissociationType.HCD)
                //            m.ActivationType = "HCD";
                //        else if (dt == CSMSL.Proteomics.DissociationType.MPD)
                //            m.ActivationType = "MPD";
                //        else if (dt == CSMSL.Proteomics.DissociationType.NPTR)
                //            m.ActivationType = "NF";
                //        else if (dt == CSMSL.Proteomics.DissociationType.PQD)
                //            m.ActivationType = "PQD";
                //        else
                //            m.ActivationType = null;

                //    }

                //    MZAnalyzerType mzat = mm.GetMzAnalyzer(m.ScanNumber);

                //    if (mzat == MZAnalyzerType.FTICR)
                //    {
                //        m.InstrumentType = "FTICR";
                //    }
                //    else if (mzat == MZAnalyzerType.IonTrap2D)
                //    {
                //        m.InstrumentType = "Ion Trap 2D";
                //    }
                //    else if (mzat == MZAnalyzerType.IonTrap3D)
                //    {
                //        m.InstrumentType = "ITMS";
                //    }
                //    else if (mzat == MZAnalyzerType.Orbitrap)
                //    {
                //        m.InstrumentType = "FTMS";
                //    }
                //    else if (mzat == MZAnalyzerType.Quadrupole)
                //    {
                //        m.InstrumentType = "Quadrupole";
                //    }
                //    else if (mzat == MZAnalyzerType.Sector)
                //    {
                //        m.InstrumentType = "Sector";
                //    }
                //    else if (mzat == MZAnalyzerType.TOF)
                //    {
                //        m.InstrumentType = "TOF";
                //    }
                //    else if (mzat == MZAnalyzerType.Unknown)
                //    {
                //        m.InstrumentType = "Unknown";
                //    }

                //    theSpectra.Add(m);


                //    Console.WriteLine(m.ScanNumber);
                //}



                //return theSpectra;

                return null;

            }
            else
            {
                throw new Exception(fi.Extension + " is not an acceptable extendion");
            }
        }


    }
}
