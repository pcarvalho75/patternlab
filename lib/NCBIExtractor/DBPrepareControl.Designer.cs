namespace NCBIExtractor
{
    partial class DBPrepareControl
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNameRegex = new System.Windows.Forms.TextBox();
            this.buttonGO = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.utilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanDBRemveSequencesFollowingARegexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nucleotideToProteinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSequencesRemoved = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBoxInsertContaminants = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonDBUniprot = new System.Windows.Forms.RadioButton();
            this.radioButtonDBGeneric = new System.Windows.Forms.RadioButton();
            this.radioButtonDBIPI = new System.Windows.Forms.RadioButton();
            this.radioButtonDBNCBI = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxEnzyme = new System.Windows.Forms.ComboBox();
            this.radioButtonTPRMR = new System.Windows.Forms.RadioButton();
            this.radioButtonT = new System.Windows.Forms.RadioButton();
            this.radioButtonTRoT = new System.Windows.Forms.RadioButton();
            this.groupBoxInputDB = new System.Windows.Forms.GroupBox();
            this.dataGridViewInputDBs = new System.Windows.Forms.DataGridView();
            this.ColumnFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxEliminateSubsetSequences = new System.Windows.Forms.CheckBox();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.labelIdentity = new System.Windows.Forms.Label();
            this.numericUpDownIdentity = new System.Windows.Forms.NumericUpDown();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.radioButtonNextProt = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxInputDB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputDBs)).BeginInit();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIdentity)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(796, 17);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Main DB(s): ";
            // 
            // textBoxNameRegex
            // 
            this.textBoxNameRegex.Location = new System.Drawing.Point(120, 154);
            this.textBoxNameRegex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxNameRegex.Name = "textBoxNameRegex";
            this.textBoxNameRegex.Size = new System.Drawing.Size(666, 26);
            this.textBoxNameRegex.TabIndex = 4;
            // 
            // buttonGO
            // 
            this.buttonGO.Enabled = false;
            this.buttonGO.Location = new System.Drawing.Point(9, 588);
            this.buttonGO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGO.Name = "buttonGO";
            this.buttonGO.Size = new System.Drawing.Size(918, 35);
            this.buttonGO.TabIndex = 5;
            this.buttonGO.Text = "GO!";
            this.buttonGO.UseVisualStyleBackColor = true;
            this.buttonGO.Click += new System.EventHandler(this.buttonGO_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.utilsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(978, 35);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // utilsToolStripMenuItem
            // 
            this.utilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cleanDBRemveSequencesFollowingARegexToolStripMenuItem,
            this.nucleotideToProteinToolStripMenuItem});
            this.utilsToolStripMenuItem.Name = "utilsToolStripMenuItem";
            this.utilsToolStripMenuItem.Size = new System.Drawing.Size(58, 29);
            this.utilsToolStripMenuItem.Text = "Utils";
            // 
            // cleanDBRemveSequencesFollowingARegexToolStripMenuItem
            // 
            this.cleanDBRemveSequencesFollowingARegexToolStripMenuItem.Name = "cleanDBRemveSequencesFollowingARegexToolStripMenuItem";
            this.cleanDBRemveSequencesFollowingARegexToolStripMenuItem.Size = new System.Drawing.Size(479, 30);
            this.cleanDBRemveSequencesFollowingARegexToolStripMenuItem.Text = "Clean DB (Remove sequences following a Regex)";
            this.cleanDBRemveSequencesFollowingARegexToolStripMenuItem.Click += new System.EventHandler(this.cleanDBRemveSequencesFollowingARegexToolStripMenuItem_Click);
            // 
            // nucleotideToProteinToolStripMenuItem
            // 
            this.nucleotideToProteinToolStripMenuItem.Name = "nucleotideToProteinToolStripMenuItem";
            this.nucleotideToProteinToolStripMenuItem.Size = new System.Drawing.Size(479, 30);
            this.nucleotideToProteinToolStripMenuItem.Text = "Nucleotide to Protein";
            this.nucleotideToProteinToolStripMenuItem.Click += new System.EventHandler(this.nucleotideToProteinToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(74, 29);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Output:";
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(84, 29);
            this.textBoxOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(702, 26);
            this.textBoxOutput.TabIndex = 9;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(796, 26);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(112, 35);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save as";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(16, 158);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(94, 20);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "RegexFilter:";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelSequencesRemoved});
            this.statusStrip1.Location = new System.Drawing.Point(0, 687);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(978, 30);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(56, 25);
            this.toolStripStatusLabel1.Text = "Idle   ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(185, 25);
            this.toolStripStatusLabel2.Text = "SequencesRemoved : ";
            // 
            // toolStripStatusLabelSequencesRemoved
            // 
            this.toolStripStatusLabelSequencesRemoved.Name = "toolStripStatusLabelSequencesRemoved";
            this.toolStripStatusLabelSequencesRemoved.Size = new System.Drawing.Size(20, 25);
            this.toolStripStatusLabelSequencesRemoved.Text = "?";
            // 
            // checkBoxInsertContaminants
            // 
            this.checkBoxInsertContaminants.AutoSize = true;
            this.checkBoxInsertContaminants.Checked = true;
            this.checkBoxInsertContaminants.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxInsertContaminants.Location = new System.Drawing.Point(9, 133);
            this.checkBoxInsertContaminants.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxInsertContaminants.Name = "checkBoxInsertContaminants";
            this.checkBoxInsertContaminants.Size = new System.Drawing.Size(353, 24);
            this.checkBoxInsertContaminants.TabIndex = 13;
            this.checkBoxInsertContaminants.Text = "Include common contaminants in the Targets";
            this.checkBoxInsertContaminants.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonNextProt);
            this.groupBox1.Controls.Add(this.radioButtonDBUniprot);
            this.groupBox1.Controls.Add(this.radioButtonDBGeneric);
            this.groupBox1.Controls.Add(this.radioButtonDBIPI);
            this.groupBox1.Controls.Add(this.radioButtonDBNCBI);
            this.groupBox1.Location = new System.Drawing.Point(9, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(256, 213);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input database type";
            // 
            // radioButtonDBUniprot
            // 
            this.radioButtonDBUniprot.AutoSize = true;
            this.radioButtonDBUniprot.Location = new System.Drawing.Point(26, 29);
            this.radioButtonDBUniprot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonDBUniprot.Name = "radioButtonDBUniprot";
            this.radioButtonDBUniprot.Size = new System.Drawing.Size(87, 24);
            this.radioButtonDBUniprot.TabIndex = 3;
            this.radioButtonDBUniprot.Text = "UniProt";
            this.radioButtonDBUniprot.UseVisualStyleBackColor = true;
            // 
            // radioButtonDBGeneric
            // 
            this.radioButtonDBGeneric.AutoSize = true;
            this.radioButtonDBGeneric.Checked = true;
            this.radioButtonDBGeneric.Location = new System.Drawing.Point(26, 162);
            this.radioButtonDBGeneric.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonDBGeneric.Name = "radioButtonDBGeneric";
            this.radioButtonDBGeneric.Size = new System.Drawing.Size(222, 24);
            this.radioButtonDBGeneric.TabIndex = 2;
            this.radioButtonDBGeneric.TabStop = true;
            this.radioButtonDBGeneric.Text = "IdentifierSpaceDescription";
            this.radioButtonDBGeneric.UseVisualStyleBackColor = true;
            // 
            // radioButtonDBIPI
            // 
            this.radioButtonDBIPI.AutoSize = true;
            this.radioButtonDBIPI.Location = new System.Drawing.Point(26, 98);
            this.radioButtonDBIPI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonDBIPI.Name = "radioButtonDBIPI";
            this.radioButtonDBIPI.Size = new System.Drawing.Size(54, 24);
            this.radioButtonDBIPI.TabIndex = 1;
            this.radioButtonDBIPI.Text = "IPI";
            this.radioButtonDBIPI.UseVisualStyleBackColor = true;
            // 
            // radioButtonDBNCBI
            // 
            this.radioButtonDBNCBI.AutoSize = true;
            this.radioButtonDBNCBI.Location = new System.Drawing.Point(26, 63);
            this.radioButtonDBNCBI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonDBNCBI.Name = "radioButtonDBNCBI";
            this.radioButtonDBNCBI.Size = new System.Drawing.Size(72, 24);
            this.radioButtonDBNCBI.TabIndex = 0;
            this.radioButtonDBNCBI.Text = "NCBI";
            this.radioButtonDBNCBI.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxEnzyme);
            this.groupBox2.Controls.Add(this.radioButtonTPRMR);
            this.groupBox2.Controls.Add(this.radioButtonT);
            this.groupBox2.Controls.Add(this.checkBoxInsertContaminants);
            this.groupBox2.Controls.Add(this.radioButtonTRoT);
            this.groupBox2.Location = new System.Drawing.Point(274, 29);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(652, 213);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output database format";
            // 
            // comboBoxEnzyme
            // 
            this.comboBoxEnzyme.Enabled = false;
            this.comboBoxEnzyme.FormattingEnabled = true;
            this.comboBoxEnzyme.Items.AddRange(new object[] {
            "Trypsin"});
            this.comboBoxEnzyme.Location = new System.Drawing.Point(320, 63);
            this.comboBoxEnzyme.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxEnzyme.Name = "comboBoxEnzyme";
            this.comboBoxEnzyme.Size = new System.Drawing.Size(259, 28);
            this.comboBoxEnzyme.TabIndex = 25;
            this.comboBoxEnzyme.Text = "Trypsin";
            // 
            // radioButtonTPRMR
            // 
            this.radioButtonTPRMR.AutoSize = true;
            this.radioButtonTPRMR.Location = new System.Drawing.Point(9, 65);
            this.radioButtonTPRMR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonTPRMR.Name = "radioButtonTPRMR";
            this.radioButtonTPRMR.Size = new System.Drawing.Size(289, 24);
            this.radioButtonTPRMR.TabIndex = 24;
            this.radioButtonTPRMR.Text = "Target-PairReverses-MiddleReverse";
            this.radioButtonTPRMR.UseVisualStyleBackColor = true;
            this.radioButtonTPRMR.CheckedChanged += new System.EventHandler(this.radioButtonTSWRSW_CheckedChanged);
            // 
            // radioButtonT
            // 
            this.radioButtonT.AutoSize = true;
            this.radioButtonT.Location = new System.Drawing.Point(9, 99);
            this.radioButtonT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonT.Name = "radioButtonT";
            this.radioButtonT.Size = new System.Drawing.Size(80, 24);
            this.radioButtonT.TabIndex = 22;
            this.radioButtonT.Text = "Target";
            this.radioButtonT.UseVisualStyleBackColor = true;
            this.radioButtonT.CheckedChanged += new System.EventHandler(this.radioButtonT_CheckedChanged);
            // 
            // radioButtonTRoT
            // 
            this.radioButtonTRoT.AutoSize = true;
            this.radioButtonTRoT.Checked = true;
            this.radioButtonTRoT.Location = new System.Drawing.Point(9, 29);
            this.radioButtonTRoT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonTRoT.Name = "radioButtonTRoT";
            this.radioButtonTRoT.Size = new System.Drawing.Size(200, 24);
            this.radioButtonTRoT.TabIndex = 21;
            this.radioButtonTRoT.TabStop = true;
            this.radioButtonTRoT.Text = "Target-Reverse(Target)";
            this.radioButtonTRoT.UseVisualStyleBackColor = true;
            this.radioButtonTRoT.CheckedChanged += new System.EventHandler(this.radioButtonTRoT_CheckedChanged);
            // 
            // groupBoxInputDB
            // 
            this.groupBoxInputDB.Controls.Add(this.dataGridViewInputDBs);
            this.groupBoxInputDB.Controls.Add(this.label1);
            this.groupBoxInputDB.Controls.Add(this.button1);
            this.groupBoxInputDB.Controls.Add(this.textBoxNameRegex);
            this.groupBoxInputDB.Controls.Add(this.linkLabel1);
            this.groupBoxInputDB.Location = new System.Drawing.Point(9, 252);
            this.groupBoxInputDB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxInputDB.Name = "groupBoxInputDB";
            this.groupBoxInputDB.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxInputDB.Size = new System.Drawing.Size(918, 205);
            this.groupBoxInputDB.TabIndex = 21;
            this.groupBoxInputDB.TabStop = false;
            this.groupBoxInputDB.Text = "Input";
            // 
            // dataGridViewInputDBs
            // 
            this.dataGridViewInputDBs.AllowUserToAddRows = false;
            this.dataGridViewInputDBs.AllowUserToDeleteRows = false;
            this.dataGridViewInputDBs.AllowUserToOrderColumns = true;
            this.dataGridViewInputDBs.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridViewInputDBs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInputDBs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFileName});
            this.dataGridViewInputDBs.Location = new System.Drawing.Point(122, 25);
            this.dataGridViewInputDBs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewInputDBs.Name = "dataGridViewInputDBs";
            this.dataGridViewInputDBs.ReadOnly = true;
            this.dataGridViewInputDBs.Size = new System.Drawing.Size(666, 123);
            this.dataGridViewInputDBs.TabIndex = 12;
            // 
            // ColumnFileName
            // 
            this.ColumnFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnFileName.HeaderText = "FileName";
            this.ColumnFileName.Name = "ColumnFileName";
            this.ColumnFileName.ReadOnly = true;
            // 
            // checkBoxEliminateSubsetSequences
            // 
            this.checkBoxEliminateSubsetSequences.AutoSize = true;
            this.checkBoxEliminateSubsetSequences.Location = new System.Drawing.Point(84, 69);
            this.checkBoxEliminateSubsetSequences.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxEliminateSubsetSequences.Name = "checkBoxEliminateSubsetSequences";
            this.checkBoxEliminateSubsetSequences.Size = new System.Drawing.Size(234, 24);
            this.checkBoxEliminateSubsetSequences.TabIndex = 13;
            this.checkBoxEliminateSubsetSequences.Text = "Eliminate subset sequences";
            this.checkBoxEliminateSubsetSequences.UseVisualStyleBackColor = true;
            this.checkBoxEliminateSubsetSequences.CheckedChanged += new System.EventHandler(this.checkBoxEliminateSubsetSequences_CheckedChanged);
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.groupBoxOutput);
            this.groupBoxInput.Controls.Add(this.groupBox1);
            this.groupBoxInput.Controls.Add(this.groupBoxInputDB);
            this.groupBoxInput.Controls.Add(this.buttonGO);
            this.groupBoxInput.Controls.Add(this.groupBox2);
            this.groupBoxInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxInput.Location = new System.Drawing.Point(0, 35);
            this.groupBoxInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxInput.Size = new System.Drawing.Size(978, 652);
            this.groupBoxInput.TabIndex = 24;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input";
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.labelIdentity);
            this.groupBoxOutput.Controls.Add(this.numericUpDownIdentity);
            this.groupBoxOutput.Controls.Add(this.checkBoxEliminateSubsetSequences);
            this.groupBoxOutput.Controls.Add(this.textBoxOutput);
            this.groupBoxOutput.Controls.Add(this.buttonSave);
            this.groupBoxOutput.Controls.Add(this.label3);
            this.groupBoxOutput.Location = new System.Drawing.Point(9, 467);
            this.groupBoxOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxOutput.Size = new System.Drawing.Size(918, 111);
            this.groupBoxOutput.TabIndex = 24;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output";
            // 
            // labelIdentity
            // 
            this.labelIdentity.AutoSize = true;
            this.labelIdentity.Enabled = false;
            this.labelIdentity.Location = new System.Drawing.Point(352, 71);
            this.labelIdentity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIdentity.Name = "labelIdentity";
            this.labelIdentity.Size = new System.Drawing.Size(228, 20);
            this.labelIdentity.TabIndex = 15;
            this.labelIdentity.Text = "Identity to coalesce sequences";
            // 
            // numericUpDownIdentity
            // 
            this.numericUpDownIdentity.DecimalPlaces = 1;
            this.numericUpDownIdentity.Enabled = false;
            this.numericUpDownIdentity.Location = new System.Drawing.Point(594, 68);
            this.numericUpDownIdentity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownIdentity.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownIdentity.Name = "numericUpDownIdentity";
            this.numericUpDownIdentity.Size = new System.Drawing.Size(99, 26);
            this.numericUpDownIdentity.TabIndex = 14;
            this.numericUpDownIdentity.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // radioButtonNextProt
            // 
            this.radioButtonNextProt.AutoSize = true;
            this.radioButtonNextProt.Location = new System.Drawing.Point(26, 130);
            this.radioButtonNextProt.Name = "radioButtonNextProt";
            this.radioButtonNextProt.Size = new System.Drawing.Size(99, 24);
            this.radioButtonNextProt.TabIndex = 4;
            this.radioButtonNextProt.TabStop = true;
            this.radioButtonNextProt.Text = "NeXtProt";
            this.radioButtonNextProt.UseVisualStyleBackColor = true;
            // 
            // DBPrepareControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DBPrepareControl";
            this.Size = new System.Drawing.Size(978, 717);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxInputDB.ResumeLayout(false);
            this.groupBoxInputDB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputDBs)).EndInit();
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIdentity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNameRegex;
        private System.Windows.Forms.Button buttonGO;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox checkBoxInsertContaminants;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonDBIPI;
        private System.Windows.Forms.RadioButton radioButtonDBNCBI;
        private System.Windows.Forms.RadioButton radioButtonDBGeneric;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxInputDB;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dataGridViewInputDBs;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFileName;
        private System.Windows.Forms.CheckBox checkBoxEliminateSubsetSequences;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSequencesRemoved;
        private System.Windows.Forms.RadioButton radioButtonTRoT;
        private System.Windows.Forms.RadioButton radioButtonT;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.RadioButton radioButtonTPRMR;
        private System.Windows.Forms.ComboBox comboBoxEnzyme;
        private System.Windows.Forms.RadioButton radioButtonDBUniprot;
        private System.Windows.Forms.ToolStripMenuItem utilsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanDBRemveSequencesFollowingARegexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nucleotideToProteinToolStripMenuItem;
        private System.Windows.Forms.Label labelIdentity;
        private System.Windows.Forms.NumericUpDown numericUpDownIdentity;
        private System.Windows.Forms.RadioButton radioButtonNextProt;
    }
}

