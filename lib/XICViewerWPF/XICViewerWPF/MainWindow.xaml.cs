using OxyPlot;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonPlotExample_Click(object sender, RoutedEventArgs e)
        {
            double refMass = 500;

            string seriesName = "Run A";
            List<IonLight> theIons = new List<IonLight>() { new IonLight(55.5, 100, 10, 20), new IonLight(55.56, 120, 11, 23), new IonLight(55.55, 90, 12, 27) };
            List<int> ms2ScanNo = new List<int>() { 24 };

            string seriesNameB = "Run B";
            List<IonLight> theIonsB = new List<IonLight>() { new IonLight(55.5, 90, 10, 20), new IonLight(55.56, 150, 11, 23), new IonLight(55.55, 88, 12, 27) };
            List<int> ms2ScanNoB = new List<int>() { 30 };

            Tuple<int, string, List<IonLight>, List<int>> t = new Tuple<int, string, List<IonLight>, List<int>>(2, seriesName, theIons, ms2ScanNo);
            Tuple<int, string, List<IonLight>, List<int>> t2 = new Tuple<int, string, List<IonLight>, List<int>>(3, seriesNameB, theIonsB, ms2ScanNoB);

            MyXICView.ReferenceMass = refMass;
            MyXICView.MyData = new List<Tuple<int, string, List<IonLight>, List<int>>>() { t, t2 };
            MyXICView.Plot();
        }
    }
}
