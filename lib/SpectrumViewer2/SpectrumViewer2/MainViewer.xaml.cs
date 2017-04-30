using PatternTools.MSParserLight;
using PatternTools.PTMMods;
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
    /// Interaction logic for MainViewer.xaml
    /// </summary>
    public partial class MainViewer : UserControl
    {
        public MainViewer()
        {
            InitializeComponent();
        }

        public void PlotAnnotatedPeptide (List<Tuple<string,string,string>> annotatedSequece)
        {
            SequenceAnotation1.Plot(annotatedSequece);
        }

        public void PlotSpectrum(MSLight msExperimental)
        {
            SpectrumEye1.Plot(msExperimental.Ions, msExperimental.Ions[0].MZ, msExperimental.Ions.Last().MZ, 0);
        }

        public void PlotAnotatedSpectrum(MSLight msExperimental, double ppm, string peptide)
        {

            //Predict the spectrum
            //Find PTMs

            List<ModificationItem> mods = SpectraPredictor.GetVMods(peptide);
            SpectralPredictionParameters spp = new SpectralPredictionParameters(false, true, false, false, true, false, true, true, 2, true, mods);
            SpectraPredictor sp = new SpectraPredictor(spp);
            List<PredictedIon> theoreticalSpectrum = sp.PredictPeaks(peptide, 2, 1950, msExperimental, 500);

            DataGridResultTable.ItemsSource = theoreticalSpectrum;

            

            SpectrumEye1.Plot(msExperimental.Ions, msExperimental.Ions[0].MZ, msExperimental.Ions.Last().MZ, ppm, theoreticalSpectrum);
        }
    }
}
