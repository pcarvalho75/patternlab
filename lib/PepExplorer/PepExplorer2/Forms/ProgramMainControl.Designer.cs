using System;
namespace PepExplorer2.Forms
{
    partial class ProgramMainControl
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
            this.components = new System.ComponentModel.Container();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.progressBarProgramMainForm = new System.Windows.Forms.ProgressBar();
            this.buttonStart = new System.Windows.Forms.Button();
            this.comboBoxDeNovoSoftware = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownMinIdentity = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinDenovoScore = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPeptideSize = new System.Windows.Forms.NumericUpDown();
            this.labelPeptideSize = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseMPEXSearch = new System.Windows.Forms.Button();
            this.textBoxmpexFile = new System.Windows.Forms.TextBox();
            this.radioButtonMpexUnasigned = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.richTextBoxPeptideList = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxDeNovoOutput = new System.Windows.Forms.TextBox();
            this.buttonDeNovoOutput = new System.Windows.Forms.Button();
            this.groupBoxDataBase = new System.Windows.Forms.GroupBox();
            this.radioButtonSwissProt = new System.Windows.Forms.RadioButton();
            this.radioButtonNextProt = new System.Windows.Forms.RadioButton();
            this.radioButtonNcbi = new System.Windows.Forms.RadioButton();
            this.radioButtonGeneric = new System.Windows.Forms.RadioButton();
            this.textBoxDecoyTag = new System.Windows.Forms.TextBox();
            this.labelDecoyTag = new System.Windows.Forms.Label();
            this.buttonDataBasePath = new System.Windows.Forms.Button();
            this.textBoxDataBaseFile = new System.Windows.Forms.TextBox();
            this.labelDataBasePath = new System.Windows.Forms.Label();
            this.saveFileDialogProgramMainForm = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogProgramMainForm = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogProgramMainForm = new System.Windows.Forms.FolderBrowserDialog();
            this.timerProgramMainForm = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorkerProgramMainForm = new System.ComponentModel.BackgroundWorker();
            this.menuStripMainFormProgram = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openResultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.programArgsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageStandardSearch = new System.Windows.Forms.TabPage();
            this.tabPageGridSearch = new System.Windows.Forms.TabPage();
            this.buttonGridSearch = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownGridDeNovoIncrement = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGridIdIncrement = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGridMaxDeNovoScore = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGridMaxId = new System.Windows.Forms.NumericUpDown();
            this.groupBoxParameters.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinIdentity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinDenovoScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPeptideSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxDataBase.SuspendLayout();
            this.menuStripMainFormProgram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.programArgsBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageStandardSearch.SuspendLayout();
            this.tabPageGridSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridDeNovoIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridIdIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridMaxDeNovoScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridMaxId)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.Controls.Add(this.progressBarProgramMainForm);
            this.groupBoxParameters.Controls.Add(this.buttonStart);
            this.groupBoxParameters.Controls.Add(this.comboBoxDeNovoSoftware);
            this.groupBoxParameters.Controls.Add(this.groupBox2);
            this.groupBoxParameters.Controls.Add(this.groupBox1);
            this.groupBoxParameters.Controls.Add(this.groupBoxDataBase);
            this.groupBoxParameters.Controls.Add(this.textBoxDecoyTag);
            this.groupBoxParameters.Controls.Add(this.labelDecoyTag);
            this.groupBoxParameters.Controls.Add(this.buttonDataBasePath);
            this.groupBoxParameters.Controls.Add(this.textBoxDataBaseFile);
            this.groupBoxParameters.Controls.Add(this.labelDataBasePath);
            this.groupBoxParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxParameters.Location = new System.Drawing.Point(3, 4);
            this.groupBoxParameters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxParameters.MinimumSize = new System.Drawing.Size(1052, 480);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxParameters.Size = new System.Drawing.Size(1088, 556);
            this.groupBoxParameters.TabIndex = 1;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Global Parameters";
            // 
            // progressBarProgramMainForm
            // 
            this.progressBarProgramMainForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarProgramMainForm.Location = new System.Drawing.Point(122, 512);
            this.progressBarProgramMainForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBarProgramMainForm.Name = "progressBarProgramMainForm";
            this.progressBarProgramMainForm.Size = new System.Drawing.Size(955, 35);
            this.progressBarProgramMainForm.TabIndex = 4;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStart.Location = new System.Drawing.Point(9, 512);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(105, 35);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // comboBoxDeNovoSoftware
            // 
            this.comboBoxDeNovoSoftware.FormattingEnabled = true;
            this.comboBoxDeNovoSoftware.Items.AddRange(new object[] {
            "Select Software",
            "MSGFDB",
            "NOVOR v1.05.0573",
            "PEAKS 7.0",
            "PEAKS 7.5",
            "PEAKS 8.0",
            "PNovo+",
            "PepNovo+ (output_dnv)",
            "PepNovo+ (output_full)",
            "Peptide List"});
            this.comboBoxDeNovoSoftware.Location = new System.Drawing.Point(10, 354);
            this.comboBoxDeNovoSoftware.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxDeNovoSoftware.Name = "comboBoxDeNovoSoftware";
            this.comboBoxDeNovoSoftware.Size = new System.Drawing.Size(484, 28);
            this.comboBoxDeNovoSoftware.TabIndex = 5;
            this.comboBoxDeNovoSoftware.Text = "Select de novo software";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownMinIdentity);
            this.groupBox2.Controls.Add(this.numericUpDownMinDenovoScore);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numericUpDownPeptideSize);
            this.groupBox2.Controls.Add(this.labelPeptideSize);
            this.groupBox2.Location = new System.Drawing.Point(10, 129);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(486, 135);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameters";
            // 
            // numericUpDownMinIdentity
            // 
            this.numericUpDownMinIdentity.DecimalPlaces = 2;
            this.numericUpDownMinIdentity.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownMinIdentity.Location = new System.Drawing.Point(35, 66);
            this.numericUpDownMinIdentity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownMinIdentity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinIdentity.Name = "numericUpDownMinIdentity";
            this.numericUpDownMinIdentity.Size = new System.Drawing.Size(91, 26);
            this.numericUpDownMinIdentity.TabIndex = 23;
            this.numericUpDownMinIdentity.Value = new decimal(new int[] {
            75,
            0,
            0,
            131072});
            // 
            // numericUpDownMinDenovoScore
            // 
            this.numericUpDownMinDenovoScore.DecimalPlaces = 1;
            this.numericUpDownMinDenovoScore.Location = new System.Drawing.Point(291, 66);
            this.numericUpDownMinDenovoScore.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownMinDenovoScore.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownMinDenovoScore.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownMinDenovoScore.Name = "numericUpDownMinDenovoScore";
            this.numericUpDownMinDenovoScore.Size = new System.Drawing.Size(129, 26);
            this.numericUpDownMinDenovoScore.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "MinIdentity";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "de novo score cutoff";
            // 
            // numericUpDownPeptideSize
            // 
            this.numericUpDownPeptideSize.Location = new System.Drawing.Point(166, 66);
            this.numericUpDownPeptideSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownPeptideSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPeptideSize.Name = "numericUpDownPeptideSize";
            this.numericUpDownPeptideSize.Size = new System.Drawing.Size(91, 26);
            this.numericUpDownPeptideSize.TabIndex = 16;
            this.numericUpDownPeptideSize.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // labelPeptideSize
            // 
            this.labelPeptideSize.AutoSize = true;
            this.labelPeptideSize.Location = new System.Drawing.Point(162, 39);
            this.labelPeptideSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPeptideSize.Name = "labelPeptideSize";
            this.labelPeptideSize.Size = new System.Drawing.Size(95, 20);
            this.labelPeptideSize.TabIndex = 15;
            this.labelPeptideSize.Text = "Peptide size";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(505, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.MinimumSize = new System.Drawing.Size(537, 369);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(573, 486);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "de novo Input";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.buttonBrowseMPEXSearch);
            this.groupBox5.Controls.Add(this.textBoxmpexFile);
            this.groupBox5.Controls.Add(this.radioButtonMpexUnasigned);
            this.groupBox5.Location = new System.Drawing.Point(14, 151);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(559, 86);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Option 2: Search a mpex result against another database";
            // 
            // buttonBrowseMPEXSearch
            // 
            this.buttonBrowseMPEXSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseMPEXSearch.Location = new System.Drawing.Point(427, 28);
            this.buttonBrowseMPEXSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBrowseMPEXSearch.Name = "buttonBrowseMPEXSearch";
            this.buttonBrowseMPEXSearch.Size = new System.Drawing.Size(96, 35);
            this.buttonBrowseMPEXSearch.TabIndex = 3;
            this.buttonBrowseMPEXSearch.Text = "Browse";
            this.buttonBrowseMPEXSearch.UseVisualStyleBackColor = true;
            this.buttonBrowseMPEXSearch.Click += new System.EventHandler(this.buttonBrowseMPEXSearch_Click);
            // 
            // textBoxmpexFile
            // 
            this.textBoxmpexFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxmpexFile.Location = new System.Drawing.Point(168, 31);
            this.textBoxmpexFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxmpexFile.Name = "textBoxmpexFile";
            this.textBoxmpexFile.Size = new System.Drawing.Size(248, 26);
            this.textBoxmpexFile.TabIndex = 2;
            // 
            // radioButtonMpexUnasigned
            // 
            this.radioButtonMpexUnasigned.AutoSize = true;
            this.radioButtonMpexUnasigned.Checked = true;
            this.radioButtonMpexUnasigned.Location = new System.Drawing.Point(9, 32);
            this.radioButtonMpexUnasigned.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonMpexUnasigned.Name = "radioButtonMpexUnasigned";
            this.radioButtonMpexUnasigned.Size = new System.Drawing.Size(146, 24);
            this.radioButtonMpexUnasigned.TabIndex = 1;
            this.radioButtonMpexUnasigned.TabStop = true;
            this.radioButtonMpexUnasigned.Text = "Only Unasigned";
            this.radioButtonMpexUnasigned.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.richTextBoxPeptideList);
            this.groupBox4.Location = new System.Drawing.Point(14, 246);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(550, 232);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Option 3: Paste a list of peptides";
            // 
            // richTextBoxPeptideList
            // 
            this.richTextBoxPeptideList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxPeptideList.Location = new System.Drawing.Point(4, 24);
            this.richTextBoxPeptideList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBoxPeptideList.Name = "richTextBoxPeptideList";
            this.richTextBoxPeptideList.Size = new System.Drawing.Size(542, 203);
            this.richTextBoxPeptideList.TabIndex = 9;
            this.richTextBoxPeptideList.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.textBoxDeNovoOutput);
            this.groupBox3.Controls.Add(this.buttonDeNovoOutput);
            this.groupBox3.Location = new System.Drawing.Point(14, 31);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(550, 111);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Option 1 : Enter a directory containing de novo results";
            // 
            // textBoxDeNovoOutput
            // 
            this.textBoxDeNovoOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDeNovoOutput.Location = new System.Drawing.Point(9, 51);
            this.textBoxDeNovoOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDeNovoOutput.Name = "textBoxDeNovoOutput";
            this.textBoxDeNovoOutput.Size = new System.Drawing.Size(408, 26);
            this.textBoxDeNovoOutput.TabIndex = 4;
            // 
            // buttonDeNovoOutput
            // 
            this.buttonDeNovoOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeNovoOutput.Location = new System.Drawing.Point(427, 46);
            this.buttonDeNovoOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeNovoOutput.Name = "buttonDeNovoOutput";
            this.buttonDeNovoOutput.Size = new System.Drawing.Size(96, 35);
            this.buttonDeNovoOutput.TabIndex = 7;
            this.buttonDeNovoOutput.Text = "Browse";
            this.buttonDeNovoOutput.UseVisualStyleBackColor = true;
            this.buttonDeNovoOutput.Click += new System.EventHandler(this.buttonDeNovoOutput_Click);
            // 
            // groupBoxDataBase
            // 
            this.groupBoxDataBase.Controls.Add(this.radioButtonSwissProt);
            this.groupBoxDataBase.Controls.Add(this.radioButtonNextProt);
            this.groupBoxDataBase.Controls.Add(this.radioButtonNcbi);
            this.groupBoxDataBase.Controls.Add(this.radioButtonGeneric);
            this.groupBoxDataBase.Location = new System.Drawing.Point(10, 274);
            this.groupBoxDataBase.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDataBase.Name = "groupBoxDataBase";
            this.groupBoxDataBase.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDataBase.Size = new System.Drawing.Size(486, 71);
            this.groupBoxDataBase.TabIndex = 12;
            this.groupBoxDataBase.TabStop = false;
            this.groupBoxDataBase.Text = "DataBase Format";
            // 
            // radioButtonSwissProt
            // 
            this.radioButtonSwissProt.AutoSize = true;
            this.radioButtonSwissProt.Location = new System.Drawing.Point(251, 28);
            this.radioButtonSwissProt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonSwissProt.Name = "radioButtonSwissProt";
            this.radioButtonSwissProt.Size = new System.Drawing.Size(108, 24);
            this.radioButtonSwissProt.TabIndex = 3;
            this.radioButtonSwissProt.Text = "UniProtKB";
            this.radioButtonSwissProt.UseVisualStyleBackColor = true;
            // 
            // radioButtonNextProt
            // 
            this.radioButtonNextProt.AutoSize = true;
            this.radioButtonNextProt.Location = new System.Drawing.Point(370, 28);
            this.radioButtonNextProt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonNextProt.Name = "radioButtonNextProt";
            this.radioButtonNextProt.Size = new System.Drawing.Size(95, 24);
            this.radioButtonNextProt.TabIndex = 2;
            this.radioButtonNextProt.Text = "NextProt";
            this.radioButtonNextProt.UseVisualStyleBackColor = true;
            // 
            // radioButtonNcbi
            // 
            this.radioButtonNcbi.AutoSize = true;
            this.radioButtonNcbi.Location = new System.Drawing.Point(166, 28);
            this.radioButtonNcbi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonNcbi.Name = "radioButtonNcbi";
            this.radioButtonNcbi.Size = new System.Drawing.Size(72, 24);
            this.radioButtonNcbi.TabIndex = 1;
            this.radioButtonNcbi.Text = "NCBI";
            this.radioButtonNcbi.UseVisualStyleBackColor = true;
            // 
            // radioButtonGeneric
            // 
            this.radioButtonGeneric.AutoSize = true;
            this.radioButtonGeneric.Checked = true;
            this.radioButtonGeneric.Location = new System.Drawing.Point(9, 28);
            this.radioButtonGeneric.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonGeneric.Name = "radioButtonGeneric";
            this.radioButtonGeneric.Size = new System.Drawing.Size(146, 24);
            this.radioButtonGeneric.TabIndex = 0;
            this.radioButtonGeneric.TabStop = true;
            this.radioButtonGeneric.Text = "Generic FASTA";
            this.radioButtonGeneric.UseVisualStyleBackColor = true;
            // 
            // textBoxDecoyTag
            // 
            this.textBoxDecoyTag.Location = new System.Drawing.Point(122, 89);
            this.textBoxDecoyTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDecoyTag.Name = "textBoxDecoyTag";
            this.textBoxDecoyTag.Size = new System.Drawing.Size(264, 26);
            this.textBoxDecoyTag.TabIndex = 6;
            this.textBoxDecoyTag.Text = "Reverse";
            // 
            // labelDecoyTag
            // 
            this.labelDecoyTag.AutoSize = true;
            this.labelDecoyTag.Location = new System.Drawing.Point(22, 94);
            this.labelDecoyTag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDecoyTag.Name = "labelDecoyTag";
            this.labelDecoyTag.Size = new System.Drawing.Size(85, 20);
            this.labelDecoyTag.TabIndex = 5;
            this.labelDecoyTag.Text = "Decoy Tag";
            // 
            // buttonDataBasePath
            // 
            this.buttonDataBasePath.Location = new System.Drawing.Point(396, 41);
            this.buttonDataBasePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDataBasePath.Name = "buttonDataBasePath";
            this.buttonDataBasePath.Size = new System.Drawing.Size(100, 32);
            this.buttonDataBasePath.TabIndex = 2;
            this.buttonDataBasePath.Text = "Browse";
            this.buttonDataBasePath.UseVisualStyleBackColor = true;
            this.buttonDataBasePath.Click += new System.EventHandler(this.buttonDataBasePath_Click);
            // 
            // textBoxDataBaseFile
            // 
            this.textBoxDataBaseFile.Location = new System.Drawing.Point(122, 44);
            this.textBoxDataBaseFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDataBaseFile.Name = "textBoxDataBaseFile";
            this.textBoxDataBaseFile.Size = new System.Drawing.Size(264, 26);
            this.textBoxDataBaseFile.TabIndex = 1;
            // 
            // labelDataBasePath
            // 
            this.labelDataBasePath.AutoSize = true;
            this.labelDataBasePath.Location = new System.Drawing.Point(27, 48);
            this.labelDataBasePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDataBasePath.Name = "labelDataBasePath";
            this.labelDataBasePath.Size = new System.Drawing.Size(85, 20);
            this.labelDataBasePath.TabIndex = 0;
            this.labelDataBasePath.Text = "Data Base";
            // 
            // openFileDialogProgramMainForm
            // 
            this.openFileDialogProgramMainForm.FileName = "openFileDialog1";
            // 
            // timerProgramMainForm
            // 
            this.timerProgramMainForm.Interval = 1000;
            this.timerProgramMainForm.Tick += new System.EventHandler(this.timerProgramMainForm_Tick);
            // 
            // backgroundWorkerProgramMainForm
            // 
            this.backgroundWorkerProgramMainForm.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerProgramMainForm_DoWork);
            this.backgroundWorkerProgramMainForm.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerProgramMainForm_RunWorkerCompleted);
            // 
            // menuStripMainFormProgram
            // 
            this.menuStripMainFormProgram.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStripMainFormProgram.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataViewerToolStripMenuItem});
            this.menuStripMainFormProgram.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainFormProgram.Name = "menuStripMainFormProgram";
            this.menuStripMainFormProgram.Padding = new System.Windows.Forms.Padding(9, 4, 0, 4);
            this.menuStripMainFormProgram.Size = new System.Drawing.Size(1102, 37);
            this.menuStripMainFormProgram.TabIndex = 5;
            this.menuStripMainFormProgram.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openResultToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openResultToolStripMenuItem
            // 
            this.openResultToolStripMenuItem.Name = "openResultToolStripMenuItem";
            this.openResultToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
            this.openResultToolStripMenuItem.Text = "Open result";
            this.openResultToolStripMenuItem.Click += new System.EventHandler(this.openResultToolStripMenuItem_Click);
            // 
            // dataViewerToolStripMenuItem
            // 
            this.dataViewerToolStripMenuItem.Name = "dataViewerToolStripMenuItem";
            this.dataViewerToolStripMenuItem.Size = new System.Drawing.Size(12, 29);
            // 
            // programArgsBindingSource
            // 
            this.programArgsBindingSource.DataSource = typeof(PepExplorer2.ProgramArgs);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageStandardSearch);
            this.tabControl1.Controls.Add(this.tabPageGridSearch);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 37);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1102, 597);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPageStandardSearch
            // 
            this.tabPageStandardSearch.Controls.Add(this.groupBoxParameters);
            this.tabPageStandardSearch.Location = new System.Drawing.Point(4, 29);
            this.tabPageStandardSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageStandardSearch.Name = "tabPageStandardSearch";
            this.tabPageStandardSearch.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageStandardSearch.Size = new System.Drawing.Size(1094, 564);
            this.tabPageStandardSearch.TabIndex = 0;
            this.tabPageStandardSearch.Text = "Standard Search";
            this.tabPageStandardSearch.UseVisualStyleBackColor = true;
            // 
            // tabPageGridSearch
            // 
            this.tabPageGridSearch.Controls.Add(this.buttonGridSearch);
            this.tabPageGridSearch.Controls.Add(this.label6);
            this.tabPageGridSearch.Controls.Add(this.label7);
            this.tabPageGridSearch.Controls.Add(this.label8);
            this.tabPageGridSearch.Controls.Add(this.label9);
            this.tabPageGridSearch.Controls.Add(this.numericUpDownGridDeNovoIncrement);
            this.tabPageGridSearch.Controls.Add(this.numericUpDownGridIdIncrement);
            this.tabPageGridSearch.Controls.Add(this.numericUpDownGridMaxDeNovoScore);
            this.tabPageGridSearch.Controls.Add(this.numericUpDownGridMaxId);
            this.tabPageGridSearch.Location = new System.Drawing.Point(4, 29);
            this.tabPageGridSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageGridSearch.Name = "tabPageGridSearch";
            this.tabPageGridSearch.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageGridSearch.Size = new System.Drawing.Size(1094, 563);
            this.tabPageGridSearch.TabIndex = 1;
            this.tabPageGridSearch.Text = "Grid Search";
            this.tabPageGridSearch.UseVisualStyleBackColor = true;
            // 
            // buttonGridSearch
            // 
            this.buttonGridSearch.Location = new System.Drawing.Point(30, 148);
            this.buttonGridSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonGridSearch.Name = "buttonGridSearch";
            this.buttonGridSearch.Size = new System.Drawing.Size(579, 29);
            this.buttonGridSearch.TabIndex = 41;
            this.buttonGridSearch.Text = "Go (This will take a while !)";
            this.buttonGridSearch.UseVisualStyleBackColor = true;
            this.buttonGridSearch.Click += new System.EventHandler(this.buttonGridSearch_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(324, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 20);
            this.label6.TabIndex = 40;
            this.label6.Text = "De novo score increment";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(324, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 20);
            this.label7.TabIndex = 39;
            this.label7.Text = "Identity Increment";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(187, 20);
            this.label8.TabIndex = 38;
            this.label8.Text = "Maximum De Novo Score";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 20);
            this.label9.TabIndex = 37;
            this.label9.Text = "Maximum Identity";
            // 
            // numericUpDownGridDeNovoIncrement
            // 
            this.numericUpDownGridDeNovoIncrement.DecimalPlaces = 2;
            this.numericUpDownGridDeNovoIncrement.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownGridDeNovoIncrement.Location = new System.Drawing.Point(519, 88);
            this.numericUpDownGridDeNovoIncrement.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownGridDeNovoIncrement.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownGridDeNovoIncrement.Name = "numericUpDownGridDeNovoIncrement";
            this.numericUpDownGridDeNovoIncrement.Size = new System.Drawing.Size(91, 26);
            this.numericUpDownGridDeNovoIncrement.TabIndex = 36;
            this.numericUpDownGridDeNovoIncrement.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDownGridIdIncrement
            // 
            this.numericUpDownGridIdIncrement.DecimalPlaces = 2;
            this.numericUpDownGridIdIncrement.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownGridIdIncrement.Location = new System.Drawing.Point(519, 50);
            this.numericUpDownGridIdIncrement.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownGridIdIncrement.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGridIdIncrement.Name = "numericUpDownGridIdIncrement";
            this.numericUpDownGridIdIncrement.Size = new System.Drawing.Size(91, 26);
            this.numericUpDownGridIdIncrement.TabIndex = 35;
            this.numericUpDownGridIdIncrement.Value = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            // 
            // numericUpDownGridMaxDeNovoScore
            // 
            this.numericUpDownGridMaxDeNovoScore.DecimalPlaces = 2;
            this.numericUpDownGridMaxDeNovoScore.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownGridMaxDeNovoScore.Location = new System.Drawing.Point(222, 88);
            this.numericUpDownGridMaxDeNovoScore.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownGridMaxDeNovoScore.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownGridMaxDeNovoScore.Name = "numericUpDownGridMaxDeNovoScore";
            this.numericUpDownGridMaxDeNovoScore.Size = new System.Drawing.Size(91, 26);
            this.numericUpDownGridMaxDeNovoScore.TabIndex = 34;
            this.numericUpDownGridMaxDeNovoScore.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDownGridMaxId
            // 
            this.numericUpDownGridMaxId.DecimalPlaces = 2;
            this.numericUpDownGridMaxId.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownGridMaxId.Location = new System.Drawing.Point(222, 46);
            this.numericUpDownGridMaxId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownGridMaxId.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGridMaxId.Name = "numericUpDownGridMaxId";
            this.numericUpDownGridMaxId.Size = new System.Drawing.Size(91, 26);
            this.numericUpDownGridMaxId.TabIndex = 33;
            this.numericUpDownGridMaxId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ProgramMainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStripMainFormProgram);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ProgramMainControl";
            this.Size = new System.Drawing.Size(1102, 634);
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinIdentity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinDenovoScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPeptideSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxDataBase.ResumeLayout(false);
            this.groupBoxDataBase.PerformLayout();
            this.menuStripMainFormProgram.ResumeLayout(false);
            this.menuStripMainFormProgram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.programArgsBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageStandardSearch.ResumeLayout(false);
            this.tabPageGridSearch.ResumeLayout(false);
            this.tabPageGridSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridDeNovoIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridIdIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridMaxDeNovoScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridMaxId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.Button buttonDataBasePath;
        private System.Windows.Forms.TextBox textBoxDataBaseFile;
        private System.Windows.Forms.Label labelDataBasePath;
        private System.Windows.Forms.TextBox textBoxDeNovoOutput;
        private System.Windows.Forms.TextBox textBoxDecoyTag;
        private System.Windows.Forms.Label labelDecoyTag;
        private System.Windows.Forms.Button buttonDeNovoOutput;
        private System.Windows.Forms.GroupBox groupBoxDataBase;
        private System.Windows.Forms.Label labelPeptideSize;
        private System.Windows.Forms.NumericUpDown numericUpDownPeptideSize;
        private System.Windows.Forms.RadioButton radioButtonSwissProt;
        private System.Windows.Forms.RadioButton radioButtonNextProt;
        private System.Windows.Forms.RadioButton radioButtonNcbi;
        private System.Windows.Forms.RadioButton radioButtonGeneric;
        private System.Windows.Forms.BindingSource programArgsBindingSource;
        private System.Windows.Forms.SaveFileDialog saveFileDialogProgramMainForm;
        private System.Windows.Forms.OpenFileDialog openFileDialogProgramMainForm;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogProgramMainForm;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ProgressBar progressBarProgramMainForm;
        private System.Windows.Forms.Timer timerProgramMainForm;
        private System.ComponentModel.BackgroundWorker backgroundWorkerProgramMainForm;
        private System.Windows.Forms.MenuStrip menuStripMainFormProgram;
        private System.Windows.Forms.NumericUpDown numericUpDownMinDenovoScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBoxPeptideList;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem dataViewerToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxDeNovoSoftware;
        private System.Windows.Forms.NumericUpDown numericUpDownMinIdentity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openResultToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonBrowseMPEXSearch;
        private System.Windows.Forms.TextBox textBoxmpexFile;
        private System.Windows.Forms.RadioButton radioButtonMpexUnasigned;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageStandardSearch;
        private System.Windows.Forms.TabPage tabPageGridSearch;
        private System.Windows.Forms.Button buttonGridSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownGridDeNovoIncrement;
        private System.Windows.Forms.NumericUpDown numericUpDownGridIdIncrement;
        private System.Windows.Forms.NumericUpDown numericUpDownGridMaxDeNovoScore;
        private System.Windows.Forms.NumericUpDown numericUpDownGridMaxId;
    }
}