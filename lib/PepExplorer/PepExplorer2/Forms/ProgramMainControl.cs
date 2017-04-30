using PatternTools.FastaParser;
using PepExplorer2.Result2;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PepExplorer2.Forms
{
    public partial class ProgramMainControl : UserControl
    {
        private bool DbRadio { get; set; }
        private bool DeNovoRadio { get; set; }
        private static ProgramEngine Engine { get; set; }

        public ProgramMainControl()
        {

            InitializeComponent();

            radioButtonNextProt.Enabled = false;
            dataViewerToolStripMenuItem.Enabled = false;
        }


        private void buttonDataBasePath_Click(object sender, EventArgs e)
        {
            openFileDialogProgramMainForm.Filter = "All files (*.*)|*.*|Fasta (*.fasta)|*.fasta|Target-Reverse(*.T-R)|*.T-R|Target(*.T)|*.T";
            if (openFileDialogProgramMainForm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxDataBaseFile.Text = openFileDialogProgramMainForm.FileName;
            }
        }

        private void buttonDeNovoOutput_Click(object sender, EventArgs e)
        {
            openFileDialogProgramMainForm.Filter = "All files (*.*)|*.*";
            if (folderBrowserDialogProgramMainForm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxDeNovoOutput.Text = folderBrowserDialogProgramMainForm.SelectedPath;
            }

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StandardSearch(true);
        }

        private void StandardSearch(bool doAsync)
        {
            ProgramArgs args;
            try
            {
                args = GetParamsFromScreen();
            }
            catch (Exception e3)
            {
                MessageBox.Show(e3.Message);
                return;
            }

            //Fire up the stuff

            Engine = new ProgramEngine(args);
            //Lets pass this info in case the user is providing a peptide list

            Engine.PeptideList = richTextBoxPeptideList.Text;

            if (doAsync)
            {

                backgroundWorkerProgramMainForm.RunWorkerAsync();

                timerProgramMainForm.Enabled = true;
                tabControl1.Enabled = false;
            }
            else
            {
                Engine.RunFullAnalysisMAlginer();
            }
        }


        [STAThread]
        private void backgroundWorkerProgramMainForm_DoWork(object sender, DoWorkEventArgs e)
        {
            Engine.RunFullAnalysisMAlginer();
        }

        private void timerProgramMainForm_Tick(object sender, EventArgs e)
        {
            progressBarProgramMainForm.Value = Convert.ToInt16(Engine.Progress);
            this.Update();
        }

        private void backgroundWorkerProgramMainForm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerProgramMainForm.Enabled = false;
            progressBarProgramMainForm.Value = 100;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error");
            }
            else
            {

                MAlginerrForm mf = new MAlginerrForm();
                mf.MyViewer.MyResultPackage = Engine.MResultPckg;
                mf.MyViewer.UpdateDisplay();
                mf.ShowDialog();
            }

            buttonStart.Enabled = true;
            dataViewerToolStripMenuItem.Enabled = true;
         
            dataViewerToolStripMenuItem.Enabled = true;
        }


        private ProgramArgs GetParamsFromScreen()
        {

            //Verify if arguments are valid.
            if (!File.Exists(textBoxDataBaseFile.Text))
            {
                throw new Exception("Database file " + textBoxDataBaseFile.Text + " not found");
            }

            if (!Directory.Exists(textBoxDeNovoOutput.Text) && richTextBoxPeptideList.Text.Length == 0 && textBoxmpexFile.Text.Length == 0)
            {
                throw new Exception("Please enter a valid directory containing de novo output files or enter a list of peptides to be considered.");
            }
            else if (richTextBoxPeptideList.Text.Length == 0 && buttonBrowseMPEXSearch.Text.Length == 0)
            {
                if (Directory.GetFiles(textBoxDeNovoOutput.Text).Count() == 0)
                {
                    throw new Exception("No files were found in the De novo output directory");
                }
            }

            if (comboBoxDeNovoSoftware.SelectedText.Equals("Select de novo software"))
            {
                throw new Exception("Please select the de novo sequencing tool / format");
            }

            string dbPath = textBoxDataBaseFile.Text;
            string dnvPath = textBoxDeNovoOutput.Text;
            string decoy = textBoxDecoyTag.Text;
            int hits = 1;
            int pepSize = Convert.ToInt16(numericUpDownPeptideSize.Value);
            MatrixOption matrix = MatrixOption.PAM30ms;
            double minDeNovoScore = (double)numericUpDownMinDenovoScore.Value;

            matrix = MatrixOption.PAM30ms;

            DataBaseOption dbOpt = DataBaseOption.Generic;

            if (radioButtonGeneric.Checked == true)
            {
                dbOpt = DataBaseOption.Generic;
            }
            else if (radioButtonNcbi.Checked == true)
            {
                dbOpt = DataBaseOption.NCBI;
            }
            else if (radioButtonNextProt.Checked == true)
            {
                dbOpt = DataBaseOption.NextProt;
            }
            else if (radioButtonSwissProt.Checked == true)
            {
                dbOpt = DataBaseOption.SwissProt;
            }

            DeNovoOption dnvOpt = DeNovoOption.Peaks75;

            if (comboBoxDeNovoSoftware.SelectedItem != null)
            {

                if (comboBoxDeNovoSoftware.SelectedItem.Equals("PEAKS 7.0"))
                {
                    dnvOpt = DeNovoOption.Peaks70;
                } else if (comboBoxDeNovoSoftware.SelectedItem.Equals("PEAKS 7.5"))
                {
                    dnvOpt = DeNovoOption.Peaks75;
                } else if (comboBoxDeNovoSoftware.SelectedItem.Equals("PEAKS 8.0"))
                {
                    dnvOpt = DeNovoOption.Peaks80;
                }
                else if (comboBoxDeNovoSoftware.SelectedItem.Equals("PepNovo+ (output_dnv)"))
                {
                    dnvOpt = DeNovoOption.PepNovo;
                }
                else if (comboBoxDeNovoSoftware.SelectedItem.Equals("PNovo+"))
                {
                    dnvOpt = DeNovoOption.PNovo;
                }
                else if (comboBoxDeNovoSoftware.SelectedItem.Equals("PepNovo+ (output_full)"))
                {
                    dnvOpt = DeNovoOption.PepNovoFull;
                }
                else if (comboBoxDeNovoSoftware.SelectedItem.Equals("MSGFDB"))
                {
                    dnvOpt = DeNovoOption.MSGFDB;
                } else if (comboBoxDeNovoSoftware.SelectedItem.Equals("NOVOR v1.05.0573"))
                {
                    dnvOpt = DeNovoOption.NOVOR;
                }
            }
            else
            {
                dnvOpt = DeNovoOption.ListOfPeptides;
            }

            double minIdentity = (double)numericUpDownMinIdentity.Value;

            ProgramArgs myArgs = new ProgramArgs(
                dnvOpt,
                dnvPath,
                textBoxmpexFile.Text,
                dbOpt,
                dbPath,
                matrix,
                hits,
                pepSize,
                decoy,
                minDeNovoScore,
                minIdentity
                );

            return myArgs;
        }

        private void UpdateScreenWithParams(ProgramArgs theParams)
        {
            textBoxDataBaseFile.Text = theParams.DataBaseFile;
            textBoxDeNovoOutput.Text = theParams.DeNovoResultDirectory;
            textBoxDecoyTag.Text = theParams.DecoyLabel;
            numericUpDownPeptideSize.Value = theParams.PeptideMinNumAA;
            numericUpDownMinDenovoScore.Value = (decimal)theParams.MinDeNovoScore;

            if (String.Equals(theParams.DataBaseFormat.ToString(), "Generic"))
            {
                radioButtonGeneric.Checked = true;
            }
            else if (String.Equals(theParams.DataBaseFormat.ToString(), "NCBI"))
            {
                radioButtonNcbi.Checked = true;
            }
            else if (String.Equals(theParams.DataBaseFormat.ToString(), "UniProt"))
            {
                radioButtonSwissProt.Checked = true;
            }
            else if (String.Equals(theParams.DataBaseFormat.ToString(), "NextProt"))
            {
                radioButtonNextProt.Checked = true;
            }

            if (String.Equals(theParams.DeNovoOption.ToString(), "Peaks"))
            {
                comboBoxDeNovoSoftware.Text = "PEAKS";
            }
            else if (String.Equals(theParams.DeNovoOption.ToString(), "PepNovo"))
            {
                comboBoxDeNovoSoftware.Text = "PepNovo+ (output_dnv)";
            }
            else if (String.Equals(theParams.DeNovoOption.ToString(), "PNovo"))
            {
                comboBoxDeNovoSoftware.Text = "PNovo+";
            }
            else if (String.Equals(theParams.DeNovoOption.ToString(), "PepNovoFull"))
            {
                comboBoxDeNovoSoftware.Text = "PepNovo+ (output_full)";
            }
            else if (String.Equals(theParams.DeNovoOption.ToString(), "MSGFDB"))
            {
                comboBoxDeNovoSoftware.Text = "MSGFDB";
            }
            else if (String.Equals(theParams.DeNovoOption.ToString(), "NOVOR v1.03.0479"))
            {
                comboBoxDeNovoSoftware.Text = "NOVOR v1.03.0479";
            }
        }

        private void buttonClearPeptideList_Click(object sender, EventArgs e)
        {
            richTextBoxPeptideList.Clear();
        }


        private void openResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogProgramMainForm.Filter = "mpex files (*.mpex)|*.mpex";

            if (openFileDialogProgramMainForm.ShowDialog() != DialogResult.Cancel)
            {
                MAlginerrForm maf = new MAlginerrForm();
                maf.Width = 900;
                maf.MyViewer.LoadResult(openFileDialogProgramMainForm.FileName);
                maf.Text = "PepExplorer 2.0 - " + openFileDialogProgramMainForm.FileName;
                maf.ShowDialog();
            }
        }

        private void buttonBrowseMPEXSearch_Click(object sender, EventArgs e)
        {
            openFileDialogProgramMainForm.Filter = "mpex files (*.mpex)|*.mpex";

            openFileDialogProgramMainForm.FileName = "";

            if (openFileDialogProgramMainForm.ShowDialog() != DialogResult.Cancel)
            {
                textBoxmpexFile.Text = openFileDialogProgramMainForm.FileName;
            }
        }

        private void buttonGridSearch_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBoxDeNovoOutput.Text))
            {
                MessageBox.Show("This method requires you to inform an existing directory in the novo dir");
                return;
            }

            tabControl1.SelectedIndex = 0;
            tabControl1.Enabled = false;

            decimal minIdent = numericUpDownMinIdentity.Value;
            decimal minDeNovo = numericUpDownMinDenovoScore.Value;
            bool firstSearch = true;

            StreamWriter sw = new StreamWriter(textBoxDeNovoOutput.Text + "/ResultSummary");
            sw.WriteLine("#Identity\tDNovo\tAlignments\tDecoys");
            for (decimal dNovo = minDeNovo; dNovo <= numericUpDownGridMaxDeNovoScore.Value; dNovo += numericUpDownGridDeNovoIncrement.Value)
            {
                for (decimal ident = minIdent; ident <= numericUpDownGridMaxId.Value; ident += numericUpDownGridIdIncrement.Value)
                {
                    Console.WriteLine("Working on combination Denovo = {0} and Identity = {1}", dNovo, ident);

                    numericUpDownMinIdentity.Value = ident;
                    numericUpDownMinDenovoScore.Value = dNovo;
                    this.Update();

                    StandardSearch(false);

                    //Save the results
                    Console.WriteLine("Saving...");
                    string workingDir = textBoxDeNovoOutput.Text + "/Grid/";
                    if (!Directory.Exists(workingDir))
                    {
                        Directory.CreateDirectory(workingDir);
                    }
                    Engine.MResultPckg.SerializeResultPackage(workingDir + "GridIdentity_" + ident + "_DeNovo_" + dNovo + ".mpex");
                    sw.WriteLine("{0}\t{1}\t{2}\t{3}", ident, dNovo, Engine.MResultPckg.Alignments.Count, Engine.MResultPckg.MyFasta.Count(a => a.SequenceIdentifier.StartsWith(textBoxDecoyTag.Text)));
                    sw.Flush();

                    //Lets create a smaller dataset so the next results can be faster
                    if (firstSearch)
                    {
                        string dbName = textBoxDeNovoOutput.Text + "/CondensedDB.fasta";

                        StreamWriter fastaWriter = new StreamWriter(dbName);

                        foreach (FastaItem fi in Engine.MResultPckg.MyFasta)
                        {
                            fastaWriter.WriteLine(">" + fi.SequenceIdentifier + " " + fi.Description);
                            fastaWriter.WriteLine(fi.Sequence);
                        }

                        fastaWriter.Close();
                        textBoxDataBaseFile.Text = dbName;
                        firstSearch = false;
                    }
                }

            }

            sw.Close();
        }

    }

}
