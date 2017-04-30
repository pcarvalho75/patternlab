using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TrendQuest
{
    public partial class InternalStandard : Form
    {
        List<int> standards = new List<int>();

        public List<int> Standards
        {
            get { return standards; }
        }

        public InternalStandard()
        {
            InitializeComponent();
        }

        private void kryptonButtonOk_Click(object sender, EventArgs e)
        {
            

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Regex lineSplitter = new Regex(@"\n");

            standards.Clear();
            string[] lines = lineSplitter.Split(richTextBox1.Text);
            foreach (string line in lines)
            {
                standards.Add(int.Parse(line));
            }

            this.Close();
        }
    }
}
