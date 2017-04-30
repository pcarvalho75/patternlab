using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SEPRPackage;
using System;
using System.Collections.Generic;
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

namespace SEProcessor.Result
{
    /// <summary>
    /// Interaction logic for LengthQuantCore.xaml
    /// </summary>
    public partial class LengthQuantCore : UserControl
    {
        public LengthQuantCore()
        {
            InitializeComponent();
        }

        public List<MyProtein> MyProteinList {get; set;}

        public void Plot()
        {
            if (MyProteinList == null) { return; }
            var plotModel1 = new PlotModel();
            plotModel1.Title = "Quant versus #aa correlation";

            var linearAxis1 = new LinearAxis();
            linearAxis1.Position = AxisPosition.Bottom;

            linearAxis1.Title = "MW";
            if (!(bool)RadioButonMW.IsChecked)
            {
                linearAxis1.Title = "# AA";
            }
            plotModel1.Axes.Add(linearAxis1);

            var linearAxis2 = new LinearAxis();
            linearAxis2.Title = "NSAF";
            if ((bool)RadioSpecCount.IsChecked)
            {
                linearAxis2.Title = "Spec Count";
            } else if ((bool)RadioSeqCount.IsChecked)
            {
                linearAxis2.Title = "Sequence Count";
            }
            plotModel1.Axes.Add(linearAxis2);

            var scatterSeries1 = new ScatterSeries();
            scatterSeries1.MarkerType = MarkerType.Circle;

            foreach (MyProtein p in MyProteinList)
            {
                double x = p.NSAF;
                double y = p.MolWt;

                if (!(bool)RadioButonMW.IsChecked)
                {
                    y = p.Sequence.Length;
                }

                if ((bool)RadioSpecCount.IsChecked)
                {
                    if ((bool)CheckboxOnlyUniquePeptides.IsChecked)
                    {
                        List<PeptideResult> peps = p.PeptideResults.FindAll(a => a.MyMapableProteins.Count == 1).ToList();
                        x = peps.Sum(a => a.NoMyScans);
                    }
                    else
                    {
                        x = p.Scans.Count;
                    }
                }
                else if ((bool)RadioSeqCount.IsChecked)
                {
                    if ((bool)CheckboxOnlyUniquePeptides.IsChecked)
                    {
                        x = p.PeptideResults.FindAll(a => a.MyMapableProteins.Count == 1).Count;
                    }
                    else
                    {
                        x = p.PeptideResults.Count;
                    }
                }

                if (x > 0)
                {
                    scatterSeries1.Points.Add(new ScatterPoint(y, x, 3, 0, null));
                }
            }

            plotModel1.Series.Add(scatterSeries1);

            MyPlot.Model = plotModel1;

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

        private void RadioNSAF_Checked(object sender, RoutedEventArgs e)
        {
            Plot();
        }

        private void RadioSpecCount_Checked(object sender, RoutedEventArgs e)
        {
            Plot();
        }

        private void CheckboxOnlyUniquePeptides_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)CheckboxOnlyUniquePeptides.IsChecked)
            {
                RadioSpecCount.IsChecked = true;
            }
            Plot();
        }

        private void RadioButtonNoAA_Checked(object sender, RoutedEventArgs e)
        {
            Plot();
        }

        private void RadioButonMW_Checked(object sender, RoutedEventArgs e)
        {
            Plot();
        }

        private void RadioSeqCount_Checked(object sender, RoutedEventArgs e)
        {
            Plot();
        }
    }

}
