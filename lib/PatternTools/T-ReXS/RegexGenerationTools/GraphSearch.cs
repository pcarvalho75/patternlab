//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace PatternTools.TReXS.RegexGenerationTools
//{
//    public class GraphSearch
//    {
//        List<GraphNode> myNodes = new List<GraphNode>();
//        List<int> nodesExpanded = new List<int>();

//        public List<int> NodesExpanded
//        {
//            get { return nodesExpanded; }
//        }

//        public GraphSearch(List<GraphNode> theNodes)
//        {
//            myNodes = theNodes;
//        }

//        public void FindLeastErrorPathBetweenNodesAndAddToResultMatrix(int NodeOfLowerMass, int NodeOfHigherMass, List<PathCandidate>[,] resultMatrix)
//        {
//            //It is better to use a depth first search approach
//            double maxAcceptableMassDifference = myNodes[NodeOfHigherMass].MZ - myNodes[NodeOfLowerMass].MZ + 0.3; //We add the 0.3 just in case

//            Stack<GraphNode> openStack = new Stack<GraphNode>();
//            List<PathCandidate> thePaths = new List<PathCandidate>();
//            List<PathCandidate> solutionPaths = new List<PathCandidate>();

//            openStack.Push(myNodes[NodeOfLowerMass]);
//            thePaths.Add(new PathCandidate(new List<GraphNode>() { myNodes[NodeOfLowerMass] }));


//            while (openStack.Count != 0)
//            {
//                GraphNode nodeToExpand = openStack.Pop();
//                int indexOfExpandingNode = myNodes.IndexOf(nodeToExpand);

//                //Copy the DownPaths that end with the existing node to expand
//                if (nodeToExpand.ChildLinks.Count > 0 && nodeToExpand.MZ - myNodes[NodeOfLowerMass].MZ < maxAcceptableMassDifference)
//                {
//                    //Find the paths to expand
//                    List<PathCandidate> pathsToExpand = thePaths.FindAll(a => a.ThePath[a.ThePath.Count - 1].MyIndex == indexOfExpandingNode);

//                    thePaths = thePaths.Except(pathsToExpand).ToList();

//                    //DFS
//                    foreach (PatternTools.TReXS.RegexGenerationTools.GraphNode.GraphNodeLink gnl in nodeToExpand.ChildLinks)
//                    {
//                        List<PathCandidate> solutionParts = resultMatrix[gnl.TheNode, NodeOfHigherMass];
//                        if (solutionParts == null)
//                        {

//                        }
//                        else if (solutionParts.Count == 0)
//                        {
//                            //dont waste time here

//                        }
//                        else if (solutionParts.Count != 0)
//                        {
//                            foreach (PathCandidate path in pathsToExpand)
//                            {
//                                foreach (PathCandidate pc in solutionParts)
//                                {
//                                    PathCandidate aNewPath = new PathCandidate(new List<GraphNode>(path.ThePath));
//                                    aNewPath.ThePath.AddRange(pc.ThePath);
//                                    solutionPaths.Add(aNewPath);
//                                }
//                            }
//                        }

//                        else if (!openStack.Contains(myNodes[gnl.TheNode]) && gnl.TheNode <= NodeOfHigherMass)
//                        {
//                            openStack.Push(myNodes[gnl.TheNode]);
//                        }
//                    }

//                    //And to manage our paths
//                    List<PathCandidate> newPathsPart2 = new List<PathCandidate>(pathsToExpand.Count * nodeToExpand.ChildLinks.Count);

//                    foreach (PathCandidate path in pathsToExpand)
//                    {
//                        foreach (PatternTools.TReXS.RegexGenerationTools.GraphNode.GraphNodeLink i in nodeToExpand.ChildLinks)
//                        {

//                            PathCandidate aNewPath = new PathCandidate(new List<GraphNode>(path.ThePath));
//                            aNewPath.ThePath.Add(myNodes[i.TheNode]);

//                            if (i.TheNode == NodeOfHigherMass)
//                            {
//                                //We need to consider no further
//                                solutionPaths.Add(aNewPath);
//                            }
//                            else
//                            {
//                                newPathsPart2.Add(aNewPath);
//                            }
//                        }
//                    }

