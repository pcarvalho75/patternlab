using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternTools.FastaParser
{
    [Serializable]
    public class FastaItem
    {
        double mass = -1;
        double coverage = -1;
        public string SequenceIdentifier { get; set; }
        public string Description { get; set; }
        public string Sequence { get; set; }
        public double ExtraDouble { get; set; }

        public List<object> Objects { get; set; }

        public byte[] SequenceInBytes { get; set; }

        public void ResetCoverage()
        {
            coverage = -1;
        }



        public double Coverage(List<string> uniquePeptides, bool forceRecalculate = false)
        {

            if (coverage == -1 || forceRecalculate)
            {
                //calculate coverage
                //First we create an array of 0's of the size of the sequence
                int[] matchLocations = new int[Sequence.Length];

                foreach (string peptide in uniquePeptides)
                {
                    //Make sure the peptide is clean
                    string cleanedPeptide = PatternTools.pTools.CleanPeptide(peptide, true);


                    Sequence = Sequence.Replace("*", "");

                    Match location = Regex.Match(Sequence, cleanedPeptide);
                    for (int i = location.Index; i < location.Index + location.Length; i++)
                    {
                        matchLocations.SetValue(1, i);
                    }
                }

                double sum = matchLocations.Sum();
                coverage = sum / (double)Sequence.Length;

            }

            return coverage;
        }

        public List<int> CoverageLocations(List<string> peps)
        {
            List<int> matchLocations = new int[Sequence.Length].ToList();

            foreach (string peptide in peps)
            {
                string cleanPeptide = PatternTools.pTools.CleanPeptide(peptide, true);

                Match location = Regex.Match(Sequence, cleanPeptide);

                for (int i = location.Index; i < location.Index + location.Length; i++)
                {
                    matchLocations[i] = 1;
                }
            }
            return matchLocations;
        }

        
        public double MonoisotopicMass
        {
            get
            {
                if (mass == -1)
                {
                    AminoacidMasses aa = new AminoacidMasses();
                    mass = aa.CalculatePeptideMass(Sequence, false, true, false);
                }
                return mass;
            }
        }

        public FastaItem()
        {
            Objects = new List<object>();
        }

        public FastaItem(string protID, string sequence, string description)
        {
            SequenceIdentifier = protID;
            Sequence = sequence;
            Description = description;
        }

        public FastaItem(string protID, string description)
        {
            SequenceIdentifier = protID;
            Description = description;
            Sequence = "";
        }

    }
}
