using SEPRPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProQ.IsobaricQuant.PairWiseComparison
{
    public class ProtQuant
    {
        public MyProtein Protein { get; set; }
        public List<PepQuant> ThePepQuants { get; set;  }
        
        public double GlobalFold { get; set; }
        public double Binomial { get; set; }
        public double Stauffers { get; set; }
        public double UncorrectedStauffer { get; set; }

        

        public ProtQuant(MyProtein theProtein, List<PepQuant> theQuants)
        {
            int successPos = 0;
            int successNeg = 0;
            Protein = theProtein;
            ThePepQuants = theQuants;

            List<double> globalFold = new List<double>();
            List<double> correctedTTests = new List<double>();
            List<double> uncorrectedTTests = new List<double>();
            List<double> w = new List<double>();

            foreach (PepQuant pq in theQuants)
            {
                if (pq.AVGLogFold > 0) { successPos++; } else { successNeg++; }
                globalFold.Add(pq.AVGLogFold);
                correctedTTests.Add(pq.CorrectedTTest);
                uncorrectedTTests.Add(pq.TTest);
                w.Add(1);
            }

            double bin = 0;

            if (successPos > successNeg)
            {
                bin = alglib.binomialcdistribution(successPos - 1, theQuants.Count, 0.5);
            }
            else if (successNeg > successPos)
            {
                bin = alglib.binomialcdistribution(successNeg - 1, theQuants.Count, 0.5);
            }
            else
            {
                bin = 0.5;
            }

            Binomial = bin;
            GlobalFold = globalFold.Average();
            Stauffers = PatternTools.pTools.StouffersMethod(correctedTTests);
            UncorrectedStauffer = PatternTools.pTools.StouffersMethod(uncorrectedTTests);
            
        }
    }
}
