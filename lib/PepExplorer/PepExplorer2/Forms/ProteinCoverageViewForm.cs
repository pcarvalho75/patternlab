using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PepExplorer2.Forms
{
    public partial class ProteinCoverageViewForm : Form
    {
        public ProteinCoverageView MyProteinCoverageView
        {
            get { return proteinCoverageView1; }
            set { proteinCoverageView1 = value; }
        }
        public ProteinCoverageViewForm()
        {
            InitializeComponent();
        }
    }
}
