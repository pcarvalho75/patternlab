using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PatternTools.MSParserLight;

namespace PatternTools.MSParser
{
    public static class MSPrinter
    {
        public static void PrintMSListToFile (List<MSFull> mySpectra, string fileName, string header, MSFileFormat outputFormat)
        {
            StreamWriter sw = new StreamWriter(fileName);

            if (header.Length > 0)
            {
                sw.WriteLine(header);
            }

            foreach (MSFull ms in mySpectra)
            {
                string spec = "";

                if (outputFormat == MSFileFormat.ms2)
                {
                     spec = PrintSpectrumMS2(ms);
                }
                else if (outputFormat == MSFileFormat.mgf)
                {
                    throw new Exception("Not implemented");
                }

                sw.Write(spec);
            }

            sw.Close();

        }

        public static void PrintMSListToFile(List<MSLight> mySpectra, string outputFileName, string header, MSFileFormat outputFormat)
        {
            StreamWriter sw = new StreamWriter(outputFileName);

            if (header.Length > 0)
            {
                sw.WriteLine(header);
            }

            foreach (MSLight ms in mySpectra)
            {
                string spec = "";

                if (outputFormat == MSFileFormat.ms2)
                {
                    spec = PrintSpectrumMS2(ms);

                }
                else if (outputFormat == MSFileFormat.mgf)
                {
                    throw new Exception("Method not implemented");
                }

                sw.Write(spec);
            }

            sw.Close();


        }

        public static string PrintSpectrumMS2(MSLight ms)
        {
            StringBuilder sw = new StringBuilder();

            bool isFullScan = false;
            if (ms.ActivationType.Equals("")) { isFullScan = true; }

            sw.Append("S\t" + ms.ScanNumber.ToString("000000") + "\t" + ms.ScanNumber.ToString("000000"));
            if (isFullScan) {
                sw.Append("\n");
            } else {
                sw.Append("\t" + ms.ChargedPrecursor + "\n");
            }
                
            sw.Append("I\tRetTime\t" + ms.CromatographyRetentionTime + "\n");
            sw.Append("I\tIonInjectionTime\t" + ms.IonInjectionTime + "\n");
            if (!isFullScan)
            {
                sw.Append("I\tActivationType\t" + ms.ActivationType + "\n");
            }
            sw.Append("I\tInstrumentType\t" + ms.InstrumentType + "\n");

            if (!isFullScan)
            {
                foreach (string s in ms.ILines)
                {
                    sw.Append(s + "\n");
                }

                foreach (string zLine in ms.ZLines)
                {
                    //Write the Z Lines
                    sw.Append(zLine + "\n");
                }
            }

            List<Ion> myIons = ms.Ions;
            myIons.Sort((a, b) => a.MZ.CompareTo(b.MZ));

            for (int i = 0; i < myIons.Count; i++)
            {

                sw.Append(myIons[i].MZ + " " + myIons[i].Intensity + "\n");
            }


            return sw.ToString();
        }

        public static string PrintSpectrumMS2(MSFull ms)
        {
            StringBuilder sw = new StringBuilder();

            sw.Append("S\t" + ms.ScanNumber + "\t" + ms.ScanNumber + "\t" + ms.ChargedPrecursor+"\n");
            sw.Append("I\tRetTime\t" + ms.CromatographyRetentionTime + "\n");
            sw.Append("I\tIonInjectionTime\t" + ms.IonInjectionTime + "\n");
            sw.Append("I\tActivationType\t" + ms.ActivationType + "\n");
            sw.Append("I\tInstrumentType\t" + ms.InstrumentType + "\n");
            foreach (string iline in ms.Ilines)
            {
                sw.Append("I\tAdditionalInformation:\t" + iline + "\n");
            }

            if (ms.isMS2)
            {
                if (ms.ZLines.Count > 0)
                {
                    foreach (string zLine in ms.ZLines)
                    {
                        //Write the Z Lines
                        sw.Append(zLine + "\n");
                    }
                }
                else if (ms.Charges.Count > 0)
                {
                    foreach (int charge in ms.Charges)
                    {
                        sw.Append("Z\t" + charge + "\t" + PatternTools.pTools.DechargeMSPeakToPlus1(ms.ChargedPrecursor, charge) + "\n");
                    }
                }
            }

            foreach (Ion i in ms.MSData)
            {
                sw.Append(i.MZ + " " + i.Intensity + "\n");
            }


            return sw.ToString();
        }

        public static string PrintSpectrumMGF(MSFull ms)
        {
            StringBuilder sw = new StringBuilder();

            sw.AppendLine("BEGIN IONS");
            sw.AppendLine("TITLE=Spectrum " + ms.ScanNumber + "; " + string.Join("; ", ms.Ilines));
            sw.AppendLine("RITINSeconds="+ms.CromatographyRetentionTime);
            sw.AppendLine("CHARGE=" + string.Join(",", ms.Charges));
            sw.AppendLine("PEPMASS="+ms.ChargedPrecursor);
            

            foreach (Ion i in ms.MSData)
            {
                sw.AppendLine(i.MZ + " " + i.Intensity);
            }


            return sw.ToString();
        }



        
    }
}
