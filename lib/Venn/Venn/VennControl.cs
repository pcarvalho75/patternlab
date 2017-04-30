using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PatternTools;
using PatternTools.VennProbability;
using System.IO;
using PLP;

namespace Venn
{
    public partial class VennControl : UserControl
    {
        PatternLabProject plp;

        BelaGraph.VennManager venn;
        List<int> lables = new List<int>();
        Font myFont = new Font("Courrier New", 11);

        public VennControl()
        {
            InitializeComponent();
            comboBoxBackground.SelectedIndex = 0;
        }


        private bool checkConfidence(int dim, int label, double pvalue, List<IntersectionResultAnalysis> vennProbabilityDictionary)
        {
            List<double> signal = plp.MySparseMatrix.ExtractDimValues(dim, label, false);
            double averageSignal = signal.Average();

            IntersectionResultAnalysis ira = vennProbabilityDictionary.Find(a => a.ClassLabel == label && averageSignal >= a.SignalLowerBound && averageSignal <= a.SignalUpperBound && a.NoReplicatesAppeared == signal.Count);

            if (ira.BayesianProbability <= pvalue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void checkBoxPlotLabels_Click(object sender, EventArgs e)
        {
            if (checkBoxPlotLabels.Checked)
            {
                groupBoxLabels.Enabled = true;
            }
            else
            {
                groupBoxLabels.Enabled = false;
            }
        }

        private void PlotDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            List<int> itemsInCommon = new List<int>();
            foreach (int i in venn.VennDic[int.Parse(dataGridView1.Columns[e.ColumnIndex].Name)])
            {
                if (venn.VennDic[int.Parse(dataGridView1.Columns[e.RowIndex].Name)].Contains(i))
                {
                    itemsInCommon.Add(i);
                }
            }

            CellReportDisplay crd = new CellReportDisplay(itemsInCommon, plp.MySparseMatrix, plp.MyIndex);
            crd.Text = "Report :: " + dataGridView1.Columns[e.ColumnIndex].Name + " & " + dataGridView1.Columns[e.RowIndex].Name;
            crd.ShowDialog();

        }



        private void radioButtonFilteringProbability_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFilteringProbability.Checked)
            {
                numericUpDownFilteringProbability.Enabled = true;
                numericUpDownMinimumNumberOfReplicates.Enabled = false;
            }
            else
            {
                numericUpDownFilteringProbability.Enabled = false;
                numericUpDownMinimumNumberOfReplicates.Enabled = true;
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fontDialog1.ShowEffects = true;
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                myFont = fontDialog1.Font;
            }

            linkLabel1.Font = myFont;
        }



        private void buttonLoad_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "PatternLab Project (*.plp)|*.plp";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            Dictionary<int, string> allClassDescriptions = new Dictionary<int, string>();

            try
            {

                plp = new PatternLabProject(openFileDialog1.FileName);

                allClassDescriptions = plp.MySparseMatrix.ClassDescriptionDictionary;
                plp.MySparseMatrix.theMatrixInRows.Sort((a, b) => a.Lable.CompareTo(b.Lable));


                //Verify if the sparse matrix classes are appropriate
                List<int> theLabels = plp.MySparseMatrix.ExtractLabels();

                List<int> newLabels = new List<int>();
                for (int i = 0; i < theLabels.Count; i++)
                {
                    newLabels.Add(i + 1);
                }

                if (theLabels.Count != 2 || (theLabels.Count == 2 && (!theLabels.Contains(1) || !theLabels.Contains(-1))))
                {
                    ClassSelection cs = new ClassSelection(theLabels, newLabels, plp.MySparseMatrix, true, 3);
                    cs.ShowDialog();
                }

                groupBoxPlotOptions.Enabled = true;

            }
            catch
            {
                MessageBox.Show("Problems loading sparse matrix / index files");
                return;
            }

            //Verify if the classes follow the pattern 1,2,3, else we should correct and warn the uses
            List<int> lables = plp.MySparseMatrix.ExtractLabels();
            Dictionary<int, int> labelConversion = new Dictionary<int, int>();

