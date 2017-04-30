using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.TReXS.RegexGenerationTools
{
    [Serializable()]
    public class GraphNode
    {
        public double MZ { get; set; }
        public double Intensity { get; set; }
        public List<GraphNodeLink> ChildLinks { get; set; }
        public List<List<GraphNode>> DownPaths {get; set;}
        public double DownIntensity { get; set; }

        public bool NodeHasBeenEncorporatedIntoHigherNodes { get; set; }
        int myIndex = 0;
        
        public int MyIndex
        {
            get { return myIndex; }
        }

        public GraphNode(double mass, double intensity, int myIndex)
        {
            this.MZ = mass;
            ChildLinks = new List<GraphNodeLink>();
            DownPaths = new List<List<GraphNode>>();
            NodeHasBeenEncorporatedIntoHigherNodes = false;
            Intensity = intensity;
            this.myIndex = myIndex;
            DownIntensity = 0;
        }

        [Serializable()]
        public class GraphNodeLink
        {
            public int TheNode { get; set; }
            public double ErrorCostPPM { get; set; }
            public GapItem MyGapItem { get; set; }
            public double Intensity { get; set; }

            public GraphNodeLink(int theNode, double errorCost, GapItem g, double intensity)
            {
                this.TheNode = theNode;
                this.ErrorCostPPM = errorCost;
                this.MyGapItem = g;
                this.Intensity = intensity;
            }

            public GraphNodeLink(int theNode)
            {
                this.TheNode = theNode;
            }
        }


    }


}
