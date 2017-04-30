using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternTools.SQTParser
{
    [Serializable]
    public class SQTScan
    {
        double ppm = -1;
        public int NoEnzymeCleavages { get; set; }
        string tmpPeptideSequence = "-1";
        double tmpPeptideLengthNoParenthesis = -1;

        //Number of spectral counts peptides with this sequence has
        public double Sibblings { get; set; }
        public MSParserLight.MSLight MSLight { get; set; }

        public SQTScan(int ScanNumber, int MatchNumber)
        {
            this.ScanNumber = ScanNumber;
            this.MatchNumber = MatchNumber;
            IdentificationNames = new List<string>();
        }


        /// <summary>
        /// Returns the peptide sequence withouth the cleavage information
        /// </summary>
        public string PeptideSequenceCleaned
        {
            get
            {
                if (tmpPeptideSequence.Equals("-1"))
                {
                    tmpPeptideSequence = PatternTools.pTools.CleanPeptide(PeptideSequence, false);
                }
                return tmpPeptideSequence;
            }
        }

        public double LengthOfPeptideSequenceCleanedNoParenthesis
        {
            get
            {
                if (tmpPeptideLengthNoParenthesis == -1)
                {
                    tmpPeptideLengthNoParenthesis = (double)PatternTools.pTools.CleanPeptide(PeptideSequence, true).Length;
                }
                return tmpPeptideLengthNoParenthesis;
            }
        }

        public SQTScan()
        {
            IdentificationNames = new List<string>();

        }

        public double PPM_Orbitrap
        {
            get
            {

                if (ppm == -1)
                {
                    ppm = PatternTools.pTools.OrbitrapPPM(MeasuredMH, TheoreticalMH);
                }

                return ppm;
            }

            set { ppm = value; }
        }

        public int MatchNumber { get; set; }
        public int ScanNumber { get; set; }
        public int ChargeState { get; set; }
        public double ProbabilityValue { get; set; }
        public int NumberOfCandidates { get; set; }
        public int PrimaryRank { get; set; }
        public double SecondaryRank { get; set; }
        public double PrimaryScore { get; set; }
        public double SecondaryScore { get; set; }
        public double DeltaCN { get; set; }
        public int PeaksMatched { get; set; }
        public int PeaksConsidered { get; set; }
        public string PeptideSequence { get; set; }
        public List<string> IdentificationNames { get; set; }
        public double TheoreticalMH { get; set; }
        public double MeasuredMH { get; set; }
        public string FileName { get; set; }
        public int SequenceStartAANo { get; set; }
        public double TailScore { get; set; }

        int noForwardNames = -1;

        //Scoring
        public double BayesianScore { get; set; }
        public int BayesianClass { get; set; }
        public double Bayes_PresenceScore { get; set; }
        public double Bayes_DeltaCNScore { get; set; }
        public double Bayes_PrimaryScore { get; set; }
        public double Bayes_SecondaryScore { get; set; }
        public double Bayes_NumPeaksMatchedScore { get; set; }
        public double Bayes_PPMScore { get; set; }
        public double Bayes_EnzymeEfficiencyScore { get; set; }
        public double Bayes_SecondaryRankScore { get; set; }
        public List<double> Bayes_InputVector { get; set; }
        public double Bayes_ProbabilityScore { get; set; }
        public List<List<double>> Quantitation { get; set; }


        /// <summary>
        /// Returns true or false if we have more than 1 protein that matches to this peptide
        /// </summary>
        public bool IsUnique
        {
            get
            {
                if (IdentificationNames.Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            set { }
        }

        public string FileNameWithScanNumberAndChargeState
        {
            get
            {
                return (FileName + "." + ScanNumber + "." + ScanNumber + "." + ChargeState);
            }
            set {

                //We really cant set anything
            }
        }

        /// <summary>
        /// Used to certify if it is a forward or reverse hit
        /// </summary>
        public int CountNumberForwardNames(string labeledDecoyTag)
        {
                if (noForwardNames == -1)
                {
                    noForwardNames = IdentificationNames.FindAll(a => !a.StartsWith(labeledDecoyTag)).Count;
                }

                return noForwardNames;
        }


        public double Bayes_YCountScore { get; set; }
        public double Bayes_CompBYScore { get; set; }
        public double Bayes_SignalPercentageScore { get; set; }

        public int NoForms { get; set; }
    }
}
