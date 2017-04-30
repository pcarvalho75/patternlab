using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regrouper.GroupingElements.Parser
{
    class DNSAFGroupingItem
    {
        public string Locus { get; set; }
        public double USpC { get; set; }
        public int Index { get; set; }
        public double SSpC { get; set; }
        public double DSAF { get; set; }

        public DNSAFGroupingItem(string locus, int index, double uspc)
        {
            Locus = locus;
            USpC = uspc;
            Index = index;
            DSAF = 0;
        }

        public double DNSAF { get; set; }
    }
}
