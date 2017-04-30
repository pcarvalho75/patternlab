using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PepExplorer2
{
    /// <summary>
    /// The arguments passed by command line or gui will be stored and mannaged by ProgramArgs class.
    /// </summary>
    [Serializable]
    public class ProgramArgs
    {
        /// <summary>
        /// Input for searching
        /// </summary>
        public string MPexResultPath { get; set; }

        /// <summary>
        /// Minimum DeNovoScore
        /// </summary>
        ///[XmlAttribute("Minimum_denovo_score")]
        public double MinDeNovoScore { get; set; }

        /// <summary>
        /// Type of de novo result.
        /// </summary>
        //[XmlAttribute ("De_novo_output_type")]
        public DeNovoOption DeNovoOption { get; set; }

        /// <summary>
        /// The path to the directory where the de novo files are placed.
        /// </summary>
        //[XmlAttribute("De_novo_directory_path")]
        public string DeNovoResultDirectory { get; set; }

        /// <summary>
        /// The file format of the sequence database.
        /// </summary>
        //[XmlAttribute("Data_base_format")]
        public DataBaseOption DataBaseFormat { get; set; }

        /// <summary>
        /// The path to the database file 
        /// </summary>
        //[XmlAttribute("Data_base_directory_path")]
        public string DataBaseFile { get; set; }

        /// <summary>
        /// The path to the substitution matrix. Default is pam30ms.
        //[XmlAttribute("Substitution_matrix")]
        public MatrixOption SubstitutionMatrix { get; set; }

        /// <summary>
        /// The desirable alignment top hits that will be analysed.
        /// </summary>
        //[XmlAttribute("Top_hits")]
        public int TopHits { get; set; }

        /// <summary>
        /// The minimmun size of the peptides to be used in the analysis.
        /// </summary>
        //[XmlAttribute("peptide_minimun_nuber")]
        public int PeptideMinNumAA { get; set; }

        /// <summary>
        /// The tag name inserted in the decoy sequences header.
        /// </summary>
       // [XmlAttribute("Decoy_tag_name")]
        public string DecoyLabel { get; set; }

        /// <summary>
        /// FDR score for filtering.
        /// </summary>
        //[XmlAttribute("fdr_score")]
        public double FDR { get; set; }

        /// <summary>
        /// Minimum DeNovoScore
        /// </summary>
        ///[XmlAttribute("Minimum_denovo_score")]
        public double MinIdentity { get; set; }

        /// <summary>
        /// ProgramArgs constructor.
        /// </summary>
        public ProgramArgs()
        {
        }

        /// <summary>
        /// ProgramArgs constructor.
        /// </summary>
        /// <param name="dnvopt">Type of de novo result.</param>
        /// <param name="dnvPath">The path to the directory where the de novo files are placed.</param>
        /// <param name="dbOpt">The file format of the sequence database.</param>
        /// <param name="dbpPath">The path to the database directory </param>
        /// <param name="matrixPath">The substitution matrix. Default is pam30ms.</param>
        /// <param name="Thits">The desirable alignment top hits that will be analysed.</param>
        /// <param name="minAA">The minimmun size of the peptides to be used in the analysis.</param>
        /// <param name="decoy">The tag name inserted in the decoy sequences header.</param>
        /// <param name="oCost">The alignment penalty for open gaps.</param>
        /// <param name="eCost">The alignment penalty for extending gaps.</param>
        public ProgramArgs(DeNovoOption dnvopt,
            string dnvPath,
            string mPexPath,
            DataBaseOption dbOpt,
            string dbpPath,
            MatrixOption matrix,
            int Thits,
            int minAA,
            string decoy,
            double minDenovoScore,
            double minIdent
            )
        {
            DeNovoOption = dnvopt;
            DeNovoResultDirectory = dnvPath;
            MPexResultPath = mPexPath;
            DataBaseFormat = dbOpt;
            DataBaseFile = dbpPath;
            SubstitutionMatrix = matrix;
            TopHits = Thits;
            PeptideMinNumAA = minAA;
            DecoyLabel = decoy;
            MinDeNovoScore = minDenovoScore;
            MinIdentity = minIdent;
        }

    }
}
