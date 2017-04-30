using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using System.Collections.Concurrent;

namespace GO
{
    [Serializable]
    public class GOTerms
    {
        List<Term> terms = new List<Term>();
        List<System.Windows.Forms.TreeNode> treeNodes = new List<System.Windows.Forms.TreeNode>();
        List<missingToExplore> toExplore = new List<missingToExplore>();
        Dictionary<string, List<string>> isAUPDic = new Dictionary<string, List<string>>();
        bool nodesBelowComputed;  //We use this as a flag if we already computed the nodes below
        AssociationFileParser associationTranslator;

        public AssociationFileParser AssociationTranslator
        {
            get { return associationTranslator; }
        }

        //The leaf population for these GO Categories
        //We use this to speed up the HyperGeometric Calcuations
        Dictionary<string, List<string>> leafsOfCategories = new Dictionary<string,List<string>>();

        public Dictionary<string, List<string>> LeafsOfCategories
        {
            get { return leafsOfCategories; }
        }

        //---------------------


        public List<Term> Terms
        {
            get { return terms; }
        }


        //-----------------

        public List<string> findTopMostNodes ()
        {
            return terms.FindAll(a => a.isA.Count == 0 && !a.isObsolete.Equals("is_obsolete: true")).Select (a => a.id).ToList(); 
        }

        //-----------

        public Term getTermByID(string id)
        {
            Term a = new Term();
            foreach (Term t in terms)
            {
                if (t.id.Equals(id))
                {
                    a = t;
                    break;
                }
            }
            return (a);
        }

        public void LoadAssociationTable(string fileName, string IPIXRefsFilename, AssociationType aft, int GOColumn, int myColumn)
        {
            associationTranslator = new AssociationFileParser(fileName, IPIXRefsFilename, aft, GOColumn, myColumn);
        }

        //----------------------

        /// <summary>
        /// Prunes the tree and returns the number of pruned nodes
        /// </summary>
        /// <returns></returns>
        public int pruneObsoleteTerms()
        {
            int prunedTerms = terms.RemoveAll(a => a.isObsolete.Equals("is_obsolete: true"));
            return (prunedTerms);
        }

        public enum GOCategories
        {
            biological_process,
            cellular_component,
            molecular_function,
            all
        }

        public List<TreeNode> getNodesCategory(GOCategories gc)
        {
            List<TreeNode> theNodes = new List<TreeNode>();
            bool getAll = false;

            string theCategory = "namespace: biological_process";

            if (gc.Equals(GOCategories.cellular_component))
            {
                theCategory = "namespace: cellular_component";
            }
            else if (gc.Equals(GOCategories.molecular_function))
            {
                theCategory = "namespace: molecular_function";
            }
            else if (gc.Equals(GOCategories.all))
            {
                getAll = true;
            }


            foreach (TreeNode t in treeNodes)
            {
                if (t.Nodes[1].Text.Equals(theCategory) || getAll)
                {
                    theNodes.Add(t);
                }
            }

            return theNodes;
        }
        
        public void LoadFile(string fileName)
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(fileName);

                //Make sure were clean
                terms = new List<Term>(); ;

                string read;

                Term t = new Term();
                Regex GoTermMatch = new Regex(@"GO:(?<token>\S*)");
                Regex GoExtractDefinition = new Regex(@"def: (?<token>[\S| ]*)");
                Regex GoExtractName = new Regex(@"name: (?<token>[\S| ]*)");


                while ((read = sr.ReadLine()) != null)
                {
                    if (read.StartsWith("[Term]"))
                    {
                        //We are dealing with a new term
                        if (!string.IsNullOrEmpty(t.id))
                        {
                            terms.Add(t);
                        }
                        t = new Term();
                    }
                    else if (read.StartsWith("id: GO:"))
                    {
                        Match MatchGoTerm = GoTermMatch.Match(read);
                        t.id = MatchGoTerm.Groups["token"].Value;
                    }
                    else if (read.StartsWith("is_a:"))
                    {
                        Match MatchGoTerm = GoTermMatch.Match(read);
                        t.isA.Add(MatchGoTerm.Groups["token"].Value);
                    }
                    else if (read.StartsWith("name:"))
                    {
                        Match MatchGoName = GoExtractName.Match(read);
                        t.name = MatchGoName.Groups["token"].Value;
                    }
                    else if (read.StartsWith("namespace:"))
                    {
                        t.nameSpace = read;
                    }
                    else if (read.StartsWith("def:"))
                    {
                        Match MatchGoDef = GoExtractDefinition.Match(read);
                        t.definition = MatchGoDef.Groups["token"].Value;
                    }
                    else if (read.StartsWith("is_obsolete:"))
                    {
                        t.isObsolete = read;
                    }
                    Console.Write("H");
                }
                sr.Close();

