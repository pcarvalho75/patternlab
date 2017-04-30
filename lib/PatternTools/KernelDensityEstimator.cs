using System;
using System.Collections.Generic;

namespace PatternTools
{
    public class RelaxedKernelDensityEstimator
    {
        List<double> myPoints;
        double h;
        double rp; //relaxation parameter
        
        //The extreme density points
        double min = 0;
        double max = double.MinValue;
        List<double> densityVector = new List<double>();

        public RelaxedKernelDensityEstimator(List<double> points, double h, double rp)
        {
            myPoints = points;
            myPoints.Sort();
            this.h = h;
            this.rp = rp;

            //Once we have the point list, we need to find the minimum and maximum values so we can add the relaxation scale.
            for (int i = 0; i < myPoints.Count; i++)
            {
                double ev = EstimateFor(myPoints[i]);
                densityVector.Add(ev);
                if (ev > max) { max = ev; }
                if (ev < min) { min = ev; }
            }
        }

        public double RelaxedEstimateFor(double x)
        {
            double result = 0;

            for (int i = 0; i < myPoints.Count; i++)
            {
                double density = GaussianKernel((x - myPoints[i]) / h);
                double correctedH = CorrectedH(density);
                
                //Now we need to correct for H, depending on the height
                result += GaussianKernel((x - myPoints[i]) / correctedH );
            }

            //double tmpResult = result * (1 / ((double)myPoints.Count * h));

            result *= 1 / ((double)myPoints.Count * h );

            return (result);
        }

        public double EstimateFor(double x)
        {
            double result = 0;

            for (int i = 0; i < myPoints.Count; i++)
            {
                result += GaussianKernel((x - myPoints[i]) / h);
            }

            
            return (result);
        }

        private double CorrectedH(double density)
        {
            double slope = (h * (rp - 1)) / (max - min); 
            return slope * density + h ;
        }



        private double GaussianKernel(double x)
        {
            double result = 1 / Math.Sqrt(2 * Math.PI);
            result *= Math.Exp(-0.5 * Math.Pow(x, 2));
            return (result);
        }

    }


    public class KernelDensityEstimator
    {
        double gaussianNumerator = 1 / Math.Sqrt(2 * Math.PI);
        List<double> myPoints;
        double h;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="h">Gaussian Variance</param>
        public KernelDensityEstimator(List<double> points, double h)
        {
            myPoints = points;
            myPoints.Sort();
            this.h = h;
        }

        public double EstimateFor (double x)
        {
            double result = 0;

            for (int i = 0; i < myPoints.Count; i++)
            {
                result += GaussianKernel( (x - myPoints[i]) / h );
            }

            result *= 1 / ( (double)myPoints.Count * h );

            return (result);
        }

        private double GaussianKernel(double x)
        {
            return gaussianNumerator * Math.Exp(-0.5 * Math.Pow(x,2));
        }

        /// <summary>
        /// We use the trapezoidal meethod for integration
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="xf"></param>
        /// <param name="step">trapezoidal step</param>
        /// <returns></returns>
        private double IntegrateFrom(double x0, double xf, double step)
        {
            double result = 0;
            double a = 0;
            double b = 0;

            for (double i = x0; i <= xf; i += step)
            {
                b = EstimateFor(i);
                result += (step / 2) * (a + b);
                a = b;

            }

            return (result);

        }

        public double EstimatePValueFor(double x, double h)
        {

            //Now we integrate
            double x0 = myPoints[0] - h * 5;
            double xf = 1;
            double step = h / 50;

            double integralForAll = IntegrateFrom(x0, xf, step);
            double integralForPoint = IntegrateFrom(x0, x, step);


            return integralForPoint / integralForAll;

        }
    }
}
