using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.XIC
{
    [Serializable]
    public class SequenceHit
    {
        public string Seqeunce { get; set; }

        public List<int> ScanNumber { get; set; }

        public SequenceHit(string sequence, List<int> scanNumber) {
            
            this.Seqeunce = sequence;
            this.ScanNumber = scanNumber;

        }

        /// <summary>
        /// Used for serialization purposes
        /// </summary>
        public SequenceHit() { }
    }
}
