using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.SQTParser;
using SEPRPackage;
using System.Threading.Tasks;

namespace SEProcessor
{
    class PeptideManager
    {
        public List<SQTScan> MyScans { get; set; }
        public List<PeptideResult> MyPeptides { get; set; }
        Parameters myParams;
        
        public double FDR
        {
            get
            {
                List<PeptideResult> badPeptides = MyPeptides.FindAll(a => a.MyScans[0].CountNumberForwardNames(myParams.LabeledDecoyTag) == 0);
                double fdr = (double)(badPeptides.Count) / (double)MyPeptides.Count;
                return fdr;
            }
        }

        public double NoLabeledDecoyPeptides
        {
            get
            {
                List<PeptideResult> badPeptides = MyPeptides.FindAll(a => a.MyScans[0].CountNumberForwardNames(myParams.LabeledDecoyTag) == 0);
                return (badPeptides.Count);
            }
        }
        
        public PeptideManager(List<SQTScan> myScans, Parameters myParams)
        {
            this.myParams = myParams;
            MyScans = myScans;
            GenerateMyPeptidesFromScans();
        }

        public void CalculateSpectralPresenceScore ()
        {
            ////Recalculate the presence score
            //double pmax = Math.Log(MyPeptides.Max(a => a.MyScans.Count));
            //double pmin = Math.Log(MyPeptides.Min(a => a.MyScans.Count));

            //double diff = pmax - pmin;
            //if (diff == 0) { diff = 1; }

            //foreach (PeptideResult p in MyPeptides)
            //{
            //    double presenceScore = (Math.Log(p.MyScans.Count) - pmin) / (diff);

            //    foreach (SQTScan s in p.MyScans)
            //    {
            //        s.Bayes_PresenceScore = presenceScore;
            //    }
            //}

            //Lets do this by rank
            List<int> allPossibleCounts = MyPeptides.Select(a => a.MyScans.Count).Distinct().ToList();
            allPossibleCounts.Sort();
            
            foreach (PeptideResult p in MyPeptides)
            {
                double rank = (double)allPossibleCounts.IndexOf(p.MyScans.Count);
                double presenceScore = Math.Log(rank+1) / Math.Log((allPossibleCounts.Count + 2));

                foreach (SQTScan s in p.MyScans)
                {
                    s.Sibblings = p.MyScans.Count;
                    s.Bayes_PresenceScore = presenceScore;
                }
            }


        }

        public void CleanToMeetFDR(double fdr)
        {
            double DecoyPeptides = NoLabeledDecoyPeptides;
            double currentFDR = (double)(DecoyPeptides) / (double)MyPeptides.Count;

            //Remodel the Bayesian Scores


            //FindCuttoff
            if (currentFDR > fdr)
            {
                //We need to be sure of this
                foreach (PeptideResult p in MyPeptides)
                {
                    p.MyScans.Sort((a, b) => b.BayesianScore.CompareTo(a.BayesianScore));
                }

                int cutoff = MyPeptides.Count;
                double removedPeptides = 0;
                for (int i = MyPeptides.Count - 1; i >= 0; i--)
                {
                    removedPeptides++;

                    if (MyPeptides[i].MyScans[0].CountNumberForwardNames(myParams.LabeledDecoyTag) == 0)
                    {
                        //This is a peptide of a decoy protein
                        DecoyPeptides--;
                        currentFDR = (double)(DecoyPeptides) / ((double)MyPeptides.Count - removedPeptides);
                        cutoff = i;
                        if (currentFDR < fdr)
                        {
                            break;
                        }
                    }

                }

                MyPeptides.RemoveRange(cutoff, MyPeptides.Count - cutoff);

                //And now, rebuild scans from peptides
                MyScans = (from peptide in MyPeptides
                           from s in peptide.MyScans
                           select s).ToList();
            }

        }

        private void GenerateMyPeptidesFromScans()
        {
            MyPeptides = (from s in MyScans.AsParallel()
                         group s by s.PeptideSequenceCleaned into peptide
                         select new PeptideResult(peptide.Key, peptide.ToList())).ToList();

            Parallel.ForEach(MyPeptides, p =>
                {
                    p.MyScans.Sort((a, b) => b.BayesianScore.CompareTo(a.BayesianScore));
                }
            );

            MyPeptides.Sort((a, b) => b.MyScans[0].BayesianScore.CompareTo(a.MyScans[0].BayesianScore));
        }
    }
}
