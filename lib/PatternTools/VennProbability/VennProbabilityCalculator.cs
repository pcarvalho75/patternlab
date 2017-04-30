using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools;
using System.IO;

namespace PatternTools.VennProbability
{
    public class VennProbabilityCalculator
    {
        SparseMatrix sparseMatrix;
        List<IntersectionResult> intersectionResults;
        

        public VennProbabilityCalculator(SparseMatrix sparseMatrix)
        {
            this.sparseMatrix = sparseMatrix;
        }

        public VennProbabilityCalculator(string sparseMatrixFile)
        {
            sparseMatrix = new SparseMatrix(new StreamReader(sparseMatrixFile));
        }




        //------------------------------------------------------------------------------------------

        public void CalculateProbabilitiesForClass(int classOfInterest, double minSignal, double maxSignal)
        {
            //First we generate a cloned sparse matrix containing only the vectors that belong to the class of interest.
            SparseMatrix subSparseMatrix = new SparseMatrix();
            foreach (sparseMatrixRow r in sparseMatrix.theMatrixInRows)
            {
                if (r.Lable == classOfInterest)
                {
                    subSparseMatrix.addRow(r.Clone());
                }
            }

            //Now we need to make sure that all the rows in the sparse matrix contain only signal within the predefined bounds
            List<int> dimsToEliminate = new List<int>();
            List<int> allDimsTmp = subSparseMatrix.allDims();
            foreach (int dim in allDimsTmp)
            {
                double avgSignal = subSparseMatrix.ExtractDimValues(dim, 0, true).Average();
                if (avgSignal < minSignal || avgSignal > maxSignal)
                {
                    dimsToEliminate.Add(dim);
                }
            }

            foreach (int dim in dimsToEliminate)
            {
                subSparseMatrix.eliminateDim(dim, 0, true);
            }
            
            //---
            int noReplicates = subSparseMatrix.theMatrixInRows.Select(a => a.Lable = classOfInterest).ToList().Count;
            List<int> allDims = subSparseMatrix.allDims();
            if (noReplicates > 9)
            {
                throw new Exception("To many replicates for the combination engine");
            }

            if (noReplicates < 2)
            {
                throw new Exception("You need at least two repplicates for each class");
            }


            //Step 1, obtain a list of lists of all combination of the number of classes
            List<List<int>> classCombinations = pTools.GetClassCombinations(noReplicates);
            classCombinations.Sort((a, b) => a.Count.CompareTo(b.Count));

            //Step 2 - Find the Intersection of all dims
            List<int> intersectionOfAllDims = GetIntersection(classCombinations[classCombinations.Count - 1], subSparseMatrix.theMatrixInRows);

            //Step 3 - Calculate all possible intersections
            intersectionResults = new List<IntersectionResult>();
            foreach (List<int> theRows in classCombinations)
            {
                List<int> intersectionOfDims = GetIntersection(theRows, subSparseMatrix.theMatrixInRows);
                IntersectionResult ir = new IntersectionResult(theRows, intersectionOfDims);
                intersectionResults.Add(ir);

            }

            //Step 4 - Calculate the percentage of uniqueness for each overlap
            double noUniqueCount = 0;
            foreach (IntersectionResult ir in intersectionResults)
            {
                //Find the union of all other assays not belonging to the one at hand
                List<int> allOther = new List<int>();
                for (int i = 0; i < noReplicates; i++)
                {
                    if (!ir.TheSparseMatrixRows.Contains(i))
                    {
                        allOther.AddRange(subSparseMatrix.theMatrixInRows[i].Dims);
                    }
                }
                allOther = allOther.Distinct().ToList();

                List<int> intersectOfThisWithAllOthers = ir.IntersectingDims.Intersect(allOther).ToList();

                ir.NoUniqueComparedToTotal = ir.IntersectingDims.Count - intersectOfThisWithAllOthers.Count;
                noUniqueCount += (double)ir.NoUniqueComparedToTotal;
                double percentageOfUniqueness = (double)ir.NoUniqueComparedToTotal / (double)allDims.Count;
                ir.PercentageUniqueness = percentageOfUniqueness;
            }

        }

        public List<double> DetermineSignalCutoffs(int noBins, int classLabel)
        {

            PatternTools.SparseMatrix sm = new SparseMatrix();

            foreach (sparseMatrixRow smr in sparseMatrix.theMatrixInRows)
            {
                if (smr.Lable == classLabel)
                {
                    sm.addRow(smr);
                }
            }

            List<double> cutoffs = DetermineSignalCutoffs2(sm, noBins);

            if (noBins == 1)
            {
                cutoffs.Add(0);
            }

            cutoffs.Sort();



            return (cutoffs);
        }

