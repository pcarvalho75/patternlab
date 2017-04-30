using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQuant
{
    [Serializable]
    public class XQuantClusteringParameters
    {
        /// <summary>
        /// Used for serialization purposes
        /// </summary>
        public XQuantClusteringParameters() { }

        public double ClusteringPPM {get; set;}
        public int MinMS1Counts {get; set;}
        public double MinTimeGapFromItemsInCluster {get; set;}
        public List<int> AcceptableChargeStates { get; set; }
        public bool MaxParsimony {get; set;}
        public bool OnlyUniquePeptides {get; set;}
        public double MinMaxSignal { get; set; }
        public int MinNoPeptides { get; set; }

        /// <summary>
        /// Chromatographic tolerance for attributing a XIC to an ms2 event
        /// </summary>
        /// 
        public bool RetainOptimal { get; set; }
    }
}
