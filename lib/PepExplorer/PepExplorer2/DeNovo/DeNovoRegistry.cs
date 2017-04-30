using PatternTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PepExplorer2.DeNovo
{
    /// <summary>
    /// Representation of the identified peptide.
    /// </summary>
    [Serializable]
    public class DeNovoRegistry
    {
        //just to speed up
        public bool IsDecoy { get; set; }

        /// <summary>
        /// The rank of the peptide in the output file.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// The peptide scan number.
        /// </summary>
        public int ScanNumber { get; set; }

        /// <summary>
        /// The Score attributed by the de novo tool.
        /// </summary>
        public double DeNovoScore { get; set; }

        /// <summary>
        /// The peptdide sequence.
        /// </summary>
        public string Sequence { get; set; }

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

        public string CleanSequence { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DeNovoRegistry()
        {
        }

        /// <summary>
        /// Peptide representation.
        /// </summary>
        /// <param name="rnk">The rank of the peptide in the output file</param>
        /// <param name="scan">The peptide scan number</param>
        /// <param name="dnvScr">The Score attributed by the de novo tool</param>
        /// <param name="ptmSeq">The peptide sequence with modifications</param>
        /// <param name="chrg">The peptide charge</param>
        /// <param name="file">The peptide original file name</param>
        public DeNovoRegistry(int rnk, int scan, double dnvScr, string ptmSeq, int chrg, string file)
        {
            Rank = rnk;
            ScanNumber = scan;
            DeNovoScore = dnvScr;
            Sequence = Regex.Replace(ptmSeq, @"[\+|\-|\d|\.|(|)|\?]", "");
            PtmSequence = ptmSeq;
            Charge = chrg;
            FileName = file;
            CleanSequence = PatternTools.pTools.CleanPeptide(PtmSequence, true);
        }

        public double GX { get; set; }
    }
}
