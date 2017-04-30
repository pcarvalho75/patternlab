using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace XQuant
{
    public partial class Parameters : UserControl
    {
        public XQuantClusteringParameters MyXQuantClusteringParameters
        {
            get
            {
                return GetFromScreen();
            }
        }

        public XQuantClusteringParameters GetFromScreen()
        {
            XQuantClusteringParameters cp = new XQuantClusteringParameters();
            cp.ClusteringPPM = (int)numericUpDownXICPPM.Value;

            List<int> acceptableZ = new List<int>();
            string[] cols = Regex.Split(textBoxAcceptableZ.Text, ",");
            foreach (string col in cols)
            {
                acceptableZ.Add(int.Parse(col));
            }
            cp.AcceptableChargeStates = acceptableZ;

            return cp;
        }

        public void UpdatePanel(XQuantClusteringParameters cp)
        {
            numericUpDownXICPPM.Value = (int)cp.ClusteringPPM;
            textBoxAcceptableZ.Text = string.Join(", ", cp.AcceptableChargeStates);

        }
        public Parameters()
        {
            InitializeComponent();
        }
    }
}
