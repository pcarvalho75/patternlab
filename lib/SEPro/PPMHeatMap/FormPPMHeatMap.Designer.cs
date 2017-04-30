namespace SEProcessor.PPMHeatMap
{
    partial class FormPPMHeatMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPPMHeatMap));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownScaleMaximum = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownScaleMiddle = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownScaleMinimum = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownStepsMZ = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownStepsSCN = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMinScan = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownMaxMZ = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxScan = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMinMZ = new System.Windows.Forms.NumericUpDown();
            this.comboBoxFiles = new System.Windows.Forms.ComboBox();
            this.buttonPlot = new System.Windows.Forms.Button();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.heatMap1 = new PatternTools.HeatMap();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaleMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaleMiddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaleMinimum)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStepsMZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStepsSCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxMZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinMZ)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxFiles);
            this.splitContainer1.Panel1.Controls.Add(this.buttonPlot);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.elementHost1);
            this.splitContainer1.Size = new System.Drawing.Size(744, 524);
            this.splitContainer1.SplitterDistance = 175;
            this.splitContainer1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(399, 90);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(307, 42);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "In the X axis the scan numbers are increasing from left to right. In the Y axis M" +
    "Zs are increasing bottom to top.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownScaleMaximum);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numericUpDownScaleMiddle);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numericUpDownScaleMinimum);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(12, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 53);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scale";
            // 
            // numericUpDownScaleMaximum
            // 
            this.numericUpDownScaleMaximum.Location = new System.Drawing.Point(297, 24);
            this.numericUpDownScaleMaximum.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownScaleMaximum.Name = "numericUpDownScaleMaximum";
            this.numericUpDownScaleMaximum.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownScaleMaximum.TabIndex = 5;
            this.numericUpDownScaleMaximum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(237, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Maximum";
            // 
            // numericUpDownScaleMiddle
            // 
            this.numericUpDownScaleMiddle.Location = new System.Drawing.Point(171, 24);
            this.numericUpDownScaleMiddle.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownScaleMiddle.Name = "numericUpDownScaleMiddle";
            this.numericUpDownScaleMiddle.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownScaleMiddle.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(127, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Middle";
            // 
            // numericUpDownScaleMinimum
            // 
            this.numericUpDownScaleMinimum.Location = new System.Drawing.Point(59, 24);
            this.numericUpDownScaleMinimum.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownScaleMinimum.Name = "numericUpDownScaleMinimum";
            this.numericUpDownScaleMinimum.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownScaleMinimum.TabIndex = 1;
            this.numericUpDownScaleMinimum.Value = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Minimum";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownStepsMZ);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numericUpDownStepsSCN);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDownMinScan);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownMaxMZ);
            this.groupBox1.Controls.Add(this.numericUpDownMaxScan);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericUpDownMinMZ);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 63);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bounds";
            // 
            // numericUpDownStepsMZ
            // 
            this.numericUpDownStepsMZ.Location = new System.Drawing.Point(660, 23);
            this.numericUpDownStepsMZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownStepsMZ.Name = "numericUpDownStepsMZ";
            this.numericUpDownStepsMZ.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownStepsMZ.TabIndex = 13;
            this.numericUpDownStepsMZ.Value = new decimal(new int[] {
            35,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(606, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "StepsMZ";
            // 
            // numericUpDownStepsSCN
            // 
            this.numericUpDownStepsSCN.Location = new System.Drawing.Point(540, 23);
            this.numericUpDownStepsSCN.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownStepsSCN.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownStepsSCN.Name = "numericUpDownStepsSCN";
            this.numericUpDownStepsSCN.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownStepsSCN.TabIndex = 11;
            this.numericUpDownStepsSCN.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(478, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "StepsSCN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "MinScan";
            // 
            // numericUpDownMinScan
            // 
            this.numericUpDownMinScan.Location = new System.Drawing.Point(61, 23);
            this.numericUpDownMinScan.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownMinScan.Name = "numericUpDownMinScan";
            this.numericUpDownMinScan.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownMinScan.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MaxScan";
            // 
            // numericUpDownMaxMZ
            // 
            this.numericUpDownMaxMZ.Location = new System.Drawing.Point(412, 23);
            this.numericUpDownMaxMZ.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownMaxMZ.Name = "numericUpDownMaxMZ";
            this.numericUpDownMaxMZ.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownMaxMZ.TabIndex = 9;
            // 
            // numericUpDownMaxScan
            // 
            this.numericUpDownMaxScan.Location = new System.Drawing.Point(185, 23);
            this.numericUpDownMaxScan.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownMaxScan.Name = "numericUpDownMaxScan";
            this.numericUpDownMaxScan.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownMaxScan.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "MaxMZ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "MinMZ";
            // 
            // numericUpDownMinMZ
            // 
            this.numericUpDownMinMZ.Location = new System.Drawing.Point(297, 23);
            this.numericUpDownMinMZ.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownMinMZ.Name = "numericUpDownMinMZ";
            this.numericUpDownMinMZ.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownMinMZ.TabIndex = 8;
            // 
            // comboBoxFiles
            // 
            this.comboBoxFiles.FormattingEnabled = true;
            this.comboBoxFiles.Location = new System.Drawing.Point(12, 140);
            this.comboBoxFiles.Name = "comboBoxFiles";
            this.comboBoxFiles.Size = new System.Drawing.Size(639, 21);
            this.comboBoxFiles.TabIndex = 1;
            this.comboBoxFiles.SelectedIndexChanged += new System.EventHandler(this.comboBoxFiles_SelectedIndexChanged);
            // 
            // buttonPlot
            // 
            this.buttonPlot.Location = new System.Drawing.Point(657, 138);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(75, 23);
            this.buttonPlot.TabIndex = 0;
            this.buttonPlot.Text = "Plot";
            this.buttonPlot.UseVisualStyleBackColor = true;
            this.buttonPlot.Click += new System.EventHandler(this.buttonPlot_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(744, 345);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.heatMap1;
            // 
            // FormPPMHeatMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 524);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPPMHeatMap";
            this.Text = "PPM HeatMap";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaleMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaleMiddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaleMinimum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStepsMZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStepsSCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxMZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinMZ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonPlot;
        private System.Windows.Forms.ComboBox comboBoxFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownMinScan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxScan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownMinMZ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxMZ;
        private System.Windows.Forms.NumericUpDown numericUpDownStepsMZ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownStepsSCN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownScaleMiddle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownScaleMinimum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownScaleMaximum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private PatternTools.HeatMap heatMap1;
    }
}