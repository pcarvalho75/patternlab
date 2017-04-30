using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SEProQ
{
    public partial class MassDistributionViewer : Form
    {
        public MassDistributionViewer()
        {
            InitializeComponent();
        }

        public List<double>  MyMasses
        {
            set
            {

                StringBuilder sb = new StringBuilder();
                foreach (double m in value)
                {
                    sb.AppendLine(m.ToString());
                }

                richTextBoxData.AppendText(sb.ToString());
 
            }
        }

        private void buttonUseBins_Click(object sender, EventArgs e)
        {
            int noBins = (int)numericUpDownNoBins.Value;
            List<string> theData = StartGraph();

            Dictionary<int, List<KeyValuePair<double, double>>> theDictBins = new Dictionary<int, List<KeyValuePair<double, double>>>();

            foreach (string line in theData)
            {
                List<string> pts = Regex.Split(line, "\t").ToList();

                if (pts.Last().Length == 0) { pts.RemoveAt(pts.Count -1); }

                double div = (double)pts.Count % 2;

                if (div != 0)
                {
                    MessageBox.Show("This mode requires information to be paired. i.e., bin and no entries");
                    return;
                }

                Console.WriteLine(line);

                pts.RemoveAll(a => a.Length == 0);
                List<double> ptsD = pts.Select(a => double.Parse(a)).ToList();
                
                for (int i = 0; i <= (int)ptsD.Count / 2; i = i + 2)
                {
                    double bin = ptsD[i];
                    double value = ptsD[i + 1];

                    KeyValuePair<double, double> kvp = new KeyValuePair<double, double>(bin, value);
                    if (theDictBins.ContainsKey(i))
                    {
                        theDictBins[i].Add(kvp);
                    }
                    else
                    {
                        theDictBins.Add(i, new List<KeyValuePair<double, double>>() { kvp });
                    }
                }
                
            }

            //Now we join the bins in each key to satisfy the required amount of bins.
            //Find the step
            List<int> keys = theDictBins.Keys.ToList();
            Dictionary<double, List<double>> prePlotData = new Dictionary<double, List<double>>();

            double step = ((double)numericUpDownMaxValue.Value - (double)numericUpDownMinValue.Value) / (double)numericUpDownNoBins.Value;
            for (double b = (double)numericUpDownMinValue.Value; b <= (double)numericUpDownMaxValue.Value; b += step)
            {
                List<double> qs = new List<double>();
                foreach (int key in keys)
                {
                    List<KeyValuePair<double, double>> theList = theDictBins[key].FindAll(a => a.Key >= b && a.Key < b + step);
                    double sumXIC = theList.Sum(a => a.Value);
                    qs.Add(sumXIC);
                }

                prePlotData.Add(b, qs);

            }

            PlotThis(prePlotData);

            
            Console.WriteLine("Done");
        }


        private void buttonPlot_Click(object sender, EventArgs e)
        {

            List<string> theData = StartGraph();

            Dictionary<int, List<double>> masses = new Dictionary<int,List<double>>();

            int noBins = (int)numericUpDownNoBins.Value;

            foreach (string s in theData)
            {
                List<string> pts = Regex.Split(s, "\t").ToList();
                pts.RemoveAll(a => a.Equals(""));
                
                for (int i = 0; i <pts.Count; i++) 
                {
                    if (masses.ContainsKey(i))
                    {
                        try
                        {
                            masses[i].Add(double.Parse(pts[i]));
                        }
                        catch
                        {
                            Console.Write(".");
                        }
                    } else 
                    {
                        try
                        {
                            masses.Add(i, new List<double>() { double.Parse(pts[i]) });
                        }
                        catch
                        {
                            Console.Write(".");
                        }
                    }
                }
            }

            if (checkBoxLogLFQ.Checked)
            {
                foreach (KeyValuePair<int, List<double>> kvp in masses) 
                {
                    masses[kvp.Key] = kvp.Value.Select(a => Math.Log(a)).ToList();
                }
            }

            List<int> theKeys = masses.Keys.ToList();

            foreach (int k in theKeys)
            {
                masses[k] = masses[k].FindAll(a => a > (double)numericUpDownMinValue.Value && a < (double)numericUpDownMaxValue.Value);
            }

            Dictionary<int, List<PatternTools.HistogramHelper.HistogramBin>> theBinsDict = new Dictionary<int, List<PatternTools.HistogramHelper.HistogramBin>>();

            foreach (KeyValuePair<int, List<double>> kvp in masses)
            {
                theBinsDict.Add(kvp.Key, PatternTools.HistogramHelper.BinData(kvp.Value, (double)numericUpDownMinValue.Value, (double)numericUpDownMaxValue.Value, (int)numericUpDownNoBins.Value, true));
            }

            Dictionary<double, List<double>> theBins = new Dictionary<double, List<double>>();

            foreach (KeyValuePair<int, List<PatternTools.HistogramHelper.HistogramBin>> kvp in theBinsDict)
            {
                foreach (PatternTools.HistogramHelper.HistogramBin b in kvp.Value)
                {
                    double k = Math.Round(b.IntervalFloor, 2);
                    if (theBins.ContainsKey(k)) 
                    {
                        theBins[k].Add(b.TheData.Count);
                    } 
                    else 
                    {
                        theBins.Add(k, new List<double> () {b.TheData.Count});
                    }
                }
            }

            PlotThis(theBins);
        }

        private void PlotThis(Dictionary<double, List<double>> theBins)
        {
            foreach (KeyValuePair<double, List<double>> kvp in theBins)
            {
                //Columns
                double x = kvp.Key;
                double y = kvp.Value.Average();
                chart1.Series[0].Points.AddXY(x, y);

                //Error bar
                double y2 = PatternTools.pTools.Stdev(kvp.Value, true);
                chart1.Series[1].Points.AddXY(x, y, y, y + y2);
            }


            if (numericUpDownYMax.Value > 0)
            {
                chart1.ChartAreas[0].AxisY.Maximum = (double)numericUpDownYMax.Value;
                //Set the yMax
            }

            chart1.ChartAreas[0].Axes[0].Minimum = (double)numericUpDownMinValue.Value;
            chart1.ChartAreas[0].Axes[0].Maximum = (double)numericUpDownMaxValue.Value;
        }

        private List<string> StartGraph()
        {
            chart1.Series[0].Points.Clear(); //The columns
            chart1.Series[1].Points.Clear(); //The error bars


            List<string> theData = Regex.Split(richTextBoxData.Text, "\n").ToList();
            theData.RemoveAll(a => a.Equals(""));

            return theData;
        }


        private void saveGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG graph | *.png";

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show("Image saved");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBoxData.Clear();
        }


    }
}
