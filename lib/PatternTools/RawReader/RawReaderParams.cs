using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.RawReader
{
    [Serializable]
    public class RawReaderParams
    {
        public bool ExtractMS1 { get; set; }
        public bool ExtractMS2 { get; set; }
        public bool ExtractMS3 { get; set; }

        public bool UseThermoMonoIsotopicPrediction { get; set; }
        
        /// <summary>
        /// Some Thermo inferences are screwed up, so we try to inferr them and discard their algorithm
        /// </summary>
        public double PPMToleranceForInferingPrecursorMZ { get; set; }

        public bool ActivateSpectraCleaner { get; set; }
        public int SpectraCleanerWindowSize { get; set; }
        public int SpectraCleanerMaxPeaksPerWindow { get; set; }
        public int SpectraCleanerMaxSpectraPerFile { get; set; }

        public void LoadDefaults()
        {
            ExtractMS1 = false;
            ExtractMS2 = true;
            UseThermoMonoIsotopicPrediction = true;
            PPMToleranceForInferingPrecursorMZ = 60;
            ActivateSpectraCleaner = false;
            SpectraCleanerWindowSize = 100;
            SpectraCleanerMaxPeaksPerWindow = 30;
            SpectraCleanerMaxSpectraPerFile = 1000;
        }
    }
}
