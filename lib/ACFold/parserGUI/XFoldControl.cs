using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Windows.Media;
using System.Text.RegularExpressions;
using PatternTools;
using System.Linq;
using System.IO;
using PLP;

namespace parserGUI
{
    public delegate void statusDelegate(string text);

    public partial class XFoldControl : UserControl
    {
        List<XFoldStruct> theData;
        List<List<XFoldStruct>> theDataMultiple;
        List<double> folds = new List<double>();
        List<MultiClassItem> multiClassItems;
        PatternLabProject plp;
        double addPseudoCounts = 1;

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="useACTest">true means we will use AC, else we will use t</param>
        public XFoldControl (bool useACTest)
        {
            //bool useACTest = false;
            InitializeComponent();
            if (useACTest)
            {
                radioButtonAC.Checked = true;
                radioButtonUsingLabeledData.Enabled = false;
                numericUpDownMinReplicates.Enabled = false;
                //radioButtonNTS.Enabled = false;
                //radioButtonNRowSigma.Enabled = false;

            }
            else
            {
                radioButtonTTest.Checked = true;
                radioButtonUsingLabeledData.Enabled = true;
            }
        }


        /// <summary>
        /// Starts it directly on TFole
        /// </summary>
        public XFoldControl()
        {
            //bool useACTest = false;
            InitializeComponent();
            radioButtonTTest.Checked = true;
            radioButtonUsingLabeledData.Enabled = true;
        }


        private void PerformMultiClassAnalysis()
        {
            List<int> theLabels = plp.MySparseMatrix.ExtractLabels();

            multiClassItems = new List<MultiClassItem>();

            List<double> allPValues = new List<double>();

            //Make sure we have everyone sorted by p-values
            foreach (List<XFoldStruct> theList in theDataMultiple)
            {
                theList.Sort((a, b) => a.pValue.CompareTo(b.pValue));
                allPValues.AddRange (theList.Select(a => a.pValue).ToList());
            }
            allPValues = allPValues.Distinct().ToList();
            allPValues.Sort((a, b) => a.CompareTo(b));

            double qCuttoff = GetQuantitationCuttoff();
            foreach (SparseMatrixIndexParserV2.Index index in plp.MyIndex.TheIndexes)
            {
                //Obtain a list of conditions in which the protein is differentialy expressed
                double[] ApprovedComparisons = new double[theDataMultiple.Count];
                int bCounter = 0;
                int score = 0;

                foreach (List<XFoldStruct> theList in theDataMultiple)
                {                    
                    //verify if the protein is a blue dot
                    int theIndex = theList.FindIndex(a => a.GPI == index.ID);

                    if (theIndex == -1) { continue; }

                    theList[theIndex].Color = GetPointColorVFold(theList[theIndex], allPValues[1], (double)numericUpDownA.Value, (double)numericUpDownQValue.Value, 1, qCuttoff);

                    //The color is blue
                    if (theList[theIndex].Color == (int)dotcolors.blue)
                    {
                        ApprovedComparisons[bCounter] = 1;
                        score++;
                    }
                    bCounter++;
                }

                //We dont want to print stuff that is not blue
                if (score == 0) { continue; }

                MultiClassItem item = new MultiClassItem(index.ID, ApprovedComparisons);

                multiClassItems.Add(item);

            }

        }

        


