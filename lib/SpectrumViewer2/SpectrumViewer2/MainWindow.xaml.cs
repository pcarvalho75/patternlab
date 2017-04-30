using PatternTools.MSParserLight;
using PatternTools.SpectraPrediction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SpectrumViewer2
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

        private void ButtonLoadExample_Click(object sender, RoutedEventArgs e)
        {

            string peptide = TextBoxPeptide.Text;


            //Get the experimental
            string[] lines = Regex.Split(SpectrumViewer2.Properties.Resources.ExampleSpectrum, "\n");

            MSLight msExperimental = new MSLight();

            List<double> mz = new List<double>();
            List<double> intensities = new List<double>();

            foreach (string l in lines)
            {
                string[] mzI = Regex.Split(l, "\t");
                mz.Add(double.Parse(mzI[0]));
                intensities.Add(double.Parse(mzI[1]));
            }

            msExperimental.MZ = mz;
            msExperimental.Intensity = intensities;



            MainViewer1.PlotAnotatedSpectrum(msExperimental, 500, peptide);

            

        }
    }
}
