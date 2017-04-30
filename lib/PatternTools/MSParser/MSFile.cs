using System;
using System.Collections.Generic;
using System.Text;

namespace PatternTools.MSParser
{
    public class MSFile
    {
        public string FileName { get; set; }


        /// <summary>
        /// The windowdictionary contains the center of the window where the scans happened, its values is the list of MS2 spectra
        /// The list contains the MS objects.  They could be compressed or not so be careful!
        /// </summary>
        public Dictionary<double, List<MSFull>> WindowDictionary { get; set; }
        public string Header { get; set; }

        public MSFile()
        {
            //Lets put in a conservative low estimate of 500 spectra in 1 run
            WindowDictionary = new Dictionary<double, List<MSFull>>(500);
        }
    }
}
