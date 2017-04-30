using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools;
using PatternTools.SQTParser;

namespace SEPRPackage
{
    [Serializable]
    public class MyProtein
    {
        public string Locus { get; set; }
        public List<SQTScan> Scans { get; set; }

        public int GroupNo { get; set; }

        public double Length { get; set; }
        public double MolWt { get; set; }

        //We include the set just for compatibility reasons with the result browser
        public int SequenceCount { get { return DistinctPeptides.Count; } set { } }
        public int SpectrumCount { get { return Scans.Count; } set {} }
        public double Coverage { get; set; }
        public double NSAF { get; set; }
        public string Description { get; set; }
        public double BayesianScore { get; set; }
        public string Sequence { get; set; }
        public ProteinGroupType MyGroupType { get; set; }

        List<string> distinctSequences = new List<string>();

        public MyProtein(string id, List<SQTScan> scans)
        {
            this.Locus = id;
            this.Scans = scans;
            GroupNo = 0;
            MyGroupType = ProteinGroupType.Undetermined;

            Coverage = -1;
            Length = -1;
            MolWt = -1;
            Description = "?";
        }

        public double ProteinScore
        {
            get
            {
                return Scans.Sum(a => a.PrimaryScore);
            }
        }

        public int ContainsUniquePeptide
        {
            get
            {
                int noUnique = (from s in Scans
                                where s.IsUnique
                                select s.PeptideSequenceCleaned).Distinct().Count();
                
                return noUnique;

                //return Scans.Exists(a => a.IsUnique);
            }
            set { }
        }


        public List<double> InputVector
        {
            get
            {
                List<double> inputVector = new List<double>(7);

                Scans.Sort((a, b) => b.BayesianScore.CompareTo(a.BayesianScore));
                SQTScan bestScan = Scans[0];

                //from all the peptides find the peptide that averages the best primary score
                inputVector.Add(bestScan.PrimaryScore); //0
                inputVector.Add(bestScan.SecondaryScore); //1
                //inputVector.Add(bestScan.BayesianScore); //2
                inputVector.Add(bestScan.DeltaCN); //3
                inputVector.Add(bestScan.Bayes_PPMScore); //4
                inputVector.Add(DistinctPeptides.Count() / Math.Log((double)Length)); //5
                inputVector.Add(Coverage); //6

                return inputVector;
            }
        }


        /// <summary>
        /// Dynamically calculated
        /// </summary>
        public List<PeptideResult> PeptideResults
        {
            get
            {
                var groupedByPeptide = (from scan in Scans
                                        group scan by scan.PeptideSequence into g
                                        select new { sequence = g.Key, Scans = g });

                List<PeptideResult> results = new List<PeptideResult>(groupedByPeptide.Count());
                foreach (var g in groupedByPeptide)
                {
                    results.Add(new PeptideResult(g.sequence, g.Scans.ToList()));
                }

                return (results);
            }

            set { }
        }



        public List<string> DistinctPeptides
        {
            get
            {
                if (distinctSequences.Count == 0)
                {
                    distinctSequences = Scans.Select(a => a.PeptideSequenceCleaned).Distinct().ToList();
                }
                return distinctSequences;
            }

            set { }
        }

        /// <summary>
        /// The number of states, for example, if a peptide having the same sequence is identified with different charge states, both will contribute to this number; the same applied to variable mods.
        /// </summary>
        public int NoStates
        {
            get
            {
                int noStates = 0;


                                     //Group By Signal
                var groupBySequence = (from p in PeptideResults
                                       group p by p.CleanedPeptideSequence into peptideGroup
                                       select new { sequence = peptideGroup.Key , thePeptides = peptideGroup });

                Dictionary<string, List<PeptideResult>> bySequence = groupBySequence.ToDictionary(a => a.sequence, a => a.thePeptides.ToList());

                foreach (KeyValuePair<string, List<PeptideResult>> kvp in bySequence)
                {
                    List<int> chargeStates = (from pep in kvp.Value
                                             from s in pep.MyScans
                                             select s.ChargeState).Distinct().ToList();

                    noStates += chargeStates.Count;
                }

                return noStates;
            }
        }
    }

}
