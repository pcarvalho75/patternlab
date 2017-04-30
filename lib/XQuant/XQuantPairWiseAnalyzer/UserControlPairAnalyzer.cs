using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XQuant;
using System.Text.RegularExpressions;
using XQuant.Quants;
using PatternTools.FastaParser;
using XQuantPairWiseAnalyzer.PairAnalyzer;

namespace XQuantPairWiseAnalyzer
{
    public partial class UserControlPairAnalyzer : UserControl
    {
        PairAnalyzer.Core20Paired coreP;
        public UserControlPairAnalyzer()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FinishLoadingProcedures()
        {
            List<int> classes = coreP.core.myQuantPkgs.Select(a => a.ClassLabel).Distinct().ToList();
            checkedListBoxClasses.Items.Clear();

            foreach (int c in classes)
            {
                checkedListBoxClasses.Items.Add(c);
            }
        }

        private void buttonRunAnalysis_Click(object sender, EventArgs e)
        {
            buttonRunAnalysis.Text = "Working...";
            this.Update();

            List<int> acceptableClasses = new List<int>();

            foreach (var i in checkedListBoxClasses.CheckedItems)
            {
                acceptableClasses.Add(int.Parse(i.ToString()));
            }

            if (acceptableClasses.Count !=2)
            {
                MessageBox.Show("Two classes must be seleced for the pairwise comparison.");
                return;
            }

            
            coreP.RunPeptideAnalysis(checkBoxNormalize.Checked, acceptableClasses, checkBoxOnlyUniquePeptides.Checked, (int)numericUpDownMinMS1COunt.Value, (double)numericUpDownMinPeptideFold.Value);
            PrintPeptideAnalysis(acceptableClasses);
            buttonProteinReport.Enabled = true;

            buttonRunAnalysis.Text = "Run Core";

        }

        private void PrintPeptideAnalysis(List<int> acceptableClasses)
        {
            List<AssociationItem> pairs = coreP.core.MyAssociationItems.FindAll(a => acceptableClasses.Contains(a.Label));
            pairs.Sort((a, b) => a.Label.CompareTo(b.Label));

            //Build the matrix
            dataGridViewPeptideAnalysis.Rows.Clear();
            dataGridViewPeptideAnalysis.Columns.Clear();
            Console.WriteLine("Mounting peptide matrix");

            List<int> associations = pairs.Select(a => a.Assosication).Distinct().ToList();
            associations.Sort();

            foreach (int ai in associations)
            {
                var col = new DataGridViewTextBoxColumn();
                col.HeaderText = ai.ToString();
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewPeptideAnalysis.Columns.Add(col);
            }

            var pcol = new DataGridViewTextBoxColumn();
            pcol.HeaderText = "Bin P-Value";
            pcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewPeptideAnalysis.Columns.Add(pcol);

            var pcol2 = new DataGridViewTextBoxColumn();
            pcol2.HeaderText = "Bin P-Value C1";
            pcol2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pcol2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewPeptideAnalysis.Columns.Add(pcol2);

            var fcol = new DataGridViewTextBoxColumn();
            fcol.HeaderText = "Avg(Log2(fold))";
            fcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            fcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewPeptideAnalysis.Columns.Add(fcol);

            for (int y = 0; y < coreP.MyPeptideAnalyses.Count; y++)
            {
                int index = dataGridViewPeptideAnalysis.Rows.Add();
                dataGridViewPeptideAnalysis.Rows[index].HeaderCell.Value = coreP.MyPeptideAnalyses[y].Sequence + "::" + coreP.MyPeptideAnalyses[y].Z;

                for (int x = 0; x < coreP.MyPeptideAnalyses[y].AssociationResults.Length; x++)
                {
                    dataGridViewPeptideAnalysis.Rows[index].Cells[x].Value = coreP.MyPeptideAnalyses[y].AssociationResults[x];
                }

                dataGridViewPeptideAnalysis.Rows[index].Cells[coreP.MyPeptideAnalyses[y].AssociationResults.Length].Value = coreP.MyPeptideAnalyses[y].Binomial;
                dataGridViewPeptideAnalysis.Rows[index].Cells[coreP.MyPeptideAnalyses[y].AssociationResults.Length + 1].Value = coreP.MyPeptideAnalyses[y].BinomialC1;
                dataGridViewPeptideAnalysis.Rows[index].Cells[coreP.MyPeptideAnalyses[y].AssociationResults.Length + 2].Value = coreP.MyPeptideAnalyses[y].LogAVGFold;

            }


            Console.WriteLine("Rows added to peptide table: " + coreP.MyPeptideAnalyses.Count);
        }


