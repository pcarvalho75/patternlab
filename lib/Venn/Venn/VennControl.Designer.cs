namespace Venn
{
    partial class VennControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.groupBoxPlotOptions = new System.Windows.Forms.GroupBox();
            this.numericUpDownMinimumNumberOfReplicates = new System.Windows.Forms.NumericUpDown();
            this.buttonSavePlot = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownOpacity = new System.Windows.Forms.NumericUpDown();
            this.buttonPlot = new System.Windows.Forms.Button();
            this.numericUpDownScaleFactor = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxFilteringOptions = new System.Windows.Forms.GroupBox();
            this.groupBoxClassSelection = new System.Windows.Forms.GroupBox();
            this.radioButtonPerClass = new System.Windows.Forms.RadioButton();
            this.radioButtonAllClasses = new System.Windows.Forms.RadioButton();
            this.numericUpDownFilteringProbability = new System.Windows.Forms.NumericUpDown();
            this.radioButtonFilteringProbability = new System.Windows.Forms.RadioButton();
            this.radioButtonFilteringMinNumberOfReplicates = new System.Windows.Forms.RadioButton();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.comboBoxBackground = new System.Windows.Forms.ComboBox();
            this.groupBoxLabels = new System.Windows.Forms.GroupBox();
            this.checkBoxPlotLabels = new System.Windows.Forms.CheckBox();
            this.checkBoxDetailedLabel = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textBoxC1Name = new System.Windows.Forms.TextBox();
            this.textBoxC2Name = new System.Windows.Forms.TextBox();
            this.textBoxC3Name = new System.Windows.Forms.TextBox();
            this.tabControlLog = new System.Windows.Forms.TabControl();
            this.tabPageBelaGraph = new System.Windows.Forms.TabPage();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.belaGraphControl2 = new BelaGraph.BelaGraphControl();
            this.tabPagePlotData = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxPlotOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumNumberOfReplicates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaleFactor)).BeginInit();
            this.groupBoxFilteringOptions.SuspendLayout();
            this.groupBoxClassSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilteringProbability)).BeginInit();
            this.groupBoxLabels.SuspendLayout();
            this.tabControlLog.SuspendLayout();
            this.tabPageBelaGraph.SuspendLayout();
            this.tabPagePlotData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.buttonLoad);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxPlotOptions);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlLog);
            this.splitContainer1.Size = new System.Drawing.Size(750, 562);
            this.splitContainer1.SplitterDistance = 282;
            this.splitContainer1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(112, 436);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 13);
            this.label8.TabIndex = 62;
            this.label8.Text = "LeftDoubleClick: Prot. List";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 436);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 61;
            this.label7.Text = "RightClick:Move";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(18, 12);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(220, 23);
            this.buttonLoad.TabIndex = 60;
            this.buttonLoad.Text = "Load PatternLab project";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // groupBoxPlotOptions
            // 
            this.groupBoxPlotOptions.Controls.Add(this.numericUpDownMinimumNumberOfReplicates);
            this.groupBoxPlotOptions.Controls.Add(this.buttonSavePlot);
            this.groupBoxPlotOptions.Controls.Add(this.label6);
            this.groupBoxPlotOptions.Controls.Add(this.numericUpDownOpacity);
            this.groupBoxPlotOptions.Controls.Add(this.buttonPlot);
            this.groupBoxPlotOptions.Controls.Add(this.numericUpDownScaleFactor);
            this.groupBoxPlotOptions.Controls.Add(this.label5);
            this.groupBoxPlotOptions.Controls.Add(this.label4);
            this.groupBoxPlotOptions.Controls.Add(this.label3);
            this.groupBoxPlotOptions.Controls.Add(this.groupBoxFilteringOptions);
            this.groupBoxPlotOptions.Controls.Add(this.textBoxTitle);
            this.groupBoxPlotOptions.Controls.Add(this.comboBoxBackground);
            this.groupBoxPlotOptions.Controls.Add(this.groupBoxLabels);
            this.groupBoxPlotOptions.Enabled = false;
            this.groupBoxPlotOptions.Location = new System.Drawing.Point(3, 41);
            this.groupBoxPlotOptions.Name = "groupBoxPlotOptions";
            this.groupBoxPlotOptions.Size = new System.Drawing.Size(267, 392);
            this.groupBoxPlotOptions.TabIndex = 53;
            this.groupBoxPlotOptions.TabStop = false;
            this.groupBoxPlotOptions.Text = "Plot Options";
            // 
            // numericUpDownMinimumNumberOfReplicates
            // 
            this.numericUpDownMinimumNumberOfReplicates.Location = new System.Drawing.Point(173, 91);
            this.numericUpDownMinimumNumberOfReplicates.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinimumNumberOfReplicates.Name = "numericUpDownMinimumNumberOfReplicates";
            this.numericUpDownMinimumNumberOfReplicates.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownMinimumNumberOfReplicates.TabIndex = 55;
            this.numericUpDownMinimumNumberOfReplicates.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonSavePlot
            // 
            this.buttonSavePlot.Location = new System.Drawing.Point(155, 362);
            this.buttonSavePlot.Name = "buttonSavePlot";
            this.buttonSavePlot.Size = new System.Drawing.Size(75, 23);
            this.buttonSavePlot.TabIndex = 60;
            this.buttonSavePlot.Text = "Save Plot";
            this.buttonSavePlot.UseVisualStyleBackColor = true;
            this.buttonSavePlot.Click += new System.EventHandler(this.buttonSavePlot_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(154, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 58;
            this.label6.Text = "Transp:";
            // 
            // numericUpDownOpacity
            // 
            this.numericUpDownOpacity.DecimalPlaces = 1;
            this.numericUpDownOpacity.Location = new System.Drawing.Point(203, 47);
            this.numericUpDownOpacity.Name = "numericUpDownOpacity";
            this.numericUpDownOpacity.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownOpacity.TabIndex = 57;
            this.numericUpDownOpacity.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            // 
            // buttonPlot
            // 
            this.buttonPlot.Location = new System.Drawing.Point(35, 362);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(75, 23);
            this.buttonPlot.TabIndex = 59;
            this.buttonPlot.Text = "Plot";
            this.buttonPlot.UseVisualStyleBackColor = true;
            this.buttonPlot.Click += new System.EventHandler(this.buttonPlot_Click);
            // 
            // numericUpDownScaleFactor
            // 
            this.numericUpDownScaleFactor.DecimalPlaces = 1;
            this.numericUpDownScaleFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownScaleFactor.Location = new System.Drawing.Point(84, 45);
            this.numericUpDownScaleFactor.Name = "numericUpDownScaleFactor";
            this.numericUpDownScaleFactor.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownScaleFactor.TabIndex = 56;
            this.numericUpDownScaleFactor.Value = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "Scale Factor:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Bkg:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Title: ";
            // 
            // groupBoxFilteringOptions
            // 
            this.groupBoxFilteringOptions.Controls.Add(this.groupBoxClassSelection);
            this.groupBoxFilteringOptions.Controls.Add(this.numericUpDownFilteringProbability);
            this.groupBoxFilteringOptions.Controls.Add(this.radioButtonFilteringProbability);
            this.groupBoxFilteringOptions.Controls.Add(this.radioButtonFilteringMinNumberOfReplicates);
            this.groupBoxFilteringOptions.Location = new System.Drawing.Point(21, 71);
            this.groupBoxFilteringOptions.Name = "groupBoxFilteringOptions";
            this.groupBoxFilteringOptions.Size = new System.Drawing.Size(224, 130);
            this.groupBoxFilteringOptions.TabIndex = 52;
            this.groupBoxFilteringOptions.TabStop = false;
            this.groupBoxFilteringOptions.Text = "Filtering Options";
            // 
            // groupBoxClassSelection
            // 
            this.groupBoxClassSelection.Controls.Add(this.radioButtonPerClass);
            this.groupBoxClassSelection.Controls.Add(this.radioButtonAllClasses);
            this.groupBoxClassSelection.Location = new System.Drawing.Point(9, 46);
            this.groupBoxClassSelection.Name = "groupBoxClassSelection";
            this.groupBoxClassSelection.Size = new System.Drawing.Size(202, 47);
            this.groupBoxClassSelection.TabIndex = 55;
            this.groupBoxClassSelection.TabStop = false;
            this.groupBoxClassSelection.Text = "Class Replicate Selection";
            // 
            // radioButtonPerClass
            // 
            this.radioButtonPerClass.AutoSize = true;
            this.radioButtonPerClass.Location = new System.Drawing.Point(87, 19);
            this.radioButtonPerClass.Name = "radioButtonPerClass";
            this.radioButtonPerClass.Size = new System.Drawing.Size(69, 17);
            this.radioButtonPerClass.TabIndex = 1;
            this.radioButtonPerClass.Text = "Per Class";
            this.radioButtonPerClass.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllClasses
            // 
            this.radioButtonAllClasses.AutoSize = true;
            this.radioButtonAllClasses.Checked = true;
            this.radioButtonAllClasses.Location = new System.Drawing.Point(6, 19);
            this.radioButtonAllClasses.Name = "radioButtonAllClasses";
            this.radioButtonAllClasses.Size = new System.Drawing.Size(75, 17);
            this.radioButtonAllClasses.TabIndex = 0;
            this.radioButtonAllClasses.TabStop = true;
            this.radioButtonAllClasses.Text = "All Classes";
            this.radioButtonAllClasses.UseVisualStyleBackColor = true;
            // 
            // numericUpDownFilteringProbability
            // 
            this.numericUpDownFilteringProbability.DecimalPlaces = 2;
            this.numericUpDownFilteringProbability.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownFilteringProbability.Location = new System.Drawing.Point(152, 99);
            this.numericUpDownFilteringProbability.Name = "numericUpDownFilteringProbability";
            this.numericUpDownFilteringProbability.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownFilteringProbability.TabIndex = 54;
            this.numericUpDownFilteringProbability.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // radioButtonFilteringProbability
            // 
            this.radioButtonFilteringProbability.AutoSize = true;
            this.radioButtonFilteringProbability.Location = new System.Drawing.Point(6, 99);
            this.radioButtonFilteringProbability.Name = "radioButtonFilteringProbability";
            this.radioButtonFilteringProbability.Size = new System.Drawing.Size(73, 17);
            this.radioButtonFilteringProbability.TabIndex = 53;
            this.radioButtonFilteringProbability.Text = "Probability";
            this.radioButtonFilteringProbability.UseVisualStyleBackColor = true;
            this.radioButtonFilteringProbability.CheckedChanged += new System.EventHandler(this.radioButtonFilteringProbability_CheckedChanged);
            // 
            // radioButtonFilteringMinNumberOfReplicates
            // 
            this.radioButtonFilteringMinNumberOfReplicates.AutoSize = true;
            this.radioButtonFilteringMinNumberOfReplicates.Checked = true;
            this.radioButtonFilteringMinNumberOfReplicates.Location = new System.Drawing.Point(6, 19);
            this.radioButtonFilteringMinNumberOfReplicates.Name = "radioButtonFilteringMinNumberOfReplicates";
            this.radioButtonFilteringMinNumberOfReplicates.Size = new System.Drawing.Size(140, 17);
            this.radioButtonFilteringMinNumberOfReplicates.TabIndex = 52;
            this.radioButtonFilteringMinNumberOfReplicates.TabStop = true;
            this.radioButtonFilteringMinNumberOfReplicates.Text = "Min number of replicates";
            this.radioButtonFilteringMinNumberOfReplicates.UseVisualStyleBackColor = true;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(57, 19);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(70, 20);
            this.textBoxTitle.TabIndex = 3;
            // 
            // comboBoxBackground
            // 
            this.comboBoxBackground.FormattingEnabled = true;
            this.comboBoxBackground.Items.AddRange(new object[] {
            "None",
            "YellowXGradient"});
            this.comboBoxBackground.Location = new System.Drawing.Point(169, 18);
            this.comboBoxBackground.Name = "comboBoxBackground";
            this.comboBoxBackground.Size = new System.Drawing.Size(83, 21);
            this.comboBoxBackground.TabIndex = 4;
            // 
            // groupBoxLabels
            // 
            this.groupBoxLabels.Controls.Add(this.checkBoxPlotLabels);
            this.groupBoxLabels.Controls.Add(this.checkBoxDetailedLabel);
            this.groupBoxLabels.Controls.Add(this.label11);
            this.groupBoxLabels.Controls.Add(this.label10);
            this.groupBoxLabels.Controls.Add(this.label9);
            this.groupBoxLabels.Controls.Add(this.linkLabel1);
            this.groupBoxLabels.Controls.Add(this.textBoxC1Name);
            this.groupBoxLabels.Controls.Add(this.textBoxC2Name);
            this.groupBoxLabels.Controls.Add(this.textBoxC3Name);
            this.groupBoxLabels.Location = new System.Drawing.Point(21, 207);
            this.groupBoxLabels.Name = "groupBoxLabels";
            this.groupBoxLabels.Size = new System.Drawing.Size(231, 149);
            this.groupBoxLabels.TabIndex = 39;
            this.groupBoxLabels.TabStop = false;
            this.groupBoxLabels.Text = "Label controls";
            // 
            // checkBoxPlotLabels
            // 
            this.checkBoxPlotLabels.AutoSize = true;
            this.checkBoxPlotLabels.Checked = true;
            this.checkBoxPlotLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlotLabels.Location = new System.Drawing.Point(114, 20);
            this.checkBoxPlotLabels.Name = "checkBoxPlotLabels";
            this.checkBoxPlotLabels.Size = new System.Drawing.Size(78, 17);
            this.checkBoxPlotLabels.TabIndex = 50;
            this.checkBoxPlotLabels.Text = "Plot Labels";
            this.checkBoxPlotLabels.UseVisualStyleBackColor = true;
            // 
            // checkBoxDetailedLabel
            // 
            this.checkBoxDetailedLabel.AutoSize = true;
            this.checkBoxDetailedLabel.Checked = true;
            this.checkBoxDetailedLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDetailedLabel.Location = new System.Drawing.Point(14, 20);
            this.checkBoxDetailedLabel.Name = "checkBoxDetailedLabel";
            this.checkBoxDetailedLabel.Size = new System.Drawing.Size(94, 17);
            this.checkBoxDetailedLabel.TabIndex = 49;
            this.checkBoxDetailedLabel.Text = "Detailed Label";
            this.checkBoxDetailedLabel.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 48;
            this.label11.Text = "C3 Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "C2 Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 46;
            this.label9.Text = "C1 Name";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(6, 42);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(151, 13);
            this.linkLabel1.TabIndex = 45;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Click here to change label font";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBoxC1Name
            // 
            this.textBoxC1Name.Location = new System.Drawing.Point(75, 71);
            this.textBoxC1Name.Name = "textBoxC1Name";
            this.textBoxC1Name.Size = new System.Drawing.Size(134, 20);
            this.textBoxC1Name.TabIndex = 32;
            this.textBoxC1Name.Text = "Group";
            // 
            // textBoxC2Name
            // 
            this.textBoxC2Name.Location = new System.Drawing.Point(75, 97);
            this.textBoxC2Name.Name = "textBoxC2Name";
            this.textBoxC2Name.Size = new System.Drawing.Size(134, 20);
            this.textBoxC2Name.TabIndex = 33;
            this.textBoxC2Name.Text = "Group";
            // 
            // textBoxC3Name
            // 
            this.textBoxC3Name.Location = new System.Drawing.Point(75, 123);
            this.textBoxC3Name.Name = "textBoxC3Name";
            this.textBoxC3Name.Size = new System.Drawing.Size(134, 20);
            this.textBoxC3Name.TabIndex = 34;
            this.textBoxC3Name.Text = "Group";
            // 
            // tabControlLog
            // 
            this.tabControlLog.Controls.Add(this.tabPageBelaGraph);
            this.tabControlLog.Controls.Add(this.tabPagePlotData);
            this.tabControlLog.Controls.Add(this.tabPage1);
            this.tabControlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLog.Location = new System.Drawing.Point(0, 0);
            this.tabControlLog.Name = "tabControlLog";
            this.tabControlLog.SelectedIndex = 0;
            this.tabControlLog.Size = new System.Drawing.Size(464, 562);
            this.tabControlLog.TabIndex = 1;
            // 
            // tabPageBelaGraph
            // 
            this.tabPageBelaGraph.Controls.Add(this.elementHost2);
            this.tabPageBelaGraph.Location = new System.Drawing.Point(4, 22);
            this.tabPageBelaGraph.Name = "tabPageBelaGraph";
            this.tabPageBelaGraph.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageBelaGraph.Size = new System.Drawing.Size(456, 536);
            this.tabPageBelaGraph.TabIndex = 0;
            this.tabPageBelaGraph.Text = "BelaGraph";
            this.tabPageBelaGraph.UseVisualStyleBackColor = true;
            // 
            // elementHost2
            // 
            this.elementHost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost2.Location = new System.Drawing.Point(3, 3);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(450, 530);
            this.elementHost2.TabIndex = 63;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.belaGraphControl2;
            // 
            // tabPagePlotData
            // 
            this.tabPagePlotData.Controls.Add(this.dataGridView1);
            this.tabPagePlotData.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlotData.Name = "tabPagePlotData";
            this.tabPagePlotData.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPagePlotData.Size = new System.Drawing.Size(456, 536);
            this.tabPagePlotData.TabIndex = 1;
            this.tabPagePlotData.Text = "PlotData - PairWise Comparisons";
            this.tabPagePlotData.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(450, 530);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PlotDataGrid_CellContentClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBoxLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(550, 536);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLog.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(544, 530);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // VennControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "VennControl";
            this.Size = new System.Drawing.Size(750, 562);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxPlotOptions.ResumeLayout(false);
            this.groupBoxPlotOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumNumberOfReplicates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaleFactor)).EndInit();
            this.groupBoxFilteringOptions.ResumeLayout(false);
            this.groupBoxFilteringOptions.PerformLayout();
            this.groupBoxClassSelection.ResumeLayout(false);
            this.groupBoxClassSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilteringProbability)).EndInit();
            this.groupBoxLabels.ResumeLayout(false);
            this.groupBoxLabels.PerformLayout();
            this.tabControlLog.ResumeLayout(false);
            this.tabPageBelaGraph.ResumeLayout(false);
            this.tabPagePlotData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabControl tabControlLog;
        private System.Windows.Forms.TabPage tabPageBelaGraph;
        private System.Windows.Forms.TabPage tabPagePlotData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.ComboBox comboBoxBackground;
        private System.Windows.Forms.TextBox textBoxC3Name;
        private System.Windows.Forms.TextBox textBoxC2Name;
        private System.Windows.Forms.TextBox textBoxC1Name;
        private System.Windows.Forms.GroupBox groupBoxLabels;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.GroupBox groupBoxPlotOptions;
        private System.Windows.Forms.GroupBox groupBoxFilteringOptions;
        private System.Windows.Forms.RadioButton radioButtonFilteringProbability;
        private System.Windows.Forms.RadioButton radioButtonFilteringMinNumberOfReplicates;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.NumericUpDown numericUpDownOpacity;
        private System.Windows.Forms.NumericUpDown numericUpDownScaleFactor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonPlot;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonSavePlot;
        private System.Windows.Forms.CheckBox checkBoxPlotLabels;
        private System.Windows.Forms.CheckBox checkBoxDetailedLabel;
        private System.Windows.Forms.NumericUpDown numericUpDownMinimumNumberOfReplicates;
        private System.Windows.Forms.NumericUpDown numericUpDownFilteringProbability;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private BelaGraph.BelaGraphControl belaGraphControl2;
        private System.Windows.Forms.GroupBox groupBoxClassSelection;
        private System.Windows.Forms.RadioButton radioButtonPerClass;
        private System.Windows.Forms.RadioButton radioButtonAllClasses;
    }
}
