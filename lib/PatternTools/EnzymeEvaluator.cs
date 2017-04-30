using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternTools
{
    public class EnzymeEvaluator
    {
        Dictionary<Enzyme, Regex> enzimeRegexDictForPeps = new Dictionary<Enzyme, Regex>();
        Dictionary<Enzyme, Regex> enzimeRegexDictForSequences = new Dictionary<Enzyme, Regex>();

        public EnzymeEvaluator()
        {
            enzimeRegexDictForPeps.Add(Enzyme.Trypsin, new Regex("[-|K|R]" + Regex.Escape(".") + "[^P]", RegexOptions.Compiled));
            enzimeRegexDictForPeps.Add(Enzyme.LysC, new Regex("[-|K]" + Regex.Escape(".") + "[^P]", RegexOptions.Compiled));
            enzimeRegexDictForPeps.Add(Enzyme.ArgC, new Regex("[-|R]" + Regex.Escape(".") + "[^P]", RegexOptions.Compiled));

            enzimeRegexDictForSequences.Add(Enzyme.Trypsin, new Regex(@"[KR](?!P)", RegexOptions.Compiled));
            enzimeRegexDictForSequences.Add(Enzyme.LysC, new Regex(@"[K](?!P)", RegexOptions.Compiled));
            enzimeRegexDictForSequences.Add(Enzyme.ArgC, new Regex(@"[R](?!P)", RegexOptions.Compiled));
        }

        /// <summary>
        /// For a peptide peptide string
        /// </summary>
        /// <param name="peptide"></param>
        /// <param name="enzyme"></param>
        /// <returns></returns>
        public MatchCollection NoEnzymaticCleavages (string peptide, Enzyme enzyme)
        {
            return (enzimeRegexDictForPeps[enzyme].Matches(peptide));
        }

        /// <summary>
        /// The numbers report the position counting from index 0 and match the same results as in http://expasy.org/cgi-bin/peptidecutter/peptidecutter.pl
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="enzyme"></param>
        /// <param name="noEnzymaticTermini"></param>
        /// <returns></returns>
        public List<int> FindDigestionPositions(string sequence, Enzyme enzyme)
        {
            List<int> pos = new List<int>(30);
            MatchCollection mc = enzimeRegexDictForSequences[enzyme].Matches(sequence);

            foreach (Match match in mc)
            {
                if (match.Groups[0].Index + 1 < sequence.Length)
                {
                    char nextAA = sequence[match.Groups[0].Index + 1];
                    if (!nextAA.Equals("P"))
                    {
                        pos.Add(match.Groups[0].Index +1);
                    }
                }
                else
                {
                    pos.Add(match.Groups[0].Index +1);
                }
            }

            return(pos);
        }

        public List<string> DigestSequence(string sequence, Enzyme enzyme, int minPepLength)
        {
            List<string> peptides = new List<string>();
            
            //Get the cleavage sites
            List<int> pos = new List<int>() { 0 };
            pos.AddRange(FindDigestionPositions(sequence, enzyme));
            if (!pos.Contains(sequence.Length))
            {
                pos.Add(sequence.Length);
            }

            string peptide = "";
            for (int i = 0; i < pos.Count-1; i++)
            {
                string peptideTmp = sequence.Substring(pos[i], pos[i + 1] - pos[i]);

                peptide += peptideTmp;

                if (peptide.Length >= minPepLength)
                {
                    peptides.Add(peptide);
                    Console.WriteLine(peptide);
                    peptide = "";
                }

            }


            return peptides;

        }


        /// <summary>
        /// Not working well
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="enzyme"></param>
        /// <param name="noEnzymaticTermini"></param>
        /// <param name="allowedMisscleavages"></param>
        /// <param name="minPepLength"></param>
        /// <returns></returns>
        public List<string> DigestSequenceAllPossibilities(string sequence, Enzyme enzyme, int noEnzymaticTermini, int allowedMisscleavages, int minPepLength)
        {
            //Get the cleavage sites
            List<int> pos = new List<int>() { 0 };
            pos.AddRange(FindDigestionPositions(sequence, enzyme));
            if (!pos.Contains(sequence.Length))
            {
                pos.Add(sequence.Length );
            }

            List<string> peptides = new List<string>(50);

            //First Lets Generate the ones with two enzymatic termini

            for (int i = 0; i < pos.Count; i++)
            {
                for (int j = i+1; j <= i + allowedMisscleavages + 1; j++)
                {
                    if (j >= pos.Count) { continue; }
                    int length = pos[j] - pos[i];
                    if (length >= minPepLength)
                    {
                        if (noEnzymaticTermini == 2)
                        {
                            peptides.Add(sequence.Substring(pos[i], length));
                        }
                        else if (noEnzymaticTermini == 1)
                        {
                            string peptide = sequence.Substring(pos[i], length);
                            string reversePeptide = ReverseString(peptide);
                            
                            for (int k = minPepLength; k <= peptide.Length; k++)
                            {
                                peptides.Add(peptide.Substring(0, k));
                                peptides.Add(reversePeptide.Substring(0, k));
                            }
                        }
                    }
                }
            }

            peptides = peptides.Distinct().ToList();

            return peptides;
        }

        private static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);

        }
    }

    public enum Enzyme
    {
        Trypsin,
        ArgC,
        LysC
    }
}
