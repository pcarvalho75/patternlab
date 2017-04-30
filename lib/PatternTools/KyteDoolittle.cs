using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools
{
 
    public class KyteDoolittle
    {
        Dictionary<string, double> KScores = new Dictionary<string,double>();
        
        public KyteDoolittle () {
            KScores.Add("I", 4.5);
            KScores.Add("V", 4.2);
            KScores.Add("L", 3.8);
            KScores.Add("F", 2.8);
            KScores.Add("C", 2.5);
            KScores.Add("M", 1.9);
            KScores.Add("A", 1.8);
            KScores.Add("G", -0.4);
            KScores.Add("T",-0.7);
            KScores.Add("W", -0.9);
            KScores.Add("S", -0.8);
            KScores.Add("Y", -1.3);
            KScores.Add("P", -1.6);
            KScores.Add("H", -3.2);
            KScores.Add("E", -3.5);
            KScores.Add("Q", -3.5);
            KScores.Add("D", -3.5);
            KScores.Add("B", -3.5);
            KScores.Add("N", -3.5);
            KScores.Add("Z", -3.5);
            KScores.Add("K", -3.9);
            KScores.Add("R", -4.5);
        }

        public double GravyScore(string peptide) {
            double score = 0;

            for (int i = 0; i < peptide.Length; i++) {
                string s = peptide[i].ToString();
                score += KScores[s];
            }

            return score;
        }


        /// <summary>
        /// Metric Entropy (i.e., degree of uncertainty) can be measured by deviding ShannonEntropy by the string length)
        /// </summary>
        /// <param name="thePeptide"></param>
        /// <param name="entropyBase"></param>
        /// <returns></returns>
        public static double ShannonEntropy (string thePeptide, double entropyBase = 2)
        {
            //First lets generate a dictionary.

            double entropy = 0;

            List<char> peptideC = thePeptide.ToList();


            Dictionary<char, double> dict = (from c in peptideC
                                             group c by c into myGroups
                                             select new {AA = myGroups.Key, Count = (double)myGroups.Count() / (double)thePeptide.Length}).ToDictionary(a => a.AA, a => a.Count);


            foreach (KeyValuePair<char, double> kvp in dict)
            {
                entropy += kvp.Value * Math.Log(kvp.Value, entropyBase);
            }

            entropy *= -1;


            return entropy;
            

        }

        public static double PeptidePI(string peptide)
        {

            //http://isoelectric.ovh.org/files/practise-isoelectric-point.html#mozTocId826873

            int D = 0;
            int E = 0;
            int C = 0;
            int Y = 0;
            int H = 0;
            int K = 0;
            int R = 0;

            for (int i = 0; i < peptide.Length; i++)
            {
                string s = peptide[i].ToString();
                if (s.Equals("D"))
                {
                    D++;
                }
                else if (s.Equals("E"))
                {
                    E++;
                }
                else if (s.Equals("C"))
                {
                    C++;
                }
                else if (s.Equals("Y"))
                {
                    Y++;
                }
                else if (s.Equals("H"))
                {
                    H++;
                }
                else if (s.Equals("K"))
                {
                    K++;
                }
                else if (s.Equals("R"))
                {
                    R++;
                }

            }

            double pH = 0;

            double NQ = 0.0; //net charge in given pH

            double QN1 = 0;  //C-terminal charge
            double QN2 = 0;  //D charge
            double QN3 = 0;  //E charge
            double QN4 = 0;  //C charge
            double QN5 = 0;  //Y charge
            double QP1 = 0;  //H charge
            double QP2 = 0;  //NH2 charge
            double QP3 = 0;  //K charge
            double QP4 = 0;  //R charge

            while (true)
            {
                // we are using pK values form Wikipedia as they give quite good approximation
                // if you want you can change it

                QN1 = -1 / (1 + Math.Pow(10, (3.65 - pH)));
                QN2 = -D / (1 + Math.Pow(10, (3.9 - pH)));
                QN3 = -E / (1 + Math.Pow(10, (4.07 - pH)));
                QN4 = -C / (1 + Math.Pow(10, (8.18 - pH)));
                QN5 = -Y / (1 + Math.Pow(10, (10.46 - pH)));
                QP1 = H / (1 + Math.Pow(10, (pH - 6.04)));
                QP2 = 1 / (1 + Math.Pow(10, (pH - 8.2)));
                QP3 = K / (1 + Math.Pow(10, (pH - 10.54)));
                QP4 = R / (1 + Math.Pow(10, (pH - 12.48)));

                NQ = QN1 + QN2 + QN3 + QN4 + QN5 + QP1 + QP2 + QP3 + QP4;

                if (pH>=14.0)
                {
                    //you should never see this message
                    throw new Exception("ph > 14; we shoudn't be here");
                } else if (NQ<=0)
                {
                    // if this condition is true we can stop calculate
                    break;
                } else {
                    pH+=0.01;
                }
            }

            return Math.Round(pH,1);

        }
    }
}
