using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternTools.SQTParser2
{
    [Serializable]
    public class ID
    {
        public string Locus { get; set; }
        public string PeptideSequence { get; set; }

        public ID (string locus, string peptideSequence) 
        {
            Locus = locus;
            PeptideSequence = peptideSequence;
        }
    }
}
