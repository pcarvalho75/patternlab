using PatternTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RbfClassifier
{
    public class dRBF : IRBFClassifier
    {
        public double[,] PhiMatrix { get; set; }
        public Dictionary<int, List<List<double>>> DictKmeans;

        string[,] DebugPhi { get; set; }
        // list of the vclass values (centroids labels)
        public List<double> Tvector { get; set; }
        // our estimates of the std for each hidden node
        public List<double> Svector { get; set; }
        List<List<double>> allCentroids { get; set; }
        List<double> W { get; set; }
        List<int> LabelsFromSM;
        public SparseMatrix SparseMatrix { get; set; }

        Dictionary<int, double> minValues = new Dictionary<int, double>();
        Dictionary<int, double> maxValues = new Dictionary<int, double>();
        List<int> AllDims;

        bool useNormalization;
        bool useMinSigma;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useNormalization"></param>
        /// <param name="minSigma">If min sigma is true, the sigma for each centroid will be equal to the distance of the closest centroid, else, the furthest centroid.</param>
        public dRBF(bool useNormalization, bool minSigma)
        {
            this.useNormalization = useNormalization;
            useMinSigma = minSigma;
        }


        Dictionary<int, List<List<double>>> GenerateDictKmeans(SparseMatrix sm, Dictionary<int, int> dictLabelCluster)
        {
            Dictionary<int, List<List<double>>> DictKmeans = new Dictionary<int, List<List<double>>>();

            List<int> labels = sm.ExtractLabels();
            List<int> dims = sm.allDims();

            foreach (int label in labels)
            {
                List<sparseMatrixRow> inputVectors = sm.theMatrixInRows.FindAll(a => a.Lable == label);
                SparseMatrix tmpMatrix = new SparseMatrix(inputVectors);

                double[,] xy = tmpMatrix.ToDoubleArrayMatrix();
                int nPoints = inputVectors.Count;
                int nVars = dims.Count;
                int k = dictLabelCluster[label];
                int restarts = 40;
                int info;
                double[,] c;
                int[] xyc;

                alglib.kmeansgenerate(xy, nPoints, nVars, k, restarts, out info, out c, out xyc);
                List<List<double>> theseCentroids = new List<List<double>>();


                for (int i = 0; i < c.GetLength(1); i++)
                {
                    List<double> centroid = new List<double>(nVars);

                    for (int j = 0; j < c.GetLength(0); j++)
                    {
                        centroid.Add(c[j, i]);
                    }
                    theseCentroids.Add(centroid);
                }

                DictKmeans.Add(label, theseCentroids);
            }

            return DictKmeans;
        }

        /// <summary>
        /// In the traditional training clusters are asigned in an unsupervised manner
        /// </summary>
        /// <param name="_sm"></param>
        /// <param name="NoClusters"></param>
        public void TrainTraditional(SparseMatrix _sm, int NoClusters)
        {
            SparseMatrix sm = new SparseMatrix();
            foreach (sparseMatrixRow r in _sm.theMatrixInRows)
            {
                sm.addRow(r.Clone());
            }

            //Get the centroids
            double[,] xy = sm.ToDoubleArrayMatrix();
            int nPoints = sm.theMatrixInRows.Count;
            int nVars = sm.allDims().Count;
            int restarts = 50;
            int info;
            double[,] c;
            int[] xyc;

            alglib.kmeansgenerate(xy, nPoints, nVars, NoClusters, restarts, out info, out c, out xyc);
            List<List<double>> theseCentroids = new List<List<double>>();

            allCentroids = new List<List<double>>();
            for (int i = 0; i < c.GetLength(1); i++)
            {
                List<double> centroid = new List<double>(nVars);

                for (int j = 0; j < c.GetLength(0); j++)
                {
                    centroid.Add(c[j, i]);
                }
                allCentroids.Add(centroid);
            }

            DictKmeans = new Dictionary<int, List<List<double>>>();
            DictKmeans.Add(1, allCentroids);

            //And generate our internal dictionary of centroids
            Dictionary<int, List<List<double>>> dictKmeans = PatternTools.ObjectCopier.Clone(DictKmeans);


            if (useNormalization)
            {
                List<int> dims = sm.allDims();
                AllDims = dims;

                foreach (int dim in dims)
                {
                    List<double> allValues = sm.ExtractDimValues(dim, 0, true);
                    double minVal = allValues.Min();
                    double maxVal = allValues.Max();

                    //
                    double average = allValues.Average();
                    double std = PatternTools.pTools.Stdev(allValues, true);

                    if (minVal < average - (6 * std))
                    {
                        minVal = average - (6 * std);
                    }

                    if (maxVal > average + (6 * std))
                    {
                        maxVal = average + (6 * std);
                    }

                    //
                    double delta = maxVal - minVal;


                    minValues.Add(dim, minVal);
                    maxValues.Add(dim, maxVal);

                    foreach (sparseMatrixRow r in sm.theMatrixInRows)
                    {
                        int indexOf = r.Dims.IndexOf(dim);
                        if (indexOf > -1)
                        {
                            r.Values[indexOf] = (r.Values[indexOf] - minVal) / delta;
                        }
                    }

                }

                foreach (KeyValuePair<int, List<List<double>>> kvp in dictKmeans)
                {
                    foreach (List<double> centroid in kvp.Value)
                    {
                        for (int i = 0; i < dims.Count; i++)
                        {
                            double minVal = minValues[dims[i]];
                            double maxVal = maxValues[dims[i]];
                            centroid[i] = (centroid[i] - minVal) / (maxVal - minVal);

                        }

                    }
                }
            }



            // Calculate Min Sigma sigma
            List<List<double>> sigma = new List<List<double>>();


            List<double> sigmaV = new List<double>(allCentroids.Count);

            if (useMinSigma)
            {
                for (int i = 0; i < allCentroids.Count; i++)
                {
                    double distance = Double.MaxValue;

                    for (int j = 0; j < allCentroids.Count; j++)
                    {

                        if (i == j)
                        {
                            continue;
                        }

                        double dist = PatternTools.pTools.EuclidianDistance(allCentroids[i], allCentroids[j]);

                        if (dist < distance)
                        {
                            distance = dist;
                        }

                    }

                    sigmaV.Add(distance);
                }
            }
            else
            {
                for (int i = 0; i < allCentroids.Count; i++)
                {
                    double distance = Double.MinValue;

                    for (int j = 0; j < allCentroids.Count; j++)
                    {

                        if (i == j)
                        {
                            continue;
                        }

                        double dist = PatternTools.pTools.EuclidianDistance(allCentroids[i], allCentroids[j]);

                        if (dist > distance)
                        {
                            distance = dist;
                        }

                    }

                    sigmaV.Add(distance);
                }
            }

            Svector = sigmaV; // used in the previous version of the training;  // this was used in the previous version of the training, substituted by SigmaV


            sm.theMatrixInRows.Sort((a, b) => a.Lable.CompareTo(b.Lable));
            SparseMatrix = sm;

            //Generate T and S vectors
            Tvector = new List<double>(sm.theMatrixInRows.Count);

            List<int> labels = sm.ExtractLabels();
            LabelsFromSM = sm.ExtractLabels();

            foreach (int label in labels)
            {
                List<sparseMatrixRow> inputVectors = sm.theMatrixInRows.FindAll(a => a.Lable == label);
                for (int i = 0; i < inputVectors.Count; i++)
                {
                    Tvector.Add(label);
                }
                
            }

            PhiMatrix = new double[sm.theMatrixInRows.Count, allCentroids.Count];
            //DebugPhi = new string[sm.theMatrixInRows.Count, allCentroids.Count];

            for (int i = 0; i < allCentroids.Count; i++)
            {
                for (int j = 0; j < sm.theMatrixInRows.Count; j++)
                {
                    // Incorporating thew sigma value for each node
                    double result = RBFKernel(sm.theMatrixInRows[j].Values, allCentroids[i], sigmaV[i]);
                    PhiMatrix[j, i] = result;
                    //DebugPhi[j, i] = "[(" + string.Join(",", sm.theMatrixInRows[j].Values) + "),(" + string.Join(",", allCentroids[i]) + ")]";
                }
            }

            //PrintPhiMatrix(2);
            //PrintDebugPhiMatrix();

            //creating and transposing the matrix
            PatternTools.CSML.Matrix phiCSML = new PatternTools.CSML.Matrix(PhiMatrix);
            PatternTools.CSML.Matrix phiTCSML = phiCSML.Transpose();

            PatternTools.CSML.Matrix inversePhiProduct = (phiTCSML * phiCSML).Inverse();

            PatternTools.CSML.Matrix preWProduct = (inversePhiProduct * phiTCSML);

            PatternTools.CSML.Matrix TCSML = new PatternTools.CSML.Matrix(Tvector.ToArray());

            // calculationg W vector
            PatternTools.CSML.Matrix wCSML = (preWProduct * TCSML);

            W = new List<double>();
            for (int i = 0; i < wCSML.RowCount; i++)
            {
                W.Add(wCSML[i + 1].Re);
            }

        }



        #region new rbf
        /// <summary>
        /// This training funcition is novel as it employs a k-means to each class, so ks are assigned in a supervised manner
        /// </summary>
        /// <param name="_sm"></param>
        /// <param name="dictLabelCluster"></param>
        public void Train(SparseMatrix _sm,  Dictionary<int, int> dictLabelCluster)
        {
            SparseMatrix sm = new SparseMatrix();
            foreach (sparseMatrixRow r in _sm.theMatrixInRows)
            {
                sm.addRow(r.Clone());
            }

            //Get the centroids
            DictKmeans = GenerateDictKmeans(sm, dictLabelCluster);

            //And generate our internal dictionary of centroids
            Dictionary<int, List<List<double>>> dictKmeans = PatternTools.ObjectCopier.Clone(DictKmeans);


            if (useNormalization)
            {
                List<int> dims = sm.allDims();
                AllDims = dims;

                foreach (int dim in dims)
                {
                    List<double> allValues = sm.ExtractDimValues(dim, 0, true);
                    double minVal = allValues.Min();
                    double maxVal = allValues.Max();
                    
                    //
                    double average = allValues.Average();
                    double std = PatternTools.pTools.Stdev(allValues, true);

                    if (minVal < average - (6 * std))
                    {
                        minVal = average - (6 * std);
                    }

                    if (maxVal > average + (6 * std))
                    {
                        maxVal = average + (6 * std);
                    }

                    //
                    double delta = maxVal - minVal;
                    

                    minValues.Add(dim,  minVal);
                    maxValues.Add(dim, maxVal);

                    foreach (sparseMatrixRow r in sm.theMatrixInRows)
                    {
                        int indexOf = r.Dims.IndexOf(dim);
                        if (indexOf > -1)
                        {
                            r.Values[indexOf] = (r.Values[indexOf] - minVal) / delta;
                        }
                    }

                }

                foreach (KeyValuePair<int, List<List<double>>> kvp in dictKmeans)
                {
                    foreach (List<double> centroid in kvp.Value)
                    {
                        for (int i = 0; i < dims.Count; i++)
                        {
                            centroid[i] = (centroid[i] - minValues[dims[i]]) / (maxValues[dims[i]] - minValues[dims[i]]);

                        }

                    }
                }

            }



            // Calculate sigma
            Dictionary<int, List<double>> sigma = new Dictionary<int, List<double>>();

            foreach (KeyValuePair<int, List<List<double>>> kvp in dictKmeans)
            {
                List<double> sigmaV = new List<double>(kvp.Value.Count);

                if (useMinSigma)
                {
                    for (int i = 0; i < kvp.Value.Count; i++)
                    {
                        double distance = Double.MaxValue;

                        for (int j = 0; j < kvp.Value.Count; j++)
                        {

                            if (i == j)
                            {
                                continue;
                            }

                            double dist = PatternTools.pTools.EuclidianDistance(kvp.Value[i], kvp.Value[j]);

                            if (dist < distance)
                            {
                                distance = dist;
                            }

                        }

                        sigmaV.Add(distance);
                    }
                }
                else
                {
                    for (int i = 0; i < kvp.Value.Count; i++)
                    {
                        double distance = Double.MinValue;

                        for (int j = 0; j < kvp.Value.Count; j++)
                        {

                            if (i == j)
                            {
                                continue;
                            }

                            double dist = PatternTools.pTools.EuclidianDistance(kvp.Value[i], kvp.Value[j]);

                            if (dist > distance)
                            {
                                distance = dist;
                            }

                        }

                        sigmaV.Add(distance);
                    }
                }

                sigma.Add(kvp.Key, sigmaV);
            }
            
            if (dictKmeans.Keys.Count > 3)
            {
                throw new Exception("For more than 3 classes use mRBF.");
            }

            sm.theMatrixInRows.Sort((a, b) => a.Lable.CompareTo(b.Lable));
            SparseMatrix = sm;

            //Generate T and S vectors
            Tvector = new List<double>(sm.theMatrixInRows.Count);
            Svector = new List<double>(sm.theMatrixInRows.Count);
            
            List<int> labels = sm.ExtractLabels();
            allCentroids = new List<List<double>>();
            LabelsFromSM = sm.ExtractLabels();

            foreach (int label in labels)
            {
                 List<sparseMatrixRow> inputVectors = sm.theMatrixInRows.FindAll(a => a.Lable == label);
                for (int i = 0; i < inputVectors.Count; i++) 
                {
                    Tvector.Add(label);
                }
                Svector.AddRange(sigma[label]);

                allCentroids.AddRange(dictKmeans[label]);
            }

            PhiMatrix = new double[sm.theMatrixInRows.Count, allCentroids.Count];
            //DebugPhi = new string[sm.theMatrixInRows.Count, allCentroids.Count];

            for (int i = 0; i < allCentroids.Count; i++)
            {
                for (int j = 0; j < sm.theMatrixInRows.Count; j++)
                {
                    // Incorporating thew sigma value for each node
                    double result = RBFKernel(sm.theMatrixInRows[j].Values, allCentroids[i], Svector[i]);
                    PhiMatrix[j, i] = result;
                    //DebugPhi[j, i] = "[(" + string.Join(",", sm.theMatrixInRows[j].Values) + "),(" + string.Join(",", allCentroids[i]) + ")]";
                }
            }

            //PrintPhiMatrix(2);
            //PrintDebugPhiMatrix();

            //creating and transposing the matrix
            PatternTools.CSML.Matrix phiCSML = new PatternTools.CSML.Matrix(PhiMatrix);
            PatternTools.CSML.Matrix phiTCSML = phiCSML.Transpose();

            PatternTools.CSML.Matrix inversePhiProduct = (phiTCSML * phiCSML).Inverse();

            PatternTools.CSML.Matrix preWProduct = (inversePhiProduct * phiTCSML);

            PatternTools.CSML.Matrix TCSML = new PatternTools.CSML.Matrix(Tvector.ToArray());

            // calculationg W vector
            PatternTools.CSML.Matrix wCSML = (preWProduct * TCSML);
           
            W = new List<double>();
            for (int i = 0; i < wCSML.RowCount; i++)
            {
                W.Add(wCSML[i + 1].Re);
            }
        
        }
        #endregion


        public double Classify(List<double> x)
        {
            double result = 0;

            if (useNormalization)
            {
                for (int i = 0; i < x.Count; i++)
                {
                    x[i] = (x[i] - minValues[AllDims[i]]) / (maxValues[AllDims[i]] - minValues[AllDims[i]]) ;
                }
            }

            for (int i = 0; i < allCentroids.Count; i++)
            {
                // Incorporating thew sigma value for each node
                result += ( W[i] * RBFKernel(allCentroids[i], x, Svector[i]));
            }
            return result;
        }

        public int ClassifyByLabel(List<double> x, out double gx)
        {
            double result = Classify(x);
            LabelsFromSM.Sort((a, b) => Math.Abs(a - result).CompareTo(Math.Abs(b - result)));
            gx = result;
            return LabelsFromSM[0];
        }

        public void PrintPhiMatrix(int noDecimals)
        {
            for (int i = 0; i < PhiMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < PhiMatrix.GetLength(1); j++)
                {
                    Console.Write(Math.Round(PhiMatrix[i, j], noDecimals) + "\t");
                }
                Console.WriteLine("");
            }

        }

        public void PrintDebugPhiMatrix()
        {
            for (int i = 0; i < PhiMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < PhiMatrix.GetLength(1); j++)
                {
                    Console.Write(DebugPhi[i, j] + "\t");
                }
                Console.WriteLine("");
            }

        }

        private double RBFKernel(List<double> v1, List<double> v2, double sigma)
        {
            double result = 0;
            for (int i = 0; i < v1.Count; i++)
            {
                result += Math.Pow(v1[i] - v2[i], 2);
            }

            //result /= 2;
            result /= (2 * Math.Pow(sigma, 2));
            result *= -1;
            result = Math.Exp(result);
            return result;
        }


    }
}


/*************************************************************************
k-means++ clusterization

INPUT PARAMETERS:
    XY          -   dataset, array [0..NPoints-1,0..NVars-1].
    NPoints     -   dataset size, NPoints>=K
    NVars       -   number of variables, NVars>=1
    K           -   desired number of clusters, K>=1
    Restarts    -   number of restarts, Restarts>=1

OUTPUT PARAMETERS:
    Info        -   return code:
                    * -3, if task is degenerate (number of distinct points is
                          less than K)
                    * -1, if incorrect NPoints/NFeatures/K/Restarts was passed
                    *  1, if subroutine finished successfully
    C           -   array[0..NVars-1,0..K-1].matrix whose columns store
                    cluster's centers
    XYC         -   array[NPoints], which contains cluster indexes

  -- ALGLIB --
     Copyright 21.03.2009 by Bochkanov Sergey
*************************************************************************/