                //Make sure we save the last term
                terms.Add(t);


                //Build the dictionary
                foreach (Term te in terms)
                {
                    if (!isAUPDic.ContainsKey(te.id))
                    {
                        isAUPDic.Add(te.id, te.isA);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetBaseException().ToString());
            }
        }

        /// <summary>
        /// This method returns one route having as a starting point an antry its nextIndexToExplore
        /// </summary>
        /// <param name="entry">The GO ID</param>
        /// <param name="startingIndex">The index of the is_a (i.e 0 stands for the first parent, 1 for the second, etc...)</param>
        /// <returns></returns>
        private List<string> RouteToRootFrom(string entry, int isAIndex)
        {
            List<string> rout = new List<string>();
            List<string> endpoints = findTopMostNodes();

            string cEntry = entry;
            rout.Add(cEntry);

            while (!endpoints.Contains(cEntry))
            {
                List<string> theParents = new List<string>();
                isAUPDic.TryGetValue(cEntry, out theParents);

                //Lets book a point to futurely explore
                if (isAIndex + 1 < theParents.Count)
                {
                    missingToExplore te = new missingToExplore();
                    te.id = cEntry;
                    te.nextIndexToExplore = isAIndex + 1;
                    toExplore.Add(te);
                }
                
                cEntry = theParents[isAIndex];
                
                
                if (isAIndex > 0)
                {
                    isAIndex = 0;
                }

                rout.Add(cEntry);

            }

            return (rout);
        }

        //-------------

        public string findRoot(string t)
        {
            List<string> route = RouteToRootFrom(t, 0);
            return route[route.Count - 1];
        }

        public List<string> findOneRoutToRoot(string id)
        {
            return (RouteToRootFrom(id, 0));
        }

        public List<List<string>> AllRoutesToRootsFrom(string id)
        {
            toExplore.Clear();
            List<List<string>> allRoutes = new List<List<string>>();

            //We trace the first path
            List<string> route = RouteToRootFrom(id, 0);
            allRoutes.Add(route);

            //Now we will loop over the to Explore Path Until we have completed all possibilities
            while (true)
            {
                if (toExplore.Count == 0)
                {
                    break;
                }
                List<string> r = RouteToRootFrom(toExplore[0].id, toExplore[0].nextIndexToExplore);
                allRoutes.Add(r);
                toExplore.RemoveAt(0);
 
            }

            return (allRoutes);
        }

        //--------------------------------

        [Serializable]
        struct missingToExplore {

            //should only hold one 
            public string id;
            public int nextIndexToExplore;
        }

        //------------

        public List <List <string>> allRoutesToLeafsFrom(string id)
        {
            toExplore.Clear();

            List<List<string>> allRoutes = new List<List<string>>();

            //We trace the first path
            Term theTerm = getTermByID(id);
            List<string> route = RouteToLeafFrom(theTerm, 0);
            allRoutes.Add(route);

            while (true)
            {
                if (toExplore.Count == 0)
                {
                    break;
                }
                List<string> r = RouteToLeafFrom(getTermByID(toExplore[0].id), toExplore[0].nextIndexToExplore);
                allRoutes.Add(r);
                toExplore.RemoveAt(0);

            }

            return (allRoutes);

        }

        private List<string> RouteToLeafFrom(Term startTerm, int childrenIndex)
        {
            List<string> route = new List<string>();

            Term t = startTerm;

            bool needToBookAPointToExplore = true;
            
            while (true)
            {

                //Lets book a point to futurely explore
                if (childrenIndex + 1 < startTerm.Children.Count && needToBookAPointToExplore)
                {
                    missingToExplore te = new missingToExplore();
                    te.id = startTerm.Children[childrenIndex + 1];
                    te.nextIndexToExplore = childrenIndex + 1;
                    toExplore.Add(te);
                    needToBookAPointToExplore = false;
                }


                //choose the next term to explore

                if (t.Children.Count > 0)
                {
                    t = getTermByID(t.Children[0]);
                    route.Add(t.id);
                }
                else
                {
                    break;
                }
                
            }

            return (route);
        }

        //-------------------------------------

