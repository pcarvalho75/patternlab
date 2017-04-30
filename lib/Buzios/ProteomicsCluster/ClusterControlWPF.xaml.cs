using Accord.Statistics.Analysis;
using Accord.Statistics.Kernels;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Series;
using PatternTools;
using PLP;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Buzios
{
    /// <summary>
    /// Interaction logic for ClusterControlWPF.xaml
    /// </summary>
    public partial class ClusterControlWPF : UserControl
    {
        List<string> kernels = new List<string>() { "Gaussian", "Linear", "Polynomial", "Power", "Quadratic", "Sigmoid",  "Spline" };

        public ClusterControlWPF()
        {
            InitializeComponent();
            ComboBoxKPCAKernels.ItemsSource = kernels;
            ComboBoxKPCAKernels.SelectedIndex = 1;

        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PatternLab project file (*.plp)|*.plp";
            ofd.FileName = "";

            if (ofd.ShowDialog() == true)
            {
                //- do the work
                ButtonLoad.Content = "Working";
                this.UpdateLayout();

                TextBoxLegend.Clear();

                TextBoxPatternLabProjectFile.Text = ofd.FileName;

                PatternLabProject plp = new PatternLabProject(ofd.FileName);

                plp.MySparseMatrix.UnsparseTheMatrix();


                foreach (sparseMatrixRow unitSparseMatrixRow in plp.MySparseMatrix.theMatrixInRows)
                {
                    unitSparseMatrixRow.Values = PatternTools.pTools.UnitVector(unitSparseMatrixRow.Values);
                }

                //Do the clustering
                SparseMatrix smCLuster = new SparseMatrix();

                if ((bool)RadioKernelPCA.IsChecked)
                {

                    double[,] sm = plp.MySparseMatrix.ToDoubleArrayMatrix();

                    IKernel kernel = null;

                    //"Gaussian", "Linear", "Power", "Quadratic", "Sigmoid",  "Spline"

                    if (ComboBoxKPCAKernels.SelectedValue.Equals("Gaussian"))
                    {
                        kernel = new Gaussian();
                    } else if (ComboBoxKPCAKernels.SelectedValue.Equals("Linear"))
                    {
                        kernel = new Linear();
                    } else if (ComboBoxKPCAKernels.SelectedValue.Equals("Power"))
                    {
                        kernel = new Power(2);
                    } else if  (ComboBoxKPCAKernels.SelectedValue.Equals("Quadratic"))
                    {
                        kernel = new Quadratic();
                    } else if (ComboBoxKPCAKernels.SelectedValue.Equals("Sigmoid"))
                    {
                        kernel = new Sigmoid();
                    } else
                    {
                        kernel = new Spline();
                    }
                   

                    // Creates the Kernel Principal Component Analysis of the given data
                    var kpca = new KernelPrincipalComponentAnalysis(sm, kernel);

                    // Compute the Kernel Principal Component Analysis
                    kpca.Compute();

                    // Creates a projection of the information
                    double[,] components = kpca.Transform(sm, 2);
                  

                    for (int j = 0; j < components.GetLength(0); j++)
                    {
                        int l = plp.MySparseMatrix.theMatrixInRows[j].Lable;
                        sparseMatrixRow smr = new sparseMatrixRow(l, new List<int>() { 0, 1 }, new List<double>() { components[j, 0], components[j, 1] });
                        smr.FileName = plp.MySparseMatrix.theMatrixInRows[j].FileName;
                        smCLuster.addRow(smr);
                    }

                    smCLuster.ClassDescriptionDictionary = plp.MySparseMatrix.ClassDescriptionDictionary;

                    

                } else
                {
                    MDS2 mds2 = new MDS2(plp.MySparseMatrix);
                    smCLuster = mds2.Converge(250, (double)DoubleUpDownSpringOutlier.Value);
                }

                Plot(smCLuster);

                ButtonLoad.Content = "Browse";

            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG files (.png)|*.png";

            if (dlg.ShowDialog() == true)
            {
                MyPlot.SaveBitmap(dlg.FileName);
                MessageBox.Show("Chart saved");
            }
        }

        private void Plot(SparseMatrix sm) { 

            PlotModel MyModel = new PlotModel();

            MyModel.LegendOrientation = LegendOrientation.Horizontal;
            MyModel.LegendPlacement = LegendPlacement.Outside;
            MyModel.LegendPosition = LegendPosition.BottomCenter;

            int legendCounter = 0;
            foreach (var kvp in sm.ClassDescriptionDictionary)
            {
                
                var scatterSeries = new ScatterSeries();
                //scatterSeriesBlue.MarkerStroke = OxyColors.DarkBlue;
                //scatterSeriesBlue.MarkerFill = OxyColors.LightBlue;
                scatterSeries.MarkerType = MarkerType.Circle;
                scatterSeries.Title = kvp.Value;


                List<sparseMatrixRow> rows = sm.theMatrixInRows.FindAll(a => a.Lable == kvp.Key);

                TextBoxLegend.Text = "#Label\tFileName\tX\tY";

                for (int i = 0; i < rows.Count; i++)
                {
                    legendCounter++;
                    ScatterPoint sp = new ScatterPoint(rows[i].Values[0], rows[i].Values[1], 3);
                    sp.Tag = rows[i].FileName;
                    sp.Size = 4;
                    scatterSeries.Points.Add(sp);

                    if ((bool)CheckboxPrintLabels.IsChecked)
                    {
                        var textAnnotation = new TextAnnotation();
                        textAnnotation.ToolTip = rows[i].FileName;
                        textAnnotation.FontSize = 14;
                        textAnnotation.Text = legendCounter.ToString();
                        textAnnotation.TextPosition = new DataPoint(rows[i].Values[0], rows[i].Values[1]);
                        MyModel.Annotations.Add(textAnnotation);
                        TextBoxLegend.Text += legendCounter + "\t" + rows[i].FileName + "\t" + Math.Round(rows[i].Values[0],3) +  "\t" + Math.Round(rows[i].Values[1],3) + "\n";
                    }

                }

                MyModel.Series.Add(scatterSeries);

            }
            

            MyPlot.Model = MyModel;
        }

    }
}
