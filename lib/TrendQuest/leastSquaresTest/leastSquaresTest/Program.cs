using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leastSquaresTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //patternTools;
            List<double> x1 = new List<double>{1,2.3,3.1, 4.8, 5.6, 6.3};
            List<double> x2 = new List<double>{2.6, 2.8, 3.1, 4.7, 5.1, 5.3};
            List<int> dims = new List<int> { 1, 2, 3, 4, 5, 6 };

            double yIntercept = 0;
            double slope = 0;

            //patternTools.LeastSquares.Buildlinearleastsquares(x, y, ref yIntercept, ref slope);
            patternTools.pTools.LinearRegression2D(x1, x2);

            double spectralAngle = patternTools.pTools.SpectralAngle(new patternTools.sparseMatrixRow(0, dims, x1), new patternTools.sparseMatrixRow(0, dims, x2));

            Console.WriteLine("y = {0}x + {1}", slope, yIntercept);
            Console.WriteLine("Spectral angle = {0}", spectralAngle);

            Console.ReadKey();
        }
    }
}
