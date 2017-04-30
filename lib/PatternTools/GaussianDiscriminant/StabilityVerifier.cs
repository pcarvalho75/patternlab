using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.CSML;

namespace PatternTools.GaussianDiscriminant
{
    public static class StabilityVerifier
    {

        public static List<int> Verify2(List<sparseMatrixRow> rows)
        {



            //We group rows by class, and then for each class we check if there is a dim that holds the same value for all
            //We then eliminate these dims from all rows of all classes and report the eliminated dims

            List<int> unstableDims = new List<int>();

            var rowsGroupedByClasses = from r in rows
                                       group r by r.Lable into g
                                       select new { label = g.Key, rows = g };

            Dictionary<int, List<sparseMatrixRow>> lableRowDict = rowsGroupedByClasses.ToDictionary(a => a.label, a => a.rows.ToList());

            foreach (KeyValuePair<int, List<sparseMatrixRow>> kvp in lableRowDict)
            {

                SparseMatrix matrix = new SparseMatrix(rows);
                Matrix meanVector = new PatternTools.CSML.Matrix(matrix.GetMeanVector().ToArray());
                double[,] covMatrix = PatternTools.SparseMatrix.CovarianceMatrix(matrix.ToDoubleArrayMatrix(), matrix.GetMeanVector().ToArray());

                unstableDims.AddRange(checkStability(covMatrix));

            }

            unstableDims = unstableDims.Distinct().ToList();
            unstableDims.Sort((a, b) => b.CompareTo(a));

            //Now we eliminate these dims from the sparse matrix
            foreach (int d in unstableDims)
            {
                int index = rows[0].Dims.FindIndex(a => a == d);

                foreach (sparseMatrixRow r in rows)
                {
                    r.Values.RemoveAt(index);
                }

                rows[0].Dims.RemoveAt(index);
            }

            return (unstableDims);

        }

        private static List<int> checkStability(double[,] covMatrix)
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

        /// <summary>
        /// Searches for unstable dims (that have the same value), eliminates them, and returns the eliminated dims
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static List<int> Verify (List<PatternTools.sparseMatrixRow> rows) {

            //We group rows by class, and then for each class we check if there is a dim that holds the same value for all
            //We then eliminate these dims from all rows of all classes and report the eliminated dims

            List<int> unstableDims = new List<int>();

            var rowsGroupedByClasses = from r in rows
                                       group r by r.Lable into g
                                       select new { label = g.Key, rows = g };

            Dictionary<int, List<sparseMatrixRow>> lableRowDict = rowsGroupedByClasses.ToDictionary(a => a.label, a => a.rows.ToList());

            foreach (KeyValuePair<int, List<sparseMatrixRow>> kvp in lableRowDict)
            {
                Dictionary<int, int> dimValueCounter = new Dictionary<int, int>();

                //if there are more than 2 values in the rows we are safe

                for (int d = 0; d < kvp.Value[0].Dims.Count; d++)
                {
                    List<double> values = new List<double>();
                    bool includeDim = true;
                    foreach (PatternTools.sparseMatrixRow r in kvp.Value)
                    {
                        if (!values.Contains(r.Values[d]))
                        {
                            values.Add(r.Values[d]);
                        }

                        if (values.Count > 1)
                        {
                            includeDim = false;
                            break;
                        }
                    }

                    if (includeDim)
                    {
                        unstableDims.Add(kvp.Value[0].Dims[d]);
                    }
                }
            }

            unstableDims = unstableDims.Distinct().ToList();
            unstableDims.Sort((a,b) => b.CompareTo(a));

            //Now we eliminate these dims from the sparse matrix
            foreach (int d in unstableDims)
            {
                int index = rows[0].Dims.FindIndex(a=> a==d);

                foreach (sparseMatrixRow r in rows)
                {
                    r.Values.RemoveAt(index);
                }

                rows[0].Dims.RemoveAt(index);
            }

            return (unstableDims);

        }
    }
}