        private void PerformPairWiseAnalysis()
        {

            if (folds.Count == 0)
            {
                MessageBox.Show("No proteins detected.  Make sure you use the TFold test on an experiment that has replicates and that at least one protein is detected in the number of replicates specified in the Min Replicates parameter.");
                return;
            }

            double overallFoldChange = folds.Average();
            

            //Now plot for BelaGraph
            //BelaGraph
            belaGraphControl1.ClearDataBuffer();
            belaGraphControl1.XLabel = "-Log2(p)";
            belaGraphControl1.YLabel = "Log2(Fold)";
            belaGraphControl1.XRound = 2;
            belaGraphControl1.YRound = 2;


            SolidColorBrush bluebrush = System.Windows.Media.Brushes.Blue.Clone(); bluebrush.Opacity = 0.55;
            SolidColorBrush greenbrush = System.Windows.Media.Brushes.Green.Clone(); greenbrush.Opacity = 0.55;
            SolidColorBrush orangebrush = System.Windows.Media.Brushes.Orange.Clone(); orangebrush.Opacity = 0.55;
            SolidColorBrush redbrush = System.Windows.Media.Brushes.Red.Clone(); redbrush.Opacity = 0.55;


            BelaGraph.DataVector blueDV = new BelaGraph.DataVector(BelaGraph.GraphStyle.Bubble, "Blue Dots", bluebrush);
            BelaGraph.DataVector orangeDV = new BelaGraph.DataVector(BelaGraph.GraphStyle.Bubble, "Orange Dots", orangebrush);
            BelaGraph.DataVector greenDV = new BelaGraph.DataVector(BelaGraph.GraphStyle.Bubble, "Green Dots", greenbrush);
            BelaGraph.DataVector redDV = new BelaGraph.DataVector(BelaGraph.GraphStyle.Bubble, "Red Dots", redbrush);

            int noLabels = plp.MySparseMatrix.ExtractLabels().Count;
            int pointsExcluded = 0;
            List<double> allPValues = theData.Select(a => a.pValue).OrderBy(a => a).ToList();

            double userQvalue = (double)numericUpDownQValue.Value;
            double quantitationCuttoff = GetQuantitationCuttoff();

            theData.Sort((a, b) => a.pValue.CompareTo(b.pValue));
            double qValueFractionCounter = 0;

            List<XFoldStruct> greenOrBlue = theData.FindAll(a => GetPointColorVFold(a, allPValues[1], (double)numericUpDownA.Value, userQvalue, 1, quantitationCuttoff) <= 2).OrderBy(a => a.pValue).ThenBy(a=> a.GPI).ToList();
            List<XFoldStruct> orange = greenOrBlue.FindAll(a => a.QuantitationNegativeClass < quantitationCuttoff && a.QuantitationPositiveClass < quantitationCuttoff);
            greenOrBlue = greenOrBlue.Except(orange).ToList();
            
            theData.Sort((a, b) => a.pValue.CompareTo(b.pValue));

            foreach (XFoldStruct dataPoint in theData)
            {
                //We need to find the denominator; ie, all items that are green or blue
                if (!orange.Contains(dataPoint))
                {
                    qValueFractionCounter += 1 / (double)greenOrBlue.Count;
                }
                
                List<double> signalPos = plp.MySparseMatrix.ExtractDimValues(dataPoint.GPI, 1, false);
                List<double> signalNeg = plp.MySparseMatrix.ExtractDimValues(dataPoint.GPI, -1, false);

                dataPoint.QuantitationPositiveClass = pTools.Average(signalPos);
                dataPoint.QuantitationNegativeClass = pTools.Average(signalNeg);


                BelaGraph.PointCav pc = new BelaGraph.PointCav();
                pc.Y = Math.Log(dataPoint.FoldChange, 2);
                pc.X = Math.Log(dataPoint.pValue, 2) * (-1);
                pc.ExtraParamNumeric = dataPoint.pValue;


                double correctedFoldChange = dataPoint.CorrectedFold();

                double theAbsFold = Math.Abs(dataPoint.CorrectedFold());
                string tip = "";
                if (plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(1) && plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(-1))
                {
                    tip += "PositiveClass: " + Regex.Replace(plp.MySparseMatrix.ClassDescriptionDictionary[1], "\n", "") + "\n";
                    tip += "NegativeClass: " + Regex.Replace(plp.MySparseMatrix.ClassDescriptionDictionary[-1], "\n", "") + "\n";
                }

                tip += "p-value = " + Math.Round(dataPoint.pValue,5) + "\nFoldChange = " + correctedFoldChange + "\nIndex = " + dataPoint.GPI + "\nLocus = " + plp.MyIndex.GetName(dataPoint.GPI) + "\nMeanSignal(+) = " + Math.Round(dataPoint.QuantitationPositiveClass, 6) + "\nMeanSignal(-) = " + Math.Round(dataPoint.QuantitationNegativeClass, 5);


                tip += "\nSignalPos :";
                foreach (double s in signalPos) { tip += " " + Math.Round(s, 6).ToString(); }
                tip += "\n";
                tip += "SignalNeg :";
                foreach (double s in signalNeg) { tip += " " + Math.Round(s, 6).ToString(); }
                tip += "\nDescription = " + plp.MyIndex.GetDescription(dataPoint.GPI);

                pc.MouseOverTip = tip;


                //Perform the clipping
                if (dataPoint.pValue < (double)numericUpDownPValueLowerClip.Value)
                {
                    pc.X = Math.Log((double)numericUpDownPValueLowerClip.Value, 2) * (-1);
                }

                if (theAbsFold >= (double)numericUpDownUpperYScale.Value)
                {
                    if (dataPoint.CorrectedFold() < 0)
                    {
                        pc.Y = Math.Log((double)numericUpDownUpperYScale.Value, 2) * -1;
                    }
                    else
                    {
                        pc.Y = Math.Log((double)numericUpDownUpperYScale.Value, 2);
                    }
                }


                if (pc.X.Equals(double.NegativeInfinity))
                {
                    pointsExcluded++;
                    continue;
                }


                int theColor = GetPointColorVFold(dataPoint, allPValues[1], (double)numericUpDownA.Value, userQvalue, qValueFractionCounter, quantitationCuttoff);


                if (theColor == 0)
                {
                    blueDV.AddPoint(pc);
                }
                else if (theColor == 1)
                {
                    orangeDV.AddPoint(pc);
                }
                else if (theColor == 2)
                {
                    greenDV.AddPoint(pc);
                }
                else if (theColor == 3)
                {
                    redDV.AddPoint(pc);
                }

                dataPoint.Color = theColor;


            }

            belaGraphControl1.AddDataVector(blueDV);
            belaGraphControl1.AddDataVector(orangeDV);
            belaGraphControl1.AddDataVector(greenDV);
            belaGraphControl1.AddDataVector(redDV);
            


            belaGraphControl1.Plot(BelaGraph.BackgroundOption.YellowXGradient, true, true, new System.Drawing.Font("Courier New", 12));

            //Lets  add some labels

            if (plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(1) && plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(-1))
            {
                string descTop = plp.MySparseMatrix.ClassDescriptionDictionary[-1];
                string descBottom = plp.MySparseMatrix.ClassDescriptionDictionary[1];


                System.Windows.Controls.Label TopLabel = new System.Windows.Controls.Label();
                TopLabel.Content = descTop;

                System.Windows.Controls.Label BottomLabel = new System.Windows.Controls.Label();
                BottomLabel.Content = descBottom;


                belaGraphControl1.MyCanvas.Children.Add(TopLabel);

                belaGraphControl1.MyCanvas.Children.Add(BottomLabel);
                belaGraphControl1.SetBottom(BottomLabel, 0);
            
            }
            //


            //Update the dot lables
            double totalDots = greenDV.ThePoints.Count + blueDV.ThePoints.Count + redDV.ThePoints.Count + orangeDV.ThePoints.Count;
            this.labelGreenDots.Text = greenDV.ThePoints.Count + " (" + Math.Round(((double)greenDV.ThePoints.Count / (double)totalDots) * 100, 1) + "%)";
            this.labelBlueDots.Text = blueDV.ThePoints.Count + " (" + Math.Round(((double)blueDV.ThePoints.Count / (double)totalDots) * 100, 1) + "%)";
            this.labelOrangeDots.Text = orangeDV.ThePoints.Count + " (" + Math.Round(((double) orangeDV.ThePoints.Count / (double)totalDots) * 100, 1) + "%)";
            this.labelRedDots.Text = redDV.ThePoints.Count + " (" + Math.Round(((double)redDV.ThePoints.Count / (double)totalDots) * 100, 1) + "%)";
            this.labelTotalNumber.Text = totalDots.ToString();
            this.labelLCuttoff.Text = quantitationCuttoff.ToString();
            this.labelAvgFold.Text = Math.Round(overallFoldChange, 2).ToString();
        }

