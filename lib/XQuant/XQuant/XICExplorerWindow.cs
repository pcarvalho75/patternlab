using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XQuant
{
    public partial class XICExplorerWindow : Form
    {
        public XICExplorer MyXICExplorer
        {
            get { return xicExplorer1; }
            set { xicExplorer1 = value; }
        }
        public XICExplorerWindow()
        {
            InitializeComponent();
        }
    }
}
