using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools
{
    public class DNA2Protein
    {
        Dictionary<string, string> DNA2ProteinDict {get; set;}
        
        public DNA2Protein()
        {
            DNA2ProteinDict = new Dictionary<string, string>();

            DNA2ProteinDict.Add("TTT", "F");
            DNA2ProteinDict.Add("TTC", "F");
            DNA2ProteinDict.Add("TTA", "L");
            DNA2ProteinDict.Add("TTG", "L");
            DNA2ProteinDict.Add("CTT", "L");
            DNA2ProteinDict.Add("CTC", "L");
            DNA2ProteinDict.Add("CTA", "L");
            DNA2ProteinDict.Add("CTG", "L");
            DNA2ProteinDict.Add("ATT", "I");
            DNA2ProteinDict.Add("ATC", "I");
            DNA2ProteinDict.Add("ATA", "I");
            DNA2ProteinDict.Add("ATG", "M"); //This codes M and serves as an initiation site: the first ATG in a DNA's coding region is whee translation begins.
            DNA2ProteinDict.Add("GTT", "V");
            DNA2ProteinDict.Add("GTC", "V");
            DNA2ProteinDict.Add("GTA", "V");
            DNA2ProteinDict.Add("GTG", "V");

            DNA2ProteinDict.Add("TCT", "S");
            DNA2ProteinDict.Add("TCC", "S");
            DNA2ProteinDict.Add("TCA", "S");
            DNA2ProteinDict.Add("TCG", "S");
            DNA2ProteinDict.Add("CCT", "P");
            DNA2ProteinDict.Add("CCC", "P");
            DNA2ProteinDict.Add("CCA", "P");
            DNA2ProteinDict.Add("CCG", "P");
            DNA2ProteinDict.Add("ACT", "T");
            DNA2ProteinDict.Add("ACC", "T");
            DNA2ProteinDict.Add("ACA", "T");
            DNA2ProteinDict.Add("ACG", "T");
            DNA2ProteinDict.Add("GCT", "A");
            DNA2ProteinDict.Add("GCC", "A");
            DNA2ProteinDict.Add("GCA", "A");
            DNA2ProteinDict.Add("GCG", "A");

            DNA2ProteinDict.Add("TAT", "Y");
            DNA2ProteinDict.Add("TAC", "Y");
            DNA2ProteinDict.Add("TAA", "*"); //Stop
            DNA2ProteinDict.Add("TAG", "*"); //Stop
            DNA2ProteinDict.Add("CAT", "H");
            DNA2ProteinDict.Add("CAC", "H");
            DNA2ProteinDict.Add("CAA", "Q");
            DNA2ProteinDict.Add("CAG", "Q");
            DNA2ProteinDict.Add("AAT", "N");
            DNA2ProteinDict.Add("AAC", "N");
            DNA2ProteinDict.Add("AAA", "K");
            DNA2ProteinDict.Add("AAG", "K");
            DNA2ProteinDict.Add("GAT", "D");
            DNA2ProteinDict.Add("GAC", "D");
            DNA2ProteinDict.Add("GAA", "E");
            DNA2ProteinDict.Add("GAG", "E");

            DNA2ProteinDict.Add("TGT", "C");
            DNA2ProteinDict.Add("TGC", "C");
            DNA2ProteinDict.Add("TGA", "*"); //STOP
            DNA2ProteinDict.Add("TGG", "W");
            DNA2ProteinDict.Add("CGT", "R");
            DNA2ProteinDict.Add("CGC", "R");
            DNA2ProteinDict.Add("CGA", "R");
            DNA2ProteinDict.Add("CGG", "R");
            DNA2ProteinDict.Add("AGT", "S");
            DNA2ProteinDict.Add("AGC", "S");
            DNA2ProteinDict.Add("AGA", "R");
            DNA2ProteinDict.Add("AGG", "R");
            DNA2ProteinDict.Add("GGT", "G");
            DNA2ProteinDict.Add("GGC", "G");
            DNA2ProteinDict.Add("GGA", "G");
            DNA2ProteinDict.Add("GGG", "G");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">Sequence must be in upper case!</param>
        /// <returns></returns>
        public string Translate(string s)
        {
            StringBuilder t = new StringBuilder();
            for (int i = 0; i <= s.Length-3; i += 3)
            {
                string subs = s.Substring(i, 3);
                t.Append(DNA2ProteinDict[subs]);
            }

            return (t.ToString());
        }


        public List<string> Translate6 (string s) {

            s = s.ToUpper();
            List<string> results = Translate3(s);

            //The other 3 frames
            StringBuilder newR = new StringBuilder();
            for (int i = s.Length - 1; i >= 0; i--)
            {
                string theChar = s.Substring(i, 1);

                if (theChar.Equals("C"))
                {
                    newR.Append("G");
                }
                else if (theChar.Equals("G"))
                {
                    newR.Append("C");
                }
                else if (theChar.Equals("T"))
                {
                    newR.Append("A");
                }
                else if (theChar.Equals("A"))
                {
                    newR.Append("T");
                }
                else
                {
                    throw new Exception("Illelagl chanracter " + theChar + " in nucleotides.");
                }
            }

            results.AddRange(Translate3(newR.ToString()));

            return results;
        }

        public List<string> Translate3(string s)
        {
            s = s.ToUpper();
            List<string> results = new List<string>(6);
            for (int i = 0; i < 3; i++)
            {
                results.Add(Translate(s.Substring(i, s.Length - i)));
            }
            return results;
        }
    }
}
