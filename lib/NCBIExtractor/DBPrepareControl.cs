using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using PatternTools.FastaParser;

namespace NCBIExtractor
{
    public partial class DBPrepareControl : UserControl
    {
        static string buttonGoText = "";
        static string statusBarText = "";
        static int myFileParserItems = 0;
        static string outputFile;
        static string noSequencesRemoved = "0";
        static bool removeSubSequences;
        static bool includeReversed, includeScrambled0, includeScrambled1, includeContaminants;


        public DBPrepareControl()
        {
            InitializeComponent();
        }

        private void buttonGO_Click(object sender, EventArgs e)
        {
            groupBoxInput.Enabled = false;
            outputFile = textBoxOutput.Text;
            includeReversed = !radioButtonT.Checked;

            includeScrambled0 = true;
            if (radioButtonT.Checked || radioButtonTRoT.Checked)
            {
                includeScrambled0 = false;
            }
            
            includeContaminants = checkBoxInsertContaminants.Checked;
            removeSubSequences = checkBoxEliminateSubsetSequences.Checked;

            //DoJob();
            backgroundWorker1.RunWorkerAsync();

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ////Suggest a new name
            //FileInfo fi = new FileInfo(openFileDialog1.FileName);
            if (radioButtonT.Checked)
            {
                saveFileDialog1.Filter = "Target|*.T";
            }
            else if (radioButtonTRoT.Checked)
            {
                saveFileDialog1.Filter = "Target-Reversed|*.T-R";
            }
            else if (radioButtonTPRMR.Checked)
            {
                saveFileDialog1.Filter = "Target-PairReversed-MiddleReversed|*.T-PR-MR";
            }

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxOutput.Text = saveFileDialog1.FileName;
            }

