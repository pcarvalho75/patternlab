using PatternTools.MSParserLight;
using PatternTools.RawReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RawReader
{

    public static class SpectraPrinter
    {        

        public static void PrintFile(FileInfo rawFile, List<MSLight> ms2Print, string ext, RawReaderParams readerParams)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(rawFile.FullName);
            string outputFileName = rawFile.Directory.FullName + "/" + fileNameWithoutExtension + ext;

            if (readerParams.ActivateSpectraCleaner)
            {
                int fileCounter = 0;
                List<MSLight> tmpSpectraList = new List<MSLight>();
                foreach (MSLight mslight in ms2Print)
                {
                    tmpSpectraList.Add(mslight);

                    if (tmpSpectraList.Count == readerParams.SpectraCleanerMaxSpectraPerFile)
                    {
                        fileCounter++;
                        Print(tmpSpectraList, outputFileName + "_" + fileCounter + ext);
                        tmpSpectraList.Clear();
                    }
                }

                if (tmpSpectraList.Count > 0)
                {
                    fileCounter++;
                    Print(ms2Print, outputFileName + "_" + fileCounter + ext);
                    tmpSpectraList.Clear();
                }
            } else {

                Print(ms2Print, outputFileName);
            }
        }

        private static void Print(List<MSLight> ms2Print, string outputFileName)
        {
            StreamWriter sw = new StreamWriter(outputFileName);
            sw.WriteLine(PrintHeader(ms2Print));

            foreach (MSLight ms in ms2Print)
            {
                string spectrum = PatternTools.MSParser.MSPrinter.PrintSpectrumMS2(ms);
                sw.Write(spectrum);
            }
            sw.Close();
        }


        private static string PrintHeader(List<MSLight> mySpectra)
        {
            //Generate Header
            string header = "H\tCreationDate\t" + DateTime.Now + "\n";
            header += "H\tExtractor\tRawReader\n";
            header += "H\tFirstScan\t" + mySpectra[0].ScanNumber + "\n";
            header += "H\tLastScan\t" + mySpectra[mySpectra.Count - 1].ScanNumber + "\n";

            try
            {
                string[] stuff = Regex.Split(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationContext.Identity.FullName, ",");
                header += "H\tVersion\t" + stuff[1] + "\n";
            }
            catch
            {
                header += "H\tVersion\tUndetermined\n";
            }

            header += "H\tComments\tRawReader written by Paulo Costa Carvalho, 2012";
            return header;
        }
    }
}
