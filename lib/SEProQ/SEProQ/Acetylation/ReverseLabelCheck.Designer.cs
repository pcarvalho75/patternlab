namespace SEProQ.Acetylation
{
    partial class ReverseLabelCheck
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBoxSEProL1 = new System.Windows.Forms.TextBox();
            this.textBoxSEProL2 = new System.Windows.Forms.TextBox();
            this.buttonBrowseSepro1 = new System.Windows.Forms.Button();
            this.buttonBrowseSepro2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonGO = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelRSquared = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelRegressorEquation = new System.Windows.Forms.Label();
            this.labelL2 = new System.Windows.Forms.Label();
            this.labelL1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridViewPoints = new System.Windows.Forms.DataGridView();
            this.checkBoxNormalize = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPoints)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxSEProL1
            // 
            this.textBoxSEProL1.Location = new System.Drawing.Point(61, 3);
            this.textBoxSEProL1.Name = "textBoxSEProL1";
            this.textBoxSEProL1.Size = new System.Drawing.Size(176, 20);
            this.textBoxSEProL1.TabIndex = 0;
            this.textBoxSEProL1.Text = "C:\\Users\\pcarvalho\\Desktop\\Andres\\1";
            // 
            // textBoxSEProL2
            // 
            this.textBoxSEProL2.Location = new System.Drawing.Point(61, 32);
            this.textBoxSEProL2.Name = "textBoxSEProL2";
            this.textBoxSEProL2.Size = new System.Drawing.Size(176, 20);
            this.textBoxSEProL2.TabIndex = 1;
            this.textBoxSEProL2.Text = "C:\\Users\\pcarvalho\\Desktop\\Andres\\2";
            // 
            // buttonBrowseSepro1
            // 
            this.buttonBrowseSepro1.Location = new System.Drawing.Point(243, 1);
            this.buttonBrowseSepro1.Name = "buttonBrowseSepro1";
            this.buttonBrowseSepro1.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseSepro1.TabIndex = 2;
            this.buttonBrowseSepro1.Text = "Browse";
            this.buttonBrowseSepro1.UseVisualStyleBackColor = true;
            this.buttonBrowseSepro1.Click += new System.EventHandler(this.buttonBrowseSepro1_Click);
            // 
            // buttonBrowseSepro2
            // 
            this.buttonBrowseSepro2.Location = new System.Drawing.Point(243, 30);
            this.buttonBrowseSepro2.Name = "buttonBrowseSepro2";
            this.buttonBrowseSepro2.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseSepro2.TabIndex = 3;
            this.buttonBrowseSepro2.Text = "Browse";
            this.buttonBrowseSepro2.UseVisualStyleBackColor = true;
            this.buttonBrowseSepro2.Click += new System.EventHandler(this.buttonBrowseSepro2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "SEProL1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "SEProL2";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonGO
            // 
            this.buttonGO.Location = new System.Drawing.Point(243, 59);
            this.buttonGO.Name = "buttonGO";
            this.buttonGO.Size = new System.Drawing.Size(75, 22);
            this.buttonGO.TabIndex = 6;
            this.buttonGO.Text = "GO";
            this.buttonGO.UseVisualStyleBackColor = true;
            this.buttonGO.Click += new System.EventHandler(this.buttonGO_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxNormalize);
            this.splitContainer1.Panel1.Controls.Add(this.labelRSquared);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.labelRegressorEquation);
            this.splitContainer1.Panel1.Controls.Add(this.labelL2);
            this.splitContainer1.Panel1.Controls.Add(this.labelL1);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSEProL1);
            this.splitContainer1.Panel1.Controls.Add(this.buttonGO);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSEProL2);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.buttonBrowseSepro1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.buttonBrowseSepro2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(690, 381);
            this.splitContainer1.SplitterDistance = 84;
            this.splitContainer1.TabIndex = 7;
            // 
            // labelRSquared
            // 
            this.labelRSquared.AutoSize = true;
            this.labelRSquared.Location = new System.Drawing.Point(397, 64);
            this.labelRSquared.Name = "labelRSquared";
            this.labelRSquared.Size = new System.Drawing.Size(13, 13);
            this.labelRSquared.TabIndex = 13;
            this.labelRSquared.Text = "?";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(324, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "R-Squared : ";
            // 
            // labelRegressorEquation
            // 
            this.labelRegressorEquation.AutoSize = true;
            this.labelRegressorEquation.Location = new System.Drawing.Point(323, 43);
            this.labelRegressorEquation.Name = "labelRegressorEquation";
            this.labelRegressorEquation.Size = new System.Drawing.Size(82, 13);
            this.labelRegressorEquation.TabIndex = 11;
            this.labelRegressorEquation.Text = "Linear regressor";
            // 
            // labelL2
            // 
            this.labelL2.AutoSize = true;
            this.labelL2.Location = new System.Drawing.Point(355, 25);
            this.labelL2.Name = "labelL2";
            this.labelL2.Size = new System.Drawing.Size(13, 13);
            this.labelL2.TabIndex = 10;
            this.labelL2.Text = "?";
            // 
            // labelL1
            // 
            this.labelL1.AutoSize = true;
            this.labelL1.Location = new System.Drawing.Point(355, 6);
            this.labelL1.Name = "labelL1";
            this.labelL1.Size = new System.Drawing.Size(13, 13);
            this.labelL1.TabIndex = 9;
            this.labelL1.Text = "?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "L2: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "L1: ";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridViewPoints);
            this.splitContainer2.Size = new System.Drawing.Size(676, 261);
            this.splitContainer2.SplitterDistance = 396;
            this.splitContainer2.TabIndex = 1;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(396, 261);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // dataGridViewPoints
            // 
            this.dataGridViewPoints.AllowUserToAddRows = false;
            this.dataGridViewPoints.AllowUserToDeleteRows = false;
            this.dataGridViewPoints.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewPoints.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPoints.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPoints.Name = "dataGridViewPoints";
            this.dataGridViewPoints.ReadOnly = true;
            this.dataGridViewPoints.RowHeadersVisible = false;
            this.dataGridViewPoints.Size = new System.Drawing.Size(276, 261);
            this.dataGridViewPoints.TabIndex = 0;
            // 
            // checkBoxNormalize
            // 
            this.checkBoxNormalize.AutoSize = true;
            this.checkBoxNormalize.Location = new System.Drawing.Point(61, 59);
            this.checkBoxNormalize.Name = "checkBoxNormalize";
            this.checkBoxNormalize.Size = new System.Drawing.Size(98, 17);
            this.checkBoxNormalize.TabIndex = 14;
            this.checkBoxNormalize.Text = "Normalize Data";
            this.checkBoxNormalize.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(690, 293);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewReport);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(682, 267);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Peptide Ratio Report";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(682, 267);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Correlation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.AllowUserToAddRows = false;
            this.dataGridViewReport.AllowUserToDeleteRows = false;
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReport.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.ReadOnly = true;
            this.dataGridViewReport.Size = new System.Drawing.Size(676, 261);
            this.dataGridViewReport.TabIndex = 0;
            // 
            // ReverseLabelCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReverseLabelCheck";
            this.Size = new System.Drawing.Size(690, 381);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPoints)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSEProL1;
        private System.Windows.Forms.TextBox textBoxSEProL2;
        private System.Windows.Forms.Button buttonBrowseSepro1;
        private System.Windows.Forms.Button buttonBrowseSepro2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonGO;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label labelL2;
        private System.Windows.Forms.Label labelL1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridViewPoints;
        private System.Windows.Forms.Label labelRSquared;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelRegressorEquation;
        private System.Windows.Forms.CheckBox checkBoxNormalize;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
