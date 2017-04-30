using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.MSParser;
using PatternTools.TReXS.RegexGenerationTools;
using System.Text.RegularExpressions;
using PatternTools.T_ReXS.SearchEngine;
using PatternTools.PTMMods;

namespace PatternTools.T_ReXS.RegexGenerationTools.SequentialScore
{
    //[Serializable]
    //public class SequentialScore
    //{
    //    List<GapItem> gapItems = new List<GapItem>();
    //    double ppmTolerance;
    //    double largestGap;
    //    double intensityPercentageCuttoff;
    //    int resultBuffer;

    //    public List<ModificationItem> MyPTMs
    //    {
    //        set
    //        {
    //            PatternTools.AminoacidMasses aa = new AminoacidMasses();
    //            gapItems = aa.GenerateDeltaMassTable2(ppmTolerance, value);
    //        }
    //    }

    //    public SequentialScore(double ppmTolerance, List<ModificationItem> modifications, double intensityPercentageCuttoff, int resultBuffer)
    //    {
    //        PatternTools.AminoacidMasses aa = new AminoacidMasses();
    //        gapItems = aa.GenerateDeltaMassTable2(ppmTolerance, modifications);
    //        largestGap = gapItems.Max(a => a.GapSize) + 0.3;

    //        this.ppmTolerance = ppmTolerance;
    //        this.intensityPercentageCuttoff = intensityPercentageCuttoff;
    //        this.resultBuffer = resultBuffer;
    //    }


    //    //Optimized for finding most intense sequence
    //    public double FastMaxSequentialScoreCoverage(PatternTools.MSParser.MSFull theMS)
    //    {
    //        MSFull myMS = PatternTools.ObjectCopier.Clone(theMS);
    //        PrepareMSForDeNovo(myMS);


    //        List<GraphNode> allNodes = GetNodes(myMS);

    //        List<List<int>> allPaths = new List<List<int>>();
    //        foreach (GraphNode g in allNodes)
    //        {
    //            allPaths.AddRange(g.DownPaths);
    //        }

    //        allPaths = allPaths.Distinct().ToList();
    //        allPaths.Sort((a, b) => EvaluatePathForIntensity(b, allNodes).CompareTo(EvaluatePathForIntensity(a, allNodes)));

    //        double coverage = allNodes[allPaths[0][allPaths[0].Count - 1]].MZ - allNodes[allPaths[0][0]].MZ;
    //        return coverage;


    //    }


    //    //Do not use in parallel as the gappedItems sorting will screw up
    //    public List<ScoredSequence> DetailedDenovoSequencing(PatternTools.MSParser.MSFull theMS)
    //    {

    //        MSFull myMS = PatternTools.ObjectCopier.Clone(theMS);
    //        PrepareMSForDeNovo(myMS);


    //        List<GraphNode> allNodes = GetNodes(myMS);

    //        List<List<int>> allPaths = new List<List<int>>();
    //        foreach (GraphNode g in allNodes)
    //        {
    //            allPaths.AddRange(g.DownPaths);
    //        }

    //        allPaths = allPaths.Distinct().ToList();
    //        allPaths.Sort((a, b) => EvaluatePathForIntensity(b, allNodes).CompareTo(EvaluatePathForIntensity(a, allNodes)));


    //        List<ScoredSequence> theResults = new List<ScoredSequence>();
    //        foreach (List<int> p in allPaths)
    //        {
    //            double avgPPM = EvaluatePathForPPMError(p, allNodes);
    //            double intensity = EvaluatePathForIntensity(p, allNodes);
    //            int sequentialScore = p.Count;
    //            string sequence = GetSequence(p, allNodes);

    //            ScoredSequence s = new ScoredSequence();
    //            s.AvgPPMError = Math.Round(avgPPM, 1);
    //            s.Sequence = sequence;
    //            s.TotalIntensity = Math.Round(intensity, 0);
    //            s.SequentialScore = sequentialScore;
    //            theResults.Add(s);

    //        }

    //        int removed = theResults.RemoveAll(a => a.Sequence.Equals(""));
    //        Console.WriteLine("Removing bad results: " + removed);


    //        Dictionary<string, List<ScoredSequence>> cluster = (from result in theResults
    //                                                            group result by result.Sequence into resultGroup
    //                                                            select new { theRegex = resultGroup.Key, results = resultGroup }).ToDictionary(a => a.theRegex, a => a.results.ToList());

    //        theResults.Clear();

    //        foreach (KeyValuePair<string, List<ScoredSequence>> kvp in cluster)
    //        {
    //            kvp.Value.Sort((a, b) => b.TotalIntensity.CompareTo(a.TotalIntensity));
    //            theResults.Add(kvp.Value[0]);
    //        }

    //        return theResults;

    //    }


    //    private void PrepareMSForDeNovo(MSFull myMS)
    //    {
    //        double maxIntensity = myMS.MSData.Max(b => b.Intensity);
    //        myMS.MSData.RemoveAll(a => a.Intensity < intensityPercentageCuttoff * maxIntensity);
    //        myMS.MSData.Sort((a, b) => a.MZ.CompareTo(b.MZ));

