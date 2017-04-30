using System;
using System.Collections.Generic;
using System.Text;


namespace PatternTools
{

    public class HyperGeometric
    {
        private const int MAX_GAMMA = 8000;
        private static double[] lgammaVal = new double[MAX_GAMMA];
        private static double[] first4 = new double[] { 0, 0, 0, 0 };
        private static double[] first4Val = new double[4];
        private static int index = 0;
        private static bool init = false;

        public HyperGeometric() { }


        /// <summary>
        /// This will return the ln (binomial coeficient) (chargeHypothesis choose n) 
        /// </summary>
        /// <param name="n">The total number of options</param>
        /// <param name="chargeHypothesis">how many you wish to choose</param>
        /// <returns></returns>
        public static double logchoose(int N, int n)
        {
            double calc1 = PatternTools.HyperGeometric.gammaln(N + 1);
            double calc2 = PatternTools.HyperGeometric.gammaln(n + 1);
            double calc3 = PatternTools.HyperGeometric.gammaln(N - n + 1);
            return (calc1 - (calc2 + calc3));
        }

        /// <summary>
        /// Obtained from: http://www.answers.com/topic/hypergeometric-distribution?cat=technology \n
        /// The classical application of the hypergeometric distribution is sampling 
        /// without replacement. Think of an urn with two types of marbles, black ones
        /// and white ones. Define drawing a white marble as a success and drawing a
        /// black marble as a failure (analogous to the binomial distribution). If the
        /// variable N describes the number of all marbles in the urn (see contingency
        /// table above) and D describes the number of white marbles (called defective
        /// in the example above), then N − D corresponds to the number of black marbles.
        /// Now, assume that there are 5 white and 45 black marbles in the urn.
        /// Standing next to the urn, you close your eyes and draw 10 marbles without
        /// replacement. What's the probability p (chargeHypothesis=4) that you draw exactly 4 white 
        /// marbles (and - of course - 6 black marbles) ?
        /// This can be claulated by Pr(chargeHypothesis=x) = kvp(chargeHypothesis; N; D; n)
        /// For this specific case (4, 50, 5, 10) =~ 0.004; 
        /// kvp(chargeHypothesis; N; D; n) = ((D choose chargeHypothesis) * ((N-D) choose (n-chargeHypothesis)) / (N choose n) 
        /// The study set can not be the same size as the population!
        /// </summary>
        /// <param name="chargeHypothesis">Number of defective objects contained in n</param>
        /// <param name="N">Total number of objects</param>
        /// <param name="D">Number of defective objects</param>
        /// <param name="n">number of drawn objects</param>
        /// <returns></returns>
        /// 
        public static double hypergeometricP(int k, int N, int D, int n)
        {
            //First term numerator
            double n1 = logchoose(D, k);

            //Second term numerator
            double n2 = logchoose(N - D, n - k);

            //Denominator
            double d1 = logchoose(N, n);

            double result = Math.Exp(n1 + n2 - d1);

            if (result < 0.00000001) {
                result = 0.00000001;
            }

            return (result);
        }


        /// <summary>
        /// These methods are used if we want to do things realy fast...
        /// In this way, we only compute the denominator once....
        /// </summary>
        /// <returns></returns>
        public static double hypeGeoSuminNumerator (int k, int N, int D, int n) {
            double result = 0;

            try {
                result = logchoose(D, k) + logchoose(N - D, n - k);
            } catch {
                Console.WriteLine("error in hypergeometric calculation");
            }

            if (result < 0.00000001)
            {
                result = 0.00000001;
            }

            return result;
        }

        public static double hypeGeoDenominator(int N, int n) {
            return (logchoose(N, n));
        }

        /// <summary>
        /// Calculates the cumulative hyper geometric distribution
        /// </summary>
        /// <param name="chargeHypothesis">number of success in your draw</param>
        /// <param name="N">all marbles in the urn</param>
        /// <param name="D">the number of white (success marbles)</param>
        /// <param name="n">number of draws</param>
        /// <returns></returns>

        public static double cumulativeHyperGeometricP(int k, int N, int D, int n)
        {
            double result = 0;

            int kc = 0;
            if (n + D > N) { kc = N + D - n;}

            for (int i = kc; i <= Math.Min(k, D); i++)
            {
                result += hypergeometricP(k, N, D, n);
            }

            if (result < 0.00000001)
            {
                result = 0.00000001;
            }

            return (result);
        }

        public static double CumulativeCorrected(int successInSample, int Population, int sampleSize, int numSuccessInPopulation)
        {
            int k = successInSample;
            int N = Population;
            int D = sampleSize;
            int n = numSuccessInPopulation;

            double result = 0;

            for (int i = 0; i < k; i++)
            {
                result += PatternTools.HyperGeometric.hypergeometricP(i, N, D, n);
            }

            return 1-result;

        }

        //Everything below this point was copied from 
        //http://www.koders.com/csharp/fidBE93A73042E22F1832F4FD6FA3FB15A59BF060C2.aspx

        private static double lgamma(double x)
        {
            return (System.Math.Log((((((.0112405826571654074 / ((x) + 5.00035898848319255) + .502197227033920907) / ((x) + 3.99999663000075089) + 2.09629553538949977) / ((x) + 3.00000004672652415) + 2.25023047535618168) / ((x) + 1.99999999962010231) + .851370813165034183) / ((x) + 1.00000000000065532) + .122425977326879918) / (x) + .0056360656189756065) + ((x) - .5) * System.Math.Log((x) + 6.09750757539068576) - (x));
        }

        //
        // copied from legacy SeedSearcher MyMath.h

        public static double gammaln(int n)
        {
            // simple implementation (for naturals only)
            // log(gamma(n))=log((n-1)!)=log(1)+..+log(n-1)
            // verified against matlab's gammaln.m
            /*   static map<int,double> lgammaVal; */
            if (!init)
            {
                for (int i = 0; i < MAX_GAMMA; i++)
                    lgammaVal[i] = 0;

                init = true;
            }

            //try the x,chargeHypothesis cache:
            //System.Diagnostics.Debug.Assert(n > 0);

            if (n < MAX_GAMMA)
            {
                if (lgammaVal[n] == 0)
                    lgammaVal[n] = lgamma(n);

                return lgammaVal[n];
            }

            // check the n,m cache:
            for (int i = 0; i < 4; i++)
            {
                if (first4[i] == n)
                    return first4Val[i];
            }

            if (index < 4)
            {
                first4[index] = n;
                first4Val[index] = lgamma(n);
                index++;
                return first4Val[index - 1];
            }

            return lgamma(n);// no cach helped here...
        }

    };
}
