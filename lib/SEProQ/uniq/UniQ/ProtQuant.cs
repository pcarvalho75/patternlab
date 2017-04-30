using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniQ
{
    public class ProtQuant
    {
        public FastaItem MyFastaItem {get; set;}
        public List<PepQuant> MyPepQuants { get; set; }

        public ProtQuant (FastaItem fi, PepQuant pq)
        {
            MyFastaItem = fi;
            MyPepQuants = new List<PepQuant>() { pq };
        }

        public ProtQuant(FastaItem fi, List<PepQuant> pqs)
        {
            MyFastaItem = fi;
            MyPepQuants = pqs;
        }

        public ProtQuant (FastaItem fi)
        {
            MyFastaItem = fi;
            MyPepQuants = new List<PepQuant>();
        }
    }
}
