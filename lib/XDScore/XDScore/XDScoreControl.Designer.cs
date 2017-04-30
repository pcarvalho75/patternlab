namespace XDScore
{
    partial class XDScoreControl
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
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.buttonGo = new System.Windows.Forms.Button();
            this.groupBoxDeltaScore = new System.Windows.Forms.GroupBox();
            this.richTextBoxDeltaScores = new System.Windows.Forms.RichTextBox();
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.buttonGMM = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.numericUpDownXCorrPrime = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxCalculateDeltaScores = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownNoPhosphoSites = new System.Windows.Forms.NumericUpDown();
            this.buttonCalculateDeltaScores = new System.Windows.Forms.Button();
            this.numericUpDownModelChargeState = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.groupBoxEstablishQuality = new System.Windows.Forms.GroupBox();
            this.buttonBrowseForSEProFile = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageScans = new System.Windows.Forms.TabPage();
            this.richTextBoxResultScans = new System.Windows.Forms.RichTextBox();
            this.tabPagePeptides = new System.Windows.Forms.TabPage();
            this.richTextBoxPeptides = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonGetGoodHitsFromSEPro = new System.Windows.Forms.Button();
            this.textBoxSEProFileToExtractGoodPeptides = new System.Windows.Forms.TextBox();
            this.numericUpDownHistogramBins = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxPlot = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chartHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartProbability = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBoxDeltaScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXCorrPrime)).BeginInit();
            this.groupBoxCalculateDeltaScores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoPhosphoSites)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownModelChargeState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.groupBoxEstablishQuality.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageScans.SuspendLayout();
            this.tabPagePeptides.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistogramBins)).BeginInit();
            this.groupBoxPlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProbability)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(53, 5);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(791, 23);
            this.buttonGo.TabIndex = 2;
            this.buttonGo.Text = "Load SQTs";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // groupBoxDeltaScore
            // 
            this.groupBoxDeltaScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxDeltaScore.Controls.Add(this.richTextBoxDeltaScores);
            this.groupBoxDeltaScore.Location = new System.Drawing.Point(6, 134);
            this.groupBoxDeltaScore.Name = "groupBoxDeltaScore";
            this.groupBoxDeltaScore.Size = new System.Drawing.Size(149, 361);
            this.groupBoxDeltaScore.TabIndex = 3;
            this.groupBoxDeltaScore.TabStop = false;
            this.groupBoxDeltaScore.Text = "DeltaScores";
            // 
            // richTextBoxDeltaScores
            // 
            this.richTextBoxDeltaScores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDeltaScores.Location = new System.Drawing.Point(3, 16);
            this.richTextBoxDeltaScores.Name = "richTextBoxDeltaScores";
            this.richTextBoxDeltaScores.Size = new System.Drawing.Size(143, 342);
            this.richTextBoxDeltaScores.TabIndex = 0;
            this.richTextBoxDeltaScores.Text = "";
            // 
            // checkBoxLog
            // 
            this.checkBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLog.AutoSize = true;
            this.checkBoxLog.Checked = true;
            this.checkBoxLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLog.Enabled = false;
            this.checkBoxLog.Location = new System.Drawing.Point(3, 9);
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.Size = new System.Drawing.Size(44, 17);
            this.checkBoxLog.TabIndex = 6;
            this.checkBoxLog.Text = "Log";
            this.checkBoxLog.UseVisualStyleBackColor = true;
            // 
            // buttonGMM
            // 
            this.buttonGMM.Location = new System.Drawing.Point(253, 16);
            this.buttonGMM.Name = "buttonGMM";
            this.buttonGMM.Size = new System.Drawing.Size(102, 23);
            this.buttonGMM.TabIndex = 8;
            this.buttonGMM.Text = "Generate GMM";
            this.buttonGMM.UseVisualStyleBackColor = true;
            this.buttonGMM.Click += new System.EventHandler(this.buttonGMM_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // numericUpDownXCorrPrime
            // 
            this.numericUpDownXCorrPrime.DecimalPlaces = 2;
            this.numericUpDownXCorrPrime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownXCorrPrime.Location = new System.Drawing.Point(103, 19);
            this.numericUpDownXCorrPrime.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownXCorrPrime.Name = "numericUpDownXCorrPrime";
            this.numericUpDownXCorrPrime.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownXCorrPrime.TabIndex = 22;
            this.numericUpDownXCorrPrime.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "PrimeDeltaXCor";
            // 
            // groupBoxCalculateDeltaScores
            // 
            this.groupBoxCalculateDeltaScores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxCalculateDeltaScores.Controls.Add(this.label2);
            this.groupBoxCalculateDeltaScores.Controls.Add(this.label7);
            this.groupBoxCalculateDeltaScores.Controls.Add(this.numericUpDownNoPhosphoSites);
            this.groupBoxCalculateDeltaScores.Controls.Add(this.buttonCalculateDeltaScores);
            this.groupBoxCalculateDeltaScores.Controls.Add(this.groupBoxDeltaScore);
            this.groupBoxCalculateDeltaScores.Controls.Add(this.numericUpDownModelChargeState);
            this.groupBoxCalculateDeltaScores.Controls.Add(this.label5);
            this.groupBoxCalculateDeltaScores.Enabled = false;
            this.groupBoxCalculateDeltaScores.Location = new System.Drawing.Point(13, 3);
            this.groupBoxCalculateDeltaScores.Name = "groupBoxCalculateDeltaScores";
            this.groupBoxCalculateDeltaScores.Size = new System.Drawing.Size(170, 501);
            this.groupBoxCalculateDeltaScores.TabIndex = 18;
            this.groupBoxCalculateDeltaScores.TabStop = false;
            this.groupBoxCalculateDeltaScores.Text = "Step 2: Calculate Delta Scores";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "* 0 for all possibilities jointly";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "NoPhosphosites";
            // 
            // numericUpDownNoPhosphoSites
            // 
            this.numericUpDownNoPhosphoSites.Location = new System.Drawing.Point(113, 47);
            this.numericUpDownNoPhosphoSites.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownNoPhosphoSites.Name = "numericUpDownNoPhosphoSites";
            this.numericUpDownNoPhosphoSites.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownNoPhosphoSites.TabIndex = 19;
            // 
            // buttonCalculateDeltaScores
            // 
            this.buttonCalculateDeltaScores.Location = new System.Drawing.Point(9, 105);
            this.buttonCalculateDeltaScores.Name = "buttonCalculateDeltaScores";
            this.buttonCalculateDeltaScores.Size = new System.Drawing.Size(146, 23);
            this.buttonCalculateDeltaScores.TabIndex = 18;
            this.buttonCalculateDeltaScores.Text = "Calculate";
            this.buttonCalculateDeltaScores.UseVisualStyleBackColor = true;
            this.buttonCalculateDeltaScores.Click += new System.EventHandler(this.buttonCalculateDeltaScores_Click);
            // 
            // numericUpDownModelChargeState
            // 
            this.numericUpDownModelChargeState.Location = new System.Drawing.Point(113, 21);
            this.numericUpDownModelChargeState.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownModelChargeState.Name = "numericUpDownModelChargeState";
            this.numericUpDownModelChargeState.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownModelChargeState.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Model Charge State";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.buttonGo);
            this.splitContainerMain.Panel1.Controls.Add(this.checkBoxLog);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.groupBoxEstablishQuality);
            this.splitContainerMain.Panel2.Controls.Add(this.groupBoxCalculateDeltaScores);
            this.splitContainerMain.Size = new System.Drawing.Size(856, 561);
            this.splitContainerMain.SplitterDistance = 38;
            this.splitContainerMain.TabIndex = 17;
            // 
            // groupBoxEstablishQuality
            // 
            this.groupBoxEstablishQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEstablishQuality.AutoSize = true;
            this.groupBoxEstablishQuality.Controls.Add(this.buttonBrowseForSEProFile);
            this.groupBoxEstablishQuality.Controls.Add(this.tabControl1);
            this.groupBoxEstablishQuality.Controls.Add(this.label8);
            this.groupBoxEstablishQuality.Controls.Add(this.buttonGetGoodHitsFromSEPro);
            this.groupBoxEstablishQuality.Controls.Add(this.textBoxSEProFileToExtractGoodPeptides);
            this.groupBoxEstablishQuality.Controls.Add(this.numericUpDownHistogramBins);
            this.groupBoxEstablishQuality.Controls.Add(this.label6);
            this.groupBoxEstablishQuality.Controls.Add(this.label4);
            this.groupBoxEstablishQuality.Controls.Add(this.buttonGMM);
            this.groupBoxEstablishQuality.Controls.Add(this.groupBoxPlot);
            this.groupBoxEstablishQuality.Controls.Add(this.numericUpDownXCorrPrime);
            this.groupBoxEstablishQuality.Enabled = false;
            this.groupBoxEstablishQuality.Location = new System.Drawing.Point(189, 3);
            this.groupBoxEstablishQuality.Name = "groupBoxEstablishQuality";
            this.groupBoxEstablishQuality.Size = new System.Drawing.Size(655, 501);
            this.groupBoxEstablishQuality.TabIndex = 26;
            this.groupBoxEstablishQuality.TabStop = false;
            this.groupBoxEstablishQuality.Text = "Step 3 : Establish Quality";
            // 
            // buttonBrowseForSEProFile
            // 
            this.buttonBrowseForSEProFile.Location = new System.Drawing.Point(253, 49);
            this.buttonBrowseForSEProFile.Name = "buttonBrowseForSEProFile";
            this.buttonBrowseForSEProFile.Size = new System.Drawing.Size(102, 23);
            this.buttonBrowseForSEProFile.TabIndex = 30;
            this.buttonBrowseForSEProFile.Text = "Browse";
            this.buttonBrowseForSEProFile.UseVisualStyleBackColor = true;
            this.buttonBrowseForSEProFile.Click += new System.EventHandler(this.buttonBrowseForSEProFile_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageScans);
            this.tabControl1.Controls.Add(this.tabPagePeptides);
            this.tabControl1.Location = new System.Drawing.Point(361, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(288, 100);
            this.tabControl1.TabIndex = 29;
            // 
            // tabPageScans
            // 
            this.tabPageScans.Controls.Add(this.richTextBoxResultScans);
            this.tabPageScans.Location = new System.Drawing.Point(4, 22);
            this.tabPageScans.Name = "tabPageScans";
            this.tabPageScans.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScans.Size = new System.Drawing.Size(280, 74);
            this.tabPageScans.TabIndex = 0;
            this.tabPageScans.Text = "Scans";
            this.tabPageScans.UseVisualStyleBackColor = true;
            // 
            // richTextBoxResultScans
            // 
            this.richTextBoxResultScans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxResultScans.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxResultScans.Name = "richTextBoxResultScans";
            this.richTextBoxResultScans.Size = new System.Drawing.Size(274, 68);
            this.richTextBoxResultScans.TabIndex = 0;
            this.richTextBoxResultScans.Text = "";
            // 
            // tabPagePeptides
            // 
            this.tabPagePeptides.Controls.Add(this.richTextBoxPeptides);
            this.tabPagePeptides.Location = new System.Drawing.Point(4, 22);
            this.tabPagePeptides.Name = "tabPagePeptides";
            this.tabPagePeptides.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePeptides.Size = new System.Drawing.Size(280, 74);
            this.tabPagePeptides.TabIndex = 1;
            this.tabPagePeptides.Text = "Peptides";
            this.tabPagePeptides.UseVisualStyleBackColor = true;
            // 
            // richTextBoxPeptides
            // 
            this.richTextBoxPeptides.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxPeptides.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxPeptides.Name = "richTextBoxPeptides";
            this.richTextBoxPeptides.Size = new System.Drawing.Size(274, 68);
            this.richTextBoxPeptides.TabIndex = 1;
            this.richTextBoxPeptides.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "SEPro File";
            // 
            // buttonGetGoodHitsFromSEPro
            // 
            this.buttonGetGoodHitsFromSEPro.Enabled = false;
            this.buttonGetGoodHitsFromSEPro.Location = new System.Drawing.Point(34, 78);
            this.buttonGetGoodHitsFromSEPro.Name = "buttonGetGoodHitsFromSEPro";
            this.buttonGetGoodHitsFromSEPro.Size = new System.Drawing.Size(321, 23);
            this.buttonGetGoodHitsFromSEPro.TabIndex = 27;
            this.buttonGetGoodHitsFromSEPro.Text = "Score SEPro Hits";
            this.buttonGetGoodHitsFromSEPro.UseVisualStyleBackColor = true;
            this.buttonGetGoodHitsFromSEPro.Click += new System.EventHandler(this.buttonGetGoodHitsFromSEPro_Click);
            // 
            // textBoxSEProFileToExtractGoodPeptides
            // 
            this.textBoxSEProFileToExtractGoodPeptides.Location = new System.Drawing.Point(103, 51);
            this.textBoxSEProFileToExtractGoodPeptides.Name = "textBoxSEProFileToExtractGoodPeptides";
            this.textBoxSEProFileToExtractGoodPeptides.Size = new System.Drawing.Size(144, 20);
            this.textBoxSEProFileToExtractGoodPeptides.TabIndex = 26;
            // 
            // numericUpDownHistogramBins
            // 
            this.numericUpDownHistogramBins.Location = new System.Drawing.Point(198, 19);
            this.numericUpDownHistogramBins.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownHistogramBins.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHistogramBins.Name = "numericUpDownHistogramBins";
            this.numericUpDownHistogramBins.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownHistogramBins.TabIndex = 25;
            this.numericUpDownHistogramBins.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Bins :";
            // 
            // groupBoxPlot
            // 
            this.groupBoxPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPlot.Controls.Add(this.splitContainer1);
            this.groupBoxPlot.Location = new System.Drawing.Point(9, 114);
            this.groupBoxPlot.Name = "groupBoxPlot";
            this.groupBoxPlot.Size = new System.Drawing.Size(640, 381);
            this.groupBoxPlot.TabIndex = 23;
            this.groupBoxPlot.TabStop = false;
            this.groupBoxPlot.Text = "Plot";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chartHistogram);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chartProbability);
            this.splitContainer1.Size = new System.Drawing.Size(634, 362);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // chartHistogram
            // 
            chartArea1.AxisX.Title = "ln(XD-Score)";
            chartArea1.AxisY.Title = "# IDs";
            chartArea1.Name = "ChartArea1";
            this.chartHistogram.ChartAreas.Add(chartArea1);
            this.chartHistogram.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartHistogram.Legends.Add(legend1);
            this.chartHistogram.Location = new System.Drawing.Point(0, 0);
            this.chartHistogram.Name = "chartHistogram";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "SeriesHistogramBars";
            series2.BorderWidth = 5;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "SeriesLowMeanNormal";
            series3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series3.BorderWidth = 5;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "SeriesHighMeanNormal";
            this.chartHistogram.Series.Add(series1);
            this.chartHistogram.Series.Add(series2);
            this.chartHistogram.Series.Add(series3);
            this.chartHistogram.Size = new System.Drawing.Size(634, 210);
            this.chartHistogram.TabIndex = 1;
            this.chartHistogram.Text = "chart1";
            // 
            // chartProbability
            // 
            chartArea2.AxisX.Title = "ln(XD-Score)";
            chartArea2.AxisY.Title = "Probability";
            chartArea2.Name = "ChartArea1";
            this.chartProbability.ChartAreas.Add(chartArea2);
            this.chartProbability.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartProbability.Legends.Add(legend2);
            this.chartProbability.Location = new System.Drawing.Point(0, 0);
            this.chartProbability.Name = "chartProbability";
            this.chartProbability.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series4.BorderWidth = 7;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series4.IsVisibleInLegend = false;
            series4.Legend = "Legend1";
            series4.MarkerBorderWidth = 7;
            series4.MarkerColor = System.Drawing.Color.Purple;
            series4.MarkerSize = 7;
            series4.Name = "SeriesPValue";
            series5.BorderWidth = 3;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Red;
            series5.IsVisibleInLegend = false;
            series5.Legend = "Legend1";
            series5.MarkerSize = 4;
            series5.Name = "SeriesCumulativeBad";
            series6.BorderWidth = 3;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.Lime;
            series6.IsVisibleInLegend = false;
            series6.Legend = "Legend1";
            series6.MarkerSize = 4;
            series6.Name = "SeriesCumulativeGood";
            series7.BorderWidth = 7;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Purple;
            series7.IsVisibleInLegend = false;
            series7.Legend = "Legend1";
            series7.MarkerColor = System.Drawing.Color.Purple;
            series7.MarkerSize = 7;
            series7.Name = "SeriesPValueCumulativeIntersect";
            this.chartProbability.Series.Add(series4);
            this.chartProbability.Series.Add(series5);
            this.chartProbability.Series.Add(series6);
            this.chartProbability.Series.Add(series7);
            this.chartProbability.Size = new System.Drawing.Size(634, 148);
            this.chartProbability.TabIndex = 1;
            this.chartProbability.Text = "chart1";
            // 
            // XDScoreControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "XDScoreControl";
            this.Size = new System.Drawing.Size(856, 561);
            this.groupBoxDeltaScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXCorrPrime)).EndInit();
            this.groupBoxCalculateDeltaScores.ResumeLayout(false);
            this.groupBoxCalculateDeltaScores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoPhosphoSites)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownModelChargeState)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.groupBoxEstablishQuality.ResumeLayout(false);
            this.groupBoxEstablishQuality.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageScans.ResumeLayout(false);
            this.tabPagePeptides.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistogramBins)).EndInit();
            this.groupBoxPlot.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProbability)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.GroupBox groupBoxDeltaScore;
        private System.Windows.Forms.RichTextBox richTextBoxDeltaScores;
        private System.Windows.Forms.CheckBox checkBoxLog;
        private System.Windows.Forms.Button buttonGMM;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.NumericUpDown numericUpDownModelChargeState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBoxCalculateDeltaScores;
        private System.Windows.Forms.Button buttonCalculateDeltaScores;
        private System.Windows.Forms.NumericUpDown numericUpDownXCorrPrime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBoxPlot;
        private System.Windows.Forms.NumericUpDown numericUpDownHistogramBins;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistogram;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProbability;
        private System.Windows.Forms.RichTextBox richTextBoxResultScans;
        private System.Windows.Forms.GroupBox groupBoxEstablishQuality;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownNoPhosphoSites;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonGetGoodHitsFromSEPro;
        private System.Windows.Forms.TextBox textBoxSEProFileToExtractGoodPeptides;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageScans;
        private System.Windows.Forms.TabPage tabPagePeptides;
        private System.Windows.Forms.RichTextBox richTextBoxPeptides;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBrowseForSEProFile;
    }
}

