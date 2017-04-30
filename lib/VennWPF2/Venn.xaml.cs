using Microsoft.Win32;
using PatternTools;
using PatternTools.VennProbability;
using PLP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VennWPF2
{
    /// <summary>
    /// Interaction logic for Venn.xaml
    /// </summary>
    public partial class Venn : UserControl
    {

        PatternLabProject plp;
        List<int> lables = new List<int>();
        BelaGraph.VennManager venn;

        Font myFont = new Font("Courrier new", 14);


        public Venn()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PatternLab Project (*.plp)|*.plp";
            ofd.FileName = "";

            if (ofd.ShowDialog() != true )
            {
                return;
            }

            Dictionary<int, string> allClassDescriptions = new Dictionary<int, string>();

            try
            {
                WaitWindowWPF.Visibility = Visibility.Visible;

                await Task.Run(() => { 
                    plp = new PatternLabProject(ofd.FileName);

                    allClassDescriptions = plp.MySparseMatrix.ClassDescriptionDictionary;
                    plp.MySparseMatrix.theMatrixInRows.Sort((a, b) => a.Lable.CompareTo(b.Lable));


                    //Verify if the sparse matrix classes are appropriate
                    List<int> theLabels = plp.MySparseMatrix.ExtractLabels();

                    List<int> newLabels = new List<int>();
                    for (int i = 0; i < theLabels.Count; i++)
                    {
                        newLabels.Add(i + 1);
                    }

                    if (theLabels.Count != 2)
                    {
                        ClassSelection cs = new ClassSelection(theLabels, newLabels, plp.MySparseMatrix, true, 3);
                        cs.ShowDialog();
                    }
                });

                WaitWindowWPF.Visibility = Visibility.Collapsed;

                ComboBoxFonts.Text = "Times New Roman";

                ButtonPlot.IsEnabled = true;
                ButtonSavePlot.IsEnabled = true;

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
            if (TextBoxC1Name.Text.Equals("Group") && lables.Count >= 1 && plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(lables[0]) || allClassDescriptions.ContainsValue(TextBoxC1Name.Text))
            {
                TextBoxC1Name.Text = plp.MySparseMatrix.ClassDescriptionDictionary[lables[0]];
            }
            if (TextBoxC2Name.Text.Equals("Group") && lables.Count >= 2 && plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(lables[1]) || allClassDescriptions.ContainsValue(TextBoxC2Name.Text))
            {
                TextBoxC2Name.Text = plp.MySparseMatrix.ClassDescriptionDictionary[lables[1]];
            }
            if (TextBoxC3Name.Text.Equals("Group") && lables.Count >= 3 && plp.MySparseMatrix.ClassDescriptionDictionary.ContainsKey(lables[2]) || allClassDescriptions.ContainsValue(TextBoxC3Name.Text))
            {
                TextBoxC3Name.Text = plp.MySparseMatrix.ClassDescriptionDictionary[lables[2]];
            }

            if (lables.Count != 2)
            {
                RadioButtonFilteringProbability.IsEnabled = false;
            }


        }

        private void ButtonPlot_Click(object sender, RoutedEventArgs e)
        {
            WaitWindowWPF.Visibility = Visibility.Visible;
            Plot2();

            WaitWindowWPF.Visibility = Visibility.Collapsed;
        }

        void AllowUIToUpdate()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Render, new DispatcherOperationCallback(delegate (object parameter)
            {
                frame.Continue = false;
                return null;
            }), null);
            Dispatcher.PushFrame(frame);
        }

        private void Plot2()
        {

                belaGraphControl2.ClearDataBuffer();

                Font myFont = new Font(ComboBoxFonts.SelectedValue.ToString(), (float)DoubleUpDownFontSize.Value);
                belaGraphControl2.Plot(BelaGraph.BackgroundOption.None, false, true, myFont);

                //Lets clone our sparse matrix
                SparseMatrix vennSparseMatrixCache = PatternTools.ObjectCopier.Clone(plp.MySparseMatrix);


                List<IntersectionResultAnalysis> VennProbabilityDictionary = new List<IntersectionResultAnalysis>();

                //Qaulity filterS
                if ((bool)RadioButtonMinNoReplicates.IsChecked)
                {
                    //BY NUMBER OF REPLICATES
                    vennSparseMatrixCache.EliminateIDsThatAreNotPresentAtLeastInXReplicates((int)IntegerUpDownMinNoReplicates.Value, (bool)RadioButtonAllClasses.IsChecked);
                }
                else
                {
                    //by Probability
                    PatternTools.VennProbability.VennProbabilityCalculator vpc = new PatternTools.VennProbability.VennProbabilityCalculator(plp.MySparseMatrix);
                    VennProbabilityDictionary = vpc.GenerateProbabilisticDictionary(4);

                    string report = PatternTools.VennProbability.ResultPrinter.PrintReport(VennProbabilityDictionary, new List<string> { "Low", "Medium", "High", "Very High" });
                    RichTextBoxLog.Document.Blocks.Clear();
                    RichTextBoxLog.AppendText(report);
                }

                venn = new BelaGraph.VennManager(vennSparseMatrixCache);

                //--------------------------------


                //If we are working with probabilities, we should apply a correction
                //The venn diagram with probability only works for two classes 

                if ((bool)RadioButtonFilteringProbability.IsChecked)
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
                            if (!checkConfidence(dim, i, (double)DoubleUpDownProbability.Value, VennProbabilityDictionary))
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
                ButtonPlot.Content = "Please wait...";

                belaGraphControl2.ClearDataBuffer();

                //Lets save this matrix so we can do reports latter
                belaGraphControl2.vennSparseMatrixCache = vennSparseMatrixCache;

                belaGraphControl2.Title = TextBoxTitle.Text;
                belaGraphControl2.VennRadiusOfLargestCircleCorrectionFactor = (double)DoubleUpDownScaleFactor.Value;

                if (lables.Count <= 3)
                {

                    for (int i = 0; i < dvs.Count; i++)
                    {
                        System.Windows.Media.Color c1 = new System.Windows.Media.Color();

                        if (i == 0)
                        {
                            c1 = Colors.Green;
                        }
                        else if (i == 1)
                        {
                            c1 = Colors.Yellow;
                        }
                        else if (i == 2)
                        {
                            c1 = Colors.Blue;
                        }
                        System.Windows.Media.Color c2 = System.Windows.Media.Color.FromArgb(c1.A, c1.R, c1.G, c1.B);
                        System.Windows.Media.SolidColorBrush sb = new System.Windows.Media.SolidColorBrush(c2);
                        sb.Opacity = double.Parse(DoubleUpDownTransparency.Value.ToString());
                        dvs[i].MyBrush = sb;
                        belaGraphControl2.AddDataVector(dvs[i]);
                    }
                    BelaGraph.BackgroundOption bg = BelaGraph.BackgroundOption.None;
                    belaGraphControl2.Plot(bg, false, true, myFont);
                }

                //Fill out our table;
                DataGridPairWise.Columns.Clear();

                DataTable dt = new DataTable();
                //Add the columns

                //Add the info


                for (int i = 0; i < dvs.Count; i++)
                {
                    dt.Columns.Add(plp.MySparseMatrix.ClassDescriptionDictionary[int.Parse(dvs[i].Name)]);
                }

                for (int i = 0; i < dvs.Count; i++)
                {
                    dt.Rows.Add(dvs[i].ThePoints.Select(a => a.Y.ToString()).ToArray());
                }

                DataGridPairWise.ItemsSource = dt.DefaultView;

                if ((bool)CheckBoxPlotLabels.IsChecked && lables.Count <= 3)
                {
                    try
                    {
                        belaGraphControl2.PlotVennLabels(venn.VennDic, TextBoxC1Name.Text, TextBoxC2Name.Text, TextBoxC3Name.Text, true, (bool)CheckBoxDetailed.IsChecked, plp.MyIndex, myFont);
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show("Make sure your sparse matrix file is labeled correctly.  It should contain class 1, 2, and, 3" + "\n" + e2.Message);
                    }
                }

                ButtonPlot.Content = "Plot";
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


        private void ButtonSavePlot_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Plot";
            sfd.Filter = "PNG file (*.png)|*.png";

            if (sfd.ShowDialog() != true) { return; }
            belaGraphControl2.SaveGraph(sfd.FileName);
            MessageBox.Show("File Saved!");
        }

        private void DataGridPairWise_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentCell = DataGridPairWise.CurrentCell;
            var column = currentCell.Column;
            var currentRow = currentCell.Item;
            string columnHeader = column.Header.ToString();

            int indexColumn = currentCell.Column.DisplayIndex;
            int indexRow = DataGridPairWise.Items.IndexOf(currentCell.Item);

            string cellValue = ((DataRow)((DataRowView)currentRow).Row).ItemArray[indexColumn].ToString();

            Console.WriteLine("Value -> " + cellValue);
            List<int> classesToVerify = new List<int>() { indexColumn + 1, indexRow + 1 };

            //Get the common indexes---------------------
            List<int> commonIndexes = (from smr in plp.MySparseMatrix.theMatrixInRows
                                       where smr.Lable == classesToVerify[0]
                                       from d in smr.Dims
                                       select d).ToList();

            if (classesToVerify.Count > 1)
            {
                for (int i = 1; i < classesToVerify.Count; i++)
                {
                    List<int> fromThisClass = (from smr in plp.MySparseMatrix.theMatrixInRows
                                               where smr.Lable == classesToVerify[i]
                                               from d in smr.Dims
                                               select d).ToList();

                    commonIndexes = commonIndexes.Intersect(fromThisClass).ToList();

                } 
            }

            //-------------------------------------
            List<ReportItem> reportItems = new List<ReportItem>();
            foreach (int i in commonIndexes)
            {
                //Find out in how many input vectors of the sparse matrix this index appears
                int apperanceCounter = 0;
                double totalSpecCount = 0;
                List<string> experimentsFound = new List<string>();
                foreach (sparseMatrixRow smr in plp.MySparseMatrix.theMatrixInRows)
                {
                    if (smr.Dims.Contains(i))
                    {
                        int itsIndex = smr.Dims.IndexOf(i);
                        totalSpecCount += smr.Values[itsIndex];
                        apperanceCounter++;
                        experimentsFound.Add(smr.FileName);
                    }
                }

                ReportItem ri = new ReportItem(plp.MyIndex.GetName(i), experimentsFound, experimentsFound.Count, totalSpecCount, plp.MyIndex.GetDescription(i));
                reportItems.Add(ri);
            }

            //-----------------------------------
            ProteinReport pr = new ProteinReport();
            pr.MyReportItems = reportItems;
            pr.PlotReport();
            pr.ShowDialog();
        }

        private void DataGridPairWise_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            int ri = e.Row.GetIndex() + 1;
            string title = plp.MySparseMatrix.ClassDescriptionDictionary[ri];
            e.Row.Header = title;
        }
    }
}
