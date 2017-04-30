using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEProcessor.LockMass
{
    public class MLLMFileInterpolant
    {
        public string MyFile { get; set; }
        public alglib.idwinterpolant MyInterpolant { get; set; }
        public List<double> MyPPMs { get; set; }

        double lowerBound;
        double upperBound;

        public MLLMFileInterpolant(string fileName, alglib.idwinterpolant myInterpolant, List<double> myPPMs)
        {
            MyFile = fileName;
            MyInterpolant = myInterpolant;
            MyPPMs = myPPMs;

            double average = MyPPMs.Average();
            double std = PatternTools.pTools.Stdev(MyPPMs, false);

            lowerBound = average - 3 * std;
            upperBound = average + 3 * std;
        }

        internal double Interpolate(double thisMZ, double scanNumber, bool applyBoundConstraints)
        {
            double[] iv = new double[] {thisMZ, scanNumber };
            double ppm = alglib.idwcalc(MyInterpolant, iv);

            if (applyBoundConstraints)
            {
                if (ppm < lowerBound) { ppm = lowerBound; }
                if (ppm > upperBound) { ppm = upperBound; }
            }

            return ppm;
        }

    }
}