            Dictionary<int, string> newClassDescriptions = new Dictionary<int, string>();

            for (int i = 0; i < lables.Count; i++)
            {
                labelConversion.Add(lables[i], i + 1);
                if (lables[i] != i + 1)
                {
                    MessageBox.Show("Matrix lable " + lables[i] + " will be represented as group " + (i + 1));
                }
            }

            foreach (sparseMatrixRow r in plp.MySparseMatrix.theMatrixInRows)
            {
                r.Lable = labelConversion[r.Lable];
            }

            //----------------------------------
            //Update the descriptions, in case they have the word group
            if (textBoxC1Name.Text.Equals("Group") && lables.Count >= 1 && plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(lables[0]) || allClassDescriptions.ContainsValue(textBoxC1Name.Text))
            {
                textBoxC1Name.Text = plp.MySparseMatrix.ClassDescriptionDictionary[lables[0]];
                //textBoxC1Name.Text = sMatrix.ClassDescriptionDictionary[1];
            }
            if (textBoxC2Name.Text.Equals("Group") && lables.Count >= 2 && plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(lables[1]) || allClassDescriptions.ContainsValue(textBoxC2Name.Text))
            {
                textBoxC2Name.Text = plp.MySparseMatrix.ClassDescriptionDictionary[lables[1]];
                //textBoxC2Name.Text = sMatrix.ClassDescriptionDictionary[2];
            }
            if (textBoxC3Name.Text.Equals("Group") && lables.Count >= 3 && plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(lables[2]) || allClassDescriptions.ContainsValue(textBoxC3Name.Text))
            {
                textBoxC3Name.Text = plp.MySparseMatrix.ClassDescriptionDictionary[lables[2]]; ;
                //textBoxC3Name.Text = sMatrix.ClassDescriptionDictionary[3];
            }

            if (lables.Count != 2)
            {
                radioButtonFilteringProbability.Enabled = false;
            }
           
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            belaGraphControl2.ClearDataBuffer();
            belaGraphControl2.Plot(BelaGraph.BackgroundOption.None, false, true, myFont);

            //Lets clone our sparse matrix
            SparseMatrix vennSparseMatrixCache = PatternTools.ObjectCopier.Clone(plp.MySparseMatrix);


            List<IntersectionResultAnalysis> VennProbabilityDictionary = new List<IntersectionResultAnalysis>();

            //Qaulity filterS
            if (radioButtonFilteringMinNumberOfReplicates.Checked)
            {
                //BY NUMBER OF REPLICATES
               vennSparseMatrixCache.EliminateIDsThatAreNotPresentAtLeastInXReplicates((int)numericUpDownMinimumNumberOfReplicates.Value, radioButtonAllClasses.Checked);
            }
            else
            {
                //by Probability
                PatternTools.VennProbability.VennProbabilityCalculator vpc = new PatternTools.VennProbability.VennProbabilityCalculator(plp.MySparseMatrix);
                VennProbabilityDictionary = vpc.GenerateProbabilisticDictionary(4);

                string report = PatternTools.VennProbability.ResultPrinter.PrintReport(VennProbabilityDictionary, new List<string> { "Low", "Medium", "High", "Very High" });
                richTextBoxLog.Clear();
                richTextBoxLog.AppendText(report);
            }

            venn = new BelaGraph.VennManager(vennSparseMatrixCache);

            //--------------------------------


            //If we are working with probabilities, we should apply a correction
            //The venn diagram with probability only works for two classes 

