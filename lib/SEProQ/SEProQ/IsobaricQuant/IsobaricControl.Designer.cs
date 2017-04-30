namespace SEProQ.ITRAQ
{
    partial class ITRAQControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBoxNormalization = new System.Windows.Forms.GroupBox();
            this.checkBoxNormalizationChannelSignal = new System.Windows.Forms.CheckBox();
            this.checkBoxApplyPurityCorrection = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonLoadSepro = new System.Windows.Forms.Button();
            this.textBoxitraqSEPro = new System.Windows.Forms.TextBox();
            this.buttonMassesTMT = new System.Windows.Forms.Button();
            this.buttonMassesItraq = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.buttonLoadTMTDefaults = new System.Windows.Forms.Button();
            this.buttonLoadPurityDefaultsITRAQ = new System.Windows.Forms.Button();
            this.dataGridViewPurity = new System.Windows.Forms.DataGridView();
            this.ColumnPM2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPM1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPP1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPP2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericUpDownIonCountThreshold = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxIsobaricMasses = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownTMSPPM = new System.Windows.Forms.NumericUpDown();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonTwoConditionsExperiment = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.radioButtonAnalysisPeptideReport = new System.Windows.Forms.RadioButton();
            this.radioButtonSparseMatrix = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxCorrectedYadaDirectory = new System.Windows.Forms.TextBox();
            this.buttonBrowseYada = new System.Windows.Forms.Button();
            this.buttonCombinePeptidePValues = new System.Windows.Forms.Button();
            this.buttonMassesItraq8 = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPagePurityCorrections = new System.Windows.Forms.TabPage();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.tabPageNormalization = new System.Windows.Forms.TabPage();
            this.chartSignalPerChannel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBoxSelectFileForGraphs = new System.Windows.Forms.ComboBox();
            this.groupBoxPostProcessing = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxClassLabels = new System.Windows.Forms.TextBox();
            this.tabControlIsobaric = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonTMT10 = new System.Windows.Forms.Button();
            this.checkBoxOnlyUniquePeptides = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.multinotch1 = new SEProQ.IsobaricQuant.Multinotch();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxNormalization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIonCountThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTMSPPM)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPagePurityCorrections.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabPageNormalization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSignalPerChannel)).BeginInit();
            this.groupBoxPostProcessing.SuspendLayout();
            this.tabControlIsobaric.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxNormalization
            // 
            this.groupBoxNormalization.Controls.Add(this.checkBoxNormalizationChannelSignal);
            this.groupBoxNormalization.Controls.Add(this.checkBoxApplyPurityCorrection);
            this.groupBoxNormalization.Location = new System.Drawing.Point(30, 257);
            this.groupBoxNormalization.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxNormalization.Name = "groupBoxNormalization";
            this.groupBoxNormalization.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxNormalization.Size = new System.Drawing.Size(632, 63);
            this.groupBoxNormalization.TabIndex = 18;
            this.groupBoxNormalization.TabStop = false;
            this.groupBoxNormalization.Text = "Normalization";
            // 
            // checkBoxNormalizationChannelSignal
            // 
            this.checkBoxNormalizationChannelSignal.AutoSize = true;
            this.checkBoxNormalizationChannelSignal.Checked = true;
            this.checkBoxNormalizationChannelSignal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNormalizationChannelSignal.Location = new System.Drawing.Point(8, 23);
            this.checkBoxNormalizationChannelSignal.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxNormalizationChannelSignal.Name = "checkBoxNormalizationChannelSignal";
            this.checkBoxNormalizationChannelSignal.Size = new System.Drawing.Size(125, 21);
            this.checkBoxNormalizationChannelSignal.TabIndex = 0;
            this.checkBoxNormalizationChannelSignal.Text = "Channel Signal";
            this.checkBoxNormalizationChannelSignal.UseVisualStyleBackColor = true;
            // 
            // checkBoxApplyPurityCorrection
            // 
            this.checkBoxApplyPurityCorrection.AutoSize = true;
            this.checkBoxApplyPurityCorrection.Location = new System.Drawing.Point(149, 23);
            this.checkBoxApplyPurityCorrection.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxApplyPurityCorrection.Name = "checkBoxApplyPurityCorrection";
            this.checkBoxApplyPurityCorrection.Size = new System.Drawing.Size(171, 21);
            this.checkBoxApplyPurityCorrection.TabIndex = 17;
            this.checkBoxApplyPurityCorrection.Text = "Apply purity correction";
            this.checkBoxApplyPurityCorrection.UseVisualStyleBackColor = true;
            this.checkBoxApplyPurityCorrection.CheckedChanged += new System.EventHandler(this.checkBoxApplyPurityCorrection_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(24, 21);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(168, 17);
            this.label16.TabIndex = 1;
            this.label16.Text = "SEPro or PepExplorer file";
            // 
            // buttonLoadSepro
            // 
            this.buttonLoadSepro.Location = new System.Drawing.Point(445, 16);
            this.buttonLoadSepro.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLoadSepro.Name = "buttonLoadSepro";
            this.buttonLoadSepro.Size = new System.Drawing.Size(125, 26);
            this.buttonLoadSepro.TabIndex = 2;
            this.buttonLoadSepro.Text = "Browse";
            this.buttonLoadSepro.UseVisualStyleBackColor = true;
            this.buttonLoadSepro.Click += new System.EventHandler(this.buttonProcessITRAQ_Click);
            // 
            // textBoxitraqSEPro
            // 
            this.textBoxitraqSEPro.Location = new System.Drawing.Point(225, 17);
            this.textBoxitraqSEPro.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxitraqSEPro.Name = "textBoxitraqSEPro";
            this.textBoxitraqSEPro.Size = new System.Drawing.Size(211, 22);
            this.textBoxitraqSEPro.TabIndex = 16;
            this.textBoxitraqSEPro.TextChanged += new System.EventHandler(this.textBoxitraqSEPro_TextChanged);
            // 
            // buttonMassesTMT
            // 
            this.buttonMassesTMT.Location = new System.Drawing.Point(259, 142);
            this.buttonMassesTMT.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMassesTMT.Name = "buttonMassesTMT";
            this.buttonMassesTMT.Size = new System.Drawing.Size(104, 28);
            this.buttonMassesTMT.TabIndex = 15;
            this.buttonMassesTMT.Text = "TMT 6";
            this.buttonMassesTMT.UseVisualStyleBackColor = true;
            this.buttonMassesTMT.Click += new System.EventHandler(this.buttonMassesTMT_Click);
            // 
            // buttonMassesItraq
            // 
            this.buttonMassesItraq.Location = new System.Drawing.Point(151, 142);
            this.buttonMassesItraq.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMassesItraq.Name = "buttonMassesItraq";
            this.buttonMassesItraq.Size = new System.Drawing.Size(100, 28);
            this.buttonMassesItraq.TabIndex = 14;
            this.buttonMassesItraq.Text = "iTRAQ 4";
            this.buttonMassesItraq.UseVisualStyleBackColor = true;
            this.buttonMassesItraq.Click += new System.EventHandler(this.buttonMassesItraq_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(457, 400);
            this.buttonGo.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(205, 28);
            this.buttonGo.TabIndex = 13;
            this.buttonGo.Text = "Generate Report";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // buttonLoadTMTDefaults
            // 
            this.buttonLoadTMTDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoadTMTDefaults.Enabled = false;
            this.buttonLoadTMTDefaults.Location = new System.Drawing.Point(236, 500);
            this.buttonLoadTMTDefaults.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLoadTMTDefaults.Name = "buttonLoadTMTDefaults";
            this.buttonLoadTMTDefaults.Size = new System.Drawing.Size(213, 28);
            this.buttonLoadTMTDefaults.TabIndex = 2;
            this.buttonLoadTMTDefaults.Text = "Load TMT 6 Defaults";
            this.buttonLoadTMTDefaults.UseVisualStyleBackColor = true;
            this.buttonLoadTMTDefaults.Click += new System.EventHandler(this.buttonLoadTMTDefaults_Click);
            // 
            // buttonLoadPurityDefaultsITRAQ
            // 
            this.buttonLoadPurityDefaultsITRAQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoadPurityDefaultsITRAQ.Enabled = false;
            this.buttonLoadPurityDefaultsITRAQ.Location = new System.Drawing.Point(4, 500);
            this.buttonLoadPurityDefaultsITRAQ.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLoadPurityDefaultsITRAQ.Name = "buttonLoadPurityDefaultsITRAQ";
            this.buttonLoadPurityDefaultsITRAQ.Size = new System.Drawing.Size(221, 28);
            this.buttonLoadPurityDefaultsITRAQ.TabIndex = 1;
            this.buttonLoadPurityDefaultsITRAQ.Text = "Load iTRAQ  4 Defaults";
            this.buttonLoadPurityDefaultsITRAQ.UseVisualStyleBackColor = true;
            this.buttonLoadPurityDefaultsITRAQ.Click += new System.EventHandler(this.buttonLoadPurityDefaults_Click);
            // 
            // dataGridViewPurity
            // 
            this.dataGridViewPurity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPurity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPurity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPM2,
            this.ColumnPM1,
            this.ColumnPP1,
            this.ColumnPP2});
            this.dataGridViewPurity.Enabled = false;
            this.dataGridViewPurity.Location = new System.Drawing.Point(4, 5);
            this.dataGridViewPurity.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewPurity.Name = "dataGridViewPurity";
            this.dataGridViewPurity.RowHeadersWidth = 70;
            this.dataGridViewPurity.Size = new System.Drawing.Size(681, 486);
            this.dataGridViewPurity.TabIndex = 0;
            // 
            // ColumnPM2
            // 
            this.ColumnPM2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnPM2.HeaderText = "% of -2";
            this.ColumnPM2.Name = "ColumnPM2";
            this.ColumnPM2.Width = 82;
            // 
            // ColumnPM1
            // 
            this.ColumnPM1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnPM1.HeaderText = "% of -1";
            this.ColumnPM1.Name = "ColumnPM1";
            this.ColumnPM1.Width = 82;
            // 
            // ColumnPP1
            // 
            this.ColumnPP1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnPP1.HeaderText = "% of +1";
            this.ColumnPP1.Name = "ColumnPP1";
            this.ColumnPP1.Width = 85;
            // 
            // ColumnPP2
            // 
            this.ColumnPP2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnPP2.HeaderText = "% of +2";
            this.ColumnPP2.Name = "ColumnPP2";
            this.ColumnPP2.Width = 85;
            // 
            // numericUpDownIonCountThreshold
            // 
            this.numericUpDownIonCountThreshold.DecimalPlaces = 3;
            this.numericUpDownIonCountThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownIonCountThreshold.Location = new System.Drawing.Point(325, 93);
            this.numericUpDownIonCountThreshold.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownIonCountThreshold.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownIonCountThreshold.Name = "numericUpDownIonCountThreshold";
            this.numericUpDownIonCountThreshold.Size = new System.Drawing.Size(84, 22);
            this.numericUpDownIonCountThreshold.TabIndex = 11;
            this.numericUpDownIonCountThreshold.Value = new decimal(new int[] {
            25,
            0,
            0,
            196608});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(180, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ion Count Threshold";
            // 
            // textBoxIsobaricMasses
            // 
            this.textBoxIsobaricMasses.Location = new System.Drawing.Point(151, 189);
            this.textBoxIsobaricMasses.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxIsobaricMasses.Name = "textBoxIsobaricMasses";
            this.textBoxIsobaricMasses.Size = new System.Drawing.Size(511, 22);
            this.textBoxIsobaricMasses.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "TMS ppm";
            // 
            // numericUpDownTMSPPM
            // 
            this.numericUpDownTMSPPM.Location = new System.Drawing.Point(109, 93);
            this.numericUpDownTMSPPM.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownTMSPPM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTMSPPM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTMSPPM.Name = "numericUpDownTMSPPM";
            this.numericUpDownTMSPPM.Size = new System.Drawing.Size(63, 22);
            this.numericUpDownTMSPPM.TabIndex = 7;
            this.numericUpDownTMSPPM.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(151, 403);
            this.textBoxOutputDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(297, 22);
            this.textBoxOutputDirectory.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 406);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "Output Directory";
            // 
            // buttonTwoConditionsExperiment
            // 
            this.buttonTwoConditionsExperiment.Location = new System.Drawing.Point(8, 34);
            this.buttonTwoConditionsExperiment.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTwoConditionsExperiment.Name = "buttonTwoConditionsExperiment";
            this.buttonTwoConditionsExperiment.Size = new System.Drawing.Size(275, 27);
            this.buttonTwoConditionsExperiment.TabIndex = 29;
            this.buttonTwoConditionsExperiment.Text = "Two conditions experiment";
            this.buttonTwoConditionsExperiment.UseVisualStyleBackColor = true;
            this.buttonTwoConditionsExperiment.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 343);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 17);
            this.label9.TabIndex = 30;
            this.label9.Text = "Analysis type:";
            // 
            // radioButtonAnalysisPeptideReport
            // 
            this.radioButtonAnalysisPeptideReport.AutoSize = true;
            this.radioButtonAnalysisPeptideReport.Checked = true;
            this.radioButtonAnalysisPeptideReport.Location = new System.Drawing.Point(141, 339);
            this.radioButtonAnalysisPeptideReport.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonAnalysisPeptideReport.Name = "radioButtonAnalysisPeptideReport";
            this.radioButtonAnalysisPeptideReport.Size = new System.Drawing.Size(205, 21);
            this.radioButtonAnalysisPeptideReport.TabIndex = 31;
            this.radioButtonAnalysisPeptideReport.TabStop = true;
            this.radioButtonAnalysisPeptideReport.Text = "Peptide Quantitation Report";
            this.radioButtonAnalysisPeptideReport.UseVisualStyleBackColor = true;
            // 
            // radioButtonSparseMatrix
            // 
            this.radioButtonSparseMatrix.AutoSize = true;
            this.radioButtonSparseMatrix.Location = new System.Drawing.Point(141, 368);
            this.radioButtonSparseMatrix.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonSparseMatrix.Name = "radioButtonSparseMatrix";
            this.radioButtonSparseMatrix.Size = new System.Drawing.Size(143, 21);
            this.radioButtonSparseMatrix.TabIndex = 32;
            this.radioButtonSparseMatrix.Text = "PatternLabProject";
            this.radioButtonSparseMatrix.UseVisualStyleBackColor = true;
            this.radioButtonSparseMatrix.CheckedChanged += new System.EventHandler(this.radioButtonSparseMatrix_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 53);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(192, 17);
            this.label10.TabIndex = 33;
            this.label10.Text = "YADA multiplex correction Dir";
            // 
            // textBoxCorrectedYadaDirectory
            // 
            this.textBoxCorrectedYadaDirectory.Location = new System.Drawing.Point(225, 49);
            this.textBoxCorrectedYadaDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCorrectedYadaDirectory.Name = "textBoxCorrectedYadaDirectory";
            this.textBoxCorrectedYadaDirectory.Size = new System.Drawing.Size(211, 22);
            this.textBoxCorrectedYadaDirectory.TabIndex = 34;
            // 
            // buttonBrowseYada
            // 
            this.buttonBrowseYada.Location = new System.Drawing.Point(445, 48);
            this.buttonBrowseYada.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBrowseYada.Name = "buttonBrowseYada";
            this.buttonBrowseYada.Size = new System.Drawing.Size(125, 26);
            this.buttonBrowseYada.TabIndex = 35;
            this.buttonBrowseYada.Text = "Browse";
            this.buttonBrowseYada.UseVisualStyleBackColor = true;
            this.buttonBrowseYada.Click += new System.EventHandler(this.buttonBrowseYada_Click);
            // 
            // buttonCombinePeptidePValues
            // 
            this.buttonCombinePeptidePValues.Location = new System.Drawing.Point(300, 33);
            this.buttonCombinePeptidePValues.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCombinePeptidePValues.Name = "buttonCombinePeptidePValues";
            this.buttonCombinePeptidePValues.Size = new System.Drawing.Size(289, 28);
            this.buttonCombinePeptidePValues.TabIndex = 36;
            this.buttonCombinePeptidePValues.Text = "Ratio of ratios (Experimental feature)";
            this.buttonCombinePeptidePValues.UseVisualStyleBackColor = true;
            this.buttonCombinePeptidePValues.Click += new System.EventHandler(this.buttonCombinePeptidePValues_Click);
            // 
            // buttonMassesItraq8
            // 
            this.buttonMassesItraq8.Location = new System.Drawing.Point(371, 142);
            this.buttonMassesItraq8.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMassesItraq8.Name = "buttonMassesItraq8";
            this.buttonMassesItraq8.Size = new System.Drawing.Size(100, 28);
            this.buttonMassesItraq8.TabIndex = 38;
            this.buttonMassesItraq8.Text = "iTRAQ 8";
            this.buttonMassesItraq8.UseVisualStyleBackColor = true;
            this.buttonMassesItraq8.Click += new System.EventHandler(this.buttonMassesItraq8_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPagePurityCorrections);
            this.tabControlMain.Controls.Add(this.tabPageLog);
            this.tabControlMain.Controls.Add(this.tabPageNormalization);
            this.tabControlMain.Location = new System.Drawing.Point(699, 16);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(713, 568);
            this.tabControlMain.TabIndex = 39;
            // 
            // tabPagePurityCorrections
            // 
            this.tabPagePurityCorrections.Controls.Add(this.buttonLoadTMTDefaults);
            this.tabPagePurityCorrections.Controls.Add(this.dataGridViewPurity);
            this.tabPagePurityCorrections.Controls.Add(this.buttonLoadPurityDefaultsITRAQ);
            this.tabPagePurityCorrections.Location = new System.Drawing.Point(4, 25);
            this.tabPagePurityCorrections.Margin = new System.Windows.Forms.Padding(4);
            this.tabPagePurityCorrections.Name = "tabPagePurityCorrections";
            this.tabPagePurityCorrections.Padding = new System.Windows.Forms.Padding(4);
            this.tabPagePurityCorrections.Size = new System.Drawing.Size(705, 539);
            this.tabPagePurityCorrections.TabIndex = 0;
            this.tabPagePurityCorrections.Text = "Purity Corrections";
            this.tabPagePurityCorrections.UseVisualStyleBackColor = true;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.richTextBoxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 25);
            this.tabPageLog.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageLog.Size = new System.Drawing.Size(705, 539);
            this.tabPageLog.TabIndex = 1;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BackColor = System.Drawing.Color.Azure;
            this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLog.Location = new System.Drawing.Point(4, 4);
            this.richTextBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(697, 531);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // tabPageNormalization
            // 
            this.tabPageNormalization.Controls.Add(this.chartSignalPerChannel);
            this.tabPageNormalization.Controls.Add(this.comboBoxSelectFileForGraphs);
            this.tabPageNormalization.Location = new System.Drawing.Point(4, 25);
            this.tabPageNormalization.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageNormalization.Name = "tabPageNormalization";
            this.tabPageNormalization.Size = new System.Drawing.Size(705, 539);
            this.tabPageNormalization.TabIndex = 2;
            this.tabPageNormalization.Text = "Normalization";
            this.tabPageNormalization.UseVisualStyleBackColor = true;
            // 
            // chartSignalPerChannel
            // 
            this.chartSignalPerChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.Title = "Channel";
            chartArea1.AxisY.Title = "Total Signal From Identified MS2";
            chartArea1.Name = "ChartAreaTotalSignalUnnormalized";
            chartArea2.AxisX.Title = "Channel";
            chartArea2.AxisY.Title = "Total Signal After Normalization";
            chartArea2.Name = "ChartAreaTotalSignalNormalized";
            chartArea3.AxisX.Title = "Channel";
            chartArea3.AxisY.Title = "Total Signal Raw File";
            chartArea3.Name = "ChartAreaTotalSignalAll";
            this.chartSignalPerChannel.ChartAreas.Add(chartArea1);
            this.chartSignalPerChannel.ChartAreas.Add(chartArea2);
            this.chartSignalPerChannel.ChartAreas.Add(chartArea3);
            legend1.Name = "Legend1";
            this.chartSignalPerChannel.Legends.Add(legend1);
            this.chartSignalPerChannel.Location = new System.Drawing.Point(5, 41);
            this.chartSignalPerChannel.Margin = new System.Windows.Forms.Padding(4);
            this.chartSignalPerChannel.Name = "chartSignalPerChannel";
            series1.ChartArea = "ChartAreaTotalSignalUnnormalized";
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "SeriesSignal";
            series1.YValuesPerPoint = 3;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartAreaTotalSignalNormalized";
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.MarkerBorderWidth = 2;
            series2.Name = "SeriesSignalNormalized";
            series2.YValuesPerPoint = 3;
            series3.ChartArea = "ChartAreaTotalSignalAll";
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "SeriesSignalAllSpectra";
            this.chartSignalPerChannel.Series.Add(series1);
            this.chartSignalPerChannel.Series.Add(series2);
            this.chartSignalPerChannel.Series.Add(series3);
            this.chartSignalPerChannel.Size = new System.Drawing.Size(689, 435);
            this.chartSignalPerChannel.TabIndex = 0;
            this.chartSignalPerChannel.Text = "Spectra Volcano Plot";
            this.chartSignalPerChannel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chartSignalPerChannel_MouseClick);
            // 
            // comboBoxSelectFileForGraphs
            // 
            this.comboBoxSelectFileForGraphs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSelectFileForGraphs.Enabled = false;
            this.comboBoxSelectFileForGraphs.FormattingEnabled = true;
            this.comboBoxSelectFileForGraphs.Location = new System.Drawing.Point(4, 9);
            this.comboBoxSelectFileForGraphs.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSelectFileForGraphs.Name = "comboBoxSelectFileForGraphs";
            this.comboBoxSelectFileForGraphs.Size = new System.Drawing.Size(700, 24);
            this.comboBoxSelectFileForGraphs.TabIndex = 1;
            this.comboBoxSelectFileForGraphs.Text = "Select file";
            this.comboBoxSelectFileForGraphs.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectFileForGraphs_SelectedIndexChanged);
            // 
            // groupBoxPostProcessing
            // 
            this.groupBoxPostProcessing.Controls.Add(this.buttonTwoConditionsExperiment);
            this.groupBoxPostProcessing.Controls.Add(this.buttonCombinePeptidePValues);
            this.groupBoxPostProcessing.Location = new System.Drawing.Point(30, 440);
            this.groupBoxPostProcessing.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxPostProcessing.Name = "groupBoxPostProcessing";
            this.groupBoxPostProcessing.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxPostProcessing.Size = new System.Drawing.Size(624, 81);
            this.groupBoxPostProcessing.TabIndex = 40;
            this.groupBoxPostProcessing.TabStop = false;
            this.groupBoxPostProcessing.Text = "Post processing analysis";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 222);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 41;
            this.label1.Text = "Class labels";
            // 
            // textBoxClassLabels
            // 
            this.textBoxClassLabels.Location = new System.Drawing.Point(151, 219);
            this.textBoxClassLabels.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxClassLabels.Name = "textBoxClassLabels";
            this.textBoxClassLabels.Size = new System.Drawing.Size(511, 22);
            this.textBoxClassLabels.TabIndex = 42;
            // 
            // tabControlIsobaric
            // 
            this.tabControlIsobaric.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlIsobaric.Controls.Add(this.tabPage1);
            this.tabControlIsobaric.Controls.Add(this.tabPage2);
            this.tabControlIsobaric.Location = new System.Drawing.Point(0, 20);
            this.tabControlIsobaric.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlIsobaric.Name = "tabControlIsobaric";
            this.tabControlIsobaric.SelectedIndex = 0;
            this.tabControlIsobaric.Size = new System.Drawing.Size(691, 566);
            this.tabControlIsobaric.TabIndex = 43;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.buttonTMT10);
            this.tabPage1.Controls.Add(this.checkBoxOnlyUniquePeptides);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.textBoxClassLabels);
            this.tabPage1.Controls.Add(this.buttonMassesItraq);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonGo);
            this.tabPage1.Controls.Add(this.groupBoxPostProcessing);
            this.tabPage1.Controls.Add(this.buttonMassesTMT);
            this.tabPage1.Controls.Add(this.textBoxitraqSEPro);
            this.tabPage1.Controls.Add(this.buttonMassesItraq8);
            this.tabPage1.Controls.Add(this.numericUpDownIonCountThreshold);
            this.tabPage1.Controls.Add(this.buttonBrowseYada);
            this.tabPage1.Controls.Add(this.buttonLoadSepro);
            this.tabPage1.Controls.Add(this.textBoxCorrectedYadaDirectory);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.textBoxIsobaricMasses);
            this.tabPage1.Controls.Add(this.radioButtonSparseMatrix);
            this.tabPage1.Controls.Add(this.radioButtonAnalysisPeptideReport);
            this.tabPage1.Controls.Add(this.groupBoxNormalization);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.numericUpDownTMSPPM);
            this.tabPage1.Controls.Add(this.textBoxOutputDirectory);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(683, 537);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 192);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 45;
            this.label3.Text = "Marker masses";
            // 
            // buttonTMT10
            // 
            this.buttonTMT10.Location = new System.Drawing.Point(479, 142);
            this.buttonTMT10.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTMT10.Name = "buttonTMT10";
            this.buttonTMT10.Size = new System.Drawing.Size(100, 28);
            this.buttonTMT10.TabIndex = 44;
            this.buttonTMT10.Text = "TMT 10";
            this.buttonTMT10.UseVisualStyleBackColor = true;
            this.buttonTMT10.Click += new System.EventHandler(this.buttonTMT10_Click);
            // 
            // checkBoxOnlyUniquePeptides
            // 
            this.checkBoxOnlyUniquePeptides.AutoSize = true;
            this.checkBoxOnlyUniquePeptides.Enabled = false;
            this.checkBoxOnlyUniquePeptides.Location = new System.Drawing.Point(291, 369);
            this.checkBoxOnlyUniquePeptides.Name = "checkBoxOnlyUniquePeptides";
            this.checkBoxOnlyUniquePeptides.Size = new System.Drawing.Size(134, 21);
            this.checkBoxOnlyUniquePeptides.TabIndex = 43;
            this.checkBoxOnlyUniquePeptides.Text = "Unique Peptides";
            this.checkBoxOnlyUniquePeptides.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.multinotch1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(683, 537);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MultiNotch";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // multinotch1
            // 
            this.multinotch1.Location = new System.Drawing.Point(21, 27);
            this.multinotch1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.multinotch1.Name = "multinotch1";
            this.multinotch1.Size = new System.Drawing.Size(633, 149);
            this.multinotch1.TabIndex = 0;
            // 
            // ITRAQControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlIsobaric);
            this.Controls.Add(this.tabControlMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ITRAQControl";
            this.Size = new System.Drawing.Size(1416, 600);
            this.groupBoxNormalization.ResumeLayout(false);
            this.groupBoxNormalization.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIonCountThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTMSPPM)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPagePurityCorrections.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.tabPageNormalization.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartSignalPerChannel)).EndInit();
            this.groupBoxPostProcessing.ResumeLayout(false);
            this.tabControlIsobaric.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadSepro;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownIonCountThreshold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxIsobaricMasses;
        private System.Windows.Forms.NumericUpDown numericUpDownTMSPPM;
        private System.Windows.Forms.DataGridView dataGridViewPurity;
        private System.Windows.Forms.Button buttonLoadPurityDefaultsITRAQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPM2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPM1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPP1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPP2;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonMassesTMT;
        private System.Windows.Forms.Button buttonMassesItraq;
        private System.Windows.Forms.Button buttonLoadTMTDefaults;
        private System.Windows.Forms.TextBox textBoxitraqSEPro;
        private System.Windows.Forms.CheckBox checkBoxApplyPurityCorrection;
        private System.Windows.Forms.GroupBox groupBoxNormalization;
        private System.Windows.Forms.CheckBox checkBoxNormalizationChannelSignal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxOutputDirectory;
        private System.Windows.Forms.Button buttonTwoConditionsExperiment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radioButtonAnalysisPeptideReport;
        private System.Windows.Forms.RadioButton radioButtonSparseMatrix;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxCorrectedYadaDirectory;
        private System.Windows.Forms.Button buttonBrowseYada;
        private System.Windows.Forms.Button buttonCombinePeptidePValues;
        private System.Windows.Forms.Button buttonMassesItraq8;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPagePurityCorrections;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSignalPerChannel;
        private System.Windows.Forms.GroupBox groupBoxPostProcessing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxClassLabels;
        private System.Windows.Forms.TabControl tabControlIsobaric;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private IsobaricQuant.Multinotch multinotch1;
        private System.Windows.Forms.ComboBox comboBoxSelectFileForGraphs;
        private System.Windows.Forms.TabPage tabPageNormalization;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox checkBoxOnlyUniquePeptides;
        private System.Windows.Forms.Button buttonTMT10;
        private System.Windows.Forms.Label label3;
    }
}
