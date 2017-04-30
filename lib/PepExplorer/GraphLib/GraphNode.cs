using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib.Graph
{
    public class GraphNode<T> : Node<T>
    {
        private List<List<double>> costs;
        private List<string> descriptions;

        public GraphNode() : base() { Start(); }
        public GraphNode(T value) : base(value) { Start(); }
        public GraphNode(T value, List<GraphNode<T>> neighbors) : base(value, neighbors) { Start(); }

        public bool IsLeaf {
            get
            {
                if (Neighbors.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public int Explored
        {
            get;
            set;
        }

        public bool IsStart { get; set; }




        new public List<GraphNode<T>> Neighbors
        {
            get
            {
                if (base.Neighbors == null)
                    base.Neighbors = new List<GraphNode<T>>();

                return base.Neighbors;
            }
        }

        public List<string> Descriptions 
        {
            get
            {
                if (descriptions == null)
                    descriptions = new List<string>();

                return descriptions;
            }
        }

        private void Start()
        {
            IsStart = true;
            Explored = 0;
        }

        public List<List<double>> Costs
        {
            get
            {
                if (costs == null)
                    costs = new List<List<double>>();

                return costs;
            }
        }
    }
}
