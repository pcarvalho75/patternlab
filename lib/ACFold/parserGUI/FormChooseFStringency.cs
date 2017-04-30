using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace parserGUI
{
    public partial class FormChooseFStringency : Form
    {
        public double FStringency { get; set; }

        public FormChooseFStringency()
        {
            InitializeComponent();
        }

        public void Plot(List<double> fStringency, List<int> blueDots)
        {
            int maxBlueDots = blueDots.Max();
            double optimalFStringency = fStringency[blueDots.IndexOf(maxBlueDots)];

            numericUpDownFStringency.Value = (decimal)optimalFStringency;

            //Now lets plot the graph
            for (int i = 0; i < fStringency.Count; i++) {
                chart1.Series["Series1"].Points.AddXY(fStringency[i], blueDots[i]);

            }

            chart1.ChartAreas["Default"].AxisX.Title = "F-Stringency";
            chart1.ChartAreas["Default"].AxisY.Title = "# Blue Dots";

        }

        private void numericUpDownFStringency_ValueChanged(object sender, EventArgs e)
        {
            FStringency = (double)numericUpDownFStringency.Value;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
