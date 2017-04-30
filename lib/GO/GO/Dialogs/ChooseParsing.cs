using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GO.UniProtAssociationParser;
using System.IO;
using System.Text.RegularExpressions;

namespace GO
{
    public partial class ChooseParsing : Form
    {
        public bool doWork { get; set; }
        public string pathTOGoa { get; set; }
        public string pathTOIPIXRefsFilename { get; set; }
        public AssociationType AssociationFileType { get; set; }
        public int GOColumn { get; set; }
        public int myColumn { get; set; }

        public ChooseParsing()
        {
            InitializeComponent();
            doWork = false;
        }
        

        private void kryptonButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonUnstandard_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonUnstandard.Checked)
            {
                numericUpDownGoColumn.Enabled = true;
                numericUpDownMyColumn.Enabled = true;
                GOColumn = (int)numericUpDownGoColumn.Value;
                myColumn = (int)numericUpDownMyColumn.Value;
            }
            else
            {
                numericUpDownGoColumn.Enabled = false;
                numericUpDownMyColumn.Enabled = false;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogForGoa = new OpenFileDialog();
            OpenFileDialog openFileDialogForIPIXRefs = new OpenFileDialog();

            openFileDialogForGoa.Title = "Indicate an association file.";
            openFileDialogForIPIXRefs.Title = "Indicate the IPIXRefs file.";
            openFileDialogForIPIXRefs.Filter = "XREFS (*.xrefs)|*.xrefs";

            if (radioButtonUniProt.Checked)
            {
                openFileDialogForIPIXRefs.Filter = "Text (*.txt)|*.txt";
            }

            if (radioButtonNCBiGenePeptFull.Checked)
            {
                openFileDialogForIPIXRefs.Filter = "GenePept-Full (*.gp)|*.gp";
            }

            if (openFileDialogForGoa.ShowDialog() == DialogResult.Cancel) { return; }

            else if (radioButtonUnstandard.Checked)
            {
                AssociationFileType = AssociationType.Generic;
            }

            else
            {
                AssociationFileType = AssociationType.IPI;
            }

            GOColumn = (int)numericUpDownGoColumn.Value;
            myColumn = (int)numericUpDownMyColumn.Value;


            if (radioButtonNCBiGenePeptFull.Checked)
            {
                AssociationFileType = AssociationType.GenePeptFull;

                List<UniProtOrGenePeptItem> genePeptItems = Parser.ParseGenePeptFile(openFileDialogForGoa.FileName);

                //Generate conversion file
                FileInfo fi = new FileInfo(openFileDialogForGoa.FileName);

                //And now lets write a generic conversion file
                StreamWriter sw = new StreamWriter(fi.FullName + "_GenePeptFull.GO");


                foreach (UniProtOrGenePeptItem uItem in genePeptItems)
                {
                    uItem.IDs.RemoveAll(a => a.Length == 0);
                    foreach (string id in uItem.IDs)
                    {
                        string id2 = Regex.Replace(id, " ", "");
                        foreach (string term in uItem.GOItems)
                        {
                            sw.WriteLine(id2 + "\t" + term);
                        }
                    }
                }

                sw.Close();

                openFileDialogForGoa.FileName = fi.FullName + "_GenePeptFull.GO";
                myColumn = 0;
                GOColumn = 1;
                AssociationFileType = AssociationType.Generic;
            }

            if (radioButtonUniProt.Checked)
            {
                AssociationFileType = AssociationType.UniProt;
                List<UniProtOrGenePeptItem> uniProtItems = UniProtAssociationParser.Parser.ParseUniprotFile(openFileDialogForGoa.FileName);

                //Generate conversion file
                FileInfo fi = new FileInfo(openFileDialogForGoa.FileName);

                //And now lets write a generic conversion file
                StreamWriter sw = new StreamWriter(fi.FullName + ".GO");

              
                foreach (UniProtOrGenePeptItem uItem in uniProtItems)
                {
                    uItem.IDs.RemoveAll(a => a.Length == 0);
                    foreach (string id in uItem.IDs)
                    {
                        string id2 = Regex.Replace(id, " ", "");
                        foreach (string term in uItem.GOItems)
                        {
                            sw.WriteLine(id2 + "\t" + term);
                        }
                    }
                }

                sw.Close();

                openFileDialogForGoa.FileName = fi.FullName + ".GO";
                myColumn = 0;
                GOColumn = 1;
                AssociationFileType = AssociationType.Generic;
            }

            pathTOGoa = openFileDialogForGoa.FileName;
            doWork = true;
            this.Close();
        }

    }
}
