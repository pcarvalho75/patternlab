using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GO
{
    [Serializable]
    public class Term
    {
        public string id;
        public string name;
        public string nameSpace;
        public string definition;
        public string isObsolete = "is_obsolete: false";
        public List<string> isA = new List<string>();

        //Needs to be calculated after parsing
        List<string> children = new List<string>();
        //List<string> leafs = new List<string>();
        List<string> allNodesBelow = new List<string>();


        public List<string> Children
        {
            get { return children; }
        }

        public void AddTermToChildren(string c)
        {
            if (!children.Contains(c))
            {
                children.Add(c);
            }
        }

        /// <summary>
        /// Make sure this is a distinct list when managing
        /// </summary>
        public List<string> AllNodesBelow
        {
            get { return allNodesBelow; }
            set { allNodesBelow = value; }
        }

    }
}