        private int GetPointColorVFold(XFoldStruct dataPoint, double pThreshold, double a, double userQValue, double qValueFraction, double qCuttoff)
        {

            if (qValueFraction > 1) { qValueFraction = 1; }
            //Check as to what data vector to include this point
            ///blue = 0, orange = 1, green = 2, red = 3
            ///

            //establish the bounds
            double lowerFoldBound = Math.Pow(pThreshold / dataPoint.pValue, a);
            double upperFoldBound = Math.Pow(dataPoint.pValue / pThreshold, a);

            //Lets believe it is red
            int theColor = 2;

            if (dataPoint.pValue <= userQValue * qValueFraction)
            {
                theColor = 0;
            }

            if (dataPoint.FoldChange >= lowerFoldBound && dataPoint.FoldChange <= upperFoldBound)
            {
                theColor = 3;
            }

            if (theColor == 0 && dataPoint.QuantitationNegativeClass < qCuttoff && dataPoint.QuantitationPositiveClass < qCuttoff)
            {
                theColor = 1;
            }


            return theColor;
        }



        //---------------------




        private void printRows2(System.IO.StreamWriter WRITE, dotcolors colors2print)
        {
            int colorCode = (int)colors2print;
            List<XFoldStruct> toPrint = theData.FindAll(a => a.Color == colorCode);

            foreach (XFoldStruct theStruct in toPrint)
            {
                if (double.IsNaN(theStruct.QuantitationPositiveClass))
                {
                    theStruct.QuantitationPositiveClass = 0;
                }

                if (double.IsNaN(theStruct.QuantitationNegativeClass))
                {
                    theStruct.QuantitationNegativeClass = 0;
                }
                WRITE.WriteLine(plp.MyIndex.GetName(theStruct.GPI) + "\t" + theStruct.CorrectedFold().ToString() + "\t" + theStruct.pValue.ToString() + "\t" + theStruct.GPI + "\t" + theStruct.QuantitationPositiveClass.ToString() + "\t" + theStruct.QuantitationNegativeClass.ToString() + "\t" + plp.MyIndex.GetDescription(theStruct.GPI));

            }
        }

           

