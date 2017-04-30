using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PatternTools.MSParserLight
{
    public static class MSUltraLightPrinter
    {
        public static void PrintSpectrum(StreamWriter sw, MSUltraLight theSpectrum)
        {
            sw.WriteLine(PrintSpectrum(theSpectrum));
        }
        public static string PrintSpectrum(MSUltraLight theSpectrum)
        {
            StringBuilder sb = new StringBuilder();

            if (theSpectrum.MSLevel > 1)
            {
                sb.AppendLine("S\t" + theSpectrum.ScanNumber + "\t" + theSpectrum.ScanNumber + "\t" + theSpectrum.Precursors[0].Item1);
            } else
            {
                sb.AppendLine("S\t" + theSpectrum.ScanNumber + "\t" + theSpectrum.ScanNumber);
            }

            sb.AppendLine("I\tRetTime\t" + theSpectrum.CromatographyRetentionTime);
            sb.AppendLine("I\tInstrumentType\t" + theSpectrum.InstrumentType);

            if (theSpectrum.MSLevel > 1)
            {
                foreach (Tuple<float, short> precursor in theSpectrum.Precursors)
                {
                    sb.AppendLine("Z\t" + precursor.Item2 + "\t" + PatternTools.pTools.DechargeMSPeakToPlus1(precursor.Item1, precursor.Item2));
                }
            }

            for (int i = 0; i < theSpectrum.Ions.Count; i++)
            {
                sb.AppendLine(Math.Round(theSpectrum.Ions[i].Item1, 5) + " " + Math.Round(theSpectrum.Ions[i].Item2, 5));
            }

            return sb.ToString();
        }

        public static void PrintMS2List(List<MSUltraLight> myMS2, string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            foreach (MSUltraLight ms in myMS2)
            {
                PrintSpectrum(sw, ms);
            }
            sw.Close();
        }
    }
}