//                    //And save the new paths
//                    thePaths.AddRange(newPathsPart2);
//                }
//                else
//                {
//                    //This path is no good
//                    //Find the paths to expand
//                    List<PathCandidate> pathsToExpand = thePaths.FindAll(a => a.ThePath[a.ThePath.Count - 1].MyIndex == indexOfExpandingNode);
//                    thePaths = thePaths.Except(pathsToExpand).ToList();
//                }

//                //Keep a global record (This is our closed stack)
//                nodesExpanded.Add(indexOfExpandingNode);
//            }

//            //This will use a little more memory, but it is faster like this than leaving it in the while loop
//            nodesExpanded = nodesExpanded.Distinct().ToList();

//            //Now find the path of least error
//            //First lets calculate the average error for each path
//            solutionPaths.Sort((a, b) => a.AverageError.CompareTo(b.AverageError));

//            //Lets keep only the best solution
//            List<PathCandidate> optimalSolution = new List<PathCandidate>();
//            if (solutionPaths.Count > 5)
//            {
//                for (int i = 0; i < 5; i++)
//                {
//                    optimalSolution.Add(solutionPaths[i]);
//                }
//            }
//            else
//            {
//                optimalSolution = solutionPaths;
//            }

//            resultMatrix[NodeOfLowerMass, NodeOfHigherMass] = optimalSolution;

//        }

//        /// <summary>
//        /// Uses Depth FitstSearch
//        /// </summary>
//        /// <param name="theNodes"></param>
//        /// <param name="startNode"></param>
//        /// <returns></returns>
//        public void FindAllPathsFromANodeUsingDFS(int startNode)
//        {

//            GraphNode nodeToExpand = myNodes[startNode];
//            int indexOfExpandingNode = myNodes.IndexOf(nodeToExpand);

//            if (nodeToExpand.ChildLinks.Count > 0)
//            {
//                //DFS
//                foreach (PatternTools.TReXS.RegexGenerationTools.GraphNode.GraphNodeLink i in nodeToExpand.ChildLinks)
//                {
//                    myNodes[i.TheNode].NodeHasBeenEncorporatedIntoHigherNodes = true;

//                    if (nodesExpanded.Contains(i.TheNode))
//                    {
//                        foreach (List<int> path in myNodes[i.TheNode].DownPaths)
//                        {
//                            List<int> newPath = new List<int>() { indexOfExpandingNode };
//                            newPath.AddRange(path);
//                            nodeToExpand.DownPaths.Add(newPath);
//                        }
//                    }
//                    else
//                    {
//                        nodeToExpand.DownPaths.Add(new List<int>() { indexOfExpandingNode, i.TheNode });
//                    }
//                }

//                //Make sure there is no redundancy in the paths
//                nodeToExpand.DownPaths = nodeToExpand.DownPaths.Distinct().ToList();
//                //CheckRedundancyInPaths(nodeToExpand.DownPaths, nodeToExpand.ChildLinks.Count);
//            }
//            else
//            {
//                nodeToExpand.DownPaths.Add(new List<int>() { indexOfExpandingNode });
//            }

//            //Keep a global record (This is our closed stack
//            nodesExpanded.Add(indexOfExpandingNode);



//            //This will use a little more memory, but it is faster like this than leaving it in the while loop
//            nodesExpanded = nodesExpanded.Distinct().ToList();
//        }


//        private void CheckRedundancyInPaths(List<List<int>> thePaths, int pathsAdded)
//        {
//            thePaths.Sort((a, b) => a.Count.CompareTo(b.Count));

//            bool needToEliminate = true;
//            int externalCounter = thePaths.Count - pathsAdded - 1;
//            while (needToEliminate)
//            {
//                needToEliminate = false;
//                List<List<int>> toEliminate = new List<List<int>>();
//                for (int i = externalCounter; i < thePaths.Count; i++)
//                {
//                    for (int j = 0; j < i; j++)
//                    {
//                        //We need to check if the smaller path is contained withing the bigger path
//                        List<int> intersection = thePaths[i].Intersect(thePaths[j]).ToList();
//                        if (intersection.Count == thePaths[j].Count)
//                        {
//                            //We can discard this solution
//                            toEliminate.Add(thePaths[j]);
//                        }
//                    }
//                    if (toEliminate.Count > 0)
//                    {
//                        needToEliminate = true;

//                        foreach (List<int> s in toEliminate)
//                        {
//                            thePaths.Remove(s);
//                        }
//                        externalCounter = i + 1;
//                        break;
//                    }
//                }
//            }
//        }

//    }
//}
