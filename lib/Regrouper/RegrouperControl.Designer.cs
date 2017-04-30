namespace Regrouper
{
    partial class RegrouperControl
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainerInputDirectory = new System.Windows.Forms.SplitContainer();
            this.multipleDirectorySelector1 = new PatternTools.MultipleDirectorySelector();
            this.textBoxProjectDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxEliminateDecoys = new System.Windows.Forms.CheckBox();
            this.textBoxDecoyTag = new System.Windows.Forms.TextBox();
            this.buttonLoad2 = new System.Windows.Forms.Button();
            this.buttonGenerateSummary = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.tabControlRegrouper = new System.Windows.Forms.TabControl();
            this.tabPageInput = new System.Windows.Forms.TabPage();
            this.tabPageSpectralCountAnalysis = new System.Windows.Forms.TabPage();
            this.splitContainerSpecCount = new System.Windows.Forms.SplitContainer();
            this.buttonSaveBirdsEyeView = new System.Windows.Forms.Button();
            this.buttonSavePatternLabProject = new System.Windows.Forms.Button();
            this.groupBoxFormat = new System.Windows.Forms.GroupBox();
            this.groupBoxFilteringSoftware = new System.Windows.Forms.GroupBox();
            this.radioButtonSEPro = new System.Windows.Forms.RadioButton();
            this.radioButtonPepExplorer = new System.Windows.Forms.RadioButton();
            this.checkBoxUseMaxParsimony = new System.Windows.Forms.CheckBox();
            this.checkBoxNSAF = new System.Windows.Forms.CheckBox();
            this.comboBoxProteinContent = new System.Windows.Forms.ComboBox();
            this.radioButtonFormatPeptide = new System.Windows.Forms.RadioButton();
            this.radioButtonFormatUniquePeptide = new System.Windows.Forms.RadioButton();
            this.radioButtonFormatProtein = new System.Windows.Forms.RadioButton();
            this.buttonGo = new System.Windows.Forms.Button();
            this.groupBoxBirdsEyeView = new System.Windows.Forms.GroupBox();
            this.dataGridViewBirdsEyeView = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPageDominomics = new System.Windows.Forms.TabPage();
            this.groupBoxDominomics = new System.Windows.Forms.GroupBox();
            this.buttonDominomicsSavePatternLabProject = new System.Windows.Forms.Button();
            this.textBoxSavePLP = new System.Windows.Forms.TextBox();
            this.radioButtonProjectTypePepExplorer = new System.Windows.Forms.RadioButton();
            this.radioButtonProjectTypeSEPro = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxUnmappedDomains = new System.Windows.Forms.TextBox();
            this.buttonUnmappedDomains = new System.Windows.Forms.Button();
            this.numericUpDownDomainIEValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDomainEvalue = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label1DEValue = new System.Windows.Forms.Label();
            this.labelDominomicsStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonPredictDomainsWithFioCloud = new System.Windows.Forms.Button();
            this.tabPageSEProQXIC = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonProcessXIC = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorkerDominomics = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersXQ = new XQuant.Parameters();
            this.parametersXQuant = new XQuant.Parameters();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInputDirectory)).BeginInit();
            this.splitContainerInputDirectory.Panel1.SuspendLayout();
            this.splitContainerInputDirectory.Panel2.SuspendLayout();
            this.splitContainerInputDirectory.SuspendLayout();
            this.tabControlRegrouper.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabPageSpectralCountAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSpecCount)).BeginInit();
            this.splitContainerSpecCount.Panel1.SuspendLayout();
            this.splitContainerSpecCount.Panel2.SuspendLayout();
            this.splitContainerSpecCount.SuspendLayout();
            this.groupBoxFormat.SuspendLayout();
            this.groupBoxFilteringSoftware.SuspendLayout();
            this.groupBoxBirdsEyeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBirdsEyeView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabPageDominomics.SuspendLayout();
            this.groupBoxDominomics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDomainIEValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDomainEvalue)).BeginInit();
            this.tabPageSEProQXIC.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.splitContainerInputDirectory);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(4, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(1186, 645);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SEProcessor Results - Project organizer will search for all sub-directories withi" +
    "n the pointed directory for .sepr files";
            // 
            // splitContainerInputDirectory
            // 
            this.splitContainerInputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerInputDirectory.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerInputDirectory.Location = new System.Drawing.Point(4, 24);
            this.splitContainerInputDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainerInputDirectory.Name = "splitContainerInputDirectory";
            this.splitContainerInputDirectory.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerInputDirectory.Panel1
            // 
            this.splitContainerInputDirectory.Panel1.Controls.Add(this.multipleDirectorySelector1);
            // 
            // splitContainerInputDirectory.Panel2
            // 
            this.splitContainerInputDirectory.Panel2.Controls.Add(this.textBoxProjectDescription);
            this.splitContainerInputDirectory.Panel2.Controls.Add(this.label1);
            this.splitContainerInputDirectory.Panel2.Controls.Add(this.checkBoxEliminateDecoys);
            this.splitContainerInputDirectory.Panel2.Controls.Add(this.textBoxDecoyTag);
            this.splitContainerInputDirectory.Panel2.Controls.Add(this.buttonLoad2);
            this.splitContainerInputDirectory.Panel2.Controls.Add(this.buttonGenerateSummary);
            this.splitContainerInputDirectory.Panel2.Controls.Add(this.label3);
            this.splitContainerInputDirectory.Size = new System.Drawing.Size(1178, 616);
            this.splitContainerInputDirectory.SplitterDistance = 504;
            this.splitContainerInputDirectory.SplitterWidth = 6;
            this.splitContainerInputDirectory.TabIndex = 14;
            // 
            // multipleDirectorySelector1
            // 
            this.multipleDirectorySelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multipleDirectorySelector1.Location = new System.Drawing.Point(0, 0);
            this.multipleDirectorySelector1.Margin = new System.Windows.Forms.Padding(6);
            this.multipleDirectorySelector1.Name = "multipleDirectorySelector1";
            this.multipleDirectorySelector1.Size = new System.Drawing.Size(1178, 504);
            this.multipleDirectorySelector1.TabIndex = 0;
            this.multipleDirectorySelector1.VerifyExtension = null;
            // 
            // textBoxProjectDescription
            // 
            this.textBoxProjectDescription.Location = new System.Drawing.Point(163, 26);
            this.textBoxProjectDescription.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxProjectDescription.Name = "textBoxProjectDescription";
            this.textBoxProjectDescription.Size = new System.Drawing.Size(949, 26);
            this.textBoxProjectDescription.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Project Description";
            // 
            // checkBoxEliminateDecoys
            // 
            this.checkBoxEliminateDecoys.AutoSize = true;
            this.checkBoxEliminateDecoys.Location = new System.Drawing.Point(4, 82);
            this.checkBoxEliminateDecoys.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxEliminateDecoys.Name = "checkBoxEliminateDecoys";
            this.checkBoxEliminateDecoys.Size = new System.Drawing.Size(154, 24);
            this.checkBoxEliminateDecoys.TabIndex = 9;
            this.checkBoxEliminateDecoys.Text = "Eliminate decoys";
            this.checkBoxEliminateDecoys.UseVisualStyleBackColor = true;
            // 
            // textBoxDecoyTag
            // 
            this.textBoxDecoyTag.Location = new System.Drawing.Point(249, 78);
            this.textBoxDecoyTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDecoyTag.Name = "textBoxDecoyTag";
            this.textBoxDecoyTag.Size = new System.Drawing.Size(148, 26);
            this.textBoxDecoyTag.TabIndex = 12;
            this.textBoxDecoyTag.Text = "Reverse";
            // 
            // buttonLoad2
            // 
            this.buttonLoad2.BackColor = System.Drawing.Color.GreenYellow;
            this.buttonLoad2.Location = new System.Drawing.Point(408, 78);
            this.buttonLoad2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonLoad2.Name = "buttonLoad2";
            this.buttonLoad2.Size = new System.Drawing.Size(471, 35);
            this.buttonLoad2.TabIndex = 1;
            this.buttonLoad2.Text = "Load";
            this.buttonLoad2.UseVisualStyleBackColor = false;
            this.buttonLoad2.Click += new System.EventHandler(this.buttonLoad2_Click);
            // 
            // buttonGenerateSummary
            // 
            this.buttonGenerateSummary.Location = new System.Drawing.Point(886, 78);
            this.buttonGenerateSummary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGenerateSummary.Name = "buttonGenerateSummary";
            this.buttonGenerateSummary.Size = new System.Drawing.Size(226, 35);
            this.buttonGenerateSummary.TabIndex = 13;
            this.buttonGenerateSummary.Text = "Generate Summary";
            this.buttonGenerateSummary.UseVisualStyleBackColor = true;
            this.buttonGenerateSummary.Click += new System.EventHandler(this.buttonGenerateSummary_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Decoy tag";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(0, 0);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 0;
            // 
            // tabControlRegrouper
            // 
            this.tabControlRegrouper.Controls.Add(this.tabPageInput);
            this.tabControlRegrouper.Controls.Add(this.tabPageSpectralCountAnalysis);
            this.tabControlRegrouper.Controls.Add(this.tabPageDominomics);
            this.tabControlRegrouper.Controls.Add(this.tabPageSEProQXIC);
            this.tabControlRegrouper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlRegrouper.Location = new System.Drawing.Point(0, 0);
            this.tabControlRegrouper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControlRegrouper.Name = "tabControlRegrouper";
            this.tabControlRegrouper.SelectedIndex = 0;
            this.tabControlRegrouper.Size = new System.Drawing.Size(1202, 688);
            this.tabControlRegrouper.TabIndex = 1;
            // 
            // tabPageInput
            // 
            this.tabPageInput.Controls.Add(this.groupBox2);
            this.tabPageInput.Location = new System.Drawing.Point(4, 29);
            this.tabPageInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageInput.Name = "tabPageInput";
            this.tabPageInput.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageInput.Size = new System.Drawing.Size(1194, 655);
            this.tabPageInput.TabIndex = 0;
            this.tabPageInput.Text = "Step 1 - Input: Project Organizer";
            this.tabPageInput.UseVisualStyleBackColor = true;
            // 
            // tabPageSpectralCountAnalysis
            // 
            this.tabPageSpectralCountAnalysis.Controls.Add(this.splitContainerSpecCount);
            this.tabPageSpectralCountAnalysis.Controls.Add(this.statusStrip1);
            this.tabPageSpectralCountAnalysis.Location = new System.Drawing.Point(4, 29);
            this.tabPageSpectralCountAnalysis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSpectralCountAnalysis.Name = "tabPageSpectralCountAnalysis";
            this.tabPageSpectralCountAnalysis.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSpectralCountAnalysis.Size = new System.Drawing.Size(1194, 655);
            this.tabPageSpectralCountAnalysis.TabIndex = 2;
            this.tabPageSpectralCountAnalysis.Text = "Step 2: Spectral Counting";
            this.tabPageSpectralCountAnalysis.UseVisualStyleBackColor = true;
            // 
            // splitContainerSpecCount
            // 
            this.splitContainerSpecCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSpecCount.Enabled = false;
            this.splitContainerSpecCount.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerSpecCount.Location = new System.Drawing.Point(4, 5);
            this.splitContainerSpecCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainerSpecCount.Name = "splitContainerSpecCount";
            this.splitContainerSpecCount.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerSpecCount.Panel1
            // 
            this.splitContainerSpecCount.Panel1.Controls.Add(this.buttonSaveBirdsEyeView);
            this.splitContainerSpecCount.Panel1.Controls.Add(this.buttonSavePatternLabProject);
            this.splitContainerSpecCount.Panel1.Controls.Add(this.groupBoxFormat);
            this.splitContainerSpecCount.Panel1.Controls.Add(this.buttonGo);
            // 
            // splitContainerSpecCount.Panel2
            // 
            this.splitContainerSpecCount.Panel2.Controls.Add(this.groupBoxBirdsEyeView);
            this.splitContainerSpecCount.Size = new System.Drawing.Size(1186, 615);
            this.splitContainerSpecCount.SplitterDistance = 155;
            this.splitContainerSpecCount.SplitterWidth = 6;
            this.splitContainerSpecCount.TabIndex = 8;
            // 
            // buttonSaveBirdsEyeView
            // 
            this.buttonSaveBirdsEyeView.Enabled = false;
            this.buttonSaveBirdsEyeView.Location = new System.Drawing.Point(918, 129);
            this.buttonSaveBirdsEyeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSaveBirdsEyeView.Name = "buttonSaveBirdsEyeView";
            this.buttonSaveBirdsEyeView.Size = new System.Drawing.Size(219, 42);
            this.buttonSaveBirdsEyeView.TabIndex = 9;
            this.buttonSaveBirdsEyeView.Text = "Save Birds Eye View";
            this.buttonSaveBirdsEyeView.UseVisualStyleBackColor = true;
            this.buttonSaveBirdsEyeView.Click += new System.EventHandler(this.buttonSaveBirdsEyeView_Click);
            // 
            // buttonSavePatternLabProject
            // 
            this.buttonSavePatternLabProject.Enabled = false;
            this.buttonSavePatternLabProject.Location = new System.Drawing.Point(917, 78);
            this.buttonSavePatternLabProject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSavePatternLabProject.Name = "buttonSavePatternLabProject";
            this.buttonSavePatternLabProject.Size = new System.Drawing.Size(219, 42);
            this.buttonSavePatternLabProject.TabIndex = 10;
            this.buttonSavePatternLabProject.Text = "Save PatternLab Project";
            this.buttonSavePatternLabProject.UseVisualStyleBackColor = true;
            this.buttonSavePatternLabProject.Click += new System.EventHandler(this.buttonSavePatternLabProject_Click);
            // 
            // groupBoxFormat
            // 
            this.groupBoxFormat.Controls.Add(this.groupBoxFilteringSoftware);
            this.groupBoxFormat.Controls.Add(this.checkBoxUseMaxParsimony);
            this.groupBoxFormat.Controls.Add(this.checkBoxNSAF);
            this.groupBoxFormat.Controls.Add(this.comboBoxProteinContent);
            this.groupBoxFormat.Controls.Add(this.radioButtonFormatPeptide);
            this.groupBoxFormat.Controls.Add(this.radioButtonFormatUniquePeptide);
            this.groupBoxFormat.Controls.Add(this.radioButtonFormatProtein);
            this.groupBoxFormat.Location = new System.Drawing.Point(4, 14);
            this.groupBoxFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFormat.Name = "groupBoxFormat";
            this.groupBoxFormat.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFormat.Size = new System.Drawing.Size(904, 160);
            this.groupBoxFormat.TabIndex = 4;
            this.groupBoxFormat.TabStop = false;
            this.groupBoxFormat.Text = "Format";
            // 
            // groupBoxFilteringSoftware
            // 
            this.groupBoxFilteringSoftware.Controls.Add(this.radioButtonSEPro);
            this.groupBoxFilteringSoftware.Controls.Add(this.radioButtonPepExplorer);
            this.groupBoxFilteringSoftware.Location = new System.Drawing.Point(660, 21);
            this.groupBoxFilteringSoftware.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFilteringSoftware.Name = "groupBoxFilteringSoftware";
            this.groupBoxFilteringSoftware.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFilteringSoftware.Size = new System.Drawing.Size(201, 109);
            this.groupBoxFilteringSoftware.TabIndex = 17;
            this.groupBoxFilteringSoftware.TabStop = false;
            this.groupBoxFilteringSoftware.Text = "Filtering Software";
            // 
            // radioButtonSEPro
            // 
            this.radioButtonSEPro.AutoSize = true;
            this.radioButtonSEPro.Checked = true;
            this.radioButtonSEPro.Location = new System.Drawing.Point(30, 29);
            this.radioButtonSEPro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonSEPro.Name = "radioButtonSEPro";
            this.radioButtonSEPro.Size = new System.Drawing.Size(80, 24);
            this.radioButtonSEPro.TabIndex = 15;
            this.radioButtonSEPro.TabStop = true;
            this.radioButtonSEPro.Text = "SEPro";
            this.radioButtonSEPro.UseVisualStyleBackColor = true;
            // 
            // radioButtonPepExplorer
            // 
            this.radioButtonPepExplorer.AutoSize = true;
            this.radioButtonPepExplorer.Location = new System.Drawing.Point(30, 65);
            this.radioButtonPepExplorer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonPepExplorer.Name = "radioButtonPepExplorer";
            this.radioButtonPepExplorer.Size = new System.Drawing.Size(120, 24);
            this.radioButtonPepExplorer.TabIndex = 16;
            this.radioButtonPepExplorer.Text = "PepExplorer";
            this.radioButtonPepExplorer.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseMaxParsimony
            // 
            this.checkBoxUseMaxParsimony.AutoSize = true;
            this.checkBoxUseMaxParsimony.Location = new System.Drawing.Point(377, 66);
            this.checkBoxUseMaxParsimony.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxUseMaxParsimony.Name = "checkBoxUseMaxParsimony";
            this.checkBoxUseMaxParsimony.Size = new System.Drawing.Size(167, 24);
            this.checkBoxUseMaxParsimony.TabIndex = 14;
            this.checkBoxUseMaxParsimony.Text = "use MaxParsimony";
            this.checkBoxUseMaxParsimony.UseVisualStyleBackColor = true;
            // 
            // checkBoxNSAF
            // 
            this.checkBoxNSAF.AutoSize = true;
            this.checkBoxNSAF.Location = new System.Drawing.Point(377, 31);
            this.checkBoxNSAF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxNSAF.Name = "checkBoxNSAF";
            this.checkBoxNSAF.Size = new System.Drawing.Size(108, 24);
            this.checkBoxNSAF.TabIndex = 10;
            this.checkBoxNSAF.Text = "use NSAF";
            this.checkBoxNSAF.UseVisualStyleBackColor = true;
            // 
            // comboBoxProteinContent
            // 
            this.comboBoxProteinContent.FormattingEnabled = true;
            this.comboBoxProteinContent.Items.AddRange(new object[] {
            "SpecCountsOfAllPeptides",
            "SpecCountsOfUniquePeptides"});
            this.comboBoxProteinContent.Location = new System.Drawing.Point(105, 28);
            this.comboBoxProteinContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxProteinContent.Name = "comboBoxProteinContent";
            this.comboBoxProteinContent.Size = new System.Drawing.Size(260, 28);
            this.comboBoxProteinContent.TabIndex = 7;
            this.comboBoxProteinContent.Text = "Select Spec Count Mode";
            // 
            // radioButtonFormatPeptide
            // 
            this.radioButtonFormatPeptide.AutoSize = true;
            this.radioButtonFormatPeptide.Location = new System.Drawing.Point(9, 69);
            this.radioButtonFormatPeptide.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonFormatPeptide.Name = "radioButtonFormatPeptide";
            this.radioButtonFormatPeptide.Size = new System.Drawing.Size(96, 24);
            this.radioButtonFormatPeptide.TabIndex = 1;
            this.radioButtonFormatPeptide.Text = "Peptides";
            this.radioButtonFormatPeptide.UseVisualStyleBackColor = true;
            // 
            // radioButtonFormatUniquePeptide
            // 
            this.radioButtonFormatUniquePeptide.AutoSize = true;
            this.radioButtonFormatUniquePeptide.Location = new System.Drawing.Point(9, 105);
            this.radioButtonFormatUniquePeptide.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonFormatUniquePeptide.Name = "radioButtonFormatUniquePeptide";
            this.radioButtonFormatUniquePeptide.Size = new System.Drawing.Size(139, 24);
            this.radioButtonFormatUniquePeptide.TabIndex = 6;
            this.radioButtonFormatUniquePeptide.Text = "UniquePeptide";
            this.radioButtonFormatUniquePeptide.UseVisualStyleBackColor = true;
            // 
            // radioButtonFormatProtein
            // 
            this.radioButtonFormatProtein.AutoSize = true;
            this.radioButtonFormatProtein.Checked = true;
            this.radioButtonFormatProtein.Location = new System.Drawing.Point(9, 29);
            this.radioButtonFormatProtein.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonFormatProtein.Name = "radioButtonFormatProtein";
            this.radioButtonFormatProtein.Size = new System.Drawing.Size(84, 24);
            this.radioButtonFormatProtein.TabIndex = 0;
            this.radioButtonFormatProtein.TabStop = true;
            this.radioButtonFormatProtein.Text = "Protein";
            this.radioButtonFormatProtein.UseVisualStyleBackColor = true;
            this.radioButtonFormatProtein.CheckedChanged += new System.EventHandler(this.radioButtonFormatProtein_CheckedChanged);
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(918, 25);
            this.buttonGo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(219, 42);
            this.buttonGo.TabIndex = 6;
            this.buttonGo.Text = "Go!";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // groupBoxBirdsEyeView
            // 
            this.groupBoxBirdsEyeView.Controls.Add(this.dataGridViewBirdsEyeView);
            this.groupBoxBirdsEyeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxBirdsEyeView.Location = new System.Drawing.Point(0, 0);
            this.groupBoxBirdsEyeView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBirdsEyeView.Name = "groupBoxBirdsEyeView";
            this.groupBoxBirdsEyeView.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBirdsEyeView.Size = new System.Drawing.Size(1186, 454);
            this.groupBoxBirdsEyeView.TabIndex = 1;
            this.groupBoxBirdsEyeView.TabStop = false;
            this.groupBoxBirdsEyeView.Text = "Birds Eye View";
            // 
            // dataGridViewBirdsEyeView
            // 
            this.dataGridViewBirdsEyeView.AllowUserToAddRows = false;
            this.dataGridViewBirdsEyeView.AllowUserToDeleteRows = false;
            this.dataGridViewBirdsEyeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBirdsEyeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBirdsEyeView.Location = new System.Drawing.Point(3, 21);
            this.dataGridViewBirdsEyeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewBirdsEyeView.Name = "dataGridViewBirdsEyeView";
            this.dataGridViewBirdsEyeView.ReadOnly = true;
            this.dataGridViewBirdsEyeView.Size = new System.Drawing.Size(1180, 431);
            this.dataGridViewBirdsEyeView.TabIndex = 0;
            this.dataGridViewBirdsEyeView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewBirdsEyeView_ColumnHeaderMouseClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(4, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1186, 30);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "Idle";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 25);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(41, 25);
            this.toolStripStatusLabel2.Text = "Idle";
            // 
            // tabPageDominomics
            // 
            this.tabPageDominomics.Controls.Add(this.groupBoxDominomics);
            this.tabPageDominomics.Location = new System.Drawing.Point(4, 29);
            this.tabPageDominomics.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageDominomics.Name = "tabPageDominomics";
            this.tabPageDominomics.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageDominomics.Size = new System.Drawing.Size(1194, 655);
            this.tabPageDominomics.TabIndex = 5;
            this.tabPageDominomics.Text = "Step 2: Differential Domain Expression";
            this.tabPageDominomics.UseVisualStyleBackColor = true;
            // 
            // groupBoxDominomics
            // 
            this.groupBoxDominomics.Controls.Add(this.buttonDominomicsSavePatternLabProject);
            this.groupBoxDominomics.Controls.Add(this.textBoxSavePLP);
            this.groupBoxDominomics.Controls.Add(this.radioButtonProjectTypePepExplorer);
            this.groupBoxDominomics.Controls.Add(this.radioButtonProjectTypeSEPro);
            this.groupBoxDominomics.Controls.Add(this.label9);
            this.groupBoxDominomics.Controls.Add(this.textBoxUnmappedDomains);
            this.groupBoxDominomics.Controls.Add(this.buttonUnmappedDomains);
            this.groupBoxDominomics.Controls.Add(this.numericUpDownDomainIEValue);
            this.groupBoxDominomics.Controls.Add(this.numericUpDownDomainEvalue);
            this.groupBoxDominomics.Controls.Add(this.label11);
            this.groupBoxDominomics.Controls.Add(this.label1DEValue);
            this.groupBoxDominomics.Controls.Add(this.labelDominomicsStatus);
            this.groupBoxDominomics.Controls.Add(this.label8);
            this.groupBoxDominomics.Controls.Add(this.progressBar1);
            this.groupBoxDominomics.Controls.Add(this.buttonPredictDomainsWithFioCloud);
            this.groupBoxDominomics.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxDominomics.Enabled = false;
            this.groupBoxDominomics.Location = new System.Drawing.Point(4, 5);
            this.groupBoxDominomics.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDominomics.Name = "groupBoxDominomics";
            this.groupBoxDominomics.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDominomics.Size = new System.Drawing.Size(1186, 375);
            this.groupBoxDominomics.TabIndex = 0;
            this.groupBoxDominomics.TabStop = false;
            this.groupBoxDominomics.Text = "Map spectral counts to domains predicted using FioCloud";
            // 
            // buttonDominomicsSavePatternLabProject
            // 
            this.buttonDominomicsSavePatternLabProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDominomicsSavePatternLabProject.Location = new System.Drawing.Point(960, 135);
            this.buttonDominomicsSavePatternLabProject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDominomicsSavePatternLabProject.Name = "buttonDominomicsSavePatternLabProject";
            this.buttonDominomicsSavePatternLabProject.Size = new System.Drawing.Size(220, 35);
            this.buttonDominomicsSavePatternLabProject.TabIndex = 21;
            this.buttonDominomicsSavePatternLabProject.Text = "Save PatternLab Project";
            this.buttonDominomicsSavePatternLabProject.UseVisualStyleBackColor = true;
            this.buttonDominomicsSavePatternLabProject.Click += new System.EventHandler(this.buttonDominomicsSavePatternLabProject_Click);
            // 
            // textBoxSavePLP
            // 
            this.textBoxSavePLP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSavePLP.Enabled = false;
            this.textBoxSavePLP.Location = new System.Drawing.Point(21, 139);
            this.textBoxSavePLP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxSavePLP.Name = "textBoxSavePLP";
            this.textBoxSavePLP.Size = new System.Drawing.Size(927, 26);
            this.textBoxSavePLP.TabIndex = 20;
            // 
            // radioButtonProjectTypePepExplorer
            // 
            this.radioButtonProjectTypePepExplorer.AutoSize = true;
            this.radioButtonProjectTypePepExplorer.Location = new System.Drawing.Point(273, 40);
            this.radioButtonProjectTypePepExplorer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonProjectTypePepExplorer.Name = "radioButtonProjectTypePepExplorer";
            this.radioButtonProjectTypePepExplorer.Size = new System.Drawing.Size(190, 24);
            this.radioButtonProjectTypePepExplorer.TabIndex = 19;
            this.radioButtonProjectTypePepExplorer.TabStop = true;
            this.radioButtonProjectTypePepExplorer.Text = "de novo (PepExplorer)";
            this.radioButtonProjectTypePepExplorer.UseVisualStyleBackColor = true;
            // 
            // radioButtonProjectTypeSEPro
            // 
            this.radioButtonProjectTypeSEPro.AutoSize = true;
            this.radioButtonProjectTypeSEPro.Checked = true;
            this.radioButtonProjectTypeSEPro.Location = new System.Drawing.Point(134, 40);
            this.radioButtonProjectTypeSEPro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonProjectTypeSEPro.Name = "radioButtonProjectTypeSEPro";
            this.radioButtonProjectTypeSEPro.Size = new System.Drawing.Size(128, 24);
            this.radioButtonProjectTypeSEPro.TabIndex = 18;
            this.radioButtonProjectTypeSEPro.TabStop = true;
            this.radioButtonProjectTypeSEPro.Text = "PSM (SEPro)";
            this.radioButtonProjectTypeSEPro.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 42);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Project type : ";
            // 
            // textBoxUnmappedDomains
            // 
            this.textBoxUnmappedDomains.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUnmappedDomains.Enabled = false;
            this.textBoxUnmappedDomains.Location = new System.Drawing.Point(21, 94);
            this.textBoxUnmappedDomains.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxUnmappedDomains.Name = "textBoxUnmappedDomains";
            this.textBoxUnmappedDomains.Size = new System.Drawing.Size(927, 26);
            this.textBoxUnmappedDomains.TabIndex = 16;
            // 
            // buttonUnmappedDomains
            // 
            this.buttonUnmappedDomains.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUnmappedDomains.Location = new System.Drawing.Point(956, 89);
            this.buttonUnmappedDomains.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonUnmappedDomains.Name = "buttonUnmappedDomains";
            this.buttonUnmappedDomains.Size = new System.Drawing.Size(220, 35);
            this.buttonUnmappedDomains.TabIndex = 14;
            this.buttonUnmappedDomains.Text = "Save Unmapped Domains";
            this.buttonUnmappedDomains.UseVisualStyleBackColor = true;
            this.buttonUnmappedDomains.Click += new System.EventHandler(this.buttonUnmappedDomains_Click);
            // 
            // numericUpDownDomainIEValue
            // 
            this.numericUpDownDomainIEValue.Location = new System.Drawing.Point(332, 195);
            this.numericUpDownDomainIEValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownDomainIEValue.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownDomainIEValue.Name = "numericUpDownDomainIEValue";
            this.numericUpDownDomainIEValue.Size = new System.Drawing.Size(72, 26);
            this.numericUpDownDomainIEValue.TabIndex = 13;
            this.numericUpDownDomainIEValue.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numericUpDownDomainEvalue
            // 
            this.numericUpDownDomainEvalue.Location = new System.Drawing.Point(134, 195);
            this.numericUpDownDomainEvalue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownDomainEvalue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDomainEvalue.Name = "numericUpDownDomainEvalue";
            this.numericUpDownDomainEvalue.Size = new System.Drawing.Size(72, 26);
            this.numericUpDownDomainEvalue.TabIndex = 12;
            this.numericUpDownDomainEvalue.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(215, 199);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 20);
            this.label11.TabIndex = 11;
            this.label11.Text = "IEValue: 10E-";
            // 
            // label1DEValue
            // 
            this.label1DEValue.AutoSize = true;
            this.label1DEValue.Location = new System.Drawing.Point(17, 199);
            this.label1DEValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1DEValue.Name = "label1DEValue";
            this.label1DEValue.Size = new System.Drawing.Size(108, 20);
            this.label1DEValue.TabIndex = 10;
            this.label1DEValue.Text = "E-Value: 10E-";
            // 
            // labelDominomicsStatus
            // 
            this.labelDominomicsStatus.AutoSize = true;
            this.labelDominomicsStatus.Location = new System.Drawing.Point(78, 320);
            this.labelDominomicsStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDominomicsStatus.Name = "labelDominomicsStatus";
            this.labelDominomicsStatus.Size = new System.Drawing.Size(35, 20);
            this.labelDominomicsStatus.TabIndex = 5;
            this.labelDominomicsStatus.Text = "Idle";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 320);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "Status: ";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(4, 280);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1172, 35);
            this.progressBar1.TabIndex = 3;
            // 
            // buttonPredictDomainsWithFioCloud
            // 
            this.buttonPredictDomainsWithFioCloud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPredictDomainsWithFioCloud.Location = new System.Drawing.Point(4, 235);
            this.buttonPredictDomainsWithFioCloud.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonPredictDomainsWithFioCloud.Name = "buttonPredictDomainsWithFioCloud";
            this.buttonPredictDomainsWithFioCloud.Size = new System.Drawing.Size(1172, 35);
            this.buttonPredictDomainsWithFioCloud.TabIndex = 0;
            this.buttonPredictDomainsWithFioCloud.Text = "Predict Domains With FioCloud and write the index and sparse matrix files";
            this.buttonPredictDomainsWithFioCloud.UseVisualStyleBackColor = true;
            this.buttonPredictDomainsWithFioCloud.Click += new System.EventHandler(this.buttonPredictDomainsWithFioCloud_Click);
            // 
            // tabPageSEProQXIC
            // 
            this.tabPageSEProQXIC.Controls.Add(this.label2);
            this.tabPageSEProQXIC.Controls.Add(this.parametersXQ);
            this.tabPageSEProQXIC.Controls.Add(this.buttonProcessXIC);
            this.tabPageSEProQXIC.Location = new System.Drawing.Point(4, 29);
            this.tabPageSEProQXIC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSEProQXIC.Name = "tabPageSEProQXIC";
            this.tabPageSEProQXIC.Size = new System.Drawing.Size(1194, 655);
            this.tabPageSEProQXIC.TabIndex = 3;
            this.tabPageSEProQXIC.Text = "Step 2: XIC Analysis";
            this.tabPageSEProQXIC.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(914, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Important: All Thermo .raw files must be in the directory with the corresponding " +
    "SEPro Files (and MSFileReader properly installed)";
            // 
            // buttonProcessXIC
            // 
            this.buttonProcessXIC.Enabled = false;
            this.buttonProcessXIC.Location = new System.Drawing.Point(14, 158);
            this.buttonProcessXIC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonProcessXIC.Name = "buttonProcessXIC";
            this.buttonProcessXIC.Size = new System.Drawing.Size(549, 35);
            this.buttonProcessXIC.TabIndex = 0;
            this.buttonProcessXIC.Text = "Process XIC";
            this.buttonProcessXIC.UseVisualStyleBackColor = true;
            this.buttonProcessXIC.Click += new System.EventHandler(this.buttonSEProQ2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // backgroundWorkerDominomics
            // 
            this.backgroundWorkerDominomics.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDominomics_DoWork);
            this.backgroundWorkerDominomics.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDominomics_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // parametersXQ
            // 
            this.parametersXQ.Location = new System.Drawing.Point(14, 46);
            this.parametersXQ.Margin = new System.Windows.Forms.Padding(6);
            this.parametersXQ.Name = "parametersXQ";
            this.parametersXQ.Size = new System.Drawing.Size(549, 101);
            this.parametersXQ.TabIndex = 2;
            // 
            // parametersXQuant
            // 
            this.parametersXQuant.Enabled = false;
            this.parametersXQuant.Location = new System.Drawing.Point(3, 3);
            this.parametersXQuant.Margin = new System.Windows.Forms.Padding(4);
            this.parametersXQuant.Name = "parametersXQuant";
            this.parametersXQuant.Size = new System.Drawing.Size(440, 174);
            this.parametersXQuant.TabIndex = 2;
            // 
            // RegrouperControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlRegrouper);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RegrouperControl";
            this.Size = new System.Drawing.Size(1202, 688);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.splitContainerInputDirectory.Panel1.ResumeLayout(false);
            this.splitContainerInputDirectory.Panel2.ResumeLayout(false);
            this.splitContainerInputDirectory.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInputDirectory)).EndInit();
            this.splitContainerInputDirectory.ResumeLayout(false);
            this.tabControlRegrouper.ResumeLayout(false);
            this.tabPageInput.ResumeLayout(false);
            this.tabPageSpectralCountAnalysis.ResumeLayout(false);
            this.tabPageSpectralCountAnalysis.PerformLayout();
            this.splitContainerSpecCount.Panel1.ResumeLayout(false);
            this.splitContainerSpecCount.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSpecCount)).EndInit();
            this.splitContainerSpecCount.ResumeLayout(false);
            this.groupBoxFormat.ResumeLayout(false);
            this.groupBoxFormat.PerformLayout();
            this.groupBoxFilteringSoftware.ResumeLayout(false);
            this.groupBoxFilteringSoftware.PerformLayout();
            this.groupBoxBirdsEyeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBirdsEyeView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPageDominomics.ResumeLayout(false);
            this.groupBoxDominomics.ResumeLayout(false);
            this.groupBoxDominomics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDomainIEValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDomainEvalue)).EndInit();
            this.tabPageSEProQXIC.ResumeLayout(false);
            this.tabPageSEProQXIC.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private PatternTools.MultipleDirectorySelector multipleDirectorySelector1;
        private System.Windows.Forms.TabControl tabControlRegrouper;
        private System.Windows.Forms.TabPage tabPageInput;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.CheckBox checkBoxEliminateDecoys;
        private System.Windows.Forms.TabPage tabPageSpectralCountAnalysis;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.GroupBox groupBoxFormat;
        private System.Windows.Forms.RadioButton radioButtonFormatPeptide;
        private System.Windows.Forms.RadioButton radioButtonFormatProtein;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RadioButton radioButtonFormatUniquePeptide;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ComboBox comboBoxProteinContent;
        private System.Windows.Forms.TextBox textBoxDecoyTag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPageSEProQXIC;
        private System.Windows.Forms.TabPage tabPageDominomics;
        private System.Windows.Forms.GroupBox groupBoxDominomics;
        private System.Windows.Forms.Label labelDominomicsStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonPredictDomainsWithFioCloud;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDominomics;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.NumericUpDown numericUpDownDomainIEValue;
        private System.Windows.Forms.NumericUpDown numericUpDownDomainEvalue;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1DEValue;
        private System.Windows.Forms.TextBox textBoxUnmappedDomains;
        private System.Windows.Forms.Button buttonUnmappedDomains;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonProjectTypePepExplorer;
        private System.Windows.Forms.RadioButton radioButtonProjectTypeSEPro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonLoad2;
        private System.Windows.Forms.Button buttonGenerateSummary;
        private System.Windows.Forms.Button buttonProcessXIC;
        private System.Windows.Forms.CheckBox checkBoxNSAF;
        private XQuant.Parameters parametersXQuant;
        private System.Windows.Forms.CheckBox checkBoxUseMaxParsimony;
        private System.Windows.Forms.SplitContainer splitContainerInputDirectory;
        private System.Windows.Forms.RadioButton radioButtonPepExplorer;
        private System.Windows.Forms.RadioButton radioButtonSEPro;
        private System.Windows.Forms.GroupBox groupBoxFilteringSoftware;
        private System.Windows.Forms.SplitContainer splitContainerSpecCount;
        private System.Windows.Forms.DataGridView dataGridViewBirdsEyeView;
        private System.Windows.Forms.Button buttonSaveBirdsEyeView;
        private XQuant.Parameters parametersXQ;
        private System.Windows.Forms.Button buttonSavePatternLabProject;
        private System.Windows.Forms.TextBox textBoxProjectDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxBirdsEyeView;
        private System.Windows.Forms.TextBox textBoxSavePLP;
        private System.Windows.Forms.Button buttonDominomicsSavePatternLabProject;
        private System.Windows.Forms.Label label2;
    }
}

