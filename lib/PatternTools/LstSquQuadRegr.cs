/********************************************************************************************
                                  Class LstSquQuadRegr
             A C#  Class for Least Squares Regression for Quadratic Curve Fitting
                                  Alex Etchells  2010    
 ********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PatternTools
{

    public class LstSquQuadRegr
    {
        /* instance variables */
        ArrayList pointArray = new ArrayList();
        private int numOfEntries;
        private decimal[] pointpair;

        /*constructor */
        public LstSquQuadRegr()
        {
            numOfEntries = 0;
            pointpair = new decimal[2];
        }

        /*instance methods */
        public void AddPoints(double x, double y)
        {
            pointpair = new decimal[2];
            numOfEntries += 1;
            pointpair[0] = Convert.ToDecimal(x);
            pointpair[1] = Convert.ToDecimal(y);
            pointArray.Add(pointpair);
        }

        private decimal deciATerm()
        {
            if (numOfEntries < 3)
            {
                throw new InvalidOperationException("Insufficient pairs of co-ordinates");
            }
            //notation sjk to mean the sum of x_i^j*y_i^k. 
            decimal s40 = getSxy(4, 0);
            decimal s30 = getSxy(3, 0);
            decimal s20 = getSxy(2, 0);
            decimal s10 = getSxy(1, 0);
            decimal s00 = numOfEntries;

            decimal s21 = getSxy(2, 1);
            decimal s11 = getSxy(1, 1);
            decimal s01 = getSxy(0, 1);

            //   Da / D
            return (s21 * (s20 * s00 - s10 * s10) - s11 * (s30 * s00 - s10 * s20) + s01 * (s30 * s10 - s20 * s20))
                    /
                    (s40 * (s20 * s00 - s10 * s10) - s30 * (s30 * s00 - s10 * s20) + s20 * (s30 * s10 - s20 * s20));
        }

        public double aTerm()
        {
            return Convert.ToDouble(deciATerm());
        }

        private decimal deciBTerm()
        {
            if (numOfEntries < 3)
            {
                throw new InvalidOperationException("Insufficient pairs of co-ordinates");
            }
            //notation sjk to mean the sum of x_i^j*y_i^k. 
            decimal s40 = getSxy(4, 0);
            decimal s30 = getSxy(3, 0);
            decimal s20 = getSxy(2, 0);
            decimal s10 = getSxy(1, 0);
            decimal s00 = numOfEntries;

            decimal s21 = getSxy(2, 1);
            decimal s11 = getSxy(1, 1);
            decimal s01 = getSxy(0, 1);

            //   Db / D
            return (s40 * (s11 * s00 - s01 * s10) - s30 * (s21 * s00 - s01 * s20) + s20 * (s21 * s10 - s11 * s20))
            /
            (s40 * (s20 * s00 - s10 * s10) - s30 * (s30 * s00 - s10 * s20) + s20 * (s30 * s10 - s20 * s20));
        }

        public double bTerm()
        {
            return Convert.ToDouble(deciBTerm());
        }

        private decimal deciCTerm()
        {
            if (numOfEntries < 3)
            {
                throw new InvalidOperationException("Insufficient pairs of co-ordinates");
            }
            //notation sjk to mean the sum of x_i^j*y_i^k.  
            decimal s40 = getSxy(4, 0);
            decimal s30 = getSxy(3, 0);
            decimal s20 = getSxy(2, 0);
            decimal s10 = getSxy(1, 0);
            decimal s00 = numOfEntries;

            decimal s21 = getSxy(2, 1);
            decimal s11 = getSxy(1, 1);
            decimal s01 = getSxy(0, 1);

            //   Dc / D
            return (s40 * (s20 * s01 - s10 * s11) - s30 * (s30 * s01 - s10 * s21) + s20 * (s30 * s11 - s20 * s21))
                    /
                    (s40 * (s20 * s00 - s10 * s10) - s30 * (s30 * s00 - s10 * s20) + s20 * (s30 * s10 - s20 * s20));
        }

        public double cTerm()
        {
            return Convert.ToDouble(deciCTerm());
        }

        public double rSquare() // get rsquare
        {
            if (numOfEntries < 3)
            {
                throw new InvalidOperationException("Insufficient pairs of co-ordinates");
            }
            return Convert.ToDouble(1 - getSSerr() / getSStot());
        }


        /*helper methods*/
        private decimal getSxy(int xPower, int yPower) // get sum of x^xPower * y^yPower
        {
            decimal Sxy = 0;
            foreach (decimal[] ppair in pointArray)
            {
                decimal xToPower = 1;
                for (int i = 0; i < xPower; i++)
                {
                    xToPower = xToPower * ppair[0];
                }

                decimal yToPower = 1;
                for (int i = 0; i < yPower; i++)
                {
                    yToPower = yToPower * ppair[1];
                }
                Sxy += xToPower * yToPower;
            }
            return Sxy;
        }


        private decimal getYMean()
        {
            decimal y_tot = 0;
            foreach (decimal[] ppair in pointArray)
            {
                y_tot += ppair[1];
            }
            return y_tot / numOfEntries;
        }

        private decimal getSStot()
        {
            decimal ss_tot = 0;
            foreach (decimal[] ppair in pointArray)
            {
                ss_tot += (ppair[1] - getYMean()) * (ppair[1] - getYMean());
            }
            return ss_tot;
        }

        private decimal getSSerr()
        {
            decimal ss_err = 0;
            foreach (decimal[] ppair in pointArray)
            {
                ss_err += (ppair[1] - getPredictedY(ppair[0])) * (ppair[1] - getPredictedY(ppair[0]));
            }
            return ss_err;
        }


        private decimal getPredictedY(decimal x)
        {
            return deciATerm() * x * x + deciBTerm() * x + deciCTerm();
        }
    }
}
