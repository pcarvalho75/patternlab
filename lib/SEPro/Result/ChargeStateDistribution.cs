using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PatternTools.SQTParser;

namespace SEProcessor.Result
{
    public partial class ChargeStateDistribution : Form
    {
        public List<SQTScan> TheScans { get; set; }

        public ChargeStateDistribution()
        {
            InitializeComponent();
        }

        public void Plot()
        {
            chartChargeState.Series["Charges"].Points.Clear();

            List<int> theCharges = TheScans.Select(a => a.ChargeState).ToList();
            int noBins = theCharges.Distinct().Count();


            List<PatternTools.HistogramHelper.HistogramBin> theBins = PatternTools.HistogramHelper.BinData(theCharges);

            theBins.Sort((a, b) => a.IntervalFloor.CompareTo(b.IntervalFloor));


            foreach (PatternTools.HistogramHelper.HistogramBin bin in theBins)
            {
                chartChargeState.Series["Charges"].Points.AddXY(Math.Round(bin.IntervalFloor, 0), bin.TheData.Count);
            }
        }
    }
}
