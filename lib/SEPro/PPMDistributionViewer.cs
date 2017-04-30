using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting.Utilities;
using PatternTools.SQTParser;
using System.IO;

namespace SEProcessor
{
    public partial class PPMDistributionViewer : Form
    {
        List<SQTScan> theScans;

        public List<SQTScan> TheScans
        {
            get
            {
                return theScans;
            }

            set
            {
                theScans = value;

                List<string> theFiles = (from s in theScans
                                         orderby s.FileName
                                         select s.FileName).Distinct().ToList();

                if (theFiles.Count > 1)
                {
                    foreach (string file in theFiles)
                    {
                        comboBoxFilesToAnalyze.Items.Add(file);
                    }
                }
            }
        }

        public PPMDistributionViewer()
        {
            InitializeComponent();
        }

        public void Plot()
        {
            int noBins = (int)numericUpDownNoBins.Value;

            //Get the ppm distribution (non abs value) of all spectra identified
            List<double> realPPMs;
            chartHistogram.Series["Histogram"].Points.Clear();


            if (comboBoxFilesToAnalyze.SelectedItem.Equals("All files"))
            {
                realPPMs = (from scan in theScans
                            select scan.PPM_Orbitrap).ToList();
            }
            else
            {
                realPPMs = (from scan in theScans
                            where scan.FileName.Equals(comboBoxFilesToAnalyze.SelectedItem)
                            select scan.PPM_Orbitrap).ToList();

                if (realPPMs.Count == 0)
                {
                    MessageBox.Show("No scans found in this file");
                    return;
                }
            }

            List<PatternTools.HistogramHelper.HistogramBin> theBins;

            if (checkBoxUserSetBounds.Checked)
            {
                theBins = PatternTools.HistogramHelper.BinData(realPPMs, (double) numericUpDownBoundMin.Value, (double)numericUpDownBoundMax.Value, noBins, true);
            }
            else
            {
                theBins = PatternTools.HistogramHelper.BinData(realPPMs, noBins);
            }


            foreach (PatternTools.HistogramHelper.HistogramBin bin in theBins)
            {
                chartHistogram.Series["Histogram"].Points.AddXY(Math.Round(bin.IntervalFloor, 2), bin.TheData.Count); 
            }



            //First, lets find ppm bounds for outliers
            List<double> learningPPMs = PatternTools.ObjectCopier.Clone(realPPMs);
            for (int i = 0; i < 4; i++)
            {
                double ppmAverage = learningPPMs.Average();
                double ppmSTD = PatternTools.pTools.Stdev(learningPPMs, false);


                learningPPMs.RemoveAll(a => a > ppmAverage + (6 - i) * ppmSTD || a < ppmAverage - (6 - i) * ppmSTD);

            }

            double mean;
            double variance;
            double skewness;
            double kurtosis;

            alglib.samplemoments(learningPPMs.ToArray(), out mean, out variance, out skewness, out kurtosis);


            labelAveragePPM.Text = Math.Round(mean,2).ToString();
            labelStdDev.Text = Math.Round(Math.Sqrt(variance), 2).ToString();
            labelSkewness.Text = Math.Round(skewness, 2).ToString();
            labelKurtosis.Text = Math.Round(kurtosis, 2).ToString();


        }

        private void numericUpDownNoBins_ValueChanged(object sender, EventArgs e)
        {
            Plot();
        }

        private void saveDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = saveFileDialog1.Filter = "Text file (*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                StreamWriter sr = new StreamWriter(saveFileDialog1.FileName);

                sr.WriteLine("# Theoretical MH\t Measured MH\t PrecursorChargeState");

                foreach (SQTScan scan in TheScans)
                {
                    sr.WriteLine(scan.TheoreticalMH + "\t" + scan.MeasuredMH + "\t" + scan.ChargeState);
                }

                sr.Close();

                MessageBox.Show("File saved");
            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = saveFileDialog1.Filter = "PNG image (*.png)|*.png";

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                chartHistogram.SaveImage(fs, ChartImageFormat.Png);
                fs.Close();
                MessageBox.Show("File saved");
            }
        }

        private void comboBoxFilesToAnalyze_SelectedIndexChanged(object sender, EventArgs e)
        {
            Plot();
        }

        private void checkBoxUserSetBounds_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUserSetBounds.Checked)
            {
                numericUpDownBoundMax.Enabled = true;
                numericUpDownBoundMin.Enabled = true;
            }
            else
            {
                numericUpDownBoundMax.Enabled = false;
                numericUpDownBoundMin.Enabled = false;
            }

            Plot();
        }

        private void numericUpDownBoundMin_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