            buttonGO.Enabled = true;
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatternTools.WebBrowser.Browser b = new PatternTools.WebBrowser.Browser();
            b.SetURL(@"http://www.mikesdotnetting.com/Article/46/CSharp-Regular-Expressions-Cheat-Sheet");
            b.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = true;
            dataGridViewInputDBs.Rows.Clear();
            buttonGO.Enabled = true;

            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                foreach (string f in openFileDialog1.FileNames)
                {
                    int index = dataGridViewInputDBs.Rows.Add();
                    dataGridViewInputDBs.Rows[index].Cells[0].Value = f;
                }

            }
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DoJob();

        }

        private void DoJob()
        {
            try
            {
                PatternTools.FastaParser.FastaFileParser fp = new PatternTools.FastaParser.FastaFileParser();
                buttonGoText = "Working!";
                statusBarText = "Parsing DB...";
                backgroundWorker1.ReportProgress(1);


                Regex myRegex = new Regex(textBoxNameRegex.Text, RegexOptions.Compiled);

                PatternTools.FastaParser.DBTypes dbType;
                if (radioButtonDBNCBI.Checked)
                {
                    dbType = DBTypes.NCBInr;
                }
                else if (radioButtonDBUniprot.Checked)
                {
                    dbType = DBTypes.UniProt;
                }
                else if (radioButtonDBGeneric.Checked)
                {
                    dbType = DBTypes.IDSpaceDescription;

                } else if (radioButtonNextProt.Checked)
                {
                    dbType = DBTypes.NeXtProt;
                }
                else
                {
                    dbType = PatternTools.FastaParser.DBTypes.IPI;
                }

                if (includeContaminants)
                {
                    byte[] byteArray = Encoding.ASCII.GetBytes(NCBIExtractor.Properties.Resources.ContaminantDB);
                    StreamReader sContaminantsr = new StreamReader(new MemoryStream(byteArray));

                    fp.ParseFile(
                        sContaminantsr,
                        false,
                        PatternTools.FastaParser.DBTypes.Contaminant);

                }

                foreach (DataGridViewRow r in dataGridViewInputDBs.Rows)
                {
                    PatternTools.FastaParser.FastaFileParser fpTmp = new PatternTools.FastaParser.FastaFileParser();

                    string fileName = r.Cells[0].Value.ToString();

                    StreamReader srDB = new StreamReader(fileName);

                    fpTmp.ParseFile(
                        srDB,
                        false,
                        dbType,
                        true,
                        new Regex(textBoxNameRegex.Text, RegexOptions.IgnoreCase));

                    srDB.Close();

                    fpTmp.MyItems.RemoveAll(a => string.IsNullOrEmpty(a.Sequence));

                    fp.MyItems.AddRange(fpTmp.MyItems);
                }

                if (removeSubSequences)
                {

                    if (numericUpDownIdentity.Value == 100)
                    {
                        Console.WriteLine("Removing subset sequences");

                        List<FastaItem> toEliminate = new List<FastaItem>();
                        Console.WriteLine("Searching for subset sequences...");
                        //order by increasing length
                        fp.MyItems.Sort((a, b) => a.Sequence.Length.CompareTo(b.Sequence.Length));

                        for (int i = 0; i < fp.MyItems.Count; i++)
                        {
                            Console.WriteLine("Analyzing for sequence " + i);
                            for (int j = i + 1; i < fp.MyItems.Count; j++)
                            {
                                if (j == fp.MyItems.Count)
                                {
                                    break;
                                }

                                if (fp.MyItems[j].Sequence.Contains(fp.MyItems[i].Sequence))
                                {
                                    toEliminate.Add(fp.MyItems[i]);
                                    Console.WriteLine("subset sequence found");
                                    fp.MyItems[j].Description = fp.MyItems[j].Description + " : " + fp.MyItems[i].SequenceIdentifier + " " + fp.MyItems[i].Description;
                                    break;
                                }

                            }
                        }

                        Console.WriteLine("Sequences eliminated: " + toEliminate.Count);

                        fp.MyItems = fp.MyItems.Except(toEliminate).ToList();
                    }
                    else 
                    {
                        MAligner.MCompress.Compress c = new MAligner.MCompress.Compress();
                        c.CompressByIdentity(fp, (double)numericUpDownIdentity.Value / 100.0 , null);
                    }
                    
                }

                statusBarText = "Writing DB...";
                backgroundWorker1.ReportProgress(1);

                fp.GenerateSearchDB(outputFile, includeReversed, includeScrambled0, includeScrambled1, radioButtonTPRMR.Checked);

                myFileParserItems = fp.MyItems.Count();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonGO.Text = "GO!";
            groupBoxInput.Enabled = true;
            toolStripStatusLabel1.Text = "Total non-decoy sequences included: " + myFileParserItems;
            toolStripStatusLabelSequencesRemoved.Text = noSequencesRemoved;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            buttonGO.Text = buttonGoText;
            toolStripStatusLabel1.Text = statusBarText;
            this.Update();
        }

        private void radioButtonTSRoTS_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxInputDB.Enabled = true;
            groupBoxOutput.Enabled = true;
            textBoxOutput.Clear();
        }

        private void radioButtonTRoT_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxInputDB.Enabled = true;
            groupBoxOutput.Enabled = true;
            textBoxOutput.Clear();
        }

        private void radioButtonT_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxInputDB.Enabled = true;
            groupBoxOutput.Enabled = true;
            textBoxOutput.Clear();
        }

        private void radioButtonTSS1_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxInputDB.Enabled = true;
            groupBoxOutput.Enabled = true;
            textBoxOutput.Clear();
        }

        private void radioButtonLabeledDecoy_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxInputDB.Enabled = true;
            groupBoxOutput.Enabled = true;
            textBoxOutput.Clear();
        }

        private void radioButtonTSWRSW_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxInputDB.Enabled = true;
            groupBoxOutput.Enabled = true;
            textBoxOutput.Clear();

            if (radioButtonTPRMR.Checked)
            {
                comboBoxEnzyme.Enabled = true;
            }
            else
            {
                comboBoxEnzyme.Enabled = false;
            }
        }

        private void cleanDBRemveSequencesFollowingARegexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CleanDB c = new CleanDB();
            c.ShowDialog();
        }

        private void nucleotideToProteinDBPreparationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VirtualRibosome f1 = new VirtualRibosome();
            f1.ShowDialog();
        }

        private void nucleotideToProteinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VirtualRibosome vr = new VirtualRibosome();
            vr.ShowDialog();
        }

        private void checkBoxEliminateSubsetSequences_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEliminateSubsetSequences.Checked)
            {
                labelIdentity.Enabled = true;
                numericUpDownIdentity.Enabled = true;
            }
            else
            {
                labelIdentity.Enabled = false;
                numericUpDownIdentity.Enabled = false;
            }
        }
    }
}
