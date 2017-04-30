namespace Anova
{
    partial class AnovaControl
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonLoadPLP = new System.Windows.Forms.Button();
            this.buttonSavePlot = new System.Windows.Forms.Button();
            this.buttonRecalculate = new System.Windows.Forms.Button();
            this.labelpvaluecuttoff = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMinSignal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelStatisticallySignificant = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxTotalSignalNormalization = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownQValue = new System.Windows.Forms.NumericUpDown();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewIDs = new System.Windows.Forms.DataGridView();
            this.tabControlGraphData = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBoxDataPoints = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chartHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartAnova = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIDs)).BeginInit();
            this.tabControlGraphData.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnova)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.buttonLoadPLP);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSavePlot);
            this.splitContainer1.Panel1.Controls.Add(this.buttonRecalculate);
            this.splitContainer1.Panel1.Controls.Add(this.labelpvaluecuttoff);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxMinSignal);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.labelStatisticallySignificant);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxTotalSignalNormalization);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownQValue);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(854, 403);
            this.splitContainer1.SplitterDistance = 66;
            this.splitContainer1.TabIndex = 1;
            // 
            // buttonLoadPLP
            // 
            this.buttonLoadPLP.Location = new System.Drawing.Point(294, 3);
            this.buttonLoadPLP.Name = "buttonLoadPLP";
            this.buttonLoadPLP.Size = new System.Drawing.Size(230, 23);
            this.buttonLoadPLP.TabIndex = 13;
            this.buttonLoadPLP.Text = "Load PatternLab project file";
            this.buttonLoadPLP.UseVisualStyleBackColor = true;
            this.buttonLoadPLP.Click += new System.EventHandler(this.buttonLoadPLP_Click);
            // 
            // buttonSavePlot
            // 
            this.buttonSavePlot.Enabled = false;
            this.buttonSavePlot.Location = new System.Drawing.Point(414, 30);
            this.buttonSavePlot.Name = "buttonSavePlot";
            this.buttonSavePlot.Size = new System.Drawing.Size(110, 23);
            this.buttonSavePlot.TabIndex = 12;
            this.buttonSavePlot.Text = "Save Plot";
            this.buttonSavePlot.UseVisualStyleBackColor = true;
            this.buttonSavePlot.Click += new System.EventHandler(this.buttonSavePlot_Click);
            // 
            // buttonRecalculate
            // 
            this.buttonRecalculate.Enabled = false;
            this.buttonRecalculate.Location = new System.Drawing.Point(294, 30);
            this.buttonRecalculate.Name = "buttonRecalculate";
            this.buttonRecalculate.Size = new System.Drawing.Size(110, 23);
            this.buttonRecalculate.TabIndex = 11;
            this.buttonRecalculate.Text = "Recalculate";
            this.buttonRecalculate.UseVisualStyleBackColor = true;
            this.buttonRecalculate.Click += new System.EventHandler(this.buttonRecalculate_Click);
            // 
            // labelpvaluecuttoff
            // 
            this.labelpvaluecuttoff.AutoSize = true;
            this.labelpvaluecuttoff.Location = new System.Drawing.Point(234, 35);
            this.labelpvaluecuttoff.Name = "labelpvaluecuttoff";
            this.labelpvaluecuttoff.Size = new System.Drawing.Size(13, 13);
            this.labelpvaluecuttoff.TabIndex = 10;
            this.labelpvaluecuttoff.Text = "?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "P-Value cuttoff: ";
            // 
            // textBoxMinSignal
            // 
            this.textBoxMinSignal.Location = new System.Drawing.Point(70, 32);
            this.textBoxMinSignal.Name = "textBoxMinSignal";
            this.textBoxMinSignal.Size = new System.Drawing.Size(69, 20);
            this.textBoxMinSignal.TabIndex = 8;
            this.textBoxMinSignal.Text = "6";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "MinSignal";
            // 
            // labelStatisticallySignificant
            // 
            this.labelStatisticallySignificant.AutoSize = true;
            this.labelStatisticallySignificant.Location = new System.Drawing.Point(652, 8);
            this.labelStatisticallySignificant.Name = "labelStatisticallySignificant";
            this.labelStatisticallySignificant.Size = new System.Drawing.Size(13, 13);
            this.labelStatisticallySignificant.TabIndex = 6;
            this.labelStatisticallySignificant.Text = "?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(531, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Statistically significant: ";
            // 
            // checkBoxTotalSignalNormalization
            // 
            this.checkBoxTotalSignalNormalization.AutoSize = true;
            this.checkBoxTotalSignalNormalization.Location = new System.Drawing.Point(145, 7);
            this.checkBoxTotalSignalNormalization.Name = "checkBoxTotalSignalNormalization";
            this.checkBoxTotalSignalNormalization.Size = new System.Drawing.Size(148, 17);
            this.checkBoxTotalSignalNormalization.TabIndex = 4;
            this.checkBoxTotalSignalNormalization.Text = "Total Signal Normalization";
            this.checkBoxTotalSignalNormalization.UseVisualStyleBackColor = true;
            this.checkBoxTotalSignalNormalization.CheckedChanged += new System.EventHandler(this.checkBoxTotalSignalNormalization_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Q-Value";
            // 
            // numericUpDownQValue
            // 
            this.numericUpDownQValue.DecimalPlaces = 2;
            this.numericUpDownQValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownQValue.Location = new System.Drawing.Point(70, 6);
            this.numericUpDownQValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownQValue.Name = "numericUpDownQValue";
            this.numericUpDownQValue.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownQValue.TabIndex = 2;
            this.numericUpDownQValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownQValue.ValueChanged += new System.EventHandler(this.numericUpDownProbability_ValueChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartAnova);
            this.splitContainer2.Size = new System.Drawing.Size(854, 333);
            this.splitContainer2.SplitterDistance = 420;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataGridViewIDs);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControlGraphData);
            this.splitContainer3.Size = new System.Drawing.Size(420, 333);
            this.splitContainer3.SplitterDistance = 166;
            this.splitContainer3.TabIndex = 1;
            // 
            // dataGridViewIDs
            // 
            this.dataGridViewIDs.AllowUserToOrderColumns = true;
            this.dataGridViewIDs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIDs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewIDs.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewIDs.Name = "dataGridViewIDs";
            this.dataGridViewIDs.Size = new System.Drawing.Size(420, 166);
            this.dataGridViewIDs.TabIndex = 0;
            this.dataGridViewIDs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewIDs_CellClick);
            this.dataGridViewIDs.Sorted += new System.EventHandler(this.dataGridViewIDs_Sorted);
            // 
            // tabControlGraphData
            // 
            this.tabControlGraphData.Controls.Add(this.tabPage1);
            this.tabControlGraphData.Controls.Add(this.tabPage2);
            this.tabControlGraphData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGraphData.Location = new System.Drawing.Point(0, 0);
            this.tabControlGraphData.Name = "tabControlGraphData";
            this.tabControlGraphData.SelectedIndex = 0;
            this.tabControlGraphData.Size = new System.Drawing.Size(420, 163);
            this.tabControlGraphData.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBoxDataPoints);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(412, 137);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Plot Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDataPoints
            // 
            this.richTextBoxDataPoints.BackColor = System.Drawing.Color.Azure;
            this.richTextBoxDataPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDataPoints.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxDataPoints.Name = "richTextBoxDataPoints";
            this.richTextBoxDataPoints.Size = new System.Drawing.Size(406, 131);
            this.richTextBoxDataPoints.TabIndex = 0;
            this.richTextBoxDataPoints.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chartHistogram);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(412, 137);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plot";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chartHistogram
            // 
            chartArea1.Name = "ChartArea1";
            this.chartHistogram.ChartAreas.Add(chartArea1);
            this.chartHistogram.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartHistogram.Legends.Add(legend1);
            this.chartHistogram.Location = new System.Drawing.Point(3, 3);
            this.chartHistogram.Name = "chartHistogram";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartHistogram.Series.Add(series1);
            this.chartHistogram.Size = new System.Drawing.Size(406, 131);
            this.chartHistogram.TabIndex = 0;
            this.chartHistogram.Text = "chart1";
            title1.Name = "Quantitation Histogram";
            this.chartHistogram.Titles.Add(title1);
            // 
            // chartAnova
            // 
            chartArea2.Name = "ChartArea1";
            this.chartAnova.ChartAreas.Add(chartArea2);
            this.chartAnova.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartAnova.Legends.Add(legend2);
            this.chartAnova.Location = new System.Drawing.Point(0, 0);
            this.chartAnova.Name = "chartAnova";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartAnova.Series.Add(series2);
            this.chartAnova.Size = new System.Drawing.Size(430, 333);
            this.chartAnova.TabIndex = 0;
            this.chartAnova.Text = "chart1";
            title2.Name = "Title1";
            this.chartAnova.Titles.Add(title2);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AnovaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "AnovaControl";
            this.Size = new System.Drawing.Size(854, 403);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQValue)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIDs)).EndInit();
            this.tabControlGraphData.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnova)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridViewIDs;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAnova;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NumericUpDown numericUpDownQValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxTotalSignalNormalization;
        private System.Windows.Forms.Label labelStatisticallySignificant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistogram;
        private System.Windows.Forms.TextBox textBoxMinSignal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelpvaluecuttoff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonRecalculate;
        private System.Windows.Forms.Button buttonSavePlot;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabControl tabControlGraphData;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBoxDataPoints;
        private System.Windows.Forms.Button buttonLoadPLP;
    }
}
