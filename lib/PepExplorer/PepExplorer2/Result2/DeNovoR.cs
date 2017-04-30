using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepExplorer2.Result2
{
    [Serializable]
    public class DeNovoR
    {
        /// <summary>
        /// The peptide scan number.
        /// </summary>
        public int ScanNumber { get; set; }

                /// <summary>
        /// The Score attributed by the de novo tool.
        /// </summary>
        public double DeNovoScore { get; set; }
        
        /// <summary>
        /// The peptide sequence with modifications.
        /// </summary>
        public string PtmSequence { get; set; }

        /// <summary>
        /// The peptide charge.
        /// </summary>
        public int Charge { get; set; }

        /// <summary>
        /// The peptide original file name.
        /// </summary>
        public string FileName { get; set; }

        public DeNovoR() { }

        public DeNovoR(int scnNo, double dnScore, string ptmSeq, int z, string fileName)
        {
            ScanNumber = scnNo;
            DeNovoScore = dnScore;
            PtmSequence = ptmSeq;
            Charge = z;
            FileName = fileName;
        }


    }
}
