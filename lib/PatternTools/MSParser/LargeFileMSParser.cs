using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace PatternTools.MSParser
{
    public class LargeFileMSParser
    {
        Regex fileScanSplitter = new Regex(@"S\t", RegexOptions.Compiled);
        Regex lineSplitter = new Regex(@"\n", RegexOptions.Compiled);
        Regex tabSeparator = new Regex(@"\t", RegexOptions.Compiled);
        Regex mzSeparator = new Regex(@" ", RegexOptions.Compiled);
        Regex isNumber = new Regex(@"[[0-9]", RegexOptions.Compiled);
        int cycleCounter;
        MSFull bufferScan;

        StreamReader sr;

        public LargeFileMSParser(string fileName)
        {
           sr  = new StreamReader(fileName);
           cycleCounter = 0;
        }

        public List<MSFull> GetNext(int noSpectra)
        {

            List<MSFull> myScans = new List<MSFull>(noSpectra);

            string thisLine = "";

            MSFull thisScan;
            if (cycleCounter == 0)
            {
                thisScan = new MSParser.MSFull();
            }
            else
            {
                thisScan = bufferScan;
            }

            while (true)
            {
                thisLine = sr.ReadLine();

                if (thisLine == null) {
                    break;
                }

                if (thisLine.Length == 0) { continue; }

                if (isNumber.Match(thisLine, 0, 1).Success)
                {
                    //We are dealing with MS ion data
                    string[] ionData = mzSeparator.Split(thisLine);
                    Ion ion = new Ion(double.Parse(ionData[0]), double.Parse(ionData[1]), thisScan.CromatographyRetentionTime, thisScan.ScanNumber);

                    //Save our data
                    thisScan.MSData.Add(ion);
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

                    //Step 1:Save the Old One
                    if (thisScan.MSData.Count > 0) //Make sure we dont have an empty scan!
                    {
                        myScans.Add(thisScan);
                    }

                    
                    //Step 2:Get the new one ready
                    thisScan = new MSParser.MSFull();

                    string[] theStrings = tabSeparator.Split(thisLine);
                    thisScan.ScanNumber = int.Parse(theStrings[1]);

                    thisScan.MSData = new List<Ion>();

                    if (theStrings.Length != 3)
                    {
                        //we are dealing with MS1 - the precursor shall be 0
                        //We are dealing with MS and should save precursor information
                        thisScan.ChargedPrecursor = double.Parse(theStrings[3]);
                    }

                    if (myScans.Count == noSpectra) {

                        bufferScan = thisScan;
                        break;
                    }

                }
            }

            if (thisLine == null)
            {
                sr.Close();
            }

            cycleCounter++;

            return myScans;

        }
    }
}