            if (radioButtonFilteringProbability.Checked)
            {
                //We need to find the ones unique for class 1 and the ones unique for class 2.
                //If they are below the probability scores, we need to include them in the other class
                //this needs to be done using the dvs

                //Step 1, the uniques from each group
                List<List<int>> inLabel = new List<List<int>>();
                inLabel.Add(new List<int>());
                inLabel.Add(new List<int>());

                inLabel[0].AddRange(venn.VennDic[1]);
                inLabel[1].AddRange(venn.VennDic[2]);


                List<int> intersection = inLabel[0].Intersect(inLabel[1]).ToList();
                inLabel[0].RemoveAll(a => intersection.Contains(a));
                inLabel[1].RemoveAll(a => intersection.Contains(a));


                //Find the ones that fail the probability

                for (int i = 1; i <= 2; i++)
                {
                    List<int> newIntersectionClass = new List<int>(); //We will put all the new unique guys in here
                    foreach (int dim in inLabel[i - 1])
                    {
                        if (!checkConfidence(dim, i, (double)numericUpDownFilteringProbability.Value, VennProbabilityDictionary))
                        {
                            newIntersectionClass.Add(dim);
                        }
                    }

                    foreach (int k in newIntersectionClass)
                    {
                        venn.VennDic[i].Remove(k);
                        vennSparseMatrixCache.eliminateDim(k, 0, true);
                    }
                }
            }




            //This is where we find the unique proteins
            List<BelaGraph.DataVector> dvs = venn.GetDataVectors();


            //Plot
            buttonPlot.Text = "Please wait...";
            this.Update();

            belaGraphControl2.ClearDataBuffer();

            //Lets save this matrix so we can do reports latter
            belaGraphControl2.vennSparseMatrixCache = vennSparseMatrixCache;

            belaGraphControl2.Title = textBoxTitle.Text;
            belaGraphControl2.VennRadiusOfLargestCircleCorrectionFactor = (double)numericUpDownScaleFactor.Value;

            if (lables.Count <= 3)
            {

                for (int i = 0; i < dvs.Count; i++)
                {
                    System.Drawing.Color c1 = new Color();

                    if (i == 0)
                    {
                        c1 = Color.Green;
                    }
                    else if (i == 1)
                    {
                        c1 = Color.Yellow;
                    }
                    else if (i == 2)
                    {
                        c1 = Color.Blue;
                    }
                    System.Windows.Media.Color c2 = System.Windows.Media.Color.FromArgb(c1.A, c1.R, c1.G, c1.B);
                    System.Windows.Media.SolidColorBrush sb = new System.Windows.Media.SolidColorBrush(c2);
                    sb.Opacity = double.Parse(numericUpDownOpacity.Value.ToString());
                    dvs[i].MyBrush = sb;
                    belaGraphControl2.AddDataVector(dvs[i]);
                }
                BelaGraph.BackgroundOption bg = (BelaGraph.BackgroundOption)Enum.Parse(typeof(BelaGraph.BackgroundOption), comboBoxBackground.SelectedItem.ToString());
                belaGraphControl2.Plot(bg, false, true, myFont);
            }

            //Fill out our table;
            dataGridView1.Columns.Clear();

            //Add the columns
            foreach (var r in dvs)
            {
                DataGridViewLinkColumn col = new DataGridViewLinkColumn();
                col.Name = r.Name;
                col.HeaderText = r.Name;
                dataGridView1.Columns.Add(col);
            }

            //Add the info
            for (int i = 0; i < dvs.Count; i++)
            {

                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = dvs[i].Name;
                for (int j = 0; j < dvs[i].ThePoints.Count; j++)
                {
                    DataGridViewLinkCell cell = new DataGridViewLinkCell();
                    cell.Value = dvs[i].ThePoints[j].Y;
                    dataGridView1.Rows[i].Cells[j] = cell;

                }

            }

            if (checkBoxPlotLabels.Checked && lables.Count <= 3)
            {
                try
                {
                    belaGraphControl2.PlotVennLabels(venn.VennDic, textBoxC1Name.Text, textBoxC2Name.Text, textBoxC3Name.Text, true, checkBoxDetailedLabel.Checked, plp.MyIndex, myFont);
                }
                catch (Exception e2)
                {
                    MessageBox.Show("Make sure your sparse matrix file is labeled correctly.  It should contain class 1, 2, and, 3" + "\n" + e2.Message);
                }
            }

            buttonPlot.Text = "Plot";
        }

        private void buttonSavePlot_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save Plot";
            saveFileDialog1.Filter = "PNG file (*.png)|*.png";

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }
            belaGraphControl2.SaveGraph(saveFileDialog1.FileName);
            MessageBox.Show("File Saved!");
        }
    }
}
