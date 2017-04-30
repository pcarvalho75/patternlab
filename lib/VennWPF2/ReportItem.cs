using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VennWPF2
{
    public class ReportItem
    {
        public string ProteinID { get; set; }
        public string ExperimentsFound { get; set; }
        public int ReplicateCount { get; set; }
        public double TotalSignal { get; set; }
        public string Description { get; set; }

        public ReportItem (string proteinID, List<string> experimentsFound, int replicateCount, double totalSignal, string description)
        {
            ProteinID = proteinID;
            ExperimentsFound = string.Join(", ", experimentsFound);
            ReplicateCount = replicateCount;
            TotalSignal = totalSignal;
            Description = description;
        }
    }
}
