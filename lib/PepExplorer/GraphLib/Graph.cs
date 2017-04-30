using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib.Graph
{
    // http://msdn.microsoft.com/en-US/library/ms379574(v=vs.80).aspx

    public class Graph<T>
    {
        private List<GraphNode<T>> nodeSet;

        public Graph() {
            nodeSet = new List<GraphNode<T>>();
        }

        public Graph(List<GraphNode<T>> nodes)
        {
            nodeSet = new List<GraphNode<T>>();
            nodeSet.AddRange(nodes);
        }


        public void AddNode(GraphNode<T> node)
        {
            // adds a node to the graph
            nodeSet.Add(node);
        }

        public void AddNode(T value)
        {
            // adds a node to the graph
            nodeSet.Add(new GraphNode<T>(value));
        }

        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, List<double> cost, string description)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
            from.Descriptions.Add(description);
            to.IsStart = false;
        }

        public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, List<double> cost, string edgeDescription)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
            from.Descriptions.Add(edgeDescription);

            to.Neighbors.Add(from);
            to.Costs.Add(cost);
            to.Descriptions.Add(edgeDescription);
        }

        public bool Contains(GraphNode<T> theNode)
        {
            return nodeSet.Contains(theNode);
        }

        public bool Remove(GraphNode<T> nodeToRemove)
        {
            nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (GraphNode<T> gnode in nodeSet)
            {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                    // remove the reference to the node and associated cost
                    gnode.Neighbors.RemoveAt(index);
                    gnode.Costs.RemoveAt(index);
                    gnode.Descriptions.RemoveAt(index);
                }
            }

            return true;
        }

        public List<GraphNode<T>> Nodes
        {
            get
            {
                return nodeSet;
            }
        }

        public int Count
        {
            get { return nodeSet.Count; }
        }

        /// <summary>
        /// Returns all paths; implemented by Paulo Costa Carvalho
        /// Breadth First Search
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <returns></returns>
        public List<List<GraphNode<T>>> BFS(GraphNode<T> startingPoint)
        {

            List<List<GraphNode<T>>> theResults = new List<List<GraphNode<T>>>();
            Stack<GraphNode<T>> nodesToSearch = new Stack<GraphNode<T>>();
            nodesToSearch.Push(startingPoint);
            theResults.Add(new List<GraphNode<T>>() {startingPoint});

            while (nodesToSearch.Count > 0)
            {
                GraphNode<T> current = nodesToSearch.Pop();
                current.Explored++;

                List<GraphNode<T>> toAdd = theResults.Last();
                foreach (GraphNode<T> n in current.Neighbors)
                {
                    if (n.Explored == 0)
                    {
                        nodesToSearch.Push(n);
                        List<GraphNode<T>> newResult = new List<GraphNode<T>>();
                        newResult.AddRange(toAdd);
                        newResult.Add(n);
                        theResults.Add(newResult);
                    }
                }
            }

            nodeSet.ForEach(a => a.Explored = 0);

            return theResults;
        }
    }

}
