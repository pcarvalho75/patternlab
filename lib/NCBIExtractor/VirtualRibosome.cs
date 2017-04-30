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
using PatternTools.FastaParser;

namespace NCBIExtractor
{
    public partial class VirtualRibosome : Form
    {
        public VirtualRibosome()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            PatternTools.DNA2Protein translator = new PatternTools.DNA2Protein();

            int counter = 0;
            foreach (string s in translator.Translate6(textBoxInput.Text))
            {
                counter++;
                richTextBoxOutput.AppendText("Frame " + counter + "\n");
                richTextBoxOutput.AppendText(s + "\n\n");

            }
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxOutput.Text = saveFileDialog1.FileName;
            }
            buttonWork.Enabled = true;
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxInputFile.Text = openFileDialog1.FileName;
            }
        }

        private void buttonWork_Click(object sender, EventArgs e)
        {
            buttonWork.Text = "Busy!";

            //First, lets get the input DB in RAM
            PatternTools.FastaParser.FastaFileParser fp = new PatternTools.FastaParser.FastaFileParser();
            fp.ParseFile(new StreamReader(textBoxInputFile.Text), false, PatternTools.FastaParser.DBTypes.IDSpaceDescription);

            if (checkBoxQualityFilters.Checked)
            {
                int itemsRemovedMinNo = fp.MyItems.RemoveAll(a => a.Sequence.Length <= (int)numericUpDownMinNoNT.Value);
                labelNoNt.Text = itemsRemovedMinNo.ToString();
            } else
            {
                labelNoNt.Text = "0";
            }

            this.Update();
        

            int itemsRemovedStopCoddons = 0;
            

            StreamWriter sw = new StreamWriter(textBoxOutput.Text);

            PatternTools.DNA2Protein translator = new PatternTools.DNA2Protein();

            foreach (FastaItem fi in fp.MyItems)
            {
                List<string> translatedFrames = new List<string>();

                if (radioButton3Frame.Checked)
                {
                    translatedFrames = translator.Translate3(fi.Sequence);
                } else
                {
                    translatedFrames = translator.Translate6(fi.Sequence);
                }

                for (int i = 0; i < translatedFrames.Count; i++)
                {
                    if (checkBoxQualityFilters.Checked)
                    {
                        int noStopCodons = Regex.Matches(translatedFrames[i], Regex.Escape("*")).Count;
                        Console.WriteLine(fi.SequenceIdentifier + " Frame{0} StopCodons: {1}", i, noStopCodons);

                        if (noStopCodons >= (int)numericUpDownMaxNoStopCodons.Value) {
                            itemsRemovedStopCoddons++;
                            labelNoStopCodons.Text = itemsRemovedStopCoddons.ToString();
                            this.Update();
                        }
                    }
                    else
                    {
                        sw.WriteLine(fi.SequenceIdentifier + "_" + i);
                        sw.WriteLine(translatedFrames[i]);
                    }
                }
            
            
            }

            sw.Close();
            Console.WriteLine("Done");
            
            buttonWork.Text = "Go!";
        }

        private void checkBoxQualityFilters_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxQualityFilters.Checked)
            {
                groupBoxQualityFilters.Enabled = true;
            } else
            {
                groupBoxQualityFilters.Enabled = false;
            }
        }
    }
}
