using Accord.MachineLearning;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Distributions.Univariate;
using PatternTools.SQTParser;
using PatternTools.SQTParser2;
using SEPRPackage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XDScore
{
    public partial class XDScoreControl : UserControl
    {
        
        XDScore xdScore;
        double minX = -7;

        public XDScoreControl()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "SEQUEST files (*.sqt)|*.sqt";
            openFileDialog1.Multiselect = true;

            List<string> fileNames = new List<string>();

            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                foreach (string fName in openFileDialog1.FileNames)
                {
                    fileNames.Add(fName);
                }
            }
            else
            {
                return;
            }


            buttonGo.Text = "Parsing";
            this.Update();
            
            xdScore = new XDScore(fileNames, checkBoxLog.Checked);
            groupBoxCalculateDeltaScores.Enabled = true;

            buttonGo.Text = "Load";
        }

        private void PrepareHistogram()
        {
            //Reset the charts
            chartHistogram.Series[0].Points.Clear();
            chartHistogram.Series[1].Points.Clear();
            chartHistogram.Series[2].Points.Clear();
            List<PatternTools.HistogramHelper.HistogramBin> myBins = PatternTools.HistogramHelper.BinData(xdScore.DeltaScores, (int) numericUpDownHistogramBins.Value);

            chartProbability.Series[0].Points.Clear();
            chartProbability.Series[1].Points.Clear();
            chartProbability.Series[2].Points.Clear();
            chartProbability.Series[3].Points.Clear();

            //Get delta prime for the probability plot
            double deltaPrime = (double)numericUpDownXCorrPrime.Value;
            if (checkBoxLog.Checked)
            {
                deltaPrime = Math.Log(deltaPrime);
            }

            double xdPrime = (double)numericUpDownXCorrPrime.Value;
            if (checkBoxLog.Checked)
            {
                xdPrime = Math.Log(xdPrime);
            }


            double ceiling = -1;

            foreach (PatternTools.HistogramHelper.HistogramBin bin in myBins)
            {
                if (bin.TheData.Count > ceiling)
                {
                    ceiling = bin.TheData.Count;
                }

                if (Math.Round(bin.IntervalFloor, 2) < minX) { continue; }

                chartHistogram.Series[0].Points.AddXY(Math.Round(bin.IntervalFloor, 2), bin.TheData.Count);
            }

            ceiling *= 2;

            try
            {

                foreach (PatternTools.HistogramHelper.HistogramBin bin in myBins)
                //for (double x = -2.5; x < -2; x+=0.01 )
                {
                    double x = Math.Round(bin.IntervalFloor, 2);

                    chartHistogram.Series[1].Points.AddXY(x, xdScore.GaussianSmallMean.ProbabilityDensityFunction(x) * ceiling);
                    chartHistogram.Series[2].Points.AddXY(x, xdScore.GaussianBigMean.ProbabilityDensityFunction(x) * ceiling);

                    double bad = xdScore.GaussianSmallMean.ComplementaryDistributionFunction(new double[] { x });
                    chartProbability.Series[1].Points.AddXY(x, bad);

                    double good = xdScore.GaussianBigMean.DistributionFunction(new double[] { x });
                    chartProbability.Series[2].Points.AddXY(x, good);

                    chartProbability.Series[3].Points.AddXY(x, xdScore.ProbabilityCumulativeIntersect((double)numericUpDownXCorrPrime.Value, x));
                }


            } 
                catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }


        private void buttonGMM_Click(object sender, EventArgs e)
        {
            xdScore.GenerateGMM();
            PrepareHistogram();

        }



        private void numericUpDownHistogramBins_ValueChanged(object sender, EventArgs e)
        {
            PrepareHistogram();
        }

        private void buttonCalculateDeltaScores_Click(object sender, EventArgs e)
        {
            xdScore.CalculateDeltaScores((int)numericUpDownModelChargeState.Value, (int)numericUpDownNoPhosphoSites.Value, true);
            richTextBoxDeltaScores.Clear();
            foreach (double ds in xdScore.DeltaScores)
            {
                richTextBoxDeltaScores.AppendText(ds + "\n");
            }

            groupBoxDeltaScore.Text = "DeltaScores : " + xdScore.DeltaScores.Count;

            PrepareHistogram();
            groupBoxEstablishQuality.Enabled = true;

        }

        private void buttonGetGoodHitsFromSEPro_Click(object sender, EventArgs e)
        {

            if (!File.Exists(textBoxSEProFileToExtractGoodPeptides.Text))
            {
                MessageBox.Show("Please enter a valid SEPro file.");
                return;
            }
            ResultPackage sepro = ResultPackage.Load(textBoxSEProFileToExtractGoodPeptides.Text);

            richTextBoxResultScans.Clear();
            richTextBoxPeptides.Clear();

            richTextBoxResultScans.AppendText("File Name\tCharge\tScan Number\tSequence\tp-value\n");

            Dictionary<string, List<double>> peptideScoreDictionary = new Dictionary<string, List<double>>();

            List<SQTScan> theScans = sepro.MyProteins.AllSQTScans.Select(a => a).ToList();

            if (numericUpDownNoPhosphoSites.Value > 0)
            {

                //Calculate an XDScore for each mass spectrum
                Regex seventyNine = new Regex("79");
                theScans = theScans.FindAll(a => seventyNine.Matches(a.PeptideSequence).Count == numericUpDownNoPhosphoSites.Value);
            }

            if (numericUpDownModelChargeState.Value > 0)
            {
                theScans = theScans.FindAll(a => a.ChargeState == numericUpDownModelChargeState.Value);
            }

            foreach (SQTScan sqtScan in theScans)
            {
                if (sqtScan.PeptideSequenceCleaned.Contains("79"))
                {
                    double pValue = xdScore.RetrievePValueForScanNumber(sqtScan.ScanNumber, (double)numericUpDownXCorrPrime.Value);

                    if (pValue == -1)
                    {
                        Console.WriteLine("Shouldn´t be here");
                    }

                    if (peptideScoreDictionary.ContainsKey(sqtScan.PeptideSequenceCleaned))
                    {
                        peptideScoreDictionary[sqtScan.PeptideSequenceCleaned].Add(pValue);
                    }
                    else
                    {
                        peptideScoreDictionary.Add(sqtScan.PeptideSequenceCleaned, new List<double>() {pValue});
                    }

                    richTextBoxResultScans.AppendText(sqtScan.FileName + "\t" + sqtScan.ChargeState + "\t" + sqtScan.ScanNumber + "\t" + sqtScan.PeptideSequenceCleaned + "\t" + Math.Round(pValue,4) + "\n");
                }
            }

            foreach (KeyValuePair<string, List<double>> kvp in peptideScoreDictionary)
            {
                kvp.Value.Sort();
                richTextBoxPeptides.AppendText(kvp.Key + "\t" + kvp.Value[0] + "\n");
            }


        }

        private void buttonBrowseForSEProFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "SEPro files (*.sepr)|*.sepr";

            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxSEProFileToExtractGoodPeptides.Text = openFileDialog1.FileName;
                buttonGetGoodHitsFromSEPro.Enabled = true;
            }
        }
    }
}
