using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NCBIExtractor
{
    public partial class CleanDB : Form
    {
        public CleanDB()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxInputDB.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!File.Exists(textBoxInputDB.Text))
            {
                MessageBox.Show("Database not found");
                return;
            }

            if (textBoxRegex.Text.Length == 0)
            {
                MessageBox.Show("Please enter a tag");
                return;
            }

            Console.WriteLine("Parsing: " + textBoxInputDB.Text);
            PatternTools.FastaParser.FastaFileParser parser = new PatternTools.FastaParser.FastaFileParser();
            parser.ParseFile(new StreamReader(textBoxInputDB.Text), false, PatternTools.FastaParser.DBTypes.IDSpaceDescription);
            int itemsRemoved = parser.MyItems.RemoveAll(a => a.SequenceIdentifier.StartsWith(textBoxRegex.Text));
            Console.WriteLine("Items removed: " + itemsRemoved);

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                parser.SaveDB(saveFileDialog1.FileName);
            }

            MessageBox.Show("Done");
            
        }
    }
}