        public void buildTreeNodes()
        {
            treeNodes.Clear();
            //Build The node list
            foreach (Term t in terms)
            {
                //Add the Root Node
                TreeNode tn = new TreeNode();
                tn.Name = t.id;
                tn.Text = t.name + " (" + t.id + ")";

                //Add Child tree Node namespace
                TreeNode isANode = new TreeNode();
                isANode.Name = "is_a:";
                isANode.Text = "is_a:";
                foreach (string isA in t.isA)
                {
                    TreeNode anotherNode = new TreeNode();
                    anotherNode.Name = isA.ToString();
                    anotherNode.Text = isA.ToString();
                    isANode.Nodes.Add(anotherNode);
                }
                tn.Nodes.Add(isANode);
                tn.Nodes.Add(t.nameSpace);
                tn.Nodes.Add(t.definition);
                tn.Nodes.Add(t.isObsolete);

                treeNodes.Add(tn);
            }
        }

        //-------------------------------------
        //Methods used for building the DAG
 
        /// <summary>
        /// Finds all the leafs of our DAG
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        public List<Term> FindLeafes(ref List<Term> terms)
        {
            //A leaf is considered such if there is no one referring to it
            List<Term> leafs = new List<Term>();

            //Create a dictionary telling that everbody has no reference
            Dictionary<string, bool> isLeaf = new Dictionary<string, bool>(terms.Count);
            isLeaf = terms.ToDictionary(a => a.id, a=> true);


            //Now lets filter the leafs
            foreach (Term t in terms)
            {
                foreach (string s in t.isA)
                {
                    if (isLeaf.ContainsKey(s))
                    {
                        isLeaf[s] = false;
                    }
                }

            }
 

            //Finally, add them to our final output
            foreach (Term t in terms)
            {
                if (isLeaf[t.id])
                {
                    leafs.Add(t);
                }
            }


            return (leafs);
        }



        /// <summary>
        /// Finds all the leafs of our DAG
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        public List<Term> FindLeafes2(ref List<Term> terms)
        {
            List<int> LinkCounter = new int [terms.Count].ToList() ; //Only items with 0 counts will be leafes

            for (int i = 0; i < terms.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (LinkCounter[j] > 0) { 
                        continue;
                    }

                    if (terms[i].isA.Contains(terms[j].id))
                    {
                        LinkCounter[j]++;
                    }
                    else if (terms[j].isA.Contains(terms[i].id))
                    {
                        LinkCounter[i]++;
                    }
                }
            }
            
            List<Term> results = new List<Term>();

            for (int i = 0; i < LinkCounter.Count; i++)
            {
                if (LinkCounter[i] > 0)
                {
                    results.Add(terms[i]);
                }
            }

            return results;

        }
        //----------------------

        public void ComputeAllNodesBelowForAllNodes () {

            if (nodesBelowComputed)
            {
                return;
            }

            //Do the work
            List<Term> termCopy = new List<Term>(terms);

            int leafCount = 1;
           
            while (leafCount > 0)
            {
                //Find the leafs
                List<Term> leafs = FindLeafes(ref termCopy);
                leafCount = leafs.Count;

                //We prune them!
                termCopy = termCopy.Except(leafs).ToList();

                //Now we add the leafs to their parents
                //Parallel.ForEach(leafs, leaf =>
                foreach (Term leaf in leafs)
                {
                    foreach (string parent in leaf.isA)
                    {

                        foreach (Term t in terms.FindAll(a => parent.Equals(a.id)).ToList())
                        {
                            t.AllNodesBelow.Add(leaf.id);

                            //And then we add all his nodes below
                            t.AllNodesBelow.AddRange(leaf.AllNodesBelow);

                            //And add him to the children array
                            t.AddTermToChildren(leaf.id);
                        }

                    }

                    leaf.AllNodesBelow = leaf.AllNodesBelow.Distinct().ToList();
                }


            } //End the while

            //Now we will fill up the speed lists, fill in the leaf information....
            //The GO IDs of the topmost terms are
            List<string> topMost = findTopMostNodes();
            foreach (string s in topMost)
            {
                if (!leafsOfCategories.ContainsKey(s))
                {
                    Term t = getTermByID(s);

                    //Slight modification to comport go slim
                    if (leafsOfCategories.ContainsKey(t.nameSpace))
                    {
                        leafsOfCategories[t.nameSpace].AddRange(GetLeafs(t));
                    }
                    else
                    {
                        //This should be the working option if the standard GO is used
                        leafsOfCategories.Add(t.nameSpace, GetLeafs(t));
                    }
                }
            }

            //----
            nodesBelowComputed = true;
        }


        /// <summary>
        /// This method returns the leafs underneath a term
        /// </summary>
        /// <param name="t">Term</param>
        /// <returns></returns>
        public List<string> GetLeafs(Term t)
        {
            List<string> leafs = new List<string>();

            foreach (string nb in t.AllNodesBelow)
            {
                Term t2 = getTermByID(nb);
                if (t2.Children.Count == 0)
                {
                    leafs.Add(nb);
                }
            }

            return (leafs);
        }

    }

    //--------------------------


}
