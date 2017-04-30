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
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                MainForm mf = new MainForm();
                //mf.ShowDialog();
                return;
            }
            FileInfo file;
            file = new FileInfo(args[0]);

            RawReaderParams readerParams = new RawReaderParams();
            readerParams.LoadDefaults();

            Reader reader = new Reader(readerParams);
            
            List<MSLight> allSpectra = reader.GetSpectra(file.FullName, new List<int>(), false);

            if (readerParams.ExtractMS1)
            {
                List<MSLight> msPrint = allSpectra.FindAll( a => a.ActivationType.Equals(""));
                SpectraPrinter.PrintFile(file, msPrint , ".ms1", readerParams);
            }

            if (readerParams.ExtractMS2)
            {
                List<MSLight> msPrint = allSpectra.FindAll( a => !a.ActivationType.Equals(""));
                SpectraPrinter.PrintFile(file, msPrint , ".ms2", readerParams);
            }

            
        }


    }
}
