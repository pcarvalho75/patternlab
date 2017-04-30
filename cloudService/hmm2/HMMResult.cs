using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace hmm3
{
    [Serializable]
    public class HMMResult
    {
        /// target name        accession  tlen query name           accession
        /// qlen   E-value  score  bias   #  of 
        /// c-Evalue  i-Evalue  score  bias  from    to
        /// from    to
        /// from    to  acc description of target
        public string TargetName { get; set; }
        public string Accession { get; set; }
        public int TLen { get; set; }
        public string QName { get; set; }
        public string QAccession { get; set; }
        public int QLen { get; set; }
        public double QEValue { get; set; }
        public double QSequenceScore { get; set; }
        public double QSequenceBias { get; set; }
        public int No { get; set; }
        public int Of { get; set; }
        public double CEvalue { get; set; }
        public double IEvalue { get; set; }
        public double DScore { get; set; }
        public double DBias { get; set; }
        public int DFrom { get; set; }
        public int DTo { get; set; }
        public int AFrom { get; set; }
        public int ATo { get; set; }
        public int EnvTo { get; set; }
        public int EnvFrom { get; set; }
        public double ACC { get; set; }
        public string Description { get; set; }

        public HMMResult(string tblResultLine)
        {
            string[] cols = Regex.Split(tblResultLine, "[ ]+");

            TargetName = cols[0];
            Accession = cols[1];
            TLen = int.Parse(cols[2]);
            QName = cols[3];
            QAccession = cols[4];
            QLen = int.Parse(cols[5]);
            QEValue = double.Parse(cols[6]);
            QSequenceScore = double.Parse(cols[7]);
            QSequenceBias = double.Parse(cols[8]);
            No = int.Parse(cols[9]);
            Of = int.Parse(cols[10]);
            CEvalue = double.Parse(cols[11]);
            IEvalue = double.Parse(cols[12]);
            DScore = double.Parse(cols[13]);
            DBias = double.Parse(cols[14]);
            DFrom = int.Parse(cols[15]);
            DTo = int.Parse(cols[16]);
            AFrom = int.Parse(cols[17]);
            ATo = int.Parse(cols[18]);
            EnvFrom = int.Parse(cols[19]);
            EnvTo = int.Parse(cols[20]);

            ACC = double.Parse(cols[21]);

            Description = cols[22];

            if (cols.Length > 22)
            {
                for (int i = 23; i < cols.Length; i++)
                {
                    Description += " " + cols[i];

                }
            }

            Console.WriteLine(".");

        }
    }

}