        private void buttonProteinReport_Click(object sender, EventArgs e)
        {
            buttonProteinReport.Text = "Working ... ";
            this.Update();

            coreP.RunProteinAnalysis(); 
            PrintProteinReport();

            buttonProteinReport.Text = "Generate Protein Report";

        }

        private void PrintProteinReport()
        {
            if (coreP.MyProteinAnalyses == null)
            {
                return;
            }
            List<ProteinAnalysis> myProtAnalysis = coreP.MyProteinAnalyses;
            richTextBoxProteinAndPeptideReport.Clear();
            richTextBoxProteinReport.Clear();

            List<ProteinAnalysis> failedProteinAnalysis = myProtAnalysis.FindAll(a => a.MyPepAnalyses.Count == 0);

            myProtAnalysis = myProtAnalysis.Except(failedProteinAnalysis).ToList();

            myProtAnalysis.Sort((a, b) => a.PValueC1.CompareTo(b.PValueC1));

            string protDescription = "#S\tSequenceIdentifier\tAvgLogFold\tStaufordsMethodPValue\tPeptideCount\tUniquePeptideCount\tCoverage\tDescription\n";
            List<int> theLabels = (from ass in coreP.core.MyAssociationItems
                                   where coreP.AcceptableClasses.Contains(ass.Label)
                                   select ass.Assosication).Distinct().ToList();
            
            
            string peptDescription = "#P\tSequence\tAvgLogFold\tIsUnique\tBinomial\tBinomialC1\tZ\t" + string.Join("\t", theLabels) + "\n";

            richTextBoxProteinAndPeptideReport.AppendText(protDescription);
            richTextBoxProteinReport.AppendText(protDescription);

            richTextBoxProteinAndPeptideReport.AppendText(peptDescription);
            
            foreach (ProteinAnalysis pa in myProtAnalysis)
            {

                double avgF = pa.GetAvgFFold();
                string ToPrint = pa.MyFastaItem.SequenceIdentifier + "\t" + avgF + "\t" + pa.PValueC1 + "\t" + pa.MyPepAnalyses.Count + "\t" + pa.MyPepAnalyses.Count(a => a.IsUnique) + "\t" + pa.Coverage + "\t" + pa.MyFastaItem.Description + "\n";
                richTextBoxProteinAndPeptideReport.AppendText("S\t" + ToPrint);
                richTextBoxProteinReport.AppendText(ToPrint);

                foreach (PeptideAnalysis p in pa.MyPepAnalyses)
                {
                    richTextBoxProteinAndPeptideReport.AppendText("P\t" + p.Sequence + "\t" + p.LogAVGFold + "\t" + p.IsUnique + "\t" + p.Binomial + "\t" + p.BinomialC1 + "\t" + p.Z + "\t" + string.Join("\t", p.AssociationResults) + "\n");
                }
                
            }
        }

        private void savePairedAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Paired files (*.paired)|*.paired";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                coreP.Serialize(saveFileDialog1.FileName);
                MessageBox.Show("Analysis saved.");
            }
        }

        private void loadPairedAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void xICCoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XIC files (*.xic)|*.xic";

            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                coreP = new PairAnalyzer.Core20Paired(openFileDialog1.FileName, checkBoxInvertClasses.Checked);
                FinishLoadingProcedures();
                buttonRunAnalysis.Enabled = true;
                MessageBox.Show("File loaded.");
            }

            checkBoxInvertClasses.Enabled = false;


        }

        private void pairedAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Paired files (*.paired)|*.paired|All files(*.*)|*.*";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                buttonProteinReport.Enabled = true;
                buttonRunAnalysis.Enabled = true;

                coreP = Core20Paired.Deserialize(openFileDialog1.FileName);
                PrintPeptideAnalysis(coreP.AcceptableClasses);
                PrintProteinReport();

                foreach (int theClass in coreP.core.myQuantPkgs.Select(a => a.ClassLabel).Distinct().ToList())
                {
                    checkedListBoxClasses.Items.Add(theClass);
                }

                for (int i = 0; i < checkedListBoxClasses.Items.Count; i++)
                {
                    if (coreP.AcceptableClasses.Contains(int.Parse(checkedListBoxClasses.Items[i].ToString())))
                    {
                        checkedListBoxClasses.SetItemCheckState(i, CheckState.Checked);
                    }
                    else
                    {
                        checkedListBoxClasses.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }

            }

            checkBoxInvertClasses.Checked = coreP.InvertClasses;
            checkBoxNormalize.Checked = coreP.NormalizeTIC;
            checkBoxOnlyUniquePeptides.Checked = coreP.OnlyUniquePeptides;
            numericUpDownMinMS1COunt.Value = coreP.MinMS1Count;
            numericUpDownMinPeptideFold.Value = (decimal)coreP.MinFold;


        }
    }
}
