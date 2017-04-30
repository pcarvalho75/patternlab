using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatternTools.XIC;
using PatternTools.Deisotoping;
using PatternTools.SQTParser;
using System.Diagnostics;
using XQuant.Quants;
using PatternTools.MSParser;

namespace XQuant
{
    public partial class CoreXtractor : UserControl
    {
        XICGet5 getter;
        SignalGenerator isotopicSignal = new SignalGenerator();

        public CoreXtractor()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Console.WriteLine("Loading file " + openFileDialog1.FileName);
                getter = new XICGet5(openFileDialog1.FileName);
                buttonLoad.Text = openFileDialog1.FileName;
                groupBoxXtract.Enabled = true;
            }
        }

        private void buttonXtract_Click(object sender, EventArgs e)
        {
            if (textBoxScanNo.Text.Length == 0)
            {
                MessageBox.Show("Please enter a valid scan number");
                return;
            }
            double theoreticalMH = double.Parse(textBoxTheoreticalMass.Text);
            int scanNo = int.Parse(textBoxScanNo.Text);

            SQTLight s = new SQTLight(null);
            s.PeptideSequenceCleaned = "N/A";
            XQuantClusteringParameters myParams = parameters1.GetFromScreen();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Dictionary<string, List<Quant>> theQuants = Core35.Quant(getter, s, theoreticalMH, isotopicSignal, scanNo, myParams);
            
            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

            foreach (var kvp in theQuants)
            {
                foreach (Quant q in kvp.Value)
                {
                    //XICViewer viewer = new XICViewer();

                    //viewer.Plot(q.PrecursorMZ, q.GetIonsLight(), new List<int>() { q.ScanNoMS2 });
                    //viewer.ShowDialog();
                }
            }
        }
    }
}
