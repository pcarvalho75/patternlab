using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatternTools;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.IO;
using PLP;

namespace Anova
{
    public partial class AnovaControl : UserControl
    {
        PatternLabProject plp;
        List<AnovaItem> anovaResults;


        public AnovaControl()
        {
            InitializeComponent();
        }

        private void Populate()
        {
            List<AnovaItem> AnovaResults = new List<AnovaItem>();

            List<int> allClasses = plp.MySparseMatrix.ExtractLabels();

            if (plp.MySparseMatrix.ClassDescriptionDictionary.Keys.Count == 0)
            {
                //There is no class description dictionary so we will create one
                foreach (int c in allClasses)
                {
                    plp.MySparseMatrix.ClassDescriptionDictionary.Add(c, c.ToString());
                }
            }


            anovaResults = new List<AnovaItem>(plp.MyIndex.TheIndexes.Count);

            List<double> allQuantitationValues = new List<double>();
            foreach (PatternTools.SparseMatrixIndexParserV2.Index i in plp.MyIndex.TheIndexes)
            {
                Console.WriteLine(i.ID + "\t" + i.Name);
                Dictionary<int, List<double>> valueDict = new Dictionary<int, List<double>>();
                List<int> noItems = new List<int>();
                foreach (var kvp in plp.MySparseMatrix.ClassDescriptionDictionary)
                {
                    List<double> values = plp.MySparseMatrix.ExtractDimValues(i.ID, kvp.Key, false);
                    allQuantitationValues.AddRange(values);
                    noItems.Add(values.Count);
                    valueDict.Add(kvp.Key, values);
                }

                double sum = 0;
                foreach (KeyValuePair<int, List<double>> kvp in valueDict)
                {
                    sum += valueDict.Count;
                }

                if (sum == 0)
                {
                    continue;
                }



                //We will only consider cases of which we have the same number of readings for each class
                if (noItems.Distinct().Count() == 1 && !noItems.Contains(1))
                {
                    if (noItems[0] == 0)
                    {
                        Console.WriteLine("A protein with 0 quantitation for all three conditions was found");
                        continue;
                    }
                    AnovaItem a = new AnovaItem(i.Name, i.ID, i.Description, valueDict, plp.MySparseMatrix.ClassDescriptionDictionary, (double)numericUpDownQValue.Value);
                    if (!double.IsInfinity(a.MyAnovaResult.FRatio))
                    {
                        try
                        {
                            double p = 1 - alglib.fdistr.fdistribution((int)a.MyAnovaResult.DegreeOfFreedomBetweenGroups, (int)a.MyAnovaResult.DegreeOfFreedomWithinGroups, a.MyAnovaResult.FRatio);
                            Console.WriteLine(p);
                            anovaResults.Add(a);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                }


            }

            chartHistogram.Series[0].Points.Clear();
            List<PatternTools.HistogramHelper.HistogramBin> hBins = PatternTools.HistogramHelper.BinData(allQuantitationValues, 30);
            foreach (PatternTools.HistogramHelper.HistogramBin b in hBins)
            {
                chartHistogram.Series[0].Points.AddXY(b.IntervalFloor, b.TheData.Count);
            }




            Console.WriteLine("Done first step");


            List<double> pvalues = (from a in anovaResults
                                    where a.MaxSignal > double.Parse(textBoxMinSignal.Text)
                                    select a.PValue).ToList();

            double correctedPValue = PatternTools.pTools.BenjaminiHochbergFDR((double)numericUpDownQValue.Value, pvalues, true);
            labelpvaluecuttoff.Text = correctedPValue.ToString();

            var toDisplay = from ar in anovaResults
                            select new
                            {
                                ID = ar.Locus,
                                MaxSignal = ar.MaxSignal,
                                DegreeOfFreedomBetweenGroups = ar.MyAnovaResult.DegreeOfFreedomBetweenGroups,
                                DegreesOfFredomTotal = ar.MyAnovaResult.DegreeOfFreedomTotal,
                                DegreeOfFreedomWithinGroups = ar.MyAnovaResult.DegreeOfFreedomWithinGroups,
                                MeanSquareVarianceBetweenGroups = ar.MyAnovaResult.MeanSquareVarianceBetweenGroups.ToString("E3"),
                                MeanSquareVarianceWithinGroups = ar.MyAnovaResult.MeanSquareVarianceWithinGroups.ToString("E3"),
                                SumOfSquaresBetweenGroups = ar.MyAnovaResult.SumOfSquaresBetweenGroups.ToString("E3"),
                                SumOfSquaresTotal = ar.MyAnovaResult.SumOfSquaresTotal.ToString("E3"),
                                SumOfSquaresWithingGroups = ar.MyAnovaResult.SumOfSquaresWithinGroups.ToString("E3"),
                                FCritical = ar.MyAnovaResult.FCriticalValue.ToString("E3"),
                                FRatio = ar.MyAnovaResult.FRatio.ToString("E3"),
                                PValue = ar.PValue,
                                StatisticallySignificant = ar.StatisticallySignificant(correctedPValue).ToString(),

                                Description = ar.Description
                            };



            PatternTools.SortableBindingList<object> theList = new SortableBindingList<object>(toDisplay.ToList());

            dataGridViewIDs.DataSource = theList;

            PaintStatisticallySignificantRows();

            Console.WriteLine("Total anova results successfully calculated: " + anovaResults.Count);
            Console.WriteLine("Total proteins in index file: " + plp.MyIndex.TheIndexes.Count);

            labelStatisticallySignificant.Text =
            anovaResults.Count(a => a.StatisticallySignificant(correctedPValue) && a.MaxSignal > double.Parse(textBoxMinSignal.Text)) + " / " +
            anovaResults.Count(a => a.StatisticallySignificant(correctedPValue)) + " / " +
            anovaResults.Count;


            //dataGridViewIDs.DataSource = loccus.ToList();

            this.Update();


        }

        private void PaintStatisticallySignificantRows()
        {
            foreach (DataGridViewRow row in dataGridViewIDs.Rows)
            {
                try
                {
                    if (row.Cells["StatisticallySignificant"].Value.ToString().Equals("True"))
                    {
                        if (double.Parse(row.Cells["MaxSignal"].Value.ToString()) <= double.Parse(textBoxMinSignal.Text))
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }

                }
                catch (Exception e6)
                {
                    Console.WriteLine(e6.Message);
                }
            }
        }

        private void dataGridViewIDs_Sorted(object sender, EventArgs e)
        {
            PaintStatisticallySignificantRows();
        }

        private void dataGridViewIDs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string loccus = dataGridViewIDs.Rows[e.RowIndex].Cells[0].Value.ToString();


                Console.WriteLine(e.RowIndex.ToString() + " clicked. Locus: " + loccus);

                AnovaItem ai = anovaResults.Find(a => a.Locus.Equals(loccus));

                chartAnova.Series.Clear();

                double maxY = double.NegativeInfinity;

                richTextBoxDataPoints.Clear();

                foreach (Series s in ai.MyChart.Series)
                {
                    richTextBoxDataPoints.Text += s.Name + "\n";

                    chartAnova.Series.Add(new Series(s.Name));
                    foreach (DataPoint p in s.Points)
                    {
                        chartAnova.Series.Last().Points.Add(p);
                        richTextBoxDataPoints.Text += p.YValues[0] + "\t";

                        if (p.YValues[0] > maxY)
                        {
                            maxY = p.YValues[0];
                        }
                    }

                    richTextBoxDataPoints.Text += "\n";
                }

                chartAnova.Titles[0].Text = loccus;

                chartAnova.ChartAreas[0].AxisY.Maximum = maxY * 1.1;
                chartAnova.ChartAreas[0].AxisY.Title = "Signal";
                chartAnova.ChartAreas[0].AxisX.Title = "Replicate ID";



                this.Update();
            }
            catch
            {
                Console.WriteLine("Unable to retrieve locus information");
            }

        }

