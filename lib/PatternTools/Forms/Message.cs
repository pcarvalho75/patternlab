using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PatternTools.Forms
{
    public partial class Message : Form
    {
        public Message(string message)
        {
            InitializeComponent();
            this.richTextBoxMessage.Text = message;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
