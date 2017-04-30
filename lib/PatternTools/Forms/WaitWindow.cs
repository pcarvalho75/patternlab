using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PatternTools
{
    public partial class WaitWindow
    {
        static bool keepAlive = true;

        public bool KeepAlive {
            set { keepAlive = value; }
        }

        public WaitWindow()
        {
            InitializeComponent();
        }

        public void ChangeLable(string l)
        {
            label1.Text = l;
        }

        public void ChangeLowerLable(string l)
        {
            LabelMessage.Text = l;
        }

        public void ShowWindow()
        {
            this.ShowDialog();

            while (keepAlive)
            {
                Thread.Sleep(50);
            }

            this.Close();

            keepAlive = true;

        }
    }
}
