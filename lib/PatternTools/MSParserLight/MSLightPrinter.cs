using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PatternTools.MSParserLight
{
    public static class MSLightPrinter
    {
        public static void PrintSpectrumMS2(StreamWriter sr, MSLight theSpectrum, string identification = null, string fileName = null) {

            sr.WriteLine("S\t" + theSpectrum.ScanNumber + "\t" + theSpectrum.ScanNumber + "\t" + theSpectrum.ChargedPrecursor);
            sr.WriteLine("I\tRetTime\t" + theSpectrum.CromatographyRetentionTime);
            sr.WriteLine("I\tActivationType\t" + theSpectrum.ActivationType);
            sr.WriteLine("I\tInstrumentType\t" + theSpectrum.InstrumentType);


            foreach (string i in theSpectrum.ILines)
            {
                sr.WriteLine(i);
            }

            if (identification != null)
            {
                sr.WriteLine("I\tPeptide\t" + identification);
            }

            if (fileName != null)
            {
                sr.WriteLine("I\tFileName\t" + fileName);
            }

            foreach (string zLine in theSpectrum.ZLines)
            {
                sr.WriteLine(zLine);
            }

            for (int i = 0; i < theSpectrum.MZ.Count; i++)
            {
                sr.WriteLine(Math.Round(theSpectrum.MZ[i],5) + " " + Math.Round(theSpectrum.Intensity[i],5));
            }

        }

        public static void PrintSpectrumMS2(StreamWriter sr, MSUltraLight theSpectrum, string identification = null, string fileName = null)
        {

            sr.WriteLine("S\t" + theSpectrum.ScanNumber + "\t" + theSpectrum.ScanNumber + "\t" + theSpectrum.Precursors[0].Item1);
            sr.WriteLine("I\tRetTime\t" + theSpectrum.CromatographyRetentionTime);


            if (identification != null)
            {
                sr.WriteLine("I\tPeptide\t" + identification);
            }

            if (fileName != null)
            {
                sr.WriteLine("I\tFileName\t" + fileName);
            }

            foreach (var precursor in theSpectrum.Precursors)
            {
                sr.WriteLine("Z\t" + precursor.Item2 + "\t" + PatternTools.pTools.DechargeMSPeakToPlus1(precursor.Item1, precursor.Item2));
            }

            for (int i = 0; i < theSpectrum.Ions.Count; i++)
            {
                sr.WriteLine(Math.Round(theSpectrum.Ions[i].Item1, 5) + " " + Math.Round(theSpectrum.Ions[i].Item2, 5));
            }

        }

    }
}
