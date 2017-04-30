using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PatternTools.MSParser;
using SEPRPackage;
using PatternTools.SQTParser;
using System.IO;

namespace SEProcessor
{
    public partial class SaveMS2 : Form
    {
        public SaveMS2()
        {
            InitializeComponent();
        }

        public ProteinManager MyProteinManager { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> chargeStates = new List<int>();
            if (checkBoxZ1.Checked) {
                chargeStates.Add(1);
            }

            if (checkBoxZ2.Checked) {
                chargeStates.Add(2);
            }

            if (checkBoxZ3.Checked) {
                chargeStates.Add(3);
            }

            saveFileDialog1.Title = "Save .ms2 of identified spectra";
            saveFileDialog1.Filter = "MS2 File (*.ms2)|*.ms2";

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {


                StreamWriter result = new StreamWriter(saveFileDialog1.FileName);

                MyProteinManager.AllSQTScans.Sort((a, b) => a.ScanNumber.CompareTo(b.ScanNumber));

                foreach (SQTScan sqt in MyProteinManager.AllSQTScans)
                {
                    if (chargeStates.Contains(sqt.ChargeState) || (checkBoxZ4andUP.Checked && sqt.ChargeState > 3))
                    {
                        PatternTools.MSParserLight.MSLightPrinter.PrintSpectrumMS2(result, sqt.MSLight, sqt.PeptideSequenceCleaned, sqt.FileName);
                    }
                }

                result.Close();
            }
        }
    }
}
