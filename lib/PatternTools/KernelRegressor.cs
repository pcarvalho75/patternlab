using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

namespace PatternTools
{
  
    public class KernelRegressor
    {
        //Global variables
        List<Point> dataPoints;
        double band;

        public List<Point> DataPoints
        {
            get { return dataPoints; }
            set { dataPoints = value; }
        }


        //Constructors

        public KernelRegressor()
        {
        }

        public KernelRegressor(List<Point> l) {
            this.dataPoints = l;
        }

        public KernelRegressor(List<Point> l, double band)
        {
            this.dataPoints = l;
            this.band = band;
        }

        
        public void saveToFile(string file)
        {
            //Serialize it!
            System.IO.StreamWriter sw = new System.IO.StreamWriter(file);

            foreach (Point p in dataPoints)
            {
                sw.WriteLine(p.X + "\t" + p.Y + "\t" + p.Weight);
            }
            sw.Close();
        }

        //----------------------------------------------
        public void ResetPointWheights()
        {
            foreach (Point p in dataPoints)
            {
                p.Weight = 1;
            }
        }

        //---------------------------------------

        public void GradientDescentWeightOptimizationEvenOdd(KernelType k, double noIterations, double paramA, double paramB)
        {
            //Lets do a gradient descent

            //Find the derivative
            double eps = 1;

            bool even = true;
            int evenupperbound = dataPoints.Count;
            int oddsupperbound = dataPoints.Count;

            //Better define the upperbounds
            int remainder = 0;
            Math.DivRem(dataPoints.Count, 2, out remainder);

            if (remainder == 0)
            {
                oddsupperbound--;
            }


            double err1 = 0;
            double err2 = 0;

            for (int iteration = 0; iteration < 2 * noIterations; iteration++)
            {
                //System.Threading.Parallel.ForEach(dataPoints, p =>
                foreach (Point p in dataPoints)
                {

                    if (even)
                    {
                        even = false;
                        //Process only the even
                        for (int e = 0; e < evenupperbound+1; e += 2)
                        {
                            if (e == evenupperbound) { e--; }  // We need this or wlse we will miss the last point
                            //Find the derivative of the sqrErr
                            err1 = GetSqrErr(k, dataPoints[e], paramA, paramB);
                            dataPoints[e].Weight += 0.0001;
                            err2 = GetSqrErr(k, dataPoints[e], paramA, paramB);
                            double derivative = (err1 - err2) / 0.0001;
                            dataPoints[e].Weight += eps * derivative;
                        }

                    }
                    else
                    {
                        even = true;
                        //process only the odd
                        for (int o = 1; o < oddsupperbound; o += 2)
                        {
                            //Find the derivative of the sqrErr
                            err1 = GetSqrErr(k, dataPoints[o], paramA, paramB);
                            dataPoints[o].Weight += 0.0001;
                            err2 = GetSqrErr(k, dataPoints[o], paramA, paramB);
                            double derivative = (err1 - err2) / 0.0001;
                            dataPoints[o].Weight += eps * derivative;
                        }

                    }

                }
                //);

            }

        }


        public double EstimateMeanSquaredError(KernelType k, double paramA, double paramB)
        {
            double squaredError = 0;

            foreach (Point p in dataPoints)
            {
                squaredError += GetSqrErr(k, p, paramA, paramB);
            }

            return (squaredError/dataPoints.Count);

        }


        public double GetSqrErr(KernelType k, Point p, double paramA, double paramB)
        {
            return (Math.Pow(p.Y - KernelCompute(k, p.X, paramA, paramB), 2));
        }
 

        //---------------------------------------


        public void GradientDescentWeightOptimization(KernelType k, double noIterations, double paramA, double paramB)
        {
            //Lets do a gradient descent

            //Find the derivative
            double eps = 0.1;
            double SumSqrrdError;
            ResetPointWheights();
            

            //The direct approach, good for debug, and aquiring ideas!
            for (int iteration = 0; iteration < noIterations; iteration++) {
                SumSqrrdError = 0;
                
                foreach (Point p in dataPoints)
                {

                    //Find the derivative of the sqrErr
                    double err1 = GetSqrErr(k, p, paramA, paramB);
                    p.Weight += 0.001;
                    double err2 = GetSqrErr(k, p, paramA, paramB);
                    SumSqrrdError += err2;
                    double derivative = (err1-err2)/0.001;
                    double step = (eps * derivative) - 0.001;

                    //if (iteration > 0) {step /= iteration;}

                    p.Weight += step;
                }

            }


        }

