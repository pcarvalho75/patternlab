using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PatternTools.MSParser;
using PatternTools.SpectraPrediction;
using PatternTools.PTMMods;

namespace PatternTools.SpectrumViewer
{
    public partial class SpectrumViewerForm : Form
    {
        public SpectrumViewerForm()
        {
            InitializeComponent();
        }


        public void PlotSpectrum(MSParserLight.MSLight mslight, double ppm, string peptideSequence, List<ModificationItem> myMods, bool etdMode)
        {
            MSFull msFull = new MSFull();

            for (int i = 0; i < mslight.MZ.Count; i++)
            {
                msFull.MSData.Add(new Ion(mslight.MZ[i], mslight.Intensity[i], 0, mslight.ScanNumber));
            }

            PlotSpectrum(msFull, ppm, peptideSequence, myMods, etdMode);

        }

        public void PlotSpectrum(MSFull ms, double ppm, string peptideSequence, List<ModificationItem> myMods, bool etdMode)
        {
            msViewer1.MyMS = ms;
            msViewer1.SetMS2PPM = ppm;
            msViewer1.PeptideSequence = peptideSequence;

            msViewer1.Modifications = myMods;
            
            
            if (etdMode)
            {
                msViewer1.SetToCIDMode();
            }
            else
            {
                msViewer1.SetToETDMode();
            }
            msViewer1.SetToCIDMode();
            msViewer1.Plot();
        }
    }
}
