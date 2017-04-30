using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PatternTools.TReXS.RegexGeneration
{
    public class RegexGenerationParams
    {

        public double PrecursorLowerBound { get; set; }
        public double PrecursorUpperBound { get; set; }
        public bool UsePrecursorInformation { get; set; }
        public int MaxRegexes { get; set; }
        public double MinTrustedCharge { get; set; }
        public double PPMTolerance { get; set; }
        public int GapsInTable { get; set; }
        public int MinNonGappedAAs { get; set; }
        public int MinSingleAA { get; set; }

        public RegexGenerationParams(
            double precursorLowerBound,
            double precursorUpperBound,
            bool usePrecursorInformation,
            int maxRegexes,
            double minTrustedCharge,
            int minNonGappedAAs,
            double ppmTolerance,
            int gapsInTable,
            int minSingleAA
            )
        {
            this.PrecursorLowerBound = precursorLowerBound;
            this.PrecursorUpperBound = precursorUpperBound;
            this.UsePrecursorInformation = usePrecursorInformation;
            this.MaxRegexes = maxRegexes;
            this.MinTrustedCharge = minTrustedCharge;
            this.PPMTolerance = ppmTolerance;
            this.GapsInTable = gapsInTable;
            this.MinNonGappedAAs = minNonGappedAAs;
            this.MinSingleAA = minSingleAA;
        }



        internal string PrintHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#PrecursorLowerBound\t" + PrecursorLowerBound+ "\n");
            sb.Append("#PrecursorUpperBound\t" + PrecursorUpperBound+ "\n");
            sb.Append("#UsePrecursorInformation\t" + UsePrecursorInformation+ "\n");
            sb.Append("#MaxRegexes\t" + MaxRegexes+ "\n");
            sb.Append("#MinTrustedCharge\t" + MinTrustedCharge+ "\n");
            sb.Append("#PPMTolerance\t" + PPMTolerance+ "\n");
            sb.Append("#GapsInTable\t" + GapsInTable+ "\n");
            sb.Append("#Minimum single AA\t" + MinSingleAA+ "\n");
            return (sb.ToString());
        }
    }
}
