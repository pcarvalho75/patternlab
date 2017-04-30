using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools
{
    [Serializable]
    public static class HistogramHelper
    {

        public static List<HistogramBin> BinData(List<double> theData, double minBin, double maxBin, int noBins, bool discardValuesBeyondMinMax)
        {

            if (discardValuesBeyondMinMax)
            {
                int itemsRemoved = theData.RemoveAll(a => a < minBin || a > maxBin);
                //Console.WriteLine(itemsRemoved + " items removed when generating histogram; out of bounds");
            }

            List<HistogramBin> histogramBins = new List<HistogramBin>(noBins);
            theData.Sort();

            double interval = (maxBin - minBin) / noBins;
            double intervalFloor = minBin;


            //Create all bins
            for (double i = 0; i <= noBins; i++)
            {
                histogramBins.Add(new HistogramBin(minBin + i * interval));
            }

            //Fill the bins
            foreach (double d in theData)
            {
                HistogramBin b = histogramBins.Find(a => a.IntervalFloor > d && d < a.IntervalFloor + interval);

                try
                {
                    b.TheData.Add(d);
                }
                catch
                {
                    Console.WriteLine("Null Bin");
                }

            }

            Normalize(histogramBins);


            return histogramBins;
        }

        private static void Normalize(List<HistogramBin> histogramBins)
        {
            double total = histogramBins.Sum(a => a.TheData.Count);

            foreach (HistogramBin b in histogramBins)
            {
                b.NormalizedValue = (double)b.TheData.Count / total;
            }
        }

        public static List<HistogramBin> BinData(List<int> theData)
        {

            if (theData.Count == 0)
            {
                throw new Exception("No datapoint provided for binning");
            }

            List<HistogramBin> histogramBins = new List<HistogramBin>();

            var groups = from integer in theData
                         group integer by integer into theBin
                         select new { theBin.Key, theBin };

            foreach (var group in groups)
            {
                HistogramBin h = new HistogramBin((double)group.Key);
                h.TheData = group.theBin.Select(a => (double)a).ToList();
                histogramBins.Add(h);
            }

            return histogramBins;


        }

        public static List<HistogramBin> BinData (List<double> theData, int noBins) {

            if (theData.Count == 0)
            {
                throw new Exception("No datapoint provided for binning");
            }

            List<HistogramBin> histogramBins = new List<HistogramBin>(noBins);

            double min = theData.Min();
            double max = theData.Max();
            double delta = max - min;
            double step = delta / noBins;
            theData.Sort();

            double intervalFloor = double.MinValue;

            HistogramBin hb = new HistogramBin(0);

            //Fill the bins
            intervalFloor = min;
            foreach (double datapoint in theData)
            {
                if (datapoint > (intervalFloor + step))
                {
                    hb = new HistogramBin(datapoint);
                    hb.TheData.Add(datapoint);
                    histogramBins.Add(hb);
                    intervalFloor += step;
                }
                else
                {
                    hb.TheData.Add(datapoint);
                }
            }

            Normalize(histogramBins);

            return histogramBins;

        }


        [Serializable]
        public class HistogramBin
        {
            public double IntervalFloor { get; set; }
            public List<double> TheData { get; set; }
            public double NormalizedValue { get; set; }

            public HistogramBin(double intervalFloor)
            {
                IntervalFloor = intervalFloor;
                TheData = new List<double>();
            }

        }
    }
}
