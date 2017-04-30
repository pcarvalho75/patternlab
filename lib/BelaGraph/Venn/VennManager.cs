using System;
using System.Collections.Generic;
using System.Text;
using PatternTools;
using System.Linq;

namespace BelaGraph
{
    public class VennManager
    {
        SparseMatrix sparseMatrix;

        /// <summary>
        /// The number of the sparse matrix class followed by its existing dims
        /// </summary>
        Dictionary<int, List<int>> vennDic = new Dictionary<int,List<int>>();


        public Dictionary<int, List<int>> VennDic
        {
            get { return vennDic; }
        }

        public VennManager(SparseMatrix sm) {
            this.sparseMatrix = sm;

            //This method prepares the VennMatrix, necessary to do any comparison among classes
            
            vennDic.Clear();
            foreach (sparseMatrixRow row in sparseMatrix.theMatrixInRows)
            {
                //Make sure we have this class in the dictionary
                if (!vennDic.ContainsKey(row.Lable))
                {
                    vennDic.Add(row.Lable, new List<int>());
                }

                foreach (int dim in row.Dims)
                {
                    if (!vennDic[row.Lable].Contains(dim))
                    {
                        vennDic[row.Lable].Add(dim);
                    }
                }
            }

            //Sort the dictionary just to organzize it for debugging
            foreach (KeyValuePair<int, List<int>> kvp in vennDic) {
                kvp.Value.Sort();
            }
           
        
        }

        
        public List<DataVector> GetDataVectors ()
        {
            List<DataVector> result = new List<DataVector>();

            foreach (int key1 in vennDic.Keys)
            {
                DataVector dv = new DataVector(GraphStyle.Venn, key1.ToString());

                dv.Name = key1.ToString();
                dv.ExtraParamObject = vennDic[key1];
                
                foreach (int key2 in vennDic.Keys)
                {
                    PointCav pc = new PointCav();
                    pc.X = key2;
                    pc.Y = FindIntersection(key1, key2).Count;
                    pc.MouseOverTip = key1.ToString() + " vs " + key2.ToString();
                    dv.AddPoint(pc);
                    
                }

                result.Add(dv);
            }

            return result;
        }

        /// <summary>
        /// Calculates the overlapping area between two circles
        /// </summary>
        /// <param name="centerOfCircle1"></param>
        /// <param name="centerOfCircle2"></param>
        /// <param name="r1">Radius of circle 1</param>
        /// <param name="r2">Radius of circle 2</param>
        /// <param name="step">Use as default 1</param>
        /// <returns></returns>
        public static double OverlapBetweenCircles(PointCav centerOfCircle1, PointCav centerOfCircle2, double r1, double r2, double step)
        {
            double overlapArea = 0;
            //Our strategy is to create a box around the two circles and count the number of point that are common between them
            
            //Find out which one of the circles is smaller and define the properties of a box around it

            //find the smaller x and smaller y
            double xSmall; 
            double xBig;
            double ySmall;
            double yBig;

            if (r1 < r2)
            {
                xSmall = centerOfCircle1.X - r1;
                xBig = centerOfCircle1.X + r1;
                ySmall = centerOfCircle1.Y - r1;
                yBig = centerOfCircle1.Y + r1;
            }
            else
            {
                xSmall = centerOfCircle2.X - r2;
                xBig = centerOfCircle2.X + r2;
                ySmall = centerOfCircle2.Y - r2;
                yBig = centerOfCircle2.Y + r2;
            }

            for (double x = xSmall; x < xBig; x+=step)
            {
                for (double y = ySmall; y < yBig; y++)
                {
                    PointCav p = new PointCav();
                    p.X = x;
                    p.Y = y;

                    //If the point is within range, we sum the area
                    if (CalculateDistance(p, centerOfCircle1) <= r1 && CalculateDistance(p, centerOfCircle2) <= r2)
                    {
                        overlapArea++;
                    }
                }
            }

            return (overlapArea * step);
        }


        /// <summary>
        /// Careful, this method will make all numbers positive you should be working in the same quadrant always
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double CalculateDistance (PointCav p1, PointCav p2) {
            return (Math.Sqrt(Math.Pow(p1.X-p2.X, 2) + Math.Pow(p1.Y-p2.Y ,2)));
        }



        private List<int> FindIntersection(int label1, int label2)
        {
            return (vennDic[label1].Intersect(vennDic[label2]).ToList());
        }

    }
}