        public enum dotcolors : int
        {
            blue = 0,
            orange = 1,
            green = 2,
            red = 3
        }

        private void buttonBrowseIndex_Click(object sender, EventArgs e)
        {
    
            saveFileDialog1.Filter = "PatternLab Project (*.ppl)|*.ppl";
            saveFileDialog1.Title = "Save PatternLab Project file";
            saveFileDialog1.FileName = "MyPatternLabProject";
        }



        private void CalculateAndPlot()
        {
            buttonCalculateAndPlot.Text = "Wait!";
            this.Update();

            List<int> theLabels = plp.MySparseMatrix.ExtractLabels();

            PerformPairWiseAnalysis();

            buttonCalculateAndPlot.Text = "Calculate and Plot";
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                double pValue = double.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                double foldChange = double.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());

                BelaGraph.PointCav pc = new BelaGraph.PointCav();
                pc.Y = Math.Log(foldChange, 2);
                pc.X = Math.Log(pValue, 2) * (-1);
                pc.ExtraParamNumeric = pValue;

                //Clipping
                if (pValue < (double) numericUpDownPValueLowerClip.Value)
                {
                    pc.X = Math.Log((double) numericUpDownPValueLowerClip.Value, 2) * (-1);
                }

                double correctedFold = foldChange;
                if (correctedFold < 1)
                {
                    correctedFold = ((1 / foldChange) * (-1));
                }

                if (Math.Abs(correctedFold) >= (double) numericUpDownUpperYScale.Value)
                {
                    if (correctedFold < 0)
                    {
                        pc.Y = Math.Log((double) numericUpDownUpperYScale.Value, 2) * -1;
                    }
                    else
                    {
                        pc.Y = Math.Log((double) numericUpDownUpperYScale.Value, 2);
                    }
                }

                //-------------------------------------------


                pc = belaGraphControl1.MapPoint(pc);

                BelaGraph.DataVector dv = new BelaGraph.DataVector(BelaGraph.GraphStyle.Bubble, "ClickedProtein");
                dv.AddPoint(pc);
                dv.MyBrush = new SolidColorBrush(Colors.Azure);
                dv.MyBrush.Opacity = 0.5;

