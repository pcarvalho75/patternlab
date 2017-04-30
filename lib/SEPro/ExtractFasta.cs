using PatternTools.FastaParser;
using SEPRPackage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEProcessor
{
    public partial class ExtractFasta : Form
    {
        public ExtractFasta()
        {
            InitializeComponent();
        }

        public ResultPackage MyRP { get; set; }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Fasta Files | *.fasta";
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                //Extract IDs
                string[] lines = Regex.Split(richTextBoxFastaIDs.Text, "\n");

                List<FastaItem> fastaItems = new List<FastaItem>(lines.Length);

                foreach (string line in lines)
                {
                    string[] cols = Regex.Split(line, "\t");
                    int index = MyRP.MyProteins.MyProteinList.FindIndex(a => a.Locus.Equals(cols[0]));

                    if (index == -1) 
                    {
                        Console.WriteLine("Locus " + cols[0] + "not found in the results");
                        continue;
                    } 

                    MyProtein prot = MyRP.MyProteins.MyProteinList[index];


                    FastaItem fi = new FastaItem();
                    fi.Description = prot.Description;
                    fi.Sequence = prot.Sequence;
                    fi.SequenceIdentifier = prot.Locus;

                    fastaItems.Add(fi);
                }

                //And now save the fasta sequences

                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach (FastaItem fi in fastaItems)
                {
                    sw.WriteLine(">" + fi.SequenceIdentifier + " " + fi.Description);
                    sw.WriteLine(fi.Sequence);
                }
                sw.Close();

                MessageBox.Show("Fasta sequences saved");
                this.Close();
            }
        }
    }
}
