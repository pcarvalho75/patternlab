using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PatternTools.SpectraPrediction;

namespace SEProcessor.Result
{
    /// <summary>
    /// Interaction logic for SpecBrowserControl.xaml
    /// </summary>
    public partial class SpecBrowserControl : UserControl
    {
        public SpecBrowserControl()
        {
            InitializeComponent();
        }

        internal void Print(PatternTools.MSParserLight.MSLight ms, string cleanedPeptideSequence, double ms2PPM, int chargeState, double theoreticalMH, double relativeIntensityThreshold)
        {
            mSViewer1.MyMSLight = ms;
            mSViewer1.PeptideSequence = cleanedPeptideSequence;
            mSViewer1.ChargeState = chargeState;
            mSViewer1.TheoreticalMH = theoreticalMH;
            mSViewer1.RelativeIntensityThreshold = relativeIntensityThreshold;
            mSViewer1.FuncPrintMS();
            mSViewer1.SetMS2PPM = ms2PPM;

            if (string.IsNullOrEmpty(ms.ActivationType))
            {
                MessageBox.Show("Activation type not found in MS2 file.  Was it extracted with RawXtract?\nAssuming it is CID.");
                mSViewer1.SetToCIDMode();

            } else if (ms.ActivationType.Contains("CID"))
            {
                mSViewer1.SetToCIDMode();
            }
            else if (ms.ActivationType.Contains("ETD"))
            {
                mSViewer1.SetToETDMode();
            }
        }

        private void mSViewer1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
