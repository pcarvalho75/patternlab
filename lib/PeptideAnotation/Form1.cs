using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeptideAnotation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPeptideAnotation_Click(object sender, EventArgs e)
        {
            if (textBoxPeptideSequence2.Text.Length == 0)
            {
                peptideAnotator1.DrawIntraLink(
                    textBoxPeptideSequence1.Text,
                    richTextBoxAnotationAlfa.Text,
                    (int)numericUpDownXLPos1.Value,
                    (int)numericUpDownXLPos2.Value
                    );
            }
            else
            {
                peptideAnotator1.DrawInterLink(
                    textBoxPeptideSequence1.Text,
                    textBoxPeptideSequence2.Text,
                    richTextBoxAnotationAlfa.Text,
                    richTextBoxAnotationBeta.Text,
                    (int)numericUpDownXLPos1.Value,
                    (int)numericUpDownXLPos2.Value
                    );
            }
        }

    }
}