        private void checkBoxTotalSignalNormalization_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Sparse matrix should be reloaded");
        }

        private void numericUpDownProbability_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Sparse matrix should be reloaded");
        }

        private void buttonRecalculate_Click(object sender, EventArgs e)
        {
            Populate();
        }

        private void buttonSavePlot_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG files (*.png)|*.png";
            saveFileDialog1.FileName = "Chart";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {

                ChartImageFormat format = ChartImageFormat.Png;
                chartAnova.SaveImage(saveFileDialog1.FileName, format);
                MessageBox.Show("Chart saved");
            }

        }

        private void buttonLoadPLP_Click(object sender, EventArgs e)
        {

            //Sparse Matrix Code
            openFileDialog1.Filter = "PatternLab project file (*.plp)|*.plp";
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                plp = new PatternLabProject(openFileDialog1.FileName);


                if (plp.MySparseMatrix.allDims().Count <= 2)
                {
                    throw new Exception("The Anova test should only be used for experiments with at least 3 conditions");
                }

                if (checkBoxTotalSignalNormalization.Checked)
                {
                    plp.MySparseMatrix.totalSignalNormalization();
                    
                }

                buttonRecalculate.Enabled = true;
                buttonSavePlot.Enabled = true;
                Populate();
            }

        }
    }
}
