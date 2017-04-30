using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PLP;
using PatternTools;

namespace PatternLab
{
    public partial class SparseMatrixIndexLegacy : UserControl
    {
        public SparseMatrixIndexLegacy()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog1.Filter = "PatternLab project file (*.plp)|*.plp";
        }

        private void buttonBrowseIndex_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBoxIndex.Text = openFileDialog1.FileName;
            }
        }

        private void buttonBrowseSparseMatrix_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBoxSparseMatrix.Text = openFileDialog1.FileName;
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBoxIndex.Text))
            {
                MessageBox.Show("Please specify an index file.");
                return;
            }

            if (!File.Exists(textBoxSparseMatrix.Text))
            {
                MessageBox.Show("Please specify a sparse matrix file.");
                return;
            }

            if (textBoxProjectDescription.Text.Length == 0)
            {
                MessageBox.Show("Please enter a simple project description.");
                return;
            }

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                PatternLabProject plp = new PatternLabProject(textBoxSparseMatrix.Text, textBoxIndex.Text, textBoxProjectDescription.Text);
                plp.Save(saveFileDialog1.FileName);
                MessageBox.Show("Project converted.");
            }

            
        }
    }
}