                belaGraphControl1.PlotBubble(dv, 30);
                MessageBox.Show("This protein was circled in the graph");
            }

        }

        private void radioButtonAllClasses_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSelectionParameters.Enabled = false;
            groupBoxFunctions.Enabled = false;
        }

        private void radioButtonPerClass_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSelectionParameters.Enabled = false;
            groupBoxFunctions.Enabled = false;
        }

        private void numericUpDownQValue_ValueChanged(object sender, EventArgs e)
        {
            CalculateAndPlot();
        }

        private void numericUpDownA_ValueChanged(object sender, EventArgs e)
        {
            CalculateAndPlot();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Find the optimal F-Stringency for a given q-value;
            CalculateAndPlot();


            theData.Sort((a, b) => a.pValue.CompareTo(b.pValue));

            List<double> allPValues = theData.Select(a => a.pValue).OrderBy(a => a).ToList();

            //Estimate the exponential lambda parameter
            double qCuttof = GetQuantitationCuttoff();


            List<int> blueValues = new List<int>();
            List<double> fValues = new List<double>();

            for (double f = 0; f < 1; f += 0.01)
            {
                //Find out number of blues
                List<XFoldStruct> greenOrBlue = theData.FindAll(a => GetPointColorVFold(a, allPValues[1], f, (double)numericUpDownQValue.Value, 1, qCuttof) <= 2);

                greenOrBlue.RemoveAll(a => a.QuantitationNegativeClass < qCuttof && a.QuantitationPositiveClass < qCuttof);
                greenOrBlue.Sort((a, b) => a.pValue.CompareTo(b.pValue));

                //Now Lets get the blues
                double qValueFractionCounter = 0;
                int blueCounter = 0;
                foreach (XFoldStruct x in greenOrBlue)
                {
                    qValueFractionCounter += 1 / (double)greenOrBlue.Count;
                    int color = GetPointColorVFold(x, allPValues[1], f, (double)numericUpDownQValue.Value, qValueFractionCounter, qCuttof);
                    if (color == 0)
                    {
                        blueCounter++;
                    }
                    else
                    {
                        break;
                    }
                }

                blueValues.Add(blueCounter);
                fValues.Add(f);

            }

            //int index = blueValues.IndexOf(blueValues.Max());

            //Console.WriteLine("Q: " + q + "\t B:" + blueValues.Max() + "\t F:" + fValues[index]);
            FormChooseFStringency form = new FormChooseFStringency();
            form.Plot(fValues, blueValues);
            form.ShowDialog();
            double fs = form.FStringency;
            numericUpDownA.Value = (decimal)fs;


        }

        private double GetQuantitationCuttoff()
        {
            List<double> foldsNeg = theData.Select(a => a.QuantitationNegativeClass).OrderByDescending(a => a).ToList();
            List<double> foldsPos = theData.Select(a => a.QuantitationPositiveClass).OrderByDescending(a => a).ToList();
            List<double> folds = foldsNeg;
            folds.AddRange(foldsPos);

            double lambda = 1 / folds.Average();

            double qCuttof = (double)numericUpDownLStringency.Value * Math.Sqrt(1 / Math.Pow(lambda, 2));
            return qCuttof;
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "PatternLab Project (*.plp)|*.plp";
            openFileDialog1.FileName = "";


            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            plp = new PatternLabProject(openFileDialog1.FileName);

            //To make sure we dont duplicate info in the table
            buttonParse.Text = "Wait!";
            this.Update();
            dataGridView1.Rows.Clear();
            folds.Clear();

            //try
            //{
                //Calculate the pscores and folds
                ClassSelection cs;

                //Verify if the sparse matrix classes are appropriate
                List<int> theLabels = plp.MySparseMatrix.ExtractLabels();

                buttonOptimizeFStringency.Enabled = true;
                if (theLabels.Count > 2 || (theLabels.Count == 2 && ((!theLabels.Contains(1) || !theLabels.Contains(-1)))))
                {
                    if (radioButtonAC.Checked)
                    {
                        cs = new ClassSelection(theLabels, new List<int> { -1, 1 }, plp.MySparseMatrix, false, 2);
                    }
                    else if (radioButtonTTest.Checked)
                    {
                        cs = new ClassSelection(theLabels, new List<int> { -1, 1 }, plp.MySparseMatrix, true, 2);
                    }
                    else
                    {
                        cs = new ClassSelection(theLabels, new List<int> { -1, 1 }, plp.MySparseMatrix, true, 2);
                    }
                    cs.ShowDialog();
                }

                if (theLabels.Count == 1 && !theLabels.Contains(-1) && radioButtonTTest.Checked)
                {

                    cs = new ClassSelection(theLabels, new List<int> { -1 }, plp.MySparseMatrix, true, 1);
                    cs.ShowDialog();
                }

                //Refresh the labels
                theLabels = plp.MySparseMatrix.ExtractLabels();


                //Compress the matrix
                if (radioButtonAC.Checked)
                {
                    plp.MySparseMatrix.shatterMatrix();
                }
                else if (radioButtonTTest.Checked)
                {
                    //Eliminate dims that do not satisfy the minimum readings

                    if (radioButtonAllClasses.Checked)
                    {
                        List<int> allDims = plp.MySparseMatrix.allDims();
                        List<int> dimsToEliminate = new List<int>();

                        foreach (int dim in allDims)
                        {
                            List<double> valuesT = plp.MySparseMatrix.ExtractDimValues(dim, 0, true);

                            if (valuesT.Count < (double)numericUpDownMinReplicates.Value)
                            {
                                dimsToEliminate.Add(dim);
                            }

                        }

                        foreach (int dim in dimsToEliminate)
                        {
                            plp.MySparseMatrix.eliminateDim(dim, 0, true);
                        }
                    }
                    else if (radioButtonPerClass.Checked)
                    {
                        List<int> allDims = plp.MySparseMatrix.allDims();
                        List<int> allClasses = plp.MySparseMatrix.ExtractLabels();
                        List<int> dimsToEliminate = new List<int>();

                        foreach (int dim in allDims)
                        {
                            foreach (int label in allClasses)
                            {
                                List<double> valuesT = plp.MySparseMatrix.ExtractDimValues(dim, label, false);

                                if (valuesT.Count < (double)numericUpDownMinReplicates.Value)
                                {
                                    dimsToEliminate.Add(dim);
                                }
                            }

                        }

                        dimsToEliminate = dimsToEliminate.Distinct().ToList();

                        foreach (int dim in dimsToEliminate)
                        {
                            plp.MySparseMatrix.eliminateDim(dim, 0, true);
                        }
                    }
                }

                //matrix.saveMatrix(@"C:\Users\Paulo C Carvalho\Desktop\PatternLabExampleData\ExampleData\mstep2DimElimination.txt");
                //Add pseudo spectral counts
                if (!radioButtonUsingLabeledData.Checked)
                {
                    plp.MySparseMatrix.addPseudoCounts(1);
                }


                //Determine what normalization the user specified
                int normalization = 0;
                if (radioButtonNRowSigma.Checked)
                {
                    normalization = 1;
                }
                else if (radioButtonNTS.Checked)
                {
                    normalization = 2;
                }
                else if (radioButtonUsingLabeledData.Checked)
                {
                    normalization = 3;
                }


                if (theLabels.Count <= 2)
                {
                    XFoldCalculations fs = new XFoldCalculations(plp.MySparseMatrix);

                    theData = fs.Xfold(normalization, radioButtonAC.Checked, (double)numericUpDownPValueLowerClip.Minimum);

                    //Calculate all the folds
                    folds.Clear();

                    foreach (var dataPoint in theData)
                    {
                        double correctedFoldChange;
                        correctedFoldChange = dataPoint.CorrectedFold();

                        //keep track of fold Changes
                        double foldChangeToAdd = correctedFoldChange;
                        if (foldChangeToAdd > 3)
                        {
                            foldChangeToAdd = 3;
                        }
                        else if (foldChangeToAdd < -3)
                        {
                            foldChangeToAdd = -3;
                        }
                        folds.Add(correctedFoldChange);
                    }


                    //Populate Result Table
                    //Fill the Data Table
                    foreach (XFoldStruct ac in theData)
                    {
                        //Just in case
                        if (ac.GPI == 0) { continue; }

                        try
                        {
                            dataGridView1.Rows.Add(
                                ac.GPI,
                                plp.MyIndex.GetName(ac.GPI),
                                Math.Round(ac.FoldChange, 2),
                                Math.Round(ac.pValue, 4),
                                ac.QuantitationPositiveClass,
                                ac.QuantitationNegativeClass,
                                plp.MyIndex.GetDescription(ac.GPI)
                                );
                        }
                        catch (Exception e2)
                        {
                            MessageBox.Show(e2.Message);
                        }
                    }
                }
                

                //Were done
                groupBoxSelectionParameters.Enabled = true;
                groupBoxFunctions.Enabled = true;

                //Make sure the canvas is clean
                //acPlotWPF1.dispose();

                //Clear the labels
                labelBlueDots.Text = "0";
                labelGreenDots.Text = "0";
                labelRedDots.Text = "0";
                labelTotalNumber.Text = "0";

            //}
            //catch (Exception z)
            //{
            //    ErrorWindow ew = new ErrorWindow(z.GetBaseException().ToString());
            //    ew.ShowDialog();
            //}

            buttonParse.Text = "Parse";
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

            System.Text.RegularExpressions.Regex keyWord = new System.Text.RegularExpressions.Regex(@textBoxSearch.Text, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            dataGridView1.Rows.Clear();

            foreach (XFoldStruct ac in theData)
            {
                //Just in case
                if (ac.GPI == 0) { continue; }

                try
                {
                    if (keyWord.IsMatch(plp.MyIndex.GetName(ac.GPI)) || keyWord.IsMatch(plp.MyIndex.GetDescription(ac.GPI)))
                    {
                        dataGridView1.Rows.Add(ac.GPI, plp.MyIndex.GetName(ac.GPI), Math.Round(ac.FoldChange, 2), Math.Round(ac.pValue, 4), ac.QuantitationPositiveClass, ac.QuantitationNegativeClass, plp.MyIndex.GetDescription(ac.GPI));
                    }
                }
                catch (Exception e2)
                {
                    MessageBox.Show(e2.Message);
                }
            }

        }

        private void buttonSaveFlexible_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save report";
            saveFileDialog1.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog1.FileName = "report";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }

            System.IO.StreamWriter WRITE = new System.IO.StreamWriter(saveFileDialog1.FileName);

            string analyzer = "ACFold";
            if (radioButtonTTest.Checked) { analyzer = "TFold"; }

            string normalizationUsed = "None";
            if (radioButtonNRowSigma.Checked) { normalizationUsed = "Row Sigma"; }
            if (radioButtonNTS.Checked) { normalizationUsed = "Total spectral count"; }

            WRITE.WriteLine("#PatternLab's {0} analyzer\n\n", analyzer);
            WRITE.WriteLine("#Parameters used:");
            WRITE.WriteLine("#F-stringency :{0},\t Q-value:{1},\t FDR:{2}", numericUpDownA.Value.ToString(), (double)numericUpDownQValue.Value, (double)numericUpDownQValue.Value);
            WRITE.WriteLine("#PatternLab Project File: {0}", plp.TheFileFromWereIWasLoaded);
            WRITE.WriteLine("#Normalization used: {0}", normalizationUsed);
            WRITE.WriteLine("#PseudoCount value: {0}", addPseudoCounts);
            WRITE.WriteLine("#Locus\tFold Change\tpValue\tGPI (Index in the sparse matrix)\tSignal+\tSignal-\tDescription");

            if (plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(1) && plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(-1))
            {
                WRITE.WriteLine("#PositiveClass: " + plp.MySparseMatrix.ClassDescriptionDictionary[1]);
                WRITE.WriteLine("#NegativeClass: " + plp.MySparseMatrix.ClassDescriptionDictionary[-1]);
            }
            WRITE.WriteLine("#\n");

            //Save the blue dots
            if (checkBoxBlue.Checked)
            {
                WRITE.WriteLine("#Identifications satisfied both, the automatic fold and statistical criteria. (Blue dots)");
                printRows2(WRITE, dotcolors.blue);
            }

            //save the green dots
            if (checkBoxGreen.Checked)
            {
                WRITE.WriteLine("#These identifications satisfied the fold criteria but, most likely, this happened by chance (should be disconsidered - Green dots).");
                WRITE.WriteLine("#Folds in this section are reported in absolute values.");
                printRows2(WRITE, dotcolors.green);
            }

            //save the red dots
            if (checkBoxRed.Checked)
            {
                WRITE.WriteLine("#These identifications did not meet the fold and p-value criteria. (should be disconsidered - Red dots)");
                printRows2(WRITE, dotcolors.red);
            }

            //save the orange
            if (checkBoxOrange.Checked)
            {
                WRITE.WriteLine("#These identifications were filtered out by the L-stringency and so deserve further experimentation to verify if they are indeed differentially expressed");
                printRows2(WRITE, dotcolors.orange);
            }

            WRITE.Close();

            MessageBox.Show("Report saved");
        }

        private void buttonSavePlot_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save Plot Image";
            saveFileDialog1.Filter = "PNG file (*.png)|*.png";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }
            belaGraphControl1.SaveGraph(saveFileDialog1.FileName);
        }

        private void buttonCalculateAndPlot_Click(object sender, EventArgs e)
        {
            CalculateAndPlot();
        }

        private void buttonClusterByHamming_Click(object sender, EventArgs e)
        {
            List<List<int>> simmilarityMatrix = new List<List<int>>(multiClassItems.Count);
            List<MultiClassItemCluster> theClusters = new List<MultiClassItemCluster>();

            multiClassItems.Sort((a, b) => b.Score.CompareTo(a.Score));

            for (int i = 0; i < multiClassItems.Count; i++)
            {
                List<int> simmilarityVector = new List<int>();
                for (int j = 0; j < multiClassItems.Count; j++)
                {
                    int distance;
                    if (i == j)
                    {
                        distance = int.MaxValue;
                    }
                    else
                    {
                        distance = pTools.HammingDistance(multiClassItems[i].blueDotArray, multiClassItems[j].blueDotArray);
                    }

                    simmilarityVector.Add(distance);
                }
                simmilarityMatrix.Add(simmilarityVector);
            }



            List<int> removedItems = new List<int>();
            int clusterCounter = 0;
            for (int clusteringDistance = 0; clusteringDistance < 50; clusteringDistance++)
            {

                for (int i = 0; i < simmilarityMatrix.Count; i++)
                {
                    MultiClassItemCluster c = new MultiClassItemCluster(clusteringDistance);
                    for (int j = i; j < simmilarityMatrix[i].Count; j++)
                    {
                        if (simmilarityMatrix[i][j] == clusteringDistance)
                        {
                            if (!removedItems.Contains(i))
                            {
                                removedItems.Add(i);
                                multiClassItems[i].ClusterDistance = clusteringDistance;
                                multiClassItems[i].ClusterNumber = clusterCounter;
                                c.MyItems.Add(multiClassItems[i]);
                            }

                            if (!removedItems.Contains(j))
                            {
                                removedItems.Add(j);
                                multiClassItems[j].ClusterDistance = clusteringDistance;
                                multiClassItems[j].ClusterNumber = clusterCounter;
                                c.MyItems.Add(multiClassItems[j]);
                            }
                        }
                    }
                    if (c.MyItems.Count > 0)
                    {
                        clusterCounter++;
                        theClusters.Add(c);
                    }
                }
            }

            //Now lets rebuild the original multiclass items
            List<MultiClassItem> orderedMultiClass = new List<MultiClassItem>(multiClassItems.Count);
            foreach (MultiClassItemCluster c in theClusters)
            {
                orderedMultiClass.AddRange(c.MyItems);
            }

            multiClassItems = orderedMultiClass;
        }

        private void numericUpDownLStringency_ValueChanged(object sender, EventArgs e)
        {
            CalculateAndPlot();
        }

        private void linkLabelManuscript_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatternTools.WebBrowser.Browser wb = new PatternTools.WebBrowser.Browser();
            wb.SetURL("http://bioinformatics.oxfordjournals.org/content/early/2012/04/26/bioinformatics.bts247.abstract");
            wb.ShowDialog();
        }


        //----------------------------


    }
}