    //        myMS.MSData.Insert(0, new Ion(1.007825, maxIntensity, 0, 0));

    //        foreach (double d in myMS.DechargedPrecursorsFromZLine)
    //        {
    //            myMS.MSData.Add(new Ion(d, 1, 0, 0));
    //        }
    //    }

    //    private List<GraphNode> GetNodes(MSFull myMS)
    //    {
    //        List<GraphNode> allNodes = new List<GraphNode>(myMS.MSData.Count);

    //        //Create the Node Array
    //        for (int i = 0; i < myMS.MSData.Count; i++)
    //        {
    //            allNodes.Add(new GraphNode(Math.Round(myMS.MSData[i].MZ, 3), myMS.MSData[i].Intensity, i)); ;
    //        }


    //        //Include the Child Links
    //        for (int i = 0; i < allNodes.Count; i++)
    //        {
    //            for (int j = i; j < allNodes.Count; j++)
    //            {
    //                if (j == i) { continue; }
    //                double delta = allNodes[j].MZ - allNodes[i].MZ;

    //                if (delta > largestGap)
    //                {
    //                    break;
    //                }

    //                double thisStep = allNodes[i].MZ + delta;
    //                //List<GapItem> gp = (from g in gapItems
    //                //                    orderby (PatternTools.pTools.PPM(thisStep, allNodes[i].MZ + g.GapSize))
    //                //                    select g).ToList();


    //                foreach (GapItem gp in gapItems)
    //                {
    //                    double ppm2;
    //                    if ((ppm2 = PatternTools.pTools.PPM(allNodes[i].MZ + delta, allNodes[i].MZ + gp.GapSize)) < ppmTolerance)
    //                    {
    //                        GraphNode.GraphNodeLink gl = new GraphNode.GraphNodeLink(j, ppm2, gp, allNodes[j].Intensity);
    //                        allNodes[i].ChildLinks.Add(gl);
    //                    }
    //                }
    //            }
    //        }

    //        //Now lets sort all childlinks by error cost
    //        foreach (GraphNode g in allNodes)
    //        {
    //            //g.ChildLinks.Sort((a, b) => b.Intensity.CompareTo(a.Intensity));
    //            g.ChildLinks.Sort((a, b) => a.ErrorCostPPM.CompareTo(b.ErrorCostPPM));
    //            g.DownPaths = new List<List<int>>();
    //            g.DownPaths.Add(new List<int>());
    //        }

    //        List<double> bestIntensities = new List<double>(resultBuffer);
    //        bestIntensities.Add(0);

    //        for (int i = allNodes.Count - 1; i >= 0; i--)
    //        {
    //            GraphNode rNode = allNodes[i];

    //            if (rNode.ChildLinks.Count == 0) { continue; }

    //            foreach (GraphNode.GraphNodeLink gnl in rNode.ChildLinks)
    //            {
    //                List<List<int>> pathsToTrim = new List<List<int>>();

    //                foreach (List<int> path in allNodes[gnl.TheNode].DownPaths)
    //                {

    //                    List<int> newPath = new List<int>() { gnl.TheNode };
    //                    newPath.AddRange(path);

    //                    double downIntensity = EvaluatePathForIntensity(newPath, allNodes);

    //                    double bestIntensitiesMin = bestIntensities.Min();
    //                    if (downIntensity > bestIntensitiesMin)
    //                    {
    //                        bestIntensities.Add(downIntensity);

    //                        rNode.DownPaths.Add(newPath);

    //                        if (bestIntensities.Count > resultBuffer)
    //                        {
    //                            bestIntensities.Remove(bestIntensitiesMin);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        pathsToTrim.Add(path);
    //                    }
    //                }

    //                if (pathsToTrim.Count > 0)
    //                {
    //                    //allNodes[gnl.TheNode].DownPaths = allNodes[gnl.TheNode].DownPaths.Except(pathsToTrim).ToList();
    //                }
    //            }

    //        }


    //        foreach (GraphNode g in allNodes)
    //        {
    //            foreach (List<int> l in g.DownPaths)
    //            {
    //                l.Insert(0, g.MyIndex);
    //            }

    //            //I dont understand why this does not work
    //            //g.DownPaths.ForEach(a => a.Insert(0, g.MyIndex));
    //        }

    //        return allNodes;
    //    }



    //    /// <summary>
    //    /// This evaluation is wrong
    //    /// </summary>
    //    /// <param name="newPath"></param>
    //    /// <param name="allNodes"></param>
    //    /// <returns></returns>
    //    private double EvaluatePathForPPMError(List<int> path, List<GraphNode> allNodes)
    //    {
    //        List<double> errors = new List<double>(path.Count);
    //        for (int i = 0; i < path.Count - 1; i++)
    //        {
    //            double delta = allNodes[path[i + 1]].MZ - allNodes[path[i]].MZ;

    //            double thisStep = allNodes[i].MZ + delta;
    //            List<GapItem> gp = (from g in gapItems
    //                                orderby (PatternTools.pTools.PPM(thisStep, allNodes[i].MZ + g.GapSize))
    //                                select g).ToList();

