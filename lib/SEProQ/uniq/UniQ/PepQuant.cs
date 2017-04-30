using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniQ
{
    [Serializable]
    public class PepQuant
    {
        /// <summary>
        /// Average Log (base e) fold change
        /// </summary>
        public double AVGLogFold { get; set; }
        
        /// <summary>
        /// Fold changes
        /// </summary>
        public List<double> Folds { get; set; }
        
        /// <summary>
        /// Peptide Sequence
        /// </summary>
        public string Sequence { get; set; }
        
        
        /// <summary>
        /// Single Tail t-test
        /// </summary>
        public double TTest { get; set; }
        
        /// <summary>
        /// Data from the scans and the signal intensities
        /// </summary>
        public List<Quant> MyQuants;

        /// <summary>
        /// This is used to measure when there are many missing values
        /// </summary>
        public double BinomialProbability { get; set; }

        public int MissingValues { get; set; }

        /// <summary>
        /// The patterntools cleansequence method is evoked in the constructor and result is stored here, to save time in downstream computations
        /// </summary>
        public string CleanPeptideSequence { get; set; }

        public List<string> MappableProteins { get; set; }
        


        public PepQuant() { }

        public PepQuant(string sequence)
        {
            Sequence = sequence;
            MyQuants = new List<Quant>();
            CleanPeptideSequence = PatternTools.pTools.CleanPeptide(sequence, true);
            MappableProteins = new List<string>();
            
        }


    }
}
