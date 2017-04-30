using System;
using System.Collections.Generic;
using System.Text;

[assembly: CLSCompliant(true)]
namespace PatternTools
{
    public class ACTest
    {
        double MAXIT = 500;
        double EPS = 3.0e-30;
        double FPMIN = 1.0e-30;

        public ACTest()
        {

        }

        double betacf(double a, double b, double x)
        {
            /*	void nrerror(char error_text[]); */
            int m, m2;
            double aa, c, d, del, h, qab, qam, qap;

            qab = a + b;
            qap = a + 1.0;
            qam = a - 1.0;
            c = 1.0;
            d = 1.0 - qab * x / qap;
            if (Math.Abs(d) < FPMIN) d = FPMIN;
            d = 1.0 / d;
            h = d;
            for (m = 1; m <= MAXIT; m++)
            {
                m2 = 2 * m;
                aa = m * (b - m) * x / ((qam + m2) * (a + m2));
                d = 1.0 + aa * d;
                if (Math.Abs(d) < FPMIN) d = FPMIN;
                c = 1.0 + aa / c;
                if (Math.Abs(c) < FPMIN) c = FPMIN;
                d = 1.0 / d;
                h *= d * c;
                aa = -(a + m) * (qab + m) * x / ((a + m2) * (qap + m2));
                d = 1.0 + aa * d;
                if (Math.Abs(d) < FPMIN) d = FPMIN;
                c = 1.0 + aa / c;
                if (Math.Abs(c) < FPMIN) c = FPMIN;
                d = 1.0 / d;
                del = d * c;
                h *= del;
                if (Math.Abs(del - 1.0) < EPS) break;
            }
            if (m > MAXIT)
            {
                Exception pop = new Exception("a or b too big, or MAXIT too small in betacf");
                throw (pop);
            }
            return h;
        }

        //----------------------

        static double gammln(double xx) {
	        double x,y,tmp,ser;
            double [] cof = {76.18009172947146,-86.50532032941677,24.01409824083091,-1.231739572450155,0.1208650973866179e-2,-0.5395239384953e-5};
            int j;
            y=x=xx;
            tmp=x+5.5;
            tmp -= (x+0.5)*Math.Log(tmp);
            ser=1.000000000190015;
            for (j=0;j<=5;j++){
                ser += cof[j]/++y;
            }
	        return -tmp+Math.Log(2.5066282746310005*ser/x);
        }

        //---------

        double betai(double a, double b, double x)
        {
            double bt;

            if (x < 0.0 || x > 1.0)
            {
                Exception pop = new Exception("Bad x in routine betai");
                throw (pop);
            }
            if (x == 0.0 || x == 1.0) bt = 0.0;
            else
                bt = Math.Exp(gammln(a + b) - gammln(a) - gammln(b) + a * Math.Log(x) + b * Math.Log(1.0 - x));
            if (x < (a + 1.0) / (a + b + 2.0))
                return bt * betacf(a, b, x) / a;
            else
                return 1.0 - bt * betacf(b, a, 1.0 - x) / b;
        }

        //------------------------------

        public void compute(double n1, double n2, double x, double y)
        {

            /* Both x and y are defined so we compute the significance window */
            double p = n1 / (n1 + n2);

            double thisproba = betai((x + 1), (y + 1), p);
            double thisproba2 = betai((y + 1), (x + 1), 1 - p);
            Console.WriteLine("P( y <= {0} | x = {1} ) = {2}", y.ToString(), x.ToString(), thisproba.ToString());
            Console.WriteLine("P( y >= {0} | x = {1} ) = {2}", y.ToString(), x.ToString(), thisproba2.ToString());

        }


    }

}
