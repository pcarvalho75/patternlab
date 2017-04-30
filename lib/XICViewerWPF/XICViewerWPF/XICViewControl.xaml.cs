using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PatternTools.MSParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace XICViewerWPF
{
    /// <summary>
    /// Interaction logic for XICViewControl.xaml
    /// </summary>
    public partial class XICViewControl : UserControl
    {
        public XICViewControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// int label, string seriesName, List<IonLight> myIons, List<int> ms2ScanNumbers
        /// </summary>
        public List<Tuple<int, string, List<IonLight>, List<int>>> MyData { get; set; }
        public double ReferenceMass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceMass">The mass used to obtain the XIC</param>
        public void Plot()
        {
            tabControlTheTabs.Items.Clear();

            //Generate a list of colors
            List<Tuple<string, double[]>> theColors = new List<Tuple<string, double[]>>();
            Type brushesType = typeof(System.Windows.Media.Brushes);

            // Get all static properties
            var properties = brushesType.GetProperties(BindingFlags.Static | BindingFlags.Public);

            foreach (var prop in properties)
            {
                string name = prop.Name;
                SolidColorBrush brush = (SolidColorBrush)prop.GetValue(null, null);

                Color color = brush.Color;

                theColors.Add(new Tuple<string, double[]>(name, new double[] { color.A, color.R, color.G, color.B }));

                //Console.WriteLine("Brush named {0} has RGB colors of {1}{2}{2}", name, color.R, color.G, color.B);
            }


            PlotModel MyModel = new PlotModel();

            MyModel.Title = "Reference m/z : " + ReferenceMass;


            //Find the division factor
            IonLight mostIntenseIon = new IonLight(0, 0, 0, 0);
            foreach (Tuple<int, string, List<IonLight>, List<int>> t in MyData)
            {
                double maxInt = t.Item3.Select(a => a.Intensity).Max();
                IonLight i = t.Item3.Find(a => a.Intensity == maxInt);

                if (i.Intensity > mostIntenseIon.Intensity)
                {
                    mostIntenseIon = i;
                }

            }


            //Calculate division factor
            double f1 = Math.Floor(Math.Log10(mostIntenseIon.Intensity));
            double factor = Math.Pow(10, f1);
            Console.WriteLine("Factor = " + factor);

            var linearAxis1 = new LinearAxis();
            linearAxis1.Title = "Intensity x 10E" + f1;
            MyModel.Axes.Add(linearAxis1);

            var linearAxis2 = new LinearAxis();
            linearAxis2.Title = "Retention time";
            linearAxis2.Position = AxisPosition.Bottom;
            MyModel.Axes.Add(linearAxis2);

            List<Tuple<int, string, List<IonLight>, List<int>>> plotData = MyData;

            if ((bool)CheckBoxAlign.IsChecked)
            {
                plotData = AlignChromatograms(MyData);
            }


            foreach (var t in plotData)
            {
                LineSeries ls = new LineSeries();
                ls.Title = t.Item2;
                List<DataPoint> points = t.Item3.Select(a => new DataPoint(a.RetentionTime, a.Intensity / factor)).ToList();

                ls.Points.AddRange(points);
                ls.Smooth = true;
                ls.MarkerSize = 6;
                ls.MarkerType = MarkerType.Circle;
                ls.ToolTip = t.Item2;


                ScatterSeries ss = new ScatterSeries();

                foreach (int scanNo in t.Item4)
                {
                    //Find the closest time from the interpolated vector

                    int minDistance = t.Item3.Min(a => Math.Abs(a.ScanNumber - scanNo));
                    IonLight closestIon = t.Item3.Find(a => Math.Abs(a.ScanNumber - scanNo) == minDistance);

                    double x = closestIon.RetentionTime;
                    double y = closestIon.Intensity / factor;

                    ss.Points.Add(new ScatterPoint(x, y));
                }


                MyModel.Series.Add(ls);
                MyModel.Series.Add(ss);


                DataGrid dg = new DataGrid();
                dg.Background = Brushes.AliceBlue;
                dg.ItemsSource = t.Item3;

                TabItem ti = new TabItem();
                ti.Header = t.Item2;
                ti.Content = dg;

                tabControlTheTabs.Items.Add(ti);
            }

            
            MyPlot.Model = MyModel;         

        }


        public List<Tuple<int, string, List<IonLight>, List<int>>> AlignChromatograms(List<Tuple<int, string, List<IonLight>, List<int>>> theData)
        {
            List<Tuple<int, string, List<IonLight>, List<int>>> alignedData = new List<Tuple<int, string, List<IonLight>, List<int>>>();

            Tuple<double, double>[] massCenter = new Tuple<double, double>[theData.Count];

            //First we obtaine a weighted sum to find the center of each chromatogram
            for(int i = 0; i < theData.Count; i++)
            {
                double w = theData[i].Item3.Sum(a => a.Intensity);
                double wCenter = theData[i].Item3.Sum(a => a.RetentionTime * a.Intensity) / w;
                massCenter[i] = new Tuple<double, double>(wCenter, w);
            }

            //Now we determine the center of alignment
            double retentionCenter = massCenter.Sum(a => a.Item1 * a.Item2) / massCenter.Sum(a => a.Item2);

            //And now we shift everyone to this center
            for (int i = 0; i < massCenter.Length; i++)
            {
                //This delta
                double thisDelta = retentionCenter - massCenter[i].Item1;

                List<IonLight> alignedIons = (from ion in theData[i].Item3
                                              select new IonLight(ion.MZ, ion.Intensity, ion.RetentionTime + thisDelta, ion.ScanNumber)).ToList();

                alignedData.Add(new Tuple<int, string, List<IonLight>, List<int>>(theData[i].Item1, theData[i].Item2, alignedIons, theData[i].Item4));

            }

            return alignedData;

        }

        private void CheckBoxAlign_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Reploting");
            Plot();
        }
    }
}
