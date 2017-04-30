using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PatternTools;

namespace TrendQuest.ClusteringItems
{
    public class SpectralAngleClustering
    {
        SparseMatrix mySparseMatrix = new SparseMatrix();

        public SpectralAngleClustering(SparseMatrix sm)
        {
            this.mySparseMatrix = sm;
        }


        /// <summary>
        /// Rows will be considered as unit vectors
        /// </summary>
        /// <param name="desiredNodes"></param>
        /// <param name="isobaric"></param>
        /// <returns></returns>
        public List<SpectralNode> kMeans(double desiredNodes)
        {

            SparseMatrix smTemp = PatternTools.ObjectCopier.Clone(mySparseMatrix);

            smTemp.UnsparseTheMatrix();

            List<int> allDims = smTemp.allDims();
            List<SpectralNode> myNodes = new List<SpectralNode>((int)desiredNodes);

            for (int i = 0; i < (int)desiredNodes; i++)
            {
                myNodes.Add(new SpectralNode());
            }

            List<sparseMatrixRow> rowsToDelete = new List<sparseMatrixRow>();
            foreach (sparseMatrixRow smr in smTemp.theMatrixInRows)
            {
                smr.Values = PatternTools.pTools.UnitVector(smr.Values);
                if (smr.Values.Contains(double.NaN)) { rowsToDelete.Add(smr); }
            }

            smTemp.theMatrixInRows = smTemp.theMatrixInRows.Except(rowsToDelete).ToList();


            double[,] xy = smTemp.ToDoubleArrayMatrix();

            //if (isobaric)
            //{
            //    xy = new double[smTemp.theMatrixInRows[0].Dims.Count, smTemp.theMatrixInRows.Count];
            //    for (int y = 0; y < smTemp.theMatrixInRows.Count; y++)
            //    {
            //        for (int x = 0; x < smTemp.theMatrixInRows[y].Values.Count; x++)
            //        {
            //            xy[x, y] = smTemp.theMatrixInRows[y].Values[x];
            //        }
            //    }
            //}
            //else
            //{
            //    xy = smTemp.ToDoubleArrayMatrix();
            //}
            int noPoints = mySparseMatrix.theMatrixInRows.Count;

            int info = 0;
            double[,] c = null;
            int[] xyc = null;

            int npoints = xy.GetLength(0);
            int nVars = xy.GetLength(1);
            int k = (int)desiredNodes;

            Console.WriteLine("No points: " + npoints);
            Console.WriteLine("nvars: " + nVars);


            Console.WriteLine("kmeans - executed");


            noPoints = xy.GetLength(0);
            int noVars = xy.GetLength(1);
            alglib.kmeansgenerate(
                xy,
                noPoints,
                noVars,
                k,
                100,
                out info,
                out c,
                out xyc
                );

            Console.WriteLine("kmeans - executed");

            for (int i = 0; i < xyc.Length; i++)
            {
                myNodes[xyc[i]].MyInputVectors.Add(smTemp.theMatrixInRows[i]);
            }

            myNodes.Sort((a, b) => b.MyInputVectors.Count.CompareTo(a.MyInputVectors.Count));


            return (myNodes);

        }


        }



        //----------------------------------------------------------------------------


    }
