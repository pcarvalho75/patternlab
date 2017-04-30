using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
using static PatternTools.HistogramHelper;

namespace Histogram
{
    /// <summary>
    /// Interaction logic for HistogramPlot.xaml
    /// </summary>
    public partial class HistogramPlot : UserControl
    {
        public HistogramPlot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theBins">Each key represents a series</param>
        /// <param name="yMax"></param>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        public void Plot(Dictionary<int, List<PatternTools.HistogramHelper.HistogramBin>> theBins, double yMax, double xMin, double xMax, string title, string titleXAxis, string titleYAxis)
        {
            PlotModel MyModel = new PlotModel();

            MyModel.LegendBorderThickness = 0;
            MyModel.LegendOrientation = LegendOrientation.Horizontal;
            MyModel.LegendPlacement = LegendPlacement.Outside;
            MyModel.LegendPosition = LegendPosition.BottomCenter;
            MyModel.Title = title;


            List <int> keys = theBins.Keys.ToList();
            keys.Sort();

            if (theBins[keys[0]].Count == 0)
            {
                Console.WriteLine("There is no data to plot!");
                return;
            }

            var categoryAxis1 = new CategoryAxis();
            categoryAxis1.Angle = 45;
            foreach (HistogramBin bin in theBins[keys[0]])
            {
                categoryAxis1.Labels.Add(Math.Round(bin.IntervalFloor,4).ToString());
            }
            categoryAxis1.Title = titleXAxis;
            MyModel.Axes.Add(categoryAxis1);

            var linearAxis1 = new LinearAxis();
            linearAxis1.Title = titleYAxis;

            if (yMax != -1)
            {
                linearAxis1.Maximum = yMax;
            }
            MyModel.Axes.Add(linearAxis1);


            foreach (int k in keys)
            {  
                ColumnSeries cs = new ColumnSeries();
                foreach (HistogramBin bin in theBins[k])
                {
                    cs.Items.Add(new ColumnItem(bin.TheData.Count));
                }             
                
                MyModel.Series.Add(cs);
            }

            MyPlot.Model = MyModel;
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "IsobaricAnalyzerChart";
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG files (.png)|*.png";

            if (dlg.ShowDialog() == true)
            {
                MyPlot.SaveBitmap(dlg.FileName);
                Console.WriteLine("Chart saved");

            }
        }
    }
}
