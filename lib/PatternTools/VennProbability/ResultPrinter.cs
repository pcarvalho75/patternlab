using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.VennProbability
{
    public static class ResultPrinter
    {
        public static string PrintReport(List<IntersectionResultAnalysis> results, List<string> groupLabels)
        {
            StringBuilder sb = new StringBuilder();

            var groupByClass = (from l in results
                               group l by l.ClassLabel into label
                               orderby label.Key
                               select new { Label = label.Key, results = label });

            foreach (var label in groupByClass) {
                
                sb.AppendLine("\nLabel :: " + label.Label);
                List<IntersectionResultAnalysis> theAnalysis = label.results.ToList();

                //Group By Signal
                var groupBySignal = (from g in theAnalysis
                                     group g by g.SignalLowerBound into signal
                                     orderby signal.Key
                                     select new { signalGroup = signal.Key, results = signal });

                Dictionary<double, List<IntersectionResultAnalysis>> bySignal = groupBySignal.ToDictionary(a => a.signalGroup, a => a.results.ToList());
                int groupCounter = 0;

                foreach (KeyValuePair<double, List<IntersectionResultAnalysis>> kvp in bySignal)
                {
                    sb.AppendLine("\n ::Group Name: " + groupLabels[groupCounter] + " (" + Math.Round(kvp.Value[0].SignalLowerBound,3) + " <= Average Signal < " +  Math.Round(kvp.Value[0].SignalUpperBound,3) + ")");

                    foreach (IntersectionResultAnalysis ira in kvp.Value)
                    {
                        sb.AppendLine("  No Replicates: " + ira.NoReplicatesAppeared + " No Candidates: " + ira.NoCandidatesInGroup + " Frequency: " + Math.Round(ira.PercentageOfUniqueness,2) + " B-Prob: " + Math.Round(ira.BayesianProbability,3));
                    }
                    groupCounter++;
                }

            }

            return sb.ToString();
        }
    }
}
