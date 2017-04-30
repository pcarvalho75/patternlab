using PatternTools.MSParser;
using PatternTools.MSParserLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEProQ.IsobaricQuant
{
    public class MultiNotchMS3Item
    {
        public List<Ion> MyIons { get; set; }
        public int MS2PrecursorScan { get; set; }
        
        public MultiNotchMS3Item(List<Ion> myIons, int ms2PrecursorScan)
        {
            MyIons = myIons;
            MS2PrecursorScan = ms2PrecursorScan;
        }

        public MultiNotchMS3Item(MSLight scn)
        {
            string precursorScan = scn.ILines.Find(a => a.Contains("PrecursorScan"));

            string[] cols = Regex.Split(precursorScan, "\t");

            MyIons = scn.Ions;
            MS2PrecursorScan = int.Parse(cols[2]);
        }
    }
}
