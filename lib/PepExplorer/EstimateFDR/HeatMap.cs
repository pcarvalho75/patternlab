using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstimateFDR
{
    public partial class HeatMap : Form
    {
        public PatternTools.HeatMap MyHeatMap
        {
            get { return heatMap1; }
            set { heatMap1 = value; }
        }
        public HeatMap()
        {
            InitializeComponent();
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void HeatMap_Resize(object sender, EventArgs e)
        {
            heatMap1.Plot(this.Width, this.Height);
        }

        private void HeatMap_Load(object sender, EventArgs e)
        {

        }
    }
}
