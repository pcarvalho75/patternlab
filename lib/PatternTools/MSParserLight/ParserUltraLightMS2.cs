using PatternTools.MSParserLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatternTools.MSParserLight
{
    public static class ParserUltraLightMS2
    {

        /// <summary>
        /// The result is sorted by scan number
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<MSUltraLight> ParseSpectra(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            if (!fi.Exists)
            {
                throw new Exception("File " + fi.FullName + " not found.");
            }
            List<MSUltraLight> theSpectra = new List<MSUltraLight>(100);

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


                List<Tuple<float, float>> ionsTemp = new List<Tuple<float, float>>();

                string line;

                MSUltraLight ms = new MSUltraLight();
                ms.Precursors = new List<Tuple<float, short>>();
                while ((line = sr.ReadLine()) != null)
                {
                    if (isNumber.IsMatch(line))
                    {
                        string[] cols = mzSeparator.Split(line);
                        ionsTemp.Add(new Tuple<float, float>(float.Parse(cols[0]), float.Parse(cols[1])));
                    }
                    else if (RegExRetTime.IsMatch(line))
                    {
                        string[] theStrings2 = tabSeparator.Split(line);
                        ms.CromatographyRetentionTime = float.Parse(theStrings2[2]);
                    }
                    else if (line.StartsWith("Z"))
                    {
                        string[] cols = Regex.Split(line, "\t");
                        ms.Precursors.Add(new Tuple<float, short>((float.Parse(cols[2])), short.Parse(cols[1])));
                    }
                    else if (line.StartsWith("S"))
                    {
                        //We have a new spectra // scan
                        ms.UpdateIons(ionsTemp);

                        if (fi.Extension.Equals(".ms2"))
                        {
                            ms.MSLevel = 2;
                        }
                        else
                        {
                            ms.MSLevel = 3;
                        }

                        theSpectra.Add(ms);
                        //Step 2:Get the new one ready
                        ms = new MSUltraLight();
                        ms.Precursors = new List<Tuple<float, short>>();
                        ionsTemp = new List<Tuple<float, float>>();
                        string[] cols = tabSeparator.Split(line);
                        ms.ScanNumber = int.Parse(cols[1]);
                        ms.FileNameIndex = -1;
                    }
                }

                ms.UpdateIons(ionsTemp);

                if (fi.Extension.Equals(".ms2"))
                {
                    ms.MSLevel = 2;
                }
                else
                {
                    ms.MSLevel = 3;
                }

                theSpectra.Add(ms);

                sr.Close();

                //Just in case
                theSpectra.Sort((a, b) => a.ScanNumber.CompareTo(b.ScanNumber));
                theSpectra.RemoveAt(0);

                return theSpectra;
            }

            else
            {
                throw new Exception(fi.Extension + " is not an acceptable extendion");
            }
        }
    }
}
