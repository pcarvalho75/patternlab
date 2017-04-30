using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternTools.TReXS.RegexGenerationTools
{
    [Serializable()]
    public class PathCandidate
    {
        public List<GraphNode> ThePath { get; set; }
        double averageError = -1;
        int sequentialScore = -1;
        double maxError = -1;
        string stringRegexForwardReverse;
        string stringRegexForward;
        string stringRegexReverse;
        double mass = -1;
        int aaScore = -1;
        List<GraphNode.GraphNodeLink> graphNodeLinkSequence = new List<GraphNode.GraphNodeLink>();

        public PathCandidate(List<GraphNode> thePath)
        {
            ThePath = thePath;
            stringRegexForwardReverse = "-1";
        }

        public double Mass
        {
            get
            {
                if (mass == -1)
                {
                    mass = 0;
                    foreach (GraphNode g in ThePath)
                    {
                        mass += g.MZ;
                    }
                }
                return mass;
            }
        }

        public double MaximumError
        {
            get
            {
                if (maxError == -1)
                {
                    foreach (GraphNode.GraphNodeLink g in GraphNodeLinkSequence)
                    {
                        if (g.ErrorCostPPM > maxError)
                        {
                            maxError = g.ErrorCostPPM;
                        }
                    }
                }
                return maxError;
            }
        }

        public double AverageError
        {
            get
            {
                if (averageError == -1)
                {
                    CalculateAverageError();
                }
                return (averageError);
            }
        }

        public List<GraphNode.GraphNodeLink> GraphNodeLinkSequence
        {
            get
            {
                if (graphNodeLinkSequence.Count == 0)
                {
                    //Lets build it
                    for (int i = 0; i < ThePath.Count - 1; i++)
                    {
                        //Verify what is the next index
                        int nextIndex = ThePath[i + 1].MyIndex;

                        //Get the Regex Associated with the mode
                        graphNodeLinkSequence.Add(ThePath[i].ChildLinks.Find(a => a.TheNode == nextIndex));
                    }
                }
                return graphNodeLinkSequence;
            }
        }

        //-----------------------------------------------

        /// <summary>
        /// The number of non-gapped aminoacids
        /// </summary>
        public int AAScore
        {
            get {
                if (aaScore == -1) {
                    aaScore = graphNodeLinkSequence.FindAll(a => a.MyGapItem.NoAA == 1).Count;
                }

                return aaScore;
            }
        }

        public int SequentialScore
        {
            get
            {
                if (sequentialScore == -1)
                {
                    int partialScore = 0;
                    int maxScore = 0;
                    double aaScore = 0;

                    try
                    {
                        foreach (GraphNode.GraphNodeLink gnl in GraphNodeLinkSequence)
                        {
                            if (gnl.MyGapItem.NoAA == 1)
                            {
                                partialScore++;
                                aaScore++;
                            }
                            else
                            {
                                partialScore = 0;
                            }

                            if (partialScore > maxScore)
                            {
                                maxScore = partialScore;
                            }
                        }

                        sequentialScore = maxScore;
                    }
                    catch
                    {
                        sequentialScore = 0;
                    }
                }
                return sequentialScore;
            }
        }

        //---------------------------

        public List<int> NodePath
        {
            get
            {
                List<int> result = new List<int>(ThePath.Count);
                foreach (GraphNode g in ThePath)
                {
                    result.Add(g.MyIndex);
                }
                return result;
            }
        }

        public string StringRegexForwardReverse
        {
            get
            {
                if (stringRegexForwardReverse.Equals("-1"))
                {
                    stringRegexForwardReverse = StringRegexForward + "|" + ReverseRegex(StringRegexForward);
                }
                return stringRegexForwardReverse;
            }
            set
            {
                stringRegexForwardReverse = value;
            }
        }

        public string StringRegexForward
        {
            get
            {
                if (string.IsNullOrEmpty(stringRegexForward))
                {
                    stringRegexForward = "";
                    foreach (GraphNode.GraphNodeLink gnl in GraphNodeLinkSequence)
                    {
                        stringRegexForward += gnl.MyGapItem.AARegex;
                    }
                }
                return stringRegexForward;

            }
        }

        public string StringRegexReverse
        {
            get
            {
                if (string.IsNullOrEmpty(stringRegexReverse))
                {
                    stringRegexReverse = ReverseRegex(StringRegexForward);
                }
                return stringRegexReverse;
                
            }
        }

        private string ReverseRegex(string x)
        {

            char[] charArray = new char[x.Length];
            int len = x.Length - 1;
            for (int i = 0; i <= len; i++)
                charArray[i] = x[len - i];
            
            string s = new string(charArray);
            //Now we need to swap the brackets, we first do a temporary swap
            s = Regex.Replace(s, @"\[", "#");
            s = Regex.Replace(s, "]", "[");
            s = Regex.Replace(s, "#", "]");
            s = Regex.Replace(s, @"\(", "#");
            s = Regex.Replace(s, @"\)", @"(");
            s = Regex.Replace(s, "#", @")");

            return s;
        }


        private void CalculateAverageError()
        {
            if (GraphNodeLinkSequence != null)
            {
                averageError = graphNodeLinkSequence.Average(a => a.ErrorCostPPM);
            }
        }

    }
}
