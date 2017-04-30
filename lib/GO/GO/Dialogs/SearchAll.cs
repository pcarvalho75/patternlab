using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GO
{
    public partial class SearchAll : Form
    {
        bool work;

        public double HyperGeometricPValueCutOff
        {
            get { return (double)numericUpDownHyperGeometricPValue.Value; }
        }

        public bool Work
        {
            get { return work; }
        }

        public int MinimumDepth
        {
            get { return (int)numericUpDownMinimumDepth.Value; }
        }

        public int MinimumNumberOfTerms
        {
            get { return (int)numericUpDownMinimumNumberOfTerms.Value ; }
        }

        public bool FDR
        {
            get { return (checkBoxFDR.Checked); }
        }

        public double FDRAlfa
        {
            get { return (double)numericUpDownFDRAlfa.Value ; }
        }

        public SearchAll()
        {
            InitializeComponent();
        }

        private void kryptonButtonCheckBox_Changed(object sender, EventArgs e)
        {
            if (checkBoxFDR.Checked)
            {
                numericUpDownFDRAlfa.Enabled = true;
            }
            else
            {
                numericUpDownFDRAlfa.Enabled = false;
            }
        }

        private void kryptonButtonWork_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonWork_Click(object sender, EventArgs e)
        {
            work = true;
            this.Close();
        }

        private void checkBoxFDR_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFDR.Checked)
            {
                numericUpDownFDRAlfa.Enabled = true;
            }
            else
            {
                numericUpDownFDRAlfa.Enabled = false;
            }
        }
    }
}
