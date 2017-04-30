using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace PatternTools.MSParser
{
    public class DataDependantMS2Parser
    {
        List<MSFull> myScans = new List<MSFull>();
        List<string> headerLines = new List<string>();

        public FileInfo ParsedFileInfo { get; set; }

        Regex fileScanSplitter = new Regex(@"S\t", RegexOptions.Compiled);
        Regex lineSplitter = new Regex(@"\n", RegexOptions.Compiled);
        Regex tabSeparator = new Regex(@"\t", RegexOptions.Compiled);
        Regex mzSeparator = new Regex(@" ", RegexOptions.Compiled);
        Regex isNumber = new Regex(@"[[0-9]", RegexOptions.Compiled);

        public List<MSFull> MyScans
        {
            get { return myScans; }
        }

        public List<string> HeaderLines
        {
            get { return headerLines; }
        }


        /// <summary>
        /// The spectrum with charge 2 and 3 spectra usualy did not recieve a precise charge state, therefore it should be discarded for training
        /// </summary>
        /// <returns></returns>
        public int RemoveSpectra2or3()
        {
            int spectraRemoved = 0;
            List<MSFull> scansToRemove = new List<MSFull>();

            foreach (MSFull scan in myScans)
            {
                if (scan.Charges.Count > 1)
                {
                    spectraRemoved++;
                    scansToRemove.Add(scan);
                }
            }

            foreach (MSFull scanToRemove in scansToRemove)
            {
                myScans.Remove(scanToRemove);
            }

            return (spectraRemoved);

        }


        /// <summary>
        /// Removes scans aquired before a certain time.  This can be useful if constructing learning datasets, because the first spectra collected from the elution are usually bad quality spectra
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int RemoveSpectraAquiredBeforeXMinutes(double x)
        {
            int spectraRemoved = 0;
            List<MSFull> scansToRemove = new List<MSFull>();

            foreach (MSFull scan in myScans)
            {
                if (scan.CromatographyRetentionTime < x)
                {
                    spectraRemoved++;
                    scansToRemove.Add(scan);
                }
            }

            foreach (MSFull scanToRemove in scansToRemove)
            {
                myScans.Remove(scanToRemove);
            }


            return (spectraRemoved);
        }

        public int RemoveSpectraAquiredAfterXMinutes(double x)
        {
            int spectraRemoved = 0;
            List<MSFull> scansToRemove = new List<MSFull>();

            foreach (MSFull scan in myScans)
            {
                if (scan.CromatographyRetentionTime > x)
                {
                    spectraRemoved++;
                    scansToRemove.Add(scan);
                }
            }

            foreach (MSFull scanToRemove in scansToRemove)
            {
                myScans.Remove(scanToRemove);
            }


            return (spectraRemoved);
        }

        /// <summary>
        /// Return the number of spectra removed
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int RemoveSpectraWithLessThanXIons(int x)
        {
            int spectraRemoved = 0;
            List<MSFull> scansToRemove = new List<MSFull>();

            foreach (MSFull scan in myScans)
            {
                if (scan.MSData.Count < x)
                {
                    spectraRemoved++;
                    scansToRemove.Add(scan);
                }
            }

            foreach (MSFull scanToRemove in scansToRemove)
            {
                myScans.Remove(scanToRemove);
            }

            return (spectraRemoved);
        }

        public void ParseFile(FileInfo file, double minimumIonIntensity) {
            //Get the file to RAM
            myScans.Clear();
            headerLines.Clear();

            ParsedFileInfo = file;
            StreamReader sr = new StreamReader(file.OpenRead());

            string thisLine;
            MSFile thisFile = new MSFile();
            thisFile.FileName = file.Name;
            int lineCounter = 0; //Helps to debug.

            MSParser.MSFull thisScan = new MSParser.MSFull();

            while ((thisLine = sr.ReadLine()) != null)
            {
                lineCounter++;
                if (thisLine.Length == 0) { continue; }

                if (isNumber.Match(thisLine, 0, 1).Success)
                {
                    //We are dealing with MS ion data
                    string[] ionData = mzSeparator.Split(thisLine);
                    Ion ion = new Ion(double.Parse(ionData[0]), double.Parse(ionData[1]), thisScan.CromatographyRetentionTime, thisScan.ScanNumber);

                    //Save our data
                    thisScan.MSData.Add(ion);
                }
                else if (thisLine.StartsWith("H"))
                {
                    headerLines.Add(thisLine);
                }


                else if (thisLine.StartsWith("Z"))
                {
                    string[] theStrings3 = tabSeparator.Split(thisLine);
                    if (thisScan.Charges == null)
                    {
                        thisScan.Charges = new List<int> { int.Parse(theStrings3[1]) };
                        thisScan.Precursors = new List<double> { double.Parse(theStrings3[2]) };
                    }
                    else
                    {
                        thisScan.Charges.Add(int.Parse(theStrings3[1]));
                        if (thisScan.Precursors == null)
                        {
                            thisScan.Precursors = new List<double>();
                        }
                        thisScan.Precursors.Add(double.Parse(theStrings3[2]));
                    }
                }
                else if (thisLine.Contains("ActivationType"))
                {
                    string[] theStrings2 = tabSeparator.Split(thisLine);
                    thisScan.ActivationType = theStrings2[2];
                }
                else if (thisLine.Contains("InstrumentType"))
                {
                    string[] theStrings2 = tabSeparator.Split(thisLine);
                    thisScan.InstrumentType = theStrings2[2];

                }
                else if (thisLine.Contains("RetTime"))
                {
                    string[] theStrings2 = tabSeparator.Split(thisLine);

                    if (theStrings2.Length == 2)
                    {
                        //The retention time is separated with a space so we need to further break it up
                        string[] s = Regex.Split(theStrings2[1], @" ");
                        thisScan.CromatographyRetentionTime = double.Parse(s[1]);

                    }
                    else if (theStrings2.Length == 3)
                    {
                        thisScan.CromatographyRetentionTime = double.Parse(theStrings2[2]);
                    }
                    else
                    {
                        throw new Exception("Problems parsing Retention time Line.\n" + thisLine);
                    }

                }

                else if (thisLine.StartsWith("S"))
                {
                    //We have a new spectra // scan

                    //Denoise our scan
                    thisScan.MSData.RemoveAll(p => p.Intensity < minimumIonIntensity);

                    //Compute the ion intensity
                    foreach (var scan in thisScan.MSData)
                    {
                        thisScan.TotalIonIntensity += scan.Intensity;
                    }

                    //Step 1:Save the Old One
                    if (thisScan.MSData.Count > 0) //Make sure we dont have an empty scan!
                    {
                        myScans.Add(thisScan);
                    }
                    //Step 2:Get the new one ready
                    thisScan = new MSParser.MSFull();

                    thisScan.TotalIonIntensity = 0;

                    string[] theStrings = tabSeparator.Split(thisLine);
                    thisScan.ScanNumber = int.Parse(theStrings[1]);

                    thisScan.MSData = new List<Ion>();

                    if (theStrings.Length != 3)
                    {
                        //we are dealing with MS1 - the precursor shall be 0
                        //We are dealing with MS and should save precursor information
                        thisScan.ChargedPrecursor = double.Parse(theStrings[3]);
                    }

                }
            }

            //Save our last scan
            myScans.Add(thisScan);

            sr.Close();

        }
    }
}
