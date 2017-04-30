namespace TrendQuest
{
    partial class TrendQuestControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerControls = new System.Windows.Forms.SplitContainer();
            this.groupBoxStep1 = new System.Windows.Forms.GroupBox();
            this.buttonLoadPLP = new System.Windows.Forms.Button();
            this.groupBoxStep2 = new System.Windows.Forms.GroupBox();
            this.tabControlStep2 = new System.Windows.Forms.TabControl();
            this.tabPageClustering = new System.Windows.Forms.TabPage();
            this.splitContainerClustering = new System.Windows.Forms.SplitContainer();
            this.buttonPrepareForClustering = new System.Windows.Forms.Button();
            this.textBoxMinAvgSignalCluster = new System.Windows.Forms.TextBox();
            this.numericUpDownMinDatapointsClustering = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinNodesPerCluster = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelNoElements = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxClusteringStep3 = new System.Windows.Forms.GroupBox();
            this.splitContainerClusteringResults = new System.Windows.Forms.SplitContainer();
            this.checkBoxPlotCOnsensus = new System.Windows.Forms.CheckBox();
            this.buttonCluster = new System.Windows.Forms.Button();
            this.buttonPlotCluster = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonExpAll = new System.Windows.Forms.Button();
            this.buttonUncheck2 = new System.Windows.Forms.Button();
            this.numericUpDownTargetClusterNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewCluster = new System.Windows.Forms.DataGridView();
            this.ColumnPlot = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnColClusterElements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEuclidianSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPagePlotData = new System.Windows.Forms.TabPage();
            this.dataGridViewPlotData = new System.Windows.Forms.DataGridView();
            this.buttonRefreshPlot = new System.Windows.Forms.Button();
            this.tabPageSavePlot = new System.Windows.Forms.TabPage();
            this.buttonSavePlot = new System.Windows.Forms.Button();
            this.buttonSaveGraphData = new System.Windows.Forms.Button();
            this.chartTrend = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControls)).BeginInit();
            this.splitContainerControls.Panel1.SuspendLayout();
            this.splitContainerControls.Panel2.SuspendLayout();
            this.splitContainerControls.SuspendLayout();
            this.groupBoxStep1.SuspendLayout();
            this.groupBoxStep2.SuspendLayout();
            this.tabControlStep2.SuspendLayout();
            this.tabPageClustering.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerClustering)).BeginInit();
            this.splitContainerClustering.Panel1.SuspendLayout();
            this.splitContainerClustering.Panel2.SuspendLayout();
            this.splitContainerClustering.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinDatapointsClustering)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinNodesPerCluster)).BeginInit();
            this.groupBoxClusteringStep3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerClusteringResults)).BeginInit();
            this.splitContainerClusteringResults.Panel1.SuspendLayout();
            this.splitContainerClusteringResults.Panel2.SuspendLayout();
            this.splitContainerClusteringResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetClusterNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCluster)).BeginInit();
            this.tabPagePlotData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlotData)).BeginInit();
            this.tabPageSavePlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrend)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerControls);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.chartTrend);
            this.splitContainerMain.Size = new System.Drawing.Size(1107, 630);
            this.splitContainerMain.SplitterDistance = 392;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 0;
            // 
            // splitContainerControls
            // 
            this.splitContainerControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControls.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerControls.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControls.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerControls.Name = "splitContainerControls";
            this.splitContainerControls.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerControls.Panel1
            // 
            this.splitContainerControls.Panel1.Controls.Add(this.groupBoxStep1);
            // 
            // splitContainerControls.Panel2
            // 
            this.splitContainerControls.Panel2.Controls.Add(this.groupBoxStep2);
            this.splitContainerControls.Size = new System.Drawing.Size(392, 630);
            this.splitContainerControls.SplitterDistance = 60;
            this.splitContainerControls.SplitterWidth = 5;
            this.splitContainerControls.TabIndex = 12;
            // 
            // groupBoxStep1
            // 
            this.groupBoxStep1.Controls.Add(this.buttonLoadPLP);
            this.groupBoxStep1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxStep1.Location = new System.Drawing.Point(0, 0);
            this.groupBoxStep1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxStep1.Name = "groupBoxStep1";
            this.groupBoxStep1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxStep1.Size = new System.Drawing.Size(392, 60);
            this.groupBoxStep1.TabIndex = 11;
            this.groupBoxStep1.TabStop = false;
            this.groupBoxStep1.Text = "Step 1: Define input parameters";
            this.groupBoxStep1.Enter += new System.EventHandler(this.groupBoxStep1_Enter);
            // 
            // buttonLoadPLP
            // 
            this.buttonLoadPLP.Location = new System.Drawing.Point(9, 27);
            this.buttonLoadPLP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonLoadPLP.Name = "buttonLoadPLP";
            this.buttonLoadPLP.Size = new System.Drawing.Size(496, 28);
            this.buttonLoadPLP.TabIndex = 0;
            this.buttonLoadPLP.Text = "Load PatternLab project file";
            this.buttonLoadPLP.UseVisualStyleBackColor = true;
            this.buttonLoadPLP.Click += new System.EventHandler(this.buttonLoadPLP_Click);
            // 
            // groupBoxStep2
            // 
            this.groupBoxStep2.Controls.Add(this.tabControlStep2);
            this.groupBoxStep2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxStep2.Enabled = false;
            this.groupBoxStep2.Location = new System.Drawing.Point(0, 0);
            this.groupBoxStep2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxStep2.Name = "groupBoxStep2";
            this.groupBoxStep2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxStep2.Size = new System.Drawing.Size(392, 565);
            this.groupBoxStep2.TabIndex = 12;
            this.groupBoxStep2.TabStop = false;
            this.groupBoxStep2.Text = "Step2: Define Normalization Strategy / Working Params";
            // 
            // tabControlStep2
            // 
            this.tabControlStep2.Controls.Add(this.tabPageClustering);
            this.tabControlStep2.Controls.Add(this.tabPagePlotData);
            this.tabControlStep2.Controls.Add(this.tabPageSavePlot);
            this.tabControlStep2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlStep2.Location = new System.Drawing.Point(4, 19);
            this.tabControlStep2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControlStep2.Name = "tabControlStep2";
            this.tabControlStep2.SelectedIndex = 0;
            this.tabControlStep2.Size = new System.Drawing.Size(384, 542);
            this.tabControlStep2.TabIndex = 11;
            // 
            // tabPageClustering
            // 
            this.tabPageClustering.Controls.Add(this.splitContainerClustering);
            this.tabPageClustering.Location = new System.Drawing.Point(4, 25);
            this.tabPageClustering.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageClustering.Name = "tabPageClustering";
            this.tabPageClustering.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageClustering.Size = new System.Drawing.Size(376, 513);
            this.tabPageClustering.TabIndex = 0;
            this.tabPageClustering.Text = "Clustering";
            this.tabPageClustering.UseVisualStyleBackColor = true;
            // 
            // splitContainerClustering
            // 
            this.splitContainerClustering.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerClustering.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerClustering.Location = new System.Drawing.Point(4, 4);
            this.splitContainerClustering.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerClustering.Name = "splitContainerClustering";
            this.splitContainerClustering.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerClustering.Panel1
            // 
            this.splitContainerClustering.Panel1.Controls.Add(this.buttonPrepareForClustering);
            this.splitContainerClustering.Panel1.Controls.Add(this.textBoxMinAvgSignalCluster);
            this.splitContainerClustering.Panel1.Controls.Add(this.numericUpDownMinDatapointsClustering);
            this.splitContainerClustering.Panel1.Controls.Add(this.numericUpDownMinNodesPerCluster);
            this.splitContainerClustering.Panel1.Controls.Add(this.label12);
            this.splitContainerClustering.Panel1.Controls.Add(this.label11);
            this.splitContainerClustering.Panel1.Controls.Add(this.label10);
            this.splitContainerClustering.Panel1.Controls.Add(this.labelNoElements);
            this.splitContainerClustering.Panel1.Controls.Add(this.label9);
            // 
            // splitContainerClustering.Panel2
            // 
            this.splitContainerClustering.Panel2.Controls.Add(this.groupBoxClusteringStep3);
            this.splitContainerClustering.Size = new System.Drawing.Size(368, 505);
            this.splitContainerClustering.SplitterDistance = 98;
            this.splitContainerClustering.SplitterWidth = 5;
            this.splitContainerClustering.TabIndex = 11;
            // 
            // buttonPrepareForClustering
            // 
            this.buttonPrepareForClustering.Location = new System.Drawing.Point(403, 5);
            this.buttonPrepareForClustering.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonPrepareForClustering.Name = "buttonPrepareForClustering";
            this.buttonPrepareForClustering.Size = new System.Drawing.Size(89, 97);
            this.buttonPrepareForClustering.TabIndex = 27;
            this.buttonPrepareForClustering.Text = "Prepare";
            this.buttonPrepareForClustering.UseVisualStyleBackColor = true;
            this.buttonPrepareForClustering.Click += new System.EventHandler(this.buttonPrepareForClustering_Click);
            // 
            // textBoxMinAvgSignalCluster
            // 
            this.textBoxMinAvgSignalCluster.Location = new System.Drawing.Point(332, 7);
            this.textBoxMinAvgSignalCluster.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMinAvgSignalCluster.Name = "textBoxMinAvgSignalCluster";
            this.textBoxMinAvgSignalCluster.Size = new System.Drawing.Size(61, 22);
            this.textBoxMinAvgSignalCluster.TabIndex = 26;
            this.textBoxMinAvgSignalCluster.Text = "0";
            // 
            // numericUpDownMinDatapointsClustering
            // 
            this.numericUpDownMinDatapointsClustering.Location = new System.Drawing.Point(332, 74);
            this.numericUpDownMinDatapointsClustering.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownMinDatapointsClustering.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinDatapointsClustering.Name = "numericUpDownMinDatapointsClustering";
            this.numericUpDownMinDatapointsClustering.Size = new System.Drawing.Size(63, 22);
            this.numericUpDownMinDatapointsClustering.TabIndex = 25;
            this.numericUpDownMinDatapointsClustering.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // numericUpDownMinNodesPerCluster
            // 
            this.numericUpDownMinNodesPerCluster.Location = new System.Drawing.Point(332, 42);
            this.numericUpDownMinNodesPerCluster.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownMinNodesPerCluster.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinNodesPerCluster.Name = "numericUpDownMinNodesPerCluster";
            this.numericUpDownMinNodesPerCluster.Size = new System.Drawing.Size(63, 22);
            this.numericUpDownMinNodesPerCluster.TabIndex = 24;
            this.numericUpDownMinNodesPerCluster.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(183, 44);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 17);
            this.label12.TabIndex = 23;
            this.label12.Text = "Min items  / cluster";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 80);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(271, 17);
            this.label11.TabIndex = 22;
            this.label11.Text = "Min datapoints (considers tech replicates)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(195, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 17);
            this.label10.TabIndex = 21;
            this.label10.Text = "Min Avg Signal";
            // 
            // labelNoElements
            // 
            this.labelNoElements.AutoSize = true;
            this.labelNoElements.Location = new System.Drawing.Point(105, 11);
            this.labelNoElements.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNoElements.Name = "labelNoElements";
            this.labelNoElements.Size = new System.Drawing.Size(16, 17);
            this.labelNoElements.TabIndex = 19;
            this.labelNoElements.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 11);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 17);
            this.label9.TabIndex = 18;
            this.label9.Text = "No Elements: ";
            // 
            // groupBoxClusteringStep3
            // 
            this.groupBoxClusteringStep3.Controls.Add(this.splitContainerClusteringResults);
            this.groupBoxClusteringStep3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxClusteringStep3.Enabled = false;
            this.groupBoxClusteringStep3.Location = new System.Drawing.Point(0, 0);
            this.groupBoxClusteringStep3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxClusteringStep3.Name = "groupBoxClusteringStep3";
            this.groupBoxClusteringStep3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxClusteringStep3.Size = new System.Drawing.Size(368, 402);
            this.groupBoxClusteringStep3.TabIndex = 0;
            this.groupBoxClusteringStep3.TabStop = false;
            this.groupBoxClusteringStep3.Text = "Step 3: Clustering";
            // 
            // splitContainerClusteringResults
            // 
            this.splitContainerClusteringResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerClusteringResults.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerClusteringResults.Location = new System.Drawing.Point(4, 19);
            this.splitContainerClusteringResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerClusteringResults.Name = "splitContainerClusteringResults";
            this.splitContainerClusteringResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerClusteringResults.Panel1
            // 
            this.splitContainerClusteringResults.Panel1.Controls.Add(this.checkBoxPlotCOnsensus);
            this.splitContainerClusteringResults.Panel1.Controls.Add(this.buttonCluster);
            this.splitContainerClusteringResults.Panel1.Controls.Add(this.buttonPlotCluster);
            this.splitContainerClusteringResults.Panel1.Controls.Add(this.buttonExport);
            this.splitContainerClusteringResults.Panel1.Controls.Add(this.buttonExpAll);
            this.splitContainerClusteringResults.Panel1.Controls.Add(this.buttonUncheck2);
            this.splitContainerClusteringResults.Panel1.Controls.Add(this.numericUpDownTargetClusterNumber);
            this.splitContainerClusteringResults.Panel1.Controls.Add(this.label3);
            // 
            // splitContainerClusteringResults.Panel2
            // 
            this.splitContainerClusteringResults.Panel2.Controls.Add(this.dataGridViewCluster);
            this.splitContainerClusteringResults.Size = new System.Drawing.Size(360, 379);
            this.splitContainerClusteringResults.SplitterDistance = 69;
            this.splitContainerClusteringResults.SplitterWidth = 5;
            this.splitContainerClusteringResults.TabIndex = 1;
            // 
            // checkBoxPlotCOnsensus
            // 
            this.checkBoxPlotCOnsensus.AutoSize = true;
            this.checkBoxPlotCOnsensus.Checked = true;
            this.checkBoxPlotCOnsensus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlotCOnsensus.Location = new System.Drawing.Point(349, 6);
            this.checkBoxPlotCOnsensus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxPlotCOnsensus.Name = "checkBoxPlotCOnsensus";
            this.checkBoxPlotCOnsensus.Size = new System.Drawing.Size(128, 21);
            this.checkBoxPlotCOnsensus.TabIndex = 23;
            this.checkBoxPlotCOnsensus.Text = "Plot Consensus";
            this.checkBoxPlotCOnsensus.UseVisualStyleBackColor = true;
            // 
            // buttonCluster
            // 
            this.buttonCluster.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonCluster.Location = new System.Drawing.Point(245, 1);
            this.buttonCluster.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCluster.Name = "buttonCluster";
            this.buttonCluster.Size = new System.Drawing.Size(96, 28);
            this.buttonCluster.TabIndex = 22;
            this.buttonCluster.Text = "Cluster";
            this.buttonCluster.UseVisualStyleBackColor = false;
            this.buttonCluster.Click += new System.EventHandler(this.buttonCluster_Click);
            // 
            // buttonPlotCluster
            // 
            this.buttonPlotCluster.Enabled = false;
            this.buttonPlotCluster.Location = new System.Drawing.Point(5, 37);
            this.buttonPlotCluster.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonPlotCluster.Name = "buttonPlotCluster";
            this.buttonPlotCluster.Size = new System.Drawing.Size(96, 28);
            this.buttonPlotCluster.TabIndex = 21;
            this.buttonPlotCluster.Text = "Plot";
            this.buttonPlotCluster.UseVisualStyleBackColor = true;
            this.buttonPlotCluster.Click += new System.EventHandler(this.buttonPlotCluster_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Enabled = false;
            this.buttonExport.Location = new System.Drawing.Point(109, 37);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(128, 28);
            this.buttonExport.TabIndex = 20;
            this.buttonExport.Text = "Comb_Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonExpAll
            // 
            this.buttonExpAll.Enabled = false;
            this.buttonExpAll.Location = new System.Drawing.Point(245, 37);
            this.buttonExpAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExpAll.Name = "buttonExpAll";
            this.buttonExpAll.Size = new System.Drawing.Size(96, 28);
            this.buttonExpAll.TabIndex = 19;
            this.buttonExpAll.Text = "ExpAll";
            this.buttonExpAll.UseVisualStyleBackColor = true;
            this.buttonExpAll.Click += new System.EventHandler(this.buttonExpAll_Click);
            // 
            // buttonUncheck2
            // 
            this.buttonUncheck2.Enabled = false;
            this.buttonUncheck2.Location = new System.Drawing.Point(349, 37);
            this.buttonUncheck2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonUncheck2.Name = "buttonUncheck2";
            this.buttonUncheck2.Size = new System.Drawing.Size(96, 28);
            this.buttonUncheck2.TabIndex = 18;
            this.buttonUncheck2.Text = "Uncheck";
            this.buttonUncheck2.UseVisualStyleBackColor = true;
            this.buttonUncheck2.Click += new System.EventHandler(this.buttonUncheck2_Click);
            // 
            // numericUpDownTargetClusterNumber
            // 
            this.numericUpDownTargetClusterNumber.Location = new System.Drawing.Point(183, 5);
            this.numericUpDownTargetClusterNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownTargetClusterNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTargetClusterNumber.Name = "numericUpDownTargetClusterNumber";
            this.numericUpDownTargetClusterNumber.Size = new System.Drawing.Size(55, 22);
            this.numericUpDownTargetClusterNumber.TabIndex = 17;
            this.numericUpDownTargetClusterNumber.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Target number of clusters: ";
            // 
            // dataGridViewCluster
            // 
            this.dataGridViewCluster.AllowUserToAddRows = false;
            this.dataGridViewCluster.AllowUserToDeleteRows = false;
            this.dataGridViewCluster.AllowUserToOrderColumns = true;
            this.dataGridViewCluster.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridViewCluster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCluster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPlot,
            this.ColumnID,
            this.ColumnColClusterElements,
            this.ColumnEuclidianSum});
            this.dataGridViewCluster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCluster.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCluster.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewCluster.Name = "dataGridViewCluster";
            this.dataGridViewCluster.RowHeadersVisible = false;
            this.dataGridViewCluster.Size = new System.Drawing.Size(360, 305);
            this.dataGridViewCluster.TabIndex = 1;
            // 
            // ColumnPlot
            // 
            this.ColumnPlot.HeaderText = "Plot";
            this.ColumnPlot.Name = "ColumnPlot";
            this.ColumnPlot.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnPlot.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ColumnID
            // 
            this.ColumnID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnID.HeaderText = "ID";
            this.ColumnID.Name = "ColumnID";
            // 
            // ColumnColClusterElements
            // 
            this.ColumnColClusterElements.HeaderText = "ClusterElements";
            this.ColumnColClusterElements.Name = "ColumnColClusterElements";
            // 
            // ColumnEuclidianSum
            // 
            this.ColumnEuclidianSum.HeaderText = "Euclidian Sum";
            this.ColumnEuclidianSum.Name = "ColumnEuclidianSum";
            // 
            // tabPagePlotData
            // 
            this.tabPagePlotData.Controls.Add(this.dataGridViewPlotData);
            this.tabPagePlotData.Controls.Add(this.buttonRefreshPlot);
            this.tabPagePlotData.Location = new System.Drawing.Point(4, 25);
            this.tabPagePlotData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPagePlotData.Name = "tabPagePlotData";
            this.tabPagePlotData.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPagePlotData.Size = new System.Drawing.Size(268, 538);
            this.tabPagePlotData.TabIndex = 3;
            this.tabPagePlotData.Text = "Plot Data";
            this.tabPagePlotData.UseVisualStyleBackColor = true;
            // 
            // dataGridViewPlotData
            // 
            this.dataGridViewPlotData.AllowUserToOrderColumns = true;
            this.dataGridViewPlotData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPlotData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlotData.Location = new System.Drawing.Point(8, 43);
            this.dataGridViewPlotData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewPlotData.Name = "dataGridViewPlotData";
            this.dataGridViewPlotData.Size = new System.Drawing.Size(249, 488);
            this.dataGridViewPlotData.TabIndex = 1;
            // 
            // buttonRefreshPlot
            // 
            this.buttonRefreshPlot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefreshPlot.Location = new System.Drawing.Point(8, 7);
            this.buttonRefreshPlot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonRefreshPlot.Name = "buttonRefreshPlot";
            this.buttonRefreshPlot.Size = new System.Drawing.Size(249, 28);
            this.buttonRefreshPlot.TabIndex = 0;
            this.buttonRefreshPlot.Text = "Refresh Plot";
            this.buttonRefreshPlot.UseVisualStyleBackColor = true;
            this.buttonRefreshPlot.Click += new System.EventHandler(this.buttonRefreshPlot_Click);
            // 
            // tabPageSavePlot
            // 
            this.tabPageSavePlot.Controls.Add(this.buttonSavePlot);
            this.tabPageSavePlot.Controls.Add(this.buttonSaveGraphData);
            this.tabPageSavePlot.Location = new System.Drawing.Point(4, 25);
            this.tabPageSavePlot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageSavePlot.Name = "tabPageSavePlot";
            this.tabPageSavePlot.Size = new System.Drawing.Size(268, 538);
            this.tabPageSavePlot.TabIndex = 2;
            this.tabPageSavePlot.Text = "Save Plot";
            this.tabPageSavePlot.UseVisualStyleBackColor = true;
            // 
            // buttonSavePlot
            // 
            this.buttonSavePlot.Location = new System.Drawing.Point(41, 23);
            this.buttonSavePlot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSavePlot.Name = "buttonSavePlot";
            this.buttonSavePlot.Size = new System.Drawing.Size(100, 28);
            this.buttonSavePlot.TabIndex = 2;
            this.buttonSavePlot.Text = "Save Plot";
            this.buttonSavePlot.UseVisualStyleBackColor = true;
            this.buttonSavePlot.Click += new System.EventHandler(this.buttonSavePlot_Click);
            // 
            // buttonSaveGraphData
            // 
            this.buttonSaveGraphData.Location = new System.Drawing.Point(149, 23);
            this.buttonSaveGraphData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSaveGraphData.Name = "buttonSaveGraphData";
            this.buttonSaveGraphData.Size = new System.Drawing.Size(179, 28);
            this.buttonSaveGraphData.TabIndex = 1;
            this.buttonSaveGraphData.Text = "Save Graph Data";
            this.buttonSaveGraphData.UseVisualStyleBackColor = true;
            this.buttonSaveGraphData.Click += new System.EventHandler(this.buttonSaveGraphData_Click);
            // 
            // chartTrend
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTrend.ChartAreas.Add(chartArea1);
            this.chartTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartTrend.Legends.Add(legend1);
            this.chartTrend.Location = new System.Drawing.Point(0, 0);
            this.chartTrend.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartTrend.Name = "chartTrend";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTrend.Series.Add(series1);
            this.chartTrend.Size = new System.Drawing.Size(710, 630);
            this.chartTrend.TabIndex = 0;
            this.chartTrend.Text = "chart1";
            // 
            // TrendQuestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TrendQuestControl";
            this.Size = new System.Drawing.Size(1107, 630);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerControls.Panel1.ResumeLayout(false);
            this.splitContainerControls.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControls)).EndInit();
            this.splitContainerControls.ResumeLayout(false);
            this.groupBoxStep1.ResumeLayout(false);
            this.groupBoxStep2.ResumeLayout(false);
            this.tabControlStep2.ResumeLayout(false);
            this.tabPageClustering.ResumeLayout(false);
            this.splitContainerClustering.Panel1.ResumeLayout(false);
            this.splitContainerClustering.Panel1.PerformLayout();
            this.splitContainerClustering.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerClustering)).EndInit();
            this.splitContainerClustering.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinDatapointsClustering)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinNodesPerCluster)).EndInit();
            this.groupBoxClusteringStep3.ResumeLayout(false);
            this.splitContainerClusteringResults.Panel1.ResumeLayout(false);
            this.splitContainerClusteringResults.Panel1.PerformLayout();
            this.splitContainerClusteringResults.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerClusteringResults)).EndInit();
            this.splitContainerClusteringResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetClusterNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCluster)).EndInit();
            this.tabPagePlotData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlotData)).EndInit();
            this.tabPageSavePlot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTrend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerControls;
        private System.Windows.Forms.GroupBox groupBoxStep1;
        private System.Windows.Forms.GroupBox groupBoxStep2;
        private System.Windows.Forms.TabControl tabControlStep2;
        private System.Windows.Forms.TabPage tabPageClustering;
        private System.Windows.Forms.SplitContainer splitContainerClustering;
        private System.Windows.Forms.GroupBox groupBoxClusteringStep3;
        private System.Windows.Forms.SplitContainer splitContainerClusteringResults;
        private System.Windows.Forms.TabPage tabPageSavePlot;
        private System.Windows.Forms.Button buttonSaveGraphData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrend;
        private System.Windows.Forms.DataGridView dataGridViewCluster;
        private System.Windows.Forms.NumericUpDown numericUpDownTargetClusterNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSavePlot;
        private System.Windows.Forms.TextBox textBoxMinAvgSignalCluster;
        private System.Windows.Forms.NumericUpDown numericUpDownMinDatapointsClustering;
        private System.Windows.Forms.NumericUpDown numericUpDownMinNodesPerCluster;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelNoElements;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonPrepareForClustering;
        private System.Windows.Forms.Button buttonUncheck2;
        private System.Windows.Forms.Button buttonExpAll;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonPlotCluster;
        private System.Windows.Forms.Button buttonCluster;
        private System.Windows.Forms.Button buttonLoadPLP;
        private System.Windows.Forms.CheckBox checkBoxPlotCOnsensus;
        private System.Windows.Forms.TabPage tabPagePlotData;
        private System.Windows.Forms.Button buttonRefreshPlot;
        private System.Windows.Forms.DataGridView dataGridViewPlotData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnPlot;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColClusterElements;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEuclidianSum;
    }
}
