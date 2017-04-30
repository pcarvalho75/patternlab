using PatternTools.SQTParser;
using SEPRPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEProQ.ITRAQ
{
    class ProtQuant
    {
        public MyProtein Protein { get; set; }
        public double Fold { get; set; }
        public double P { get; set; }

        public double AvgSinal
        {
            get
            {
                List<double> signal = (from scn in Protein.Scans
                                       from q in scn.Quantitation[0]
                                       where q < 1 && q > 0
                                       select q).ToList();

                double avgSignal = double.NegativeInfinity;
                if (signal.Count > 0)
                {
                    avgSignal = signal.Average();
                }

                return avgSignal;
            }
        }

        public ProtQuant(MyProtein myProtein, double p)
        {
            Protein = myProtein;
            P = p;

            //Calculate the fold change
            List<double> class1 = new List<double>();
            List<double> class2 = new List<double>();
            foreach (SQTScan sqt in myProtein.Scans)
            {
                class1.Add(sqt.Quantitation[0].GetRange(0, 3).Average());
                class2.Add(sqt.Quantitation[0].GetRange(3, 3).Average());
            }

            Fold = class1.Average() / class2.Average();

        }
    }
}
