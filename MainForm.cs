/*
 * Created by SharpDevelop.
 * User: Paulo C. C. & Valmir C. B.
 * Date: 12/23/2006
 * Time: 12:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace msmsCluster
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm
	{
		[STAThread]
		
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitializeFormVariables();			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}


        private void dTA2MS2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormDTA2MS2 form = new FormDTA2MS2();
            form.ShowDialog();
        }

        private void buttonStartStep3_Click(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "Working...(Feature Selection)";
            this.mainFormToWorkingMode(true);
            featureSelection fs = new featureSelection(this.textBoxBrowseSparseMatrix.Text);


            if (this.comboBoxFeatureSelection.Text.Equals("T-Test"))
            {
                fs.tTest();
            }
            else
            {
                MessageBox.Show("Please enter a valid feature selection method.", "Error");
            }

            this.toolStripStatusLabel2.Text = "Working...(Parsing results)";
            resultAnalyzer rAnalyzer = new resultAnalyzer(fs.FRanking);
            this.richTextBoxResults.Text = rAnalyzer.rankList();
            
            this.dataGridViewResults.DataSource = rAnalyzer.rankTable();
            this.dataGridViewResults.Update();
            

            this.toolStripStatusLabel2.Text = "Done!";
            this.mainFormToWorkingMode(false);
        }

        private void buttonLoadIndex_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            this.textBoxBrowseIndex.Text = this.openFileDialog1.FileName;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonBrowseSparseMatrix_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            this.textBoxBrowseSparseMatrix.Text = this.openFileDialog1.FileName;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-------------------

        private void mainFormToWorkingMode(bool mode)
        {
            if (mode) //The program is working so we should lock up all possible controls
            {
                this.buttonStartStep3.Enabled = false;
                this.buttonContinueStep3.Enabled = false;
                this.buttonSaveLog.Enabled = false;
                this.buttonApplyStep2.Enabled = false;
                this.buttonBrowseIndex.Enabled = false;
                this.buttonBrowseSparseMatrix.Enabled = false;
                
                this.buttonStopStep3.Enabled = true;
            }
            else //Lets free up the controls
            {
                this.buttonStartStep3.Enabled = true;
                this.buttonContinueStep3.Enabled = true;
                this.buttonSaveLog.Enabled = true;
                this.buttonApplyStep2.Enabled = true;
                this.buttonBrowseIndex.Enabled = true;
                this.buttonBrowseSparseMatrix.Enabled = true;

                this.buttonStopStep3.Enabled = false;
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

	}
	

	
}
