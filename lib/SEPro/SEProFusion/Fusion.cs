using PatternTools.FastaParser;
using PatternTools.SQTParser;
using SEPRPackage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEProcessor.SEProFusion
{
    public partial class Fusion : Form
    {
        public Fusion()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBoxInputDirectory.Text))
            {
                richTextBoxLog.AppendText("Directory " + textBoxInputDirectory.Text + " does not exist");
                return;
            }

            DirectoryInfo di = new DirectoryInfo(textBoxInputDirectory.Text);

            List<FileInfo> SEProFiles = di.GetFiles("*.sepr", SearchOption.AllDirectories).ToList();

            if (SEProFiles.Count == 0)
            {
                MessageBox.Show("No SEPro files found in the provided directory.");
                return;
            }

            List<SQTScan> allScans = new List<SQTScan>();
            Parameters theparams = new Parameters();
            List<string> allProteinIDs = new List<string>();
            string database = "";
            List<FastaItem> fastaItems = new List<FastaItem>();
            foreach (FileInfo fi in SEProFiles)
            {
                richTextBoxLog.AppendText("Loading " + fi.Name + "\n");
                this.Update();
                ResultPackage rp = ResultPackage.Load(fi.FullName);
                allScans.AddRange(rp.MyProteins.AllSQTScans);
                theparams = rp.MyParameters;
                allProteinIDs.AddRange(rp.MyProteins.MyProteinList.Select(a => a.Locus).ToList());

                foreach (MyProtein p in rp.MyProteins.MyProteinList)
                {
                    if (!fastaItems.Exists(a => a.SequenceIdentifier.Equals(p.Locus)))
                    {
                        FastaItem fasta = new FastaItem();
                        fasta.Description = p.Description;
                        fasta.Sequence = p.Sequence;
                        fasta.SequenceIdentifier = p.Locus;
                        fastaItems.Add(fasta);
                    }
                }

                database = rp.Database;
            }

            richTextBoxLog.AppendText("Generating new SEPro file\n");
            this.Update();
         
            theparams.SeachResultDirectoy = "Fusion SEPro file of" + string.Join(",", SEProFiles.Select(a => a.FullName).ToList());
            ProteinManager pm = new ProteinManager(allScans, theparams, allProteinIDs.Distinct().ToList());

            pm.CalculateProteinCoverage(fastaItems);
            pm.GroupProteinsHavingCommonPeptides(1);
            
            saveFileDialog1.Filter = "SEPro files (*.sepr)|*.sepr";

            saveFileDialog1.InitialDirectory = textBoxInputDirectory.Text;
            saveFileDialog1.FileName = "Fusion_";
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {

                ResultPackage rp = new ResultPackage(pm, theparams, database, theparams.SeachResultDirectoy, false);
                rp.MyProteins.RebuildProteinsFromScans();
                rp.Save(saveFileDialog1.FileName);
            }

            MessageBox.Show("Done.");

        }
    }
}
