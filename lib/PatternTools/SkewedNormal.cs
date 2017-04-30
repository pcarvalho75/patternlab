using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternTools
{
    public static class SkewedNormal
    {

        public static double X(double x, double stdev, double alpha, double location, double scale)
        {
            double xTransformed = (x - location);

            double part1 = (2 / (Math.PI));
            //double part2 = alglib.normaldistribution(xTransformed);
            double part2 = StandardNormal(xTransformed, stdev);
            double part3 = alglib.errorfunctionc(alpha * xTransformed);

            alglib.normaldistr.invnormaldistribution(3);

            double result = part1 * part2 * part3 * scale;


            return result;

        }


        public static double StandardNormal(double x, double stdev)
        {
            double a = 1 / (stdev * Math.Sqrt(2 * Math.PI));
            return a * Math.Exp((-1) * (Math.Pow(x, 2) / (2 * Math.Pow(stdev, 2))));

        }

        /// <summary>
        /// Returns the Alpha, location, scale, and stdev parameters in an array
        /// </summary>
        /// <param name="myPoints"></param>
        /// <returns></returns>
        public static List<double> FindParameters(List<Point> myPoints)
        {

            //Find out if the number of points is odd or even.  If it is odd, lets consider the highest point to meet the
            //max of the skewed normal, if it is even, lets consider the middle of the highest and the second highest adjacent to the highest

            bool isEven = true;
            long remainder;
            Math.DivRem((long)myPoints.Count, 2, out remainder);

            if (remainder > 0)
            {
                isEven = false;
            }

            //----------------------



            double alpha = 1;
            double location = 1;
            double scale = 1;
            double stdev = 1;

            double bestRMS = double.MaxValue;

            //First lets fit the normal to the point of maximum and then adjust the scale parameter
            double maxYFromMyPoints = myPoints.Max(a => a.Y);

            Point pointMax;

            if (!isEven)
            {

                Point pointMaxY = myPoints.Find(a => a.Y == maxYFromMyPoints);
                pointMax = pointMaxY;
            }
            else
            {
                Point pointMaxY = myPoints.Find(a => a.Y == maxYFromMyPoints);
                int index = myPoints.IndexOf(pointMaxY);

                Point pointSecondMaxY;

                //now we find which neighbor is higher;
                double valueRight = myPoints[index + 1].Y;
                double valueLeft = myPoints[index - 1].Y;

                double newX;

                if (valueRight > valueLeft)
                {
                    pointSecondMaxY = myPoints[index + 1];
                    newX = pointMaxY.X + Math.Abs(((pointSecondMaxY.X - pointMaxY.X) / 2));
                    
                }
                else
                {
                    pointSecondMaxY = myPoints[index - 1];
                    newX = pointMaxY.X - Math.Abs(((pointSecondMaxY.X - pointMaxY.X) / 2));
                }

                //Lets now create an artifitial point
                double newY = pointMaxY.Y + (Math.Abs(pointSecondMaxY.Y - pointMaxY.Y) / 3);
                pointMax = new Point(newX, newY);
            }


            double startLocation = pointMax.Y;

            double locationMin = myPoints.Min(a => a.X);
            double locationMax = myPoints.Max(a => a.X);
            double locationStep = (locationMax - locationMin) / 25;

            double scaleMin = myPoints.Min(a => a.Y);
            double scaleMax = pointMax.Y;
            double scaleStep = (scaleMax * 2 - scaleMin) / 40;

            //find the scale to match the maxpointY with


            //alpha
            for (double a = 0; a > -20; a -= 0.1)
            {
                Console.Write(".");

                for (double sdev = 0.01; sdev < 5; sdev += 0.05)
                {

                    //Find Scale to match maxpoint Y
                    double thisScale = double.MinValue;
                    double scaleDelta = double.MaxValue;
                    for (double s = scaleMin; s < scaleMax * 2; s += scaleStep)
                    {
                        double y = X(pointMax.X, sdev, a, pointMax.X, s);
                        double dif = Math.Abs(y - pointMax.Y);

                        if (dif < scaleDelta)
                        {
                            scaleDelta = dif;
                            thisScale = s;
                        }
                    }
                        //stdev

                    double rms = CalculateRMS(myPoints, sdev, a, pointMax.X, thisScale);

                    if (rms < bestRMS)
                    {
                        bestRMS = rms;
                        stdev = sdev;
                        alpha = a;
                        location = pointMax.X;
                        scale = thisScale;

                        Console.WriteLine("Alpha: {0}, Location:{1}, Scale:{2}, Stdev: {3}, RMS:{4}", a, pointMax.X, thisScale, stdev, rms);
                    }
                }

            }

            return new List<double>() { stdev, alpha, location, scale };


        }

        /// <summary>
        /// This method will value a little bit more the extreme points
        /// </summary>
        /// <param name="myPoints"></param>
        /// <param name="stdev"></param>
        /// <param name="alpha"></param>
        /// <param name="location"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static double CalculateRMS(List<Point> myPoints, double stdev, double alpha, double location, double scale)
        {
            double rms = 0;
            for (int i = 0; i < myPoints.Count; i++)
            {
                double original = X(myPoints[i].X, stdev, alpha, location, scale);

                double extremeCorrectionFactor = 1;
                if (i == 0 || i == myPoints.Count-1) {
                    extremeCorrectionFactor = 1.2;
                }

                rms += Math.Pow(myPoints[i].Y - original, 2) * extremeCorrectionFactor;
            }

            return Math.Sqrt(rms);
        }
    }
}