    //            //gapItems.Sort((a, b) => (PatternTools.pTools.PPM(allNodes[i].MZ + delta, allNodes[i].MZ + a.GapSize).CompareTo(PatternTools.pTools.PPM(allNodes[i].MZ + delta, allNodes[i].MZ + b.GapSize))));

    //            double ppm2 = PatternTools.pTools.PPM(allNodes[i].MZ + delta, allNodes[i].MZ + gp[0].GapSize);

    //            if (ppm2 > ppmTolerance * 1.2)
    //            {
    //                Console.Write("B");
    //            }
    //            else
    //            {
    //                errors.Add(ppm2);
    //            }
    //        }

    //        if (errors.Count == 0) { return double.MaxValue; }

    //        return errors.Average();
    //    }

    //    private string GetSequence(List<int> path, List<GraphNode> allNodes)
    //    {
    //        List<GraphNode> theGPath = new List<GraphNode>();
    //        foreach (int i in path)
    //        {
    //            theGPath.Add(allNodes[i]);
    //        }


    //        string sequence = "";
    //        for (int i = 0; i < path.Count - 1; i++)
    //        {
    //            double delta = allNodes[path[i + 1]].MZ - allNodes[path[i]].MZ;


    //            double thisStep = allNodes[i].MZ + delta;
    //            List<GapItem> gp = (from g in theGPath[i].ChildLinks
    //                                orderby (PatternTools.pTools.PPM(thisStep, allNodes[i].MZ + g.MyGapItem.GapSize))
    //                                select g.MyGapItem).ToList();

    //            double ppm2 = Math.Abs(PatternTools.pTools.PPM(allNodes[i].MZ + delta, allNodes[i].MZ + gp[0].GapSize));

    //            if (ppm2 > ppmTolerance * 1.2)
    //            {
    //                Console.Write("B");
    //                return "";
    //            }
    //            else
    //            {
    //                sequence += gp[0].AARegex;
    //            }
    //        }

    //        return sequence;

    //    }

    //    private double EvaluatePathForIntensity(List<int> path, List<GraphNode> allNodes)
    //    {
    //        double sum = 0;
    //        for (int i = 0; i < path.Count; i++)
    //        {
    //            sum += allNodes[path[i]].Intensity;
    //        }
    //        return sum;
    //    }



    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="path"></param>
    //    /// <param name="allNodes">should be sorted by MZ previously to evoking this method</param>
    //    /// <returns></returns>
    //    private string WriteSequence(List<int> path, List<GraphNode> allNodes)
    //    {

    //        if (path.Count <= 2) { return ""; }
    //        path.Sort();

    //        string sequence = "";
    //        List<double> errors = new List<double>();
    //        List<double> intensities = new List<double>();
    //        List<double> mzs = new List<double>();

    //        for (int i = 0; i < path.Count - 1; i++)
    //        {
    //            GraphNode thisNode = allNodes[path[i]];
    //            GraphNode nextNode = allNodes[path[i + 1]];

    //            double delta = nextNode.MZ - thisNode.MZ;

    //            List<GapItem> gappedItems = gapItems.FindAll(a => PatternTools.pTools.PPM(thisNode.MZ + delta, thisNode.MZ + a.GapSize) <= ppmTolerance);

    //            if (gappedItems.Count == 0)
    //            {
    //                Console.Write("Problems");
    //            }
    //            else if (gappedItems.Count > 1)
    //            {
    //                gappedItems.Sort((a, b) => PatternTools.pTools.PPM(thisNode.MZ + delta, thisNode.MZ + a.GapSize).CompareTo(PatternTools.pTools.PPM(thisNode.MZ + delta, thisNode.MZ + b.GapSize)));
    //            }

    //            mzs.Add(thisNode.MZ);
    //            intensities.Add(thisNode.Intensity);
    //            errors.Add(PatternTools.pTools.PPM(thisNode.MZ + delta, thisNode.MZ + gappedItems[0].GapSize));
    //            sequence += gappedItems[0].AARegex;
    //            //Console.Write(gappedItems[0].AARegex);

    //            if (gappedItems.Count > 1)
    //            {
    //                Console.WriteLine("Shouldn't be here!");
    //            }

    //        }

    //        ////Add the last items
    //        mzs.Add(allNodes[path[path.Count - 1]].MZ);
    //        intensities.Add(allNodes[path[path.Count - 1]].Intensity);

    //        if (errors.Count > 0)
    //        {
    //            double avgError = errors.Average();
    //            double avgIntensity = intensities.Average();

    //            //Regex x = new Regex(@"DVY|YVD");
    //            //if (x.IsMatch(sequence)) {
    //            //    Console.WriteLine("*");
    //            //}
    //            Console.WriteLine(path.Count + "\t" + Math.Round(avgError, 2) + "\t" + Math.Round(avgIntensity, 2) + "\t" + sequence + "\t" + Math.Round(intensities.Sum(), 0));

    //            foreach (double m in mzs)
    //            {
    //                Console.Write(m + " ");
    //            }
    //            Console.WriteLine("");
    //        }



    //        return sequence;

    //    }
    //}
}
