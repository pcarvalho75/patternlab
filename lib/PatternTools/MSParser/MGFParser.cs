using PatternTools.MSParserLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternTools.MSParser
{
    public static class MGFParser
    {
        /// <summary>
        /// Parsers an MS2 mgf file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static List<MSUltraLight> ParseMGFFile(string file)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(file);
            List<MSUltraLight> theMS2 = new List<MSUltraLight>();

            string line = "";
            MSUltraLight ms = new MSUltraLight();
            List<double> charges = new List<double>();

            while ((line = sr.ReadLine()) != null)
            {
                //take care of the header
                if (line.StartsWith("#") || line.StartsWith("_") || line.Equals(""))
                {
                    //This is a header line
                    //msFile.Header += line;
                }
                else if (line.Equals("BEGIN IONS"))
                {
                    //Prepare Z lines
                    theMS2.Add(ms);
                    ms = new MSUltraLight();
                    ms.MSLevel = 2;
                    ms.ActivationType = -1;
                    ms.InstrumentType = -1;

                }
                else if (line.StartsWith("CHARGE"))
                {
                    string[] cols = Regex.Split(line, "=");
                    string c = cols[1].Replace(@"+", "");
                    if (c.Equals("2,3"))
                    {
                        charges = new List<double>() { 2, 3 };
                    }
                    else
                    {
                        int charge = int.Parse(c);
                        charges = new List<double>() { charge };
                    }
                }
                else if (line.StartsWith("PEPMASS"))
                {
                    string[] cols = Regex.Split(line, "=");
                    string[] nums = Regex.Split(cols[1], " ");

                    float chargedPrecursor;
                    if (nums[0].Equals(""))
                    {
                        chargedPrecursor = float.Parse(nums[1]);
                    }
                    else
                    {
                        chargedPrecursor = float.Parse(nums[0]);
                    }

                    foreach (short z in charges)
                    {
                        float mh = (chargedPrecursor + (z * 1.007276466f)) / (float)z;

                        ms.Precursors.Add(new Tuple<float, short>(mh, z)); 
                    }

                    
                }
                else if (line.StartsWith("RTINSECONDS"))
                {
                    string[] cols = Regex.Split(line, "=");
                    float retTime;

                    if (cols[1].Contains("-"))
                    {
                        string[] nums = Regex.Split(cols[1], "-");
                        retTime = float.Parse(nums[0]);
                    }
                    else
                    {
                        retTime = float.Parse(cols[1]);
                    }
                    ms.CromatographyRetentionTime = retTime;
                }
                else if (line.StartsWith("SCANS"))
                {
                    string[] cols = Regex.Split(line, "=");
                    double scan;

                    if (cols[1].Contains("-"))
                    {
                        string[] nums = Regex.Split(cols[1], "-");
                        scan = double.Parse(nums[0]);
                    }
                    else
                    {
                        scan = double.Parse(cols[1]);
                    }
                    ms.ScanNumber = (int)scan;
                }
                else if (line.StartsWith("TITLE"))
                {

                    if (line.Contains("Fragmentation:hcd"))
                    {
                        ms.ActivationType = 3;
                        ms.InstrumentType = -1;
                    }
                }
                else if (Regex.IsMatch(line, "^[0-9]+"))
                {
                    //If the line begins with a number it is an ion line
                    string[] cols = Regex.Split(line, "\t| ");
                    try
                    {
                        Tuple<float, float> i = new Tuple<float, float>(float.Parse(cols[0]), float.Parse(cols[1]));
                        ms.Ions.Add(i);
                    }
                    catch
                    {
                        throw new Exception("An inconsistency has been found in file: " + file + "\nThe line reads:\n" + line);
                    }
                }

            }


            theMS2.Add(ms);

            //The first one is always bogus!
            if (theMS2.Count > 0)
            {
                theMS2.RemoveAt(0);
            }

            sr.Close();

            return theMS2;
        }

        public static List<MSFull> ParseMGFFile(string file, string activationType = "CID", string instrumentType = "ITMS")
        {

            System.IO.StreamReader sr = new System.IO.StreamReader(file);
            List<MSFull> theMS2 = new List<MSFull>();

            string line = "";
            MSFull ms = new MSFull();

            while ((line = sr.ReadLine()) != null)
            {
                //take care of the header
                if (line.StartsWith("#") || line.StartsWith("_") || line.Equals(""))
                {
                    //This is a header line
                    //msFile.Header += line;
                }
                else if (line.Equals("BEGIN IONS"))
                {
                    //Prepare Z lines
                    theMS2.Add(ms);
                    ms = new MSFull();
                    ms.isMS2 = true;
                    ms.ActivationType = activationType;
                    ms.InstrumentType = instrumentType;

                }
                else if (line.StartsWith("CHARGE"))
                {
                    string[] cols = Regex.Split(line, "=");
                    string c = cols[1].Replace(@"+", "");
                    if (c.Equals("2,3"))
                    {
                        ms.Charges.Add(2);
                        ms.Charges.Add(3);
                    }
                    else
                    {
                        int charge = int.Parse(c);
                        ms.Charges.Add(charge);
                    }
                }
                else if (line.StartsWith("PEPMASS"))
                {
                    string[] cols = Regex.Split(line, "=");
                    string[] nums = Regex.Split(cols[1], " ");

                    double chargedPrecursor;
                    if (nums[0].Equals(""))
                    {
                        chargedPrecursor = double.Parse(nums[1]);
                    }
                    else
                    {
                        chargedPrecursor = double.Parse(nums[0]);
                    }
                    ms.ChargedPrecursor = (chargedPrecursor + ((double)ms.Charges[0] * 1.007276466)) / (double)ms.Charges[0];
                    ms.ZLines.Add("Z\t" + ms.Charges[0] + "\t" + PatternTools.pTools.DechargeMSPeakToPlus1(ms.ChargedPrecursor, ms.Charges[0]));
                }
                else if (line.StartsWith("RTINSECONDS"))
                {
                    string[] cols = Regex.Split(line, "=");
                    double retTime;

                    if (cols[1].Contains("-"))
                    {
                        string[] nums = Regex.Split(cols[1], "-");
                        retTime = double.Parse(nums[0]);
                    }
                    else
                    {
                        retTime = double.Parse(cols[1]);
                    }
                    ms.CromatographyRetentionTime = retTime;
                }
                else if (line.StartsWith("SCANS"))
                {
                    string[] cols = Regex.Split(line, "=");
                    double scan;

                    if (cols[1].Contains("-"))
                    {
                        string[] nums = Regex.Split(cols[1], "-");
                        scan = double.Parse(nums[0]);
                    }
                    else
                    {
                        scan = double.Parse(cols[1]);
                    }
                    ms.ScanNumber = (int)scan;
                }
                else if (line.StartsWith("TITLE"))
                {
                    ms.Ilines.Add(line);

                    if (line.Contains("Fragmentation:hcd")) {
                        ms.ActivationType = "HCD";
                        ms.InstrumentType = "FTMS";
                    }
                }
                else if (Regex.IsMatch(line, "^[0-9]+"))
                {
                    //If the line begins with a number it is an ion line
                    string[] cols = Regex.Split(line, "\t| ");
                    try
                    {
                        PatternTools.MSParser.Ion i = new PatternTools.MSParser.Ion(double.Parse(cols[0]), double.Parse(cols[1]), ms.CromatographyRetentionTime, ms.ScanNumber);
                        ms.MSData.Add(i);
                    }
                    catch
                    {
                        throw new Exception("An inconsistency has been found in file: " + file + "\nThe line reads:\n" + line);
                    }
                }

            }


            theMS2.Add(ms);

            //The first one is always bogus!
            if (theMS2.Count > 0)
            {
                theMS2.RemoveAt(0);
            }

            sr.Close();

            return theMS2;

        }
    }
}
