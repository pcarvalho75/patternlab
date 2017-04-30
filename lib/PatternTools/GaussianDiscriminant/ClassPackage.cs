using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.CSML;
using System.IO;

namespace PatternTools.GaussianDiscriminant
{
    [Serializable]
    //Internal Class
    public class ClassPackage
    {
        public ClassPackage()
        {
            unstableDims = new List<int>();
        }

        PatternTools.CSML.Matrix meanVector;
        PatternTools.CSML.Matrix covarianceMatrix;
        PatternTools.CSML.Matrix inverseCovarianceMatrix;

        public List<int> unstableDims;


        //------------------------------------------------


        SparseMatrix matrix = new SparseMatrix();

        public Matrix MatrixMeanVector
        {
            get { return meanVector; }
        }

        public Matrix InverseCovarianceMatrix
        {
            get { return inverseCovarianceMatrix; }
        }

        public int NumberOfEntries
        {
            get { return matrix.theMatrixInRows.Count; }
        }

        public List<double> MeanVector
        {
            get { return meanVector.GetRow(0); }
        }

        public List<double> StdVector
        {
            get { return matrix.GetStandardDevVector(); }
        }



        public double Prior
        {
            get; set;
        }

        public void ClearWorkingMatrix()
        {
            matrix.clearMatrix();
        }

        public void AddRow(sparseMatrixRow r)
        {
            matrix.addRow(r);
        }


        /// <summary>
        /// The method returns unstable dims that were elmininated from the sparse matrix and should be eliminated from future input vectors used for classification
        /// </summary>
        /// <param name="useRobustStatistics"></param>
        /// <param name="regularizationFactor"></param>
        /// <param name="saveMatrices"></param>
        /// <returns></returns>
        public List<int> ComputeMatrices(bool useRobustStatistics, bool saveMatrices)
        {

            //Get the mean vector
            double[,] covMatrix;

            if (useRobustStatistics)
            {
                meanVector = new PatternTools.CSML.Matrix(matrix.GetMedianVector().ToArray());
                covMatrix = PatternTools.SparseMatrix.CovarianceMatrix(matrix.ToDoubleArrayMatrix(), matrix.GetMedianVector().ToArray());
            }
            else
            {
                meanVector = new PatternTools.CSML.Matrix(matrix.GetMeanVector().ToArray());
                covMatrix = PatternTools.SparseMatrix.CovarianceMatrix(matrix.ToDoubleArrayMatrix(), matrix.GetMeanVector().ToArray());
            }


            unstableDims = checkStability(covMatrix);

            if (unstableDims.Count > 0)
            {
                unstableDims.Sort((a, b) => b.CompareTo(a));
                double perturbationCounter = 0;
                if (unstableDims.Count > 0)
                {
                    Console.WriteLine("**Warning:: I am adding a perturbation to the convariance matrix for stability reasons!!!");
                    foreach (int d in unstableDims)
                    {
                        foreach (sparseMatrixRow r in matrix.theMatrixInRows)
                        {
                            perturbationCounter += 0.0005;
                            r.Values[d] += perturbationCounter;
                            if (perturbationCounter > 0.05)
                            {
                                perturbationCounter = 0;
                            }
                        }
                    }
                    covMatrix = PatternTools.SparseMatrix.CovarianceMatrix(matrix.ToDoubleArrayMatrix(), matrix.GetMedianVector().ToArray());
                }

                if (useRobustStatistics)
                {
                    covMatrix = PatternTools.SparseMatrix.CovarianceMatrix(matrix.ToDoubleArrayMatrix(), matrix.GetMedianVector().ToArray());
                }
                else
                {
                    covMatrix = PatternTools.SparseMatrix.CovarianceMatrix(matrix.ToDoubleArrayMatrix(), matrix.GetMeanVector().ToArray());
                }

            }


            covarianceMatrix = new CSML.Matrix(covMatrix);
            inverseCovarianceMatrix = covarianceMatrix.Inverse();

            if (saveMatrices)
            {
                matrix.saveMatrix(matrix.theMatrixInRows[0].Lable + "FullMatrix.txt");
                StreamWriter sr = new StreamWriter(matrix.theMatrixInRows[0].Lable + "_covMatrix.txt");
                int top = (int)Math.Sqrt(covMatrix.Length);
                for (int i = 0; i < top; i++)
                {
                    for (int j = 0; j < top; j++)
                    {
                        sr.Write(covMatrix[i, j] + "\t");
                    }
                    sr.WriteLine("");
                }
                sr.Close();

                Console.WriteLine("Saving variance matrix and its inverse to disk.");
            }

            return (unstableDims);

        }

        /// <summary>
        /// We check if there are rows with same values; if there is, we need to add a perturbation of 1/50 of its magnitude to escape singularity
        /// </summary>
        /// <param name="covMatrix"></param>
        private List<int> checkStability(double[,] covMatrix)
        {
            int theDims = (int)Math.Sqrt(covMatrix.Length);
            List<List<double>> theMatrixInRows = new List<List<double>>(theDims);
            List<int> unstableDims = new List<int>();

            for (int i = 0; i < theDims; i++)
            {
                List<double> r = new List<double>(theDims);
                for (int j = 0; j < theDims; j++)
                {
                    r.Add(covMatrix[i, j]);
                }
                theMatrixInRows.Add(r);
            }

            for (int i = 0; i < theDims; i++)
            {
                if (theMatrixInRows[i].Distinct().Count() == 1)
                {
                    unstableDims.Add(i);
                }
            }

            return (unstableDims);
        }

        //Cached variable for high-trhough put classification
        bool alreadyEvaluated = false;
        PatternTools.CSML.Matrix Wi;
        PatternTools.CSML.Matrix wi;
        double wi0;

        public double Evaluate(double[] pointA)
        {

            if (!alreadyEvaluated)
            {
                //Wi = -0.5 * inverseCovarianceMatrix;
                //wi = inverseCovarianceMatrix * meanVector;
                //w0Part1 = -0.5 * meanVector.Transpose() * inverseCovarianceMatrix * meanVector;
                //w0Part2 = -(0.5 * Math.Log(covarianceMatrix.Determinant().Re, Math.E));
                //w0 = w0Part1.Determinant().Re + w0Part2;
            }

            if (!alreadyEvaluated)
            {
                Wi = -0.5 * inverseCovarianceMatrix;
                wi = inverseCovarianceMatrix * meanVector;

                Matrix wi0Part1 = -0.5 * meanVector.Transpose() * inverseCovarianceMatrix * meanVector;
                double wi0Part2 = -(0.5 * Math.Log(covarianceMatrix.Determinant().Re, Math.E));
                double wi0Part3 = Math.Log(Prior);

                wi0 = wi0Part1.Determinant().Re + wi0Part2 + wi0Part3;
            }



            alreadyEvaluated = true;

            PatternTools.CSML.Matrix point = new PatternTools.CSML.Matrix(pointA);



            //Term 1
            PatternTools.CSML.Matrix t1 = point.Transpose() * Wi * point;

            //Term 2 
            PatternTools.CSML.Matrix t2 = wi.Transpose() * point;

            double gx = t1.Determinant().Re + t2.Determinant().Re + wi0;

            return (gx);

        }

    }

}
