using HMMVerifier.HMM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMMVerifier
{
    public partial class Form1 : Form
    {
        static BatchDomainFetcher df;
        static string fasta;
        public Form1()
        {
            InitializeComponent();
            df = new BatchDomainFetcher();
        }

        private void buttonGO_Click(object sender, EventArgs e)
        {
            buttonGO.Text = "Fetching";
            richTextBoxOutput.Clear();
            progressBar1.Value = 0;

            fasta = richTextBoxInput.Text;

            backgroundWorker1.RunWorkerAsync();

            while (BatchDomainFetcher.Percent < 100)
            {
                Thread.Sleep(1000);
                progressBar1.Value = BatchDomainFetcher.Percent;
                this.Update();
            }

            this.Update();

        }

        private void WriteToTextBox(List<HMMResult> results)
        {
            //Write the results
            for (int i = 0; i < results.Count; i++)
            {
                richTextBoxOutput.AppendText(results[i].QNamek__BackingField + "\t" + results[i].Descriptionk__BackingField + "\n");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            df.Fetch(fasta, true, 10);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WriteToTextBox(BatchDomainFetcher.Results);
            progressBar1.Value = 100;
            buttonGO.Text = "GO";
        }

            
    }
}
