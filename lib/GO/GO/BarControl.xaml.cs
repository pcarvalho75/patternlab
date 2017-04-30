using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PatternTools;
using PLP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace GO
{
    /// <summary>
    /// Interaction logic for BarControl.xaml
    /// </summary>
    public partial class BarControl : UserControl
    {
        public BarControl()
        {
            InitializeComponent();
        }

        public void Plot(List<TermScoreCalculator.TermScoreAnalysis> terms, PatternLabProject plp, int fontSize)
        {
            PlotModel MyModel = new PlotModel();
            MyModel.Title = "Identification distribution along selected GO Terms";

            var categoryAxis1 = new CategoryAxis();
            categoryAxis1.Position = AxisPosition.Left;
            categoryAxis1.FontSize = fontSize;
            MyModel.Axes.Add(categoryAxis1);

            var linearAxis1 = new LinearAxis();
            linearAxis1.Position = AxisPosition.Bottom;
            linearAxis1.FontSize = fontSize;
            MyModel.Axes.Add(linearAxis1);


            if (plp == null)
            {
                var barSeries1 = new BarSeries();
                MyModel.Series.Add(barSeries1);

                foreach (var tsa in terms)
                {
                    categoryAxis1.Labels.Add(tsa.TermName);
                    barSeries1.Items.Add(new BarItem(tsa.ProteinIDs.Keys.Count, -1));
                }
            }
            else
            {
                SparseMatrix sm = plp.MySparseMatrix.ShatterMatrixSum();

                List<int> labels = sm.ExtractLabels();

                Dictionary <int, BarSeries> barDict = new Dictionary<int, BarSeries>();

                foreach (KeyValuePair<int, string> kvp in sm.ClassDescriptionDictionary)
                {
                    BarSeries bs = new BarSeries();
                    bs.Title = kvp.Value;
                    barDict.Add(kvp.Key, bs);

                    MyModel.Series.Add(bs);
                }

                foreach (var tsa in terms)
                {

                    int globalProtCounter = 0;

                    Dictionary<int, BarItem> tmpDict = new Dictionary<int, BarItem>();
                    foreach (int l in labels)
                    {
                        sparseMatrixRow theRow = sm.theMatrixInRows.Find(a => a.Lable == l);

                        int protCounter = 0;
                        foreach (string p in tsa.ProteinIDs.Keys)
                        {
                            //Search for proteins in this class
                            string cleanID = PatternTools.pTools.CleanPeptide(p, true);
                            int index = plp.MyIndex.TheIndexes.FindIndex(a => a.Name.Equals(cleanID));

                            if (index > -1)
                            {
                                double value = theRow.Values[index];

                                if (value > 0)
                                {
                                    protCounter++;
                                    globalProtCounter++;
                                } else
                                {
                                    Debug.Assert(true, "Protein not found");
                                }
                            }
                        }

                        //We need to use this as a work around as there are some terms that get 0 prots.
                        tmpDict.Add(l, new BarItem(protCounter, -1));                  
                    }

                    if (globalProtCounter > 0)
                    {
                        categoryAxis1.Labels.Add(tsa.TermName);
                        foreach (KeyValuePair<int, BarItem> kvp in tmpDict)
                        {
                            barDict[kvp.Key].Items.Add(kvp.Value);
                        }
                    }

                }
            }
            

            MyPlot.Model = MyModel;
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
    }
}
