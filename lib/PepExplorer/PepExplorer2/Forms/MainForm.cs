using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PepExplorer2.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                string[] stuff = Regex.Split(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationContext.Identity.FullName, ",");
                Console.WriteLine(stuff[1]);
                this.Text += " " + stuff[1];
            }
            catch
            {
                Console.WriteLine("Unable to retrieve version number.");
            }
        }
    }
}
