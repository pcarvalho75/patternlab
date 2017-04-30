using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Regrouper
{
    public partial class SEProSummary : Form
    {
        public RichTextBox MyRichTextBox {
            get { return richTextBoxSummary; }
            set {richTextBoxSummary = value;}
        }
        public SEProSummary()
        {
            InitializeComponent();
        }

        private void richTextBoxSummary_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
