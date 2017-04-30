using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Anova
{
    
    public class AnovaItem
    {
        public string Locus { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
        public Dictionary<int, List<double>> ClassValues { get; set; }
        public Chart MyChart { get; set; }
        public AnovaResult MyAnovaResult { get; set; }

        public bool StatisticallySignificant(double cuttoff)
        {
            if (PValue < cuttoff)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public double MaxSignal
        {
            get
            {
                double max = double.MinValue;
                foreach (var kvp in ClassValues) {
                    double m = kvp.Value.Max();
                    if (m > max) {
                        max = m;
                    }
                }
                return max;
            }
        }

        public double PValue
        {
            get
            {
                return (1 - alglib.fdistr.fdistribution((int)MyAnovaResult.DegreeOfFreedomBetweenGroups, (int)MyAnovaResult.DegreeOfFreedomWithinGroups, MyAnovaResult.FRatio));
            }
        }


        public AnovaItem(string locus,
            int id,
            string description,
            Dictionary<int, List<double>> classValues, Dictionary<int, string> classLabelDictionary,
            double probability           
            )
        
        {
            Locus = locus;
            ID = id;
            Description = description;
            ClassValues = classValues;

            MyChart = new Chart();

            foreach (KeyValuePair<int, List<double>> kvp in classValues)
            {
                MyChart.Series.Add(classLabelDictionary[kvp.Key]);
                for(int i = 0; i < kvp.Value.Count; i++) {
                    MyChart.Series.Last().Points.AddXY(i, kvp.Value[i]);
                }
            }

            MyAnovaResult = Anova(probability);

        }

        public AnovaResult Anova(double probability)
        {
            if (probability > 1 || probability < 0)
            {
                throw new Exception("Probability must be between 1 and 0");
            }

            //Verify if all groups have the same number of datapoints
            List<int> noValues = ClassValues.Select(a => a.Value.Count).Distinct().ToList();

            if (noValues.Count != 1)
            {
                Console.WriteLine(Locus + " does not present quantitation values for all replicates");
                throw new Exception("Number of values in all classes must be the same for anova test");
                
            }

            string allGroups = string.Join(",", MyChart.Series.Select(a => a.Name).ToList());


            AnovaResult result = MyChart.DataManipulator.Statistics.Anova(probability, allGroups);

            return result;
        }
    }
}
