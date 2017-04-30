using CometWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindow
{
    [Serializable]
    class cometParams
    {
        public string SequenceDatabase { get; set; }
        
        public bool IonsA { get; set; }
        public bool IonsB { get; set; }
        public bool IoncC { get; set; }
        public bool IonsX { get; set; }
        public bool IonsY { get; set; }
        public bool IonsZ { get; set; }

        public double PrecursorMassTolerance { get; set; }
        public int Enzyme { get; set; }
        public int EnzymeSpecificity { get; set; }
        public int MissedCleavages { get; set; }
        public double FragmentBinTolerance { get; set; }
        public double FragmentBinOffset { get; set; }
        public int TheoreticalFragIons { get; set; }
        public int MaxVariableModsPerPeptide { get; set; }

        public double ClearMZRangeMin { get; set; }
        public double ClearMZRangeMax { get; set; }

        public double SearchMassRangeMin { get; set; }
        public double SearchMassRangeMax { get; set; }

        List<Modification> MyModificationItems { get; set; }

        public cometParams() { }


    }
}