        internal void PrintPercentageResultsToScreen()
        {
            //Print results to screen
            double cumulativePercentage = 0;
            foreach (IntersectionResult ir in intersectionResults)
            {
                Console.Write("\nDims:");
                foreach (int d in ir.TheSparseMatrixRows)
                {
                    Console.Write(" " + d);
                }

                Console.WriteLine("\nUnique: " + ir.NoUniqueComparedToTotal);
                Console.WriteLine("Percentage of total: " + ir.PercentageUniqueness);
                cumulativePercentage += ir.PercentageUniqueness;
                Console.WriteLine("Cumulative percentage: " + cumulativePercentage);

            }
        }


        private List<double> DetermineSignalCutoffs2(SparseMatrix sm, int noBins)
        {
            List<double> cutoffs = new List<double>(noBins);

            List<double> signals = new List<double>();

            List<int> theDims = sm.allDims();

            foreach (int dim in theDims)
            {
                List<double> values = sm.ExtractDimValues(dim, 0, true);
                signals.Add(values.Average());
                //signals.Add(values.Sum() / (double)sm.theMatrixInRows.Count);
            }

            signals.Sort();


            for (int i = 0; i < signals.Count; i += (int)Math.Floor((double)signals.Count / (double)noBins))
            {
                cutoffs.Add(signals[i]);
            }

            if (cutoffs[cutoffs.Count - 1] < signals[signals.Count - 1])
            {
                cutoffs[cutoffs.Count - 1] = signals[signals.Count - 1];
            }

            cutoffs.Sort();

            return cutoffs.Distinct().ToList();
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theMatrixRowsToConsider">The rows you wish to find the intersection</param>
        /// <param name="sm">The sparse matrix</param>
        /// <returns></returns>
        public static List<int> GetIntersection(List<int> theMatrixRowsToConsider, List<PatternTools.sparseMatrixRow> sm)
        {
            List<int> result = sm[theMatrixRowsToConsider[0]].Dims;

            for (int i = 1; i < theMatrixRowsToConsider.Count; i++)
            {
                List<int> theList = sm[theMatrixRowsToConsider[i]].Dims;
                result = result.Intersect(theList).ToList();
            }
            return (result);
        }

        public List<IntersectionResultAnalysis> GenerateProbabilisticDictionary(int noOfGroups)
        {
            List<IntersectionResultAnalysis> theResults = new List<IntersectionResultAnalysis>();

            List<int> labels = (from r in sparseMatrix.theMatrixInRows
                                select r.Lable).Distinct().ToList();

            foreach (int label in labels)
            {
                List<double> cutoffsForClass = DetermineSignalCutoffs(noOfGroups, label);

                for (int i = 0; i < cutoffsForClass.Count - 1; i++)
                {
                    Console.WriteLine("Computing probabilities for class {0} for signals >={1} :: <{2}", label, Math.Round(cutoffsForClass[i],3), Math.Round(cutoffsForClass[i + 1], 3));
                    CalculateProbabilitiesForClass(label, cutoffsForClass[i], cutoffsForClass[i + 1]);
                    List<IntersectionResultAnalysis> partialResults = GetIntersectionResultAnalysis(noOfGroups);

                    foreach (IntersectionResultAnalysis ira in partialResults)
                    {
                        ira.ClassLabel = label;
                        ira.SignalLowerBound = cutoffsForClass[i];
                        ira.SignalUpperBound = cutoffsForClass[i + 1];
                    }

                    theResults.AddRange(partialResults);
                }
            }

            return (theResults);
        }


        public List<IntersectionResultAnalysis> GetIntersectionResultAnalysis(int noOfGroups)
        {
            return (IntersectionResult.IntersectionResultAnalysis(intersectionResults, noOfGroups));
        }



        public void PrintProbabilities(int noOfGroups)
        {

            List<IntersectionResultAnalysis> iras = IntersectionResult.IntersectionResultAnalysis(intersectionResults, noOfGroups);

            foreach (IntersectionResultAnalysis ira in iras)
            {
                Console.WriteLine("NoRep: {0}\t %: {1}\t groupSize: {2}\t Prob: {3}", ira.NoReplicatesAppeared, Math.Round(ira.PercentageOfUniqueness, 3), ira.NoCandidatesInGroup, Math.Round(ira.BayesianProbability, 3));
            }

        }

    }
}
