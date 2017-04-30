//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;
//using PatternTools.MSParser;
//using PatternTools.TReXS.RegexGenerationTools;

//namespace PatternTools.TReXS.RegexGenerationTools
//{
//    public class RegexGenerator
//    {
//        PatternTools.AminoacidMasses aam = new AminoacidMasses();
//        List<PatternTools.GapItem> deltaMassTable;
//        double ppmTolerance;
//        double maximumGap = double.MinValue;
//        int maxNoSpectralPeaks;

//        /// <summary>
//        /// My Amino Acid Masses
//        /// </summary>
//        public PatternTools.AminoacidMasses AAM
//        {
//            get { return aam; }
//            set { aam = value; }
//        }


//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="aAGaps">//The acceptable number of aa to be missing to establish a link</param>
//        public RegexGenerator(int aaGap, int maxNoSpectralPeaks)
//        {
//            GenerateDeltaMassTable(aaGap);
//            this.maxNoSpectralPeaks = maxNoSpectralPeaks;
//        }

//        public void GenerateDeltaMassTable(int aaGaps)
//        {
//            deltaMassTable = aam.GenerateDeltaMassTable2(ppmTolerance, PatternTools.PTMMods.DefaultModifications.TheModifications);
//            maximumGap = deltaMassTable[deltaMassTable.Count - 1].GapSize + 1;
//        }



//        public List<PathCandidate> GetRegexes(PatternTools.MSParser.MSFull myMS, RegexGeneration.RegexGenerationParams regexParams)
//        {
//            this.ppmTolerance = regexParams.PPMTolerance;

//            //Add the statring and ending points.
//            AddZeroAndPrecursor(myMS);

//            //Generate a list for all comparison masses
//            List<GraphNode> nodes = new List<GraphNode>(myMS.MSData.Count);
//            for (int i = 0; i < myMS.MSData.Count; i++)
//            {
//                nodes.Add(new GraphNode(myMS.MSData[i].MZ, myMS.MSData[i].Intensity, i));
//            }

//            if (myMS.MSData.Count > maxNoSpectralPeaks)
//            {
//                myMS.MSData.Sort((a, b) => b.Intensity.CompareTo(a.Intensity));
//                myMS.MSData.RemoveRange(maxNoSpectralPeaks - 1, myMS.MSData.Count - maxNoSpectralPeaks);
//            }

//            nodes.Sort((a, b) => a.MZ.CompareTo(b.MZ));

//            //Perform links in tree
//            GenerateTreeLinks(nodes);

//            GraphSearch gs = new GraphSearch(nodes);


//            List<PathCandidate>[,] resultMatrix = new List<PathCandidate>[nodes.Count, nodes.Count];

//            nodes.Sort((a, b) => a.MyIndex.CompareTo(b.MyIndex));
//            for (int i = nodes.Count - 1; i > -1; i--)
//            {
//                for (int k = i; k < nodes.Count; k++)
//                {
//                    if (k == i) { continue; }
//                    gs.FindLeastErrorPathBetweenNodesAndAddToResultMatrix(i, k, resultMatrix);
//                }
//            }

//            //Now lets put the result matrix in one big array to make it easier to query
//            List<PathCandidate> resultVector = new List<PathCandidate>(nodes.Count * nodes.Count);
//            for (int i = nodes.Count - 1; i > -1; i--)
//            {
//                for (int k = i; k < nodes.Count; k++)
//                {
//                    if (resultMatrix[i, k] != null)
//                    {
//                        resultVector.AddRange(resultMatrix[i, k]);
//                    }
//                }
//            }

//            resultVector.RemoveAll(a => a.SequentialScore < regexParams.MinNonGappedAAs);
//            resultVector.RemoveAll(a => a.AAScore < regexParams.MinSingleAA);
//            //Remove duplicate regexes

//            List<PathCandidate> cleanedResultVector = new List<PathCandidate>(resultVector.Count);
//            foreach (PathCandidate pc in resultVector)
//            {
//                if (!cleanedResultVector.Exists(a => a.StringRegexForward.Equals(pc.StringRegexForward)))
//                {
//                    cleanedResultVector.Add(pc);
//                }
//            }

//            cleanedResultVector.Sort((a, b) => a.AverageError.CompareTo(b.AverageError));

//            if (cleanedResultVector.Count > regexParams.MaxRegexes)
//            {
//                cleanedResultVector.RemoveRange(regexParams.MaxRegexes - 1, cleanedResultVector.Count - regexParams.MaxRegexes);
//            }

//            return (cleanedResultVector);
//        }

//        private void AddZeroAndPrecursor(PatternTools.MSParser.MSFull myMS)
//        {
//            double maxIntensity = myMS.MSData.Max(a => a.Intensity);
//            myMS.MSData.Insert(0, new Ion(1.007825, maxIntensity, 0, 0));

//            foreach (double d in myMS.DechargedPrecursorsFromZLine)
//            {
//                myMS.MSData.Add(new Ion(d, maxIntensity, 0, 0));
//            }
//        }

//        //---------------------------------------------------------------

//        private void GenerateTreeLinks(List<GraphNode> nodes)
//        {

//            //System.Threading.Parallel.For(0, nodes.Count, i =>
//            for (int i = 0; i < nodes.Count; i++)
//            {
//                for (int j = i + 1; j < nodes.Count; j++)
//                {
//                    //Just make sure we dont compute stuff in vain
//                    double delta = nodes[j].MZ - nodes[i].MZ;
//                    if (delta <= maximumGap + 1)
//                    {
//                        for (int d = 0; d < deltaMassTable.Count; d++)
//                        {
//                            double testMass = deltaMassTable[d].GapSize;
//                            if (Math.Abs(deltaMassTable[d].GapSize - delta) > 1)
//                            {
//                                continue;
//                            }
//                            else
//                            {
//                                double ppm = pTools.PPM(nodes[j].MZ + deltaMassTable[d].GapSize, nodes[j].MZ + delta);
//                                if (ppm < ppmTolerance)
//                                {
//                                    nodes[i].ChildLinks.Add(new PatternTools.TReXS.RegexGenerationTools.GraphNode.GraphNodeLink(j, ppm, deltaMassTable[d], nodes[j].Intensity));
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            //);

//            //Since there are many combinations that are very close appart, we need to make sure that we only consider each node once
//            for (int i = 0; i < nodes.Count; i++)
//            {
//                nodes[i].ChildLinks = nodes[i].ChildLinks.Distinct().ToList();
//            }

//            nodes.Sort((a, b) => a.MZ.CompareTo(a.MZ));
//        }

//    }

//}
