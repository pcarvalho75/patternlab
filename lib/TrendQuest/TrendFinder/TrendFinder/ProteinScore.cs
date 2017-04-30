using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools;

namespace TrendQuest
{

    /// <summary>
    /// Used to store data for posterior computation
    /// </summary>
    public class proteinScore
    {
        
        public double YIntercept { get; set; }
        public double RegressorSlope { get; set; }
        public List<Point> PointsForRegression { get; set; }
        public int SparseMatrixIndex { get; set; }
        public double MeanSquareError { get; set; }
        public double R { get; set; }
        public double ExtremePValue { get; set; }

        public double TSignificance
        {
            get {
                double DenoNumerator = 1-Math.Pow(R,2);
                double DenoDeno = (double)PointsForRegression.Count - 2;


                double tValue = R / Math.Sqrt(DenoNumerator / DenoDeno);
                return (alglib.studenttdistribution(PointsForRegression.Count - 2, tValue));
            }
        }


        public proteinScore (double regressorSlope, double yIntercept, double min, double max, List<Point> pointForRegression, int sparseMatrixIndex, double meanSquareError, double r, double extremePvalue) {
            
            this.RegressorSlope = regressorSlope;
            this.YIntercept = yIntercept;
            this.PointsForRegression = pointForRegression;
            this.SparseMatrixIndex = sparseMatrixIndex;
            this.MeanSquareError = meanSquareError;
            this.R = r;
            this.ExtremePValue = extremePvalue;

        }

    }

}
