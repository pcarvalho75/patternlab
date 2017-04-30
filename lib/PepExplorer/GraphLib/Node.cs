using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib.Graph
{
public class Node<T>
{
        // Private member-variables
        private T data;
        private List<GraphNode<T>> neighbors = null;

        public Node() {}
        public Node(T data) : this(data, null) {}
        public Node(T data, List<GraphNode<T>> neighbors)
        {
            this.data = data;
            this.neighbors = neighbors;
        }

        public T Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        protected List<GraphNode<T>> Neighbors
        {
            get
            {
                return neighbors;
            }
            set
            {
                neighbors = value;
            }
        }
    }
}
