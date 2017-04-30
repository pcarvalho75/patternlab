namespace SEProQ
{
    partial class MassDistributionViewer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonClearTB = new System.Windows.Forms.Button();
            this.checkBoxLogLFQ = new System.Windows.Forms.CheckBox();
            this.numericUpDownYMax = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownMaxValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMinValue = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownNoBins = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerGraph = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonUseBins = new System.Windows.Forms.Button();
            this.richTextBoxData = new System.Windows.Forms.RichTextBox();
            this.buttonPlot = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoBins)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGraph)).BeginInit();
            this.splitContainerGraph.Panel1.SuspendLayout();
            this.splitContainerGraph.Panel2.SuspendLayout();
            this.splitContainerGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonClearTB);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxLogLFQ);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownYMax);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownMaxValue);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownMinValue);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownNoBins);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainerGraph);
            this.splitContainer1.Size = new System.Drawing.Size(1083, 656);
            this.splitContainer1.SplitterDistance = 66;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // buttonClearTB
            // 
            this.buttonClearTB.Location = new System.Drawing.Point(843, 39);
            this.buttonClearTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonClearTB.Name = "buttonClearTB";
            this.buttonClearTB.Size = new System.Drawing.Size(236, 28);
            this.buttonClearTB.TabIndex = 11;
            this.buttonClearTB.Text = "Clear TB";
            this.buttonClearTB.UseVisualStyleBackColor = true;
            this.buttonClearTB.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxLogLFQ
            // 
            this.checkBoxLogLFQ.AutoSize = true;
            this.checkBoxLogLFQ.Location = new System.Drawing.Point(699, 44);
            this.checkBoxLogLFQ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxLogLFQ.Name = "checkBoxLogLFQ";
            this.checkBoxLogLFQ.Size = new System.Drawing.Size(85, 21);
            this.checkBoxLogLFQ.TabIndex = 9;
            this.checkBoxLogLFQ.Text = "Log LFQ";
            this.checkBoxLogLFQ.UseVisualStyleBackColor = true;
            // 
            // numericUpDownYMax
            // 
            this.numericUpDownYMax.Location = new System.Drawing.Point(595, 43);
            this.numericUpDownYMax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownYMax.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownYMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownYMax.Name = "numericUpDownYMax";
            this.numericUpDownYMax.Size = new System.Drawing.Size(96, 22);
            this.numericUpDownYMax.TabIndex = 8;
            this.numericUpDownYMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(541, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "YMax";
            // 
            // numericUpDownMaxValue
            // 
            this.numericUpDownMaxValue.Location = new System.Drawing.Point(449, 43);
            this.numericUpDownMaxValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownMaxValue.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numericUpDownMaxValue.Name = "numericUpDownMaxValue";
            this.numericUpDownMaxValue.Size = new System.Drawing.Size(84, 22);
            this.numericUpDownMaxValue.TabIndex = 6;
            this.numericUpDownMaxValue.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "MaxValue";
            // 
            // numericUpDownMinValue
            // 
            this.numericUpDownMinValue.Location = new System.Drawing.Point(268, 43);
            this.numericUpDownMinValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownMinValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownMinValue.Name = "numericUpDownMinValue";
            this.numericUpDownMinValue.Size = new System.Drawing.Size(93, 22);
            this.numericUpDownMinValue.TabIndex = 4;
            this.numericUpDownMinValue.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "MinValue";
            // 
            // numericUpDownNoBins
            // 
            this.numericUpDownNoBins.Location = new System.Drawing.Point(103, 43);
            this.numericUpDownNoBins.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownNoBins.Name = "numericUpDownNoBins";
            this.numericUpDownNoBins.Size = new System.Drawing.Size(81, 22);
            this.numericUpDownNoBins.TabIndex = 2;
            this.numericUpDownNoBins.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "No Bins:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1083, 28);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveGraphToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveGraphToolStripMenuItem
            // 
            this.saveGraphToolStripMenuItem.Name = "saveGraphToolStripMenuItem";
            this.saveGraphToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.saveGraphToolStripMenuItem.Text = "Save Graph";
            this.saveGraphToolStripMenuItem.Click += new System.EventHandler(this.saveGraphToolStripMenuItem_Click);
            // 
            // splitContainerGraph
            // 
            this.splitContainerGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerGraph.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerGraph.Location = new System.Drawing.Point(0, 0);
            this.splitContainerGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerGraph.Name = "splitContainerGraph";
            // 
            // splitContainerGraph.Panel1
            // 
            this.splitContainerGraph.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainerGraph.Panel2
            // 
            this.splitContainerGraph.Panel2.Controls.Add(this.buttonUseBins);
            this.splitContainerGraph.Panel2.Controls.Add(this.richTextBoxData);
            this.splitContainerGraph.Panel2.Controls.Add(this.buttonPlot);
            this.splitContainerGraph.Size = new System.Drawing.Size(1083, 585);
            this.splitContainerGraph.SplitterDistance = 895;
            this.splitContainerGraph.SplitterWidth = 5;
            this.splitContainerGraph.TabIndex = 1;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "SeriesHistogram";
            series1.YValuesPerPoint = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.ErrorBar;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "SeriesErrorBar";
            series2.YValuesPerPoint = 3;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(895, 585);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // buttonUseBins
            // 
            this.buttonUseBins.Location = new System.Drawing.Point(112, 4);
            this.buttonUseBins.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonUseBins.Name = "buttonUseBins";
            this.buttonUseBins.Size = new System.Drawing.Size(100, 28);
            this.buttonUseBins.TabIndex = 1;
            this.buttonUseBins.Text = "Use Bins";
            this.buttonUseBins.UseVisualStyleBackColor = true;
            this.buttonUseBins.Click += new System.EventHandler(this.buttonUseBins_Click);
            // 
            // richTextBoxData
            // 
            this.richTextBoxData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxData.BackColor = System.Drawing.Color.SeaShell;
            this.richTextBoxData.Location = new System.Drawing.Point(4, 39);
            this.richTextBoxData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBoxData.Name = "richTextBoxData";
            this.richTextBoxData.Size = new System.Drawing.Size(235, 545);
            this.richTextBoxData.TabIndex = 0;
            this.richTextBoxData.Text = "";
            // 
            // buttonPlot
            // 
            this.buttonPlot.Location = new System.Drawing.Point(4, 4);
            this.buttonPlot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(100, 28);
            this.buttonPlot.TabIndex = 0;
            this.buttonPlot.Text = "Bin Data";
            this.buttonPlot.UseVisualStyleBackColor = true;
            this.buttonPlot.Click += new System.EventHandler(this.buttonPlot_Click);
            // 
            // MassDistributionViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 656);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MassDistributionViewer";
            this.Text = "Histogram Maker";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoBins)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainerGraph.Panel1.ResumeLayout(false);
            this.splitContainerGraph.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGraph)).EndInit();
            this.splitContainerGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button buttonPlot;
        private System.Windows.Forms.NumericUpDown numericUpDownNoBins;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownYMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownMinValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainerGraph;
        private System.Windows.Forms.CheckBox checkBoxLogLFQ;
        private System.Windows.Forms.RichTextBox richTextBoxData;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGraphToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonClearTB;
        private System.Windows.Forms.Button buttonUseBins;
    }
}