        //--------------------------------------------------------------

        public double GradientDescentWeightOptimizationForParamA(KernelType k, int iterationsForParamA, int intermediaryKernelIterations, double pA, double pB)
        {
            //Times to run optimization for param A
            //Times to run optimization for the kernel during each intermediary run

            //The algorithm can become very crazy if it takes big steps, so lets do some clipping
            double maxStep = 0.5;
            double minStep = 0.1;

            List<double> errorList = new List<double>();
            List<double> paramAList = new List<double>();
            double derivative;
            double eps = 0.1;

            paramAList.Add(pA);
            ResetPointWheights();
            GradientDescentWeightOptimizationEvenOdd(k, intermediaryKernelIterations, pA, pB);
            errorList.Add(EstimateMeanSquaredError(k, pA, pB));

            pA += 0.1;
            paramAList.Add(pA);
            ResetPointWheights();
            GradientDescentWeightOptimizationEvenOdd(k, intermediaryKernelIterations, paramAList[paramAList.Count -1], pB);
            errorList.Add(EstimateMeanSquaredError(k, pA, pB));

            for (int i = 0; i <= iterationsForParamA; i++)
            {
                derivative = (errorList[errorList.Count - 1] - errorList[errorList.Count - 2]) / (paramAList[paramAList.Count - 1] - paramAList[paramAList.Count - 2]);
                double step = eps * derivative;

                //Check for clippings to keep us safe
                if (Math.Abs(step) > maxStep)
                {
                    if (step > 0)
                    {
                        step = maxStep;
                    }
                    else
                    {
                        step = -1 * maxStep;
                    }
                }
                else if (Math.Abs(step) < minStep)
                {
                    if (step > 0)
                    {
                        step = minStep;
                    }
                    else
                    {
                        step = -1 * minStep;
                    }
                }

                pA -= step;

                paramAList.Add(pA);
                ResetPointWheights();
                GradientDescentWeightOptimizationEvenOdd(k, intermediaryKernelIterations, paramAList[paramAList.Count - 1], pB);
                errorList.Add(EstimateMeanSquaredError(k, pA, pB));

            }

            return paramAList[paramAList.Count - 1];

        }

        //--------------------------------------------------------------

        /// <summary>
        /// Uses the gaussian kernel and the instantiated band
        /// </summary>
        /// <param name="k"></param>
        /// <param name="x"></param>
        /// <returns></returns>
       public double KernelCompute(double x) {
           return (KernelCompute (KernelType.Gaussian, x, band, 0));
       }


        /// <summary>
        /// Returns the estimated y value for the kernel regression
        /// </summary>
        /// <param name="chargeHypothesis">the kernel type</param>
        /// <param name="x">the value for which the y estimate should be computed</param>
        /// <param name="ParamA">any special kernel parameter, such as alfa for the gaussian kernel</param>
        /// <returns></returns>
        public double KernelCompute(KernelType k, double x, double ParamA, double paramB)
        {

            double wSum = 0;
            double kSum = 0;


            if (k.Equals(KernelType.Gaussian))
            {
                double speedParam = 2 * Math.Pow(ParamA, 2) * (-1);
                foreach (Point p in dataPoints)
                {
                    double r = Math.Pow(Math.E, (Math.Pow(x - p.X, 2) / (speedParam)));
                    wSum += r * p.Weight;
                    kSum += r;
                }
            } else {
                //We are using the Norm kernel

                foreach (Point p in dataPoints)
                {
                    double r = -1 * (Math.Abs(x - p.X) / ParamA) + paramB;
                    wSum += r * p.Weight;
                    kSum += r;
                }
            }
            


            double result = wSum/kSum;
            return (result);
        }



        //------------------------------------------------------------------------
        //My Structures
        [Serializable]
        public class Point
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Weight { get; set; }

            //Used for generalPurpose, here, for the Newton Rapson method
            public double TempVar { get; set; }
            

            public Point(double x, double y)
            {
                this.Weight = 1;
                this.X = x;
                this.Y = y;
             }

            public Point(double x, double y, double wheight)
            {
                this.Weight = wheight;
                this.X = x;
                this.Y = y;
            }
        }

        public enum KernelType
        {
            Gaussian,
            Norm,
            AlgLibSpline,
            LinearSpline
        }
    }
}
