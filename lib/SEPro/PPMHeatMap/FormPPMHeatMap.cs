using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SEProcessor.LockMass;
using PatternTools.SQTParser;

namespace SEProcessor.PPMHeatMap
{
    public partial class FormPPMHeatMap : Form
    {
        MLLM myLockMass;

        public SEPRPackage.ResultPackage myResultPackage;

        public SEPRPackage.ResultPackage MyResultPackage {
            set
            {
                myResultPackage = value;                
            }
        }

        public FormPPMHeatMap()
        {
            InitializeComponent();
        }

        public MLLM LockMass {

            set
            {
                myLockMass = value;

                comboBoxFiles.Items.Clear();
                foreach (MLLMFileInterpolant interpolant in myLockMass.Interpolants) {
                    comboBoxFiles.Items.Add(interpolant.MyFile);
                }

                comboBoxFiles.SelectedIndex = 0;
            }

        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            buttonPlot.Text = "Working...";
            this.Update();

            MLLMFileInterpolant theInterpolant = myLockMass.Interpolants.First(a => a.MyFile.Equals(comboBoxFiles.Text));

            //ScanNumber
            double minSC = (double)numericUpDownMinScan.Value;
            double maxSC = (double)numericUpDownMaxScan.Value;

            double minMZ = (double)numericUpDownMinMZ.Value;
            double maxMZ = (double)numericUpDownMaxMZ.Value;

            //Generate a list of Lists
            double SCStep = (maxSC - minSC) / (double)numericUpDownStepsSCN.Value;
            double MZStep = (maxMZ - minMZ) / (double)numericUpDownStepsMZ.Value;

            List<List<double>> theMatrix = new List<List<double>>();
            for (double sn = minSC; sn <= maxSC; sn += SCStep)
            {

                List<double> mzValues = new List<double>((int)numericUpDownStepsMZ.Value);
                for (double mz = minMZ; mz <= maxMZ; mz += MZStep)
                {
                    mzValues.Add(theInterpolant.Interpolate(mz, sn, true));
                }
                theMatrix.Add(mzValues);
            }

            heatMap1.MyMatrix = theMatrix;

            heatMap1.Plot((double)numericUpDownScaleMinimum.Value, (double)numericUpDownScaleMiddle.Value, (double) numericUpDownScaleMaximum.Value, splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);

            //Now lets plot the spectra
            List<PatternTools.Point> theSpectra = new List<PatternTools.Point>();
            List<SQTScan> theScans = myResultPackage.MyProteins.AllSQTScans.FindAll(a => a.FileName.Equals((comboBoxFiles.Text)));


            foreach (SQTScan scan in theScans)
            {
                double thisMZ = GetMZ(scan);
                PatternTools.Point p = new PatternTools.Point( (((double)scan.ScanNumber - minSC) / (maxSC-minSC)), (thisMZ - minMZ) / (maxMZ-minMZ));
                p.Tip = "Scan number: " + scan.ScanNumber + " MZ: " + thisMZ;
                theSpectra.Add(p);
            }

            heatMap1.AddPointsToPlot(theSpectra);

            buttonPlot.Text = "Plot";

        }

        private void comboBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFiles.Text.Length > 0)
            {
                List<SQTScan> theScans = myResultPackage.MyProteins.AllSQTScans.FindAll(a => a.FileName.Equals(comboBoxFiles.Text));
                int minScanNumber = theScans.Min(a => a.ScanNumber);
                int maxScanNumber = theScans.Max(a => a.ScanNumber);
                double minMZ = theScans.Min(a => GetMZ(a));
                double maxMZ = theScans.Max(a => GetMZ(a));

                numericUpDownMinScan.Value = minScanNumber;
                numericUpDownMaxScan.Value = maxScanNumber;
                numericUpDownMinMZ.Value = (decimal)minMZ;
                numericUpDownMaxMZ.Value = (decimal)maxMZ;

            }
        }

        private double GetMZ(SQTScan scan)
        {
            return (scan.MeasuredMH + ((double)scan.ChargeState - 1) * 1.007276466) / (double)scan.ChargeState;
        }
    }
}
