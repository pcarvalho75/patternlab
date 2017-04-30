/*
 * Created by SharpDevelop.
 * User: paulo
 * Date: 12/23/2006
 * Time: 12:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using msmsCluster.Properties;
using System.Resources;
using System.Reflection;
using System.Threading;



namespace msmsCluster
{

    partial class MainForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        //The parser will store the structure of all the files in RAM
        msmsParser parser = new msmsParser();
        msmsCluster cluster;
        sparseMatrix sMatrix = new sparseMatrix();
        bool[] acceptableCharges = new bool[100];
        System.ComponentModel.ComponentResourceManager myResources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        /// 

        private void InitializeFormVariables()
        {
            this.toolStripStatusLabel2.Text = "Waiting for work....";
            this.masterDirectoryTB.Text = "D:\\brims\\BRIMS_DTA\\quickTests";
            this.clusterMarginTB.Text = "0.1";
            for (int i = 0; i < acceptableCharges.Length; i++)
            {
                acceptableCharges[i] = false;
            }
            //Capture the values for the three existing buttons
            setValueForacceptableCharges1();
            setValueForacceptableCharges2();
            setValueForacceptableCharges3();

            //The save dialog defaults
            this.saveFileDialog1.DefaultExt = ".txt";
            this.saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            this.saveFileDialog1.FilterIndex = 1;
            this.saveFileDialog1.ShowHelp = false;

            this.comboBoxNormalization.SelectedIndex = 0;
            this.comboBoxFeatureSelection.SelectedIndex = 0;

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.loadB = new System.Windows.Forms.Button();
            this.masterDirectoryTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clusterMarginTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.clusterB = new System.Windows.Forms.Button();
            this.bWriteIndex = new System.Windows.Forms.Button();
            this.buttonWriteSparseMatrix = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.utilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dTA2MS2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.matrixNormalizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.featureSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fSVMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.golubsIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sVMRFEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressTB = new System.Windows.Forms.RichTextBox();
            this.buttonSaveLog = new System.Windows.Forms.Button();
            this.cBIonCharge1 = new System.Windows.Forms.CheckBox();
            this.ionCharge = new System.Windows.Forms.GroupBox();
            this.cBIonCharge3 = new System.Windows.Forms.CheckBox();
            this.cBIonCharge2 = new System.Windows.Forms.CheckBox();
            this.textBoxMIC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxClusteringStep1 = new System.Windows.Forms.GroupBox();
            this.buttonClearMemory = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonMS2 = new System.Windows.Forms.RadioButton();
            this.radioButtonDTA = new System.Windows.Forms.RadioButton();
            this.ButtonOpenFolderBrowserDialog1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonPrecursor = new System.Windows.Forms.RadioButton();
            this.radioButtonFullMSMS = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxClusteringStep3 = new System.Windows.Forms.GroupBox();
            this.pictureBoxExclamationMark2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxExclamationMark1 = new System.Windows.Forms.PictureBox();
            this.groupBoxClusteringStep2 = new System.Windows.Forms.GroupBox();
            this.tabPageNormalization = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonStopStep3 = new System.Windows.Forms.Button();
            this.comboBoxFeatureSelection = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonContinueStep3 = new System.Windows.Forms.Button();
            this.buttonStartStep3 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonApplyStep2 = new System.Windows.Forms.Button();
            this.comboBoxNormalization = new System.Windows.Forms.ComboBox();
            this.groupBoxPFstep1 = new System.Windows.Forms.GroupBox();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.buttonOutputDirectoryStep1 = new System.Windows.Forms.Button();
            this.textBoxBrowseSparseMatrix = new System.Windows.Forms.TextBox();
            this.textBoxBrowseIndex = new System.Windows.Forms.TextBox();
            this.buttonBrowseSparseMatrix = new System.Windows.Forms.Button();
            this.buttonBrowseIndex = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBoxResults = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ionCharge.SuspendLayout();
            this.groupBoxClusteringStep1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxClusteringStep3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExclamationMark2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExclamationMark1)).BeginInit();
            this.groupBoxClusteringStep2.SuspendLayout();
            this.tabPageNormalization.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxPFstep1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Directory";
            // 
            // loadB
            // 
            this.loadB.Location = new System.Drawing.Point(214, 210);
            this.loadB.Name = "loadB";
            this.loadB.Size = new System.Drawing.Size(150, 22);
            this.loadB.TabIndex = 2;
            this.loadB.Text = "Load to Memory";
            this.loadB.UseVisualStyleBackColor = true;
            this.loadB.Click += new System.EventHandler(this.WorkBClick);
            // 
            // masterDirectoryTB
            // 
            this.masterDirectoryTB.Location = new System.Drawing.Point(79, 27);
            this.masterDirectoryTB.MaxLength = 150;
            this.masterDirectoryTB.Name = "masterDirectoryTB";
            this.masterDirectoryTB.Size = new System.Drawing.Size(162, 22);
            this.masterDirectoryTB.TabIndex = 3;
            this.masterDirectoryTB.Text = "masterDirectory";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cluster Margin";
            // 
            // clusterMarginTB
            // 
            this.clusterMarginTB.Location = new System.Drawing.Point(108, 21);
            this.clusterMarginTB.MaxLength = 5;
            this.clusterMarginTB.Name = "clusterMarginTB";
            this.clusterMarginTB.Size = new System.Drawing.Size(89, 22);
            this.clusterMarginTB.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(257, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "PatternLab for Proteomics";
            // 
            // clusterB
            // 
            this.clusterB.Location = new System.Drawing.Point(214, 21);
            this.clusterB.Name = "clusterB";
            this.clusterB.Size = new System.Drawing.Size(156, 23);
            this.clusterB.TabIndex = 11;
            this.clusterB.Text = "Cluster";
            this.clusterB.UseVisualStyleBackColor = true;
            this.clusterB.Click += new System.EventHandler(this.ClusterBClick);
            // 
            // bWriteIndex
            // 
            this.bWriteIndex.Location = new System.Drawing.Point(44, 21);
            this.bWriteIndex.Name = "bWriteIndex";
            this.bWriteIndex.Size = new System.Drawing.Size(140, 23);
            this.bWriteIndex.TabIndex = 12;
            this.bWriteIndex.Text = "Save Index";
            this.bWriteIndex.UseVisualStyleBackColor = true;
            this.bWriteIndex.Click += new System.EventHandler(this.BSaveClusterClick);
            // 
            // buttonWriteSparseMatrix
            // 
            this.buttonWriteSparseMatrix.Location = new System.Drawing.Point(44, 54);
            this.buttonWriteSparseMatrix.Name = "buttonWriteSparseMatrix";
            this.buttonWriteSparseMatrix.Size = new System.Drawing.Size(140, 23);
            this.buttonWriteSparseMatrix.TabIndex = 13;
            this.buttonWriteSparseMatrix.Text = "Save Sparse Matrix";
            this.buttonWriteSparseMatrix.UseVisualStyleBackColor = true;
            this.buttonWriteSparseMatrix.Click += new System.EventHandler(this.BSparseMatrixClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 509);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(750, 23);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 18);
            this.toolStripStatusLabel1.Text = "Status: ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(141, 18);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.utilsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(750, 26);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(99, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // utilsToolStripMenuItem
            // 
            this.utilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conversionToolStripMenuItem,
            this.matrixNormalizationToolStripMenuItem,
            this.featureSelectionToolStripMenuItem});
            this.utilsToolStripMenuItem.Name = "utilsToolStripMenuItem";
            this.utilsToolStripMenuItem.Size = new System.Drawing.Size(46, 22);
            this.utilsToolStripMenuItem.Text = "Utils";
            // 
            // conversionToolStripMenuItem
            // 
            this.conversionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dTA2MS2ToolStripMenuItem1});
            this.conversionToolStripMenuItem.Name = "conversionToolStripMenuItem";
            this.conversionToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.conversionToolStripMenuItem.Text = "Conversion";
            // 
            // dTA2MS2ToolStripMenuItem1
            // 
            this.dTA2MS2ToolStripMenuItem1.Name = "dTA2MS2ToolStripMenuItem1";
            this.dTA2MS2ToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.dTA2MS2ToolStripMenuItem1.Text = "DTA2MS2";
            this.dTA2MS2ToolStripMenuItem1.Click += new System.EventHandler(this.dTA2MS2ToolStripMenuItem1_Click);
            // 
            // matrixNormalizationToolStripMenuItem
            // 
            this.matrixNormalizationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalToolStripMenuItem,
            this.lnToolStripMenuItem,
            this.zToolStripMenuItem});
            this.matrixNormalizationToolStripMenuItem.Name = "matrixNormalizationToolStripMenuItem";
            this.matrixNormalizationToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.matrixNormalizationToolStripMenuItem.Text = "Matrix Normalization";
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal";
            // 
            // lnToolStripMenuItem
            // 
            this.lnToolStripMenuItem.Name = "lnToolStripMenuItem";
            this.lnToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.lnToolStripMenuItem.Text = "ln";
            // 
            // zToolStripMenuItem
            // 
            this.zToolStripMenuItem.Name = "zToolStripMenuItem";
            this.zToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.zToolStripMenuItem.Text = "z";
            // 
            // featureSelectionToolStripMenuItem
            // 
            this.featureSelectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fSVMToolStripMenuItem,
            this.golubsIndexToolStripMenuItem,
            this.tTestToolStripMenuItem,
            this.sVMRFEToolStripMenuItem});
            this.featureSelectionToolStripMenuItem.Name = "featureSelectionToolStripMenuItem";
            this.featureSelectionToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.featureSelectionToolStripMenuItem.Text = "Feature Selection";
            // 
            // fSVMToolStripMenuItem
            // 
            this.fSVMToolStripMenuItem.Name = "fSVMToolStripMenuItem";
            this.fSVMToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.fSVMToolStripMenuItem.Text = "F-SVM";
            // 
            // golubsIndexToolStripMenuItem
            // 
            this.golubsIndexToolStripMenuItem.Name = "golubsIndexToolStripMenuItem";
            this.golubsIndexToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.golubsIndexToolStripMenuItem.Text = "Golubs Index";
            // 
            // tTestToolStripMenuItem
            // 
            this.tTestToolStripMenuItem.Name = "tTestToolStripMenuItem";
            this.tTestToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.tTestToolStripMenuItem.Text = "T-Test";
            // 
            // sVMRFEToolStripMenuItem
            // 
            this.sVMRFEToolStripMenuItem.Name = "sVMRFEToolStripMenuItem";
            this.sVMRFEToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.sVMRFEToolStripMenuItem.Text = "SVM-RFE";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.AboutToolStripMenuItem1Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressTB);
            this.groupBox1.Controls.Add(this.buttonSaveLog);
            this.groupBox1.Location = new System.Drawing.Point(441, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 443);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // progressTB
            // 
            this.progressTB.Location = new System.Drawing.Point(6, 21);
            this.progressTB.Name = "progressTB";
            this.progressTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.progressTB.Size = new System.Drawing.Size(285, 386);
            this.progressTB.TabIndex = 13;
            this.progressTB.Text = "";
            this.progressTB.TextChanged += new System.EventHandler(this.ProgressTBTextChanged);
            // 
            // buttonSaveLog
            // 
            this.buttonSaveLog.Location = new System.Drawing.Point(6, 413);
            this.buttonSaveLog.Name = "buttonSaveLog";
            this.buttonSaveLog.Size = new System.Drawing.Size(285, 24);
            this.buttonSaveLog.TabIndex = 11;
            this.buttonSaveLog.Text = "save log";
            this.buttonSaveLog.UseVisualStyleBackColor = true;
            this.buttonSaveLog.Click += new System.EventHandler(this.ButtonSaveLogClick);
            // 
            // cBIonCharge1
            // 
            this.cBIonCharge1.Checked = true;
            this.cBIonCharge1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBIonCharge1.Location = new System.Drawing.Point(11, 21);
            this.cBIonCharge1.Name = "cBIonCharge1";
            this.cBIonCharge1.Size = new System.Drawing.Size(47, 24);
            this.cBIonCharge1.TabIndex = 17;
            this.cBIonCharge1.Text = "+1";
            this.cBIonCharge1.UseVisualStyleBackColor = true;
            this.cBIonCharge1.CheckedChanged += new System.EventHandler(this.CBIonCharge1CheckedChanged);
            // 
            // ionCharge
            // 
            this.ionCharge.Controls.Add(this.cBIonCharge3);
            this.ionCharge.Controls.Add(this.cBIonCharge2);
            this.ionCharge.Controls.Add(this.cBIonCharge1);
            this.ionCharge.Location = new System.Drawing.Point(275, 58);
            this.ionCharge.Name = "ionCharge";
            this.ionCharge.Size = new System.Drawing.Size(89, 112);
            this.ionCharge.TabIndex = 18;
            this.ionCharge.TabStop = false;
            this.ionCharge.Text = "ion charge";
            // 
            // cBIonCharge3
            // 
            this.cBIonCharge3.Location = new System.Drawing.Point(11, 78);
            this.cBIonCharge3.Name = "cBIonCharge3";
            this.cBIonCharge3.Size = new System.Drawing.Size(49, 24);
            this.cBIonCharge3.TabIndex = 19;
            this.cBIonCharge3.Text = "+3";
            this.cBIonCharge3.UseVisualStyleBackColor = true;
            this.cBIonCharge3.CheckedChanged += new System.EventHandler(this.CBIonCharge3CheckedChanged);
            // 
            // cBIonCharge2
            // 
            this.cBIonCharge2.Checked = true;
            this.cBIonCharge2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBIonCharge2.Location = new System.Drawing.Point(11, 48);
            this.cBIonCharge2.Name = "cBIonCharge2";
            this.cBIonCharge2.Size = new System.Drawing.Size(47, 24);
            this.cBIonCharge2.TabIndex = 18;
            this.cBIonCharge2.Text = "+2";
            this.cBIonCharge2.UseVisualStyleBackColor = true;
            this.cBIonCharge2.CheckedChanged += new System.EventHandler(this.CBIonCharge2CheckedChanged);
            // 
            // textBoxMIC
            // 
            this.textBoxMIC.Enabled = false;
            this.textBoxMIC.Location = new System.Drawing.Point(29, 72);
            this.textBoxMIC.Name = "textBoxMIC";
            this.textBoxMIC.Size = new System.Drawing.Size(49, 22);
            this.textBoxMIC.TabIndex = 22;
            this.textBoxMIC.Text = "0.35";
            // 
            // label5
            // 
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(29, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 23);
            this.label5.TabIndex = 23;
            this.label5.Text = "disconsider ions with less than";
            // 
            // label6
            // 
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(87, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 23);
            this.label6.TabIndex = 24;
            this.label6.Text = "of the MIC";
            // 
            // groupBoxClusteringStep1
            // 
            this.groupBoxClusteringStep1.Controls.Add(this.buttonClearMemory);
            this.groupBoxClusteringStep1.Controls.Add(this.groupBox2);
            this.groupBoxClusteringStep1.Controls.Add(this.ButtonOpenFolderBrowserDialog1);
            this.groupBoxClusteringStep1.Controls.Add(this.groupBox3);
            this.groupBoxClusteringStep1.Controls.Add(this.ionCharge);
            this.groupBoxClusteringStep1.Controls.Add(this.loadB);
            this.groupBoxClusteringStep1.Controls.Add(this.label1);
            this.groupBoxClusteringStep1.Controls.Add(this.masterDirectoryTB);
            this.groupBoxClusteringStep1.Location = new System.Drawing.Point(15, 21);
            this.groupBoxClusteringStep1.Name = "groupBoxClusteringStep1";
            this.groupBoxClusteringStep1.Size = new System.Drawing.Size(376, 238);
            this.groupBoxClusteringStep1.TabIndex = 25;
            this.groupBoxClusteringStep1.TabStop = false;
            this.groupBoxClusteringStep1.Text = "Step 1: Load MS/MS Experiments";
            // 
            // buttonClearMemory
            // 
            this.buttonClearMemory.Location = new System.Drawing.Point(214, 181);
            this.buttonClearMemory.Name = "buttonClearMemory";
            this.buttonClearMemory.Size = new System.Drawing.Size(150, 23);
            this.buttonClearMemory.TabIndex = 31;
            this.buttonClearMemory.Text = "Clear Memory";
            this.buttonClearMemory.UseVisualStyleBackColor = true;
            this.buttonClearMemory.Click += new System.EventHandler(this.ButtonClearMemoryClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonMS2);
            this.groupBox2.Controls.Add(this.radioButtonDTA);
            this.groupBox2.Location = new System.Drawing.Point(6, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 53);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Format";
            // 
            // radioButtonMS2
            // 
            this.radioButtonMS2.Checked = true;
            this.radioButtonMS2.Location = new System.Drawing.Point(102, 19);
            this.radioButtonMS2.Name = "radioButtonMS2";
            this.radioButtonMS2.Size = new System.Drawing.Size(71, 24);
            this.radioButtonMS2.TabIndex = 29;
            this.radioButtonMS2.TabStop = true;
            this.radioButtonMS2.Text = "*.ms2";
            this.radioButtonMS2.UseVisualStyleBackColor = true;
            // 
            // radioButtonDTA
            // 
            this.radioButtonDTA.Location = new System.Drawing.Point(25, 19);
            this.radioButtonDTA.Name = "radioButtonDTA";
            this.radioButtonDTA.Size = new System.Drawing.Size(79, 24);
            this.radioButtonDTA.TabIndex = 27;
            this.radioButtonDTA.Text = "*.dta";
            this.radioButtonDTA.UseVisualStyleBackColor = true;
            // 
            // ButtonOpenFolderBrowserDialog1
            // 
            this.ButtonOpenFolderBrowserDialog1.Location = new System.Drawing.Point(275, 27);
            this.ButtonOpenFolderBrowserDialog1.Name = "ButtonOpenFolderBrowserDialog1";
            this.ButtonOpenFolderBrowserDialog1.Size = new System.Drawing.Size(95, 23);
            this.ButtonOpenFolderBrowserDialog1.TabIndex = 26;
            this.ButtonOpenFolderBrowserDialog1.Text = "Browse";
            this.ButtonOpenFolderBrowserDialog1.UseVisualStyleBackColor = true;
            this.ButtonOpenFolderBrowserDialog1.Click += new System.EventHandler(this.ButtonOpenFolderBrowserDialog1Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonPrecursor);
            this.groupBox3.Controls.Add(this.radioButtonFullMSMS);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.textBoxMIC);
            this.groupBox3.Location = new System.Drawing.Point(6, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 115);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MS/MS Extraction";
            // 
            // radioButtonPrecursor
            // 
            this.radioButtonPrecursor.Checked = true;
            this.radioButtonPrecursor.Location = new System.Drawing.Point(115, 22);
            this.radioButtonPrecursor.Name = "radioButtonPrecursor";
            this.radioButtonPrecursor.Size = new System.Drawing.Size(104, 24);
            this.radioButtonPrecursor.TabIndex = 26;
            this.radioButtonPrecursor.TabStop = true;
            this.radioButtonPrecursor.Text = "Precursor";
            this.radioButtonPrecursor.UseVisualStyleBackColor = true;
            this.radioButtonPrecursor.CheckedChanged += new System.EventHandler(this.RadioButtonPrecursorCheckedChanged);
            // 
            // radioButtonFullMSMS
            // 
            this.radioButtonFullMSMS.Location = new System.Drawing.Point(7, 22);
            this.radioButtonFullMSMS.Name = "radioButtonFullMSMS";
            this.radioButtonFullMSMS.Size = new System.Drawing.Size(104, 24);
            this.radioButtonFullMSMS.TabIndex = 25;
            this.radioButtonFullMSMS.Text = "Full MS/MS";
            this.radioButtonFullMSMS.UseVisualStyleBackColor = true;
            this.radioButtonFullMSMS.CheckedChanged += new System.EventHandler(this.RadioButtonFullMSMSCheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageNormalization);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(18, 56);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(417, 443);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxClusteringStep3);
            this.tabPage1.Controls.Add(this.groupBoxClusteringStep2);
            this.tabPage1.Controls.Add(this.groupBoxClusteringStep1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(409, 414);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MS/MS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxClusteringStep3
            // 
            this.groupBoxClusteringStep3.Controls.Add(this.pictureBoxExclamationMark2);
            this.groupBoxClusteringStep3.Controls.Add(this.pictureBoxExclamationMark1);
            this.groupBoxClusteringStep3.Controls.Add(this.bWriteIndex);
            this.groupBoxClusteringStep3.Controls.Add(this.buttonWriteSparseMatrix);
            this.groupBoxClusteringStep3.Enabled = false;
            this.groupBoxClusteringStep3.Location = new System.Drawing.Point(15, 325);
            this.groupBoxClusteringStep3.Name = "groupBoxClusteringStep3";
            this.groupBoxClusteringStep3.Size = new System.Drawing.Size(197, 83);
            this.groupBoxClusteringStep3.TabIndex = 27;
            this.groupBoxClusteringStep3.TabStop = false;
            this.groupBoxClusteringStep3.Text = "Step 3: Produce output";
            // 
            // pictureBoxExclamationMark2
            // 
            this.pictureBoxExclamationMark2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxExclamationMark2.Image")));
            this.pictureBoxExclamationMark2.Location = new System.Drawing.Point(9, 52);
            this.pictureBoxExclamationMark2.Name = "pictureBoxExclamationMark2";
            this.pictureBoxExclamationMark2.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxExclamationMark2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxExclamationMark2.TabIndex = 29;
            this.pictureBoxExclamationMark2.TabStop = false;
            // 
            // pictureBoxExclamationMark1
            // 
            this.pictureBoxExclamationMark1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxExclamationMark1.Image")));
            this.pictureBoxExclamationMark1.Location = new System.Drawing.Point(9, 19);
            this.pictureBoxExclamationMark1.Name = "pictureBoxExclamationMark1";
            this.pictureBoxExclamationMark1.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxExclamationMark1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxExclamationMark1.TabIndex = 28;
            this.pictureBoxExclamationMark1.TabStop = false;
            // 
            // groupBoxClusteringStep2
            // 
            this.groupBoxClusteringStep2.Controls.Add(this.clusterB);
            this.groupBoxClusteringStep2.Controls.Add(this.label3);
            this.groupBoxClusteringStep2.Controls.Add(this.clusterMarginTB);
            this.groupBoxClusteringStep2.Enabled = false;
            this.groupBoxClusteringStep2.Location = new System.Drawing.Point(15, 265);
            this.groupBoxClusteringStep2.Name = "groupBoxClusteringStep2";
            this.groupBoxClusteringStep2.Size = new System.Drawing.Size(376, 54);
            this.groupBoxClusteringStep2.TabIndex = 26;
            this.groupBoxClusteringStep2.TabStop = false;
            this.groupBoxClusteringStep2.Text = "Step 2: Clustering";
            // 
            // tabPageNormalization
            // 
            this.tabPageNormalization.Controls.Add(this.groupBox5);
            this.tabPageNormalization.Controls.Add(this.groupBox4);
            this.tabPageNormalization.Controls.Add(this.groupBoxPFstep1);
            this.tabPageNormalization.Location = new System.Drawing.Point(4, 25);
            this.tabPageNormalization.Name = "tabPageNormalization";
            this.tabPageNormalization.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNormalization.Size = new System.Drawing.Size(409, 414);
            this.tabPageNormalization.TabIndex = 1;
            this.tabPageNormalization.Text = "Pattern Finder";
            this.tabPageNormalization.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonStopStep3);
            this.groupBox5.Controls.Add(this.comboBoxFeatureSelection);
            this.groupBox5.Controls.Add(this.panel1);
            this.groupBox5.Controls.Add(this.buttonContinueStep3);
            this.groupBox5.Controls.Add(this.buttonStartStep3);
            this.groupBox5.Location = new System.Drawing.Point(15, 210);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(373, 198);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Step 3: Feature Selection";
            // 
            // buttonStopStep3
            // 
            this.buttonStopStep3.Enabled = false;
            this.buttonStopStep3.Location = new System.Drawing.Point(138, 159);
            this.buttonStopStep3.Name = "buttonStopStep3";
            this.buttonStopStep3.Size = new System.Drawing.Size(119, 23);
            this.buttonStopStep3.TabIndex = 11;
            this.buttonStopStep3.Text = "Stop";
            this.buttonStopStep3.UseVisualStyleBackColor = true;
            // 
            // comboBoxFeatureSelection
            // 
            this.comboBoxFeatureSelection.FormattingEnabled = true;
            this.comboBoxFeatureSelection.Items.AddRange(new object[] {
            "Feature Selection",
            "F-SVM",
            "Golubs Index",
            "nSVM",
            "SVM-RFE",
            "T-Test"});
            this.comboBoxFeatureSelection.Location = new System.Drawing.Point(11, 21);
            this.comboBoxFeatureSelection.Name = "comboBoxFeatureSelection";
            this.comboBoxFeatureSelection.Size = new System.Drawing.Size(359, 24);
            this.comboBoxFeatureSelection.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(11, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 102);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonContinueStep3
            // 
            this.buttonContinueStep3.Location = new System.Drawing.Point(263, 159);
            this.buttonContinueStep3.Name = "buttonContinueStep3";
            this.buttonContinueStep3.Size = new System.Drawing.Size(107, 23);
            this.buttonContinueStep3.TabIndex = 10;
            this.buttonContinueStep3.Text = "Continue";
            this.buttonContinueStep3.UseVisualStyleBackColor = true;
            // 
            // buttonStartStep3
            // 
            this.buttonStartStep3.Location = new System.Drawing.Point(11, 159);
            this.buttonStartStep3.Name = "buttonStartStep3";
            this.buttonStartStep3.Size = new System.Drawing.Size(121, 23);
            this.buttonStartStep3.TabIndex = 6;
            this.buttonStartStep3.Text = "Start";
            this.buttonStartStep3.UseVisualStyleBackColor = true;
            this.buttonStartStep3.Click += new System.EventHandler(this.buttonStartStep3_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonApplyStep2);
            this.groupBox4.Controls.Add(this.comboBoxNormalization);
            this.groupBox4.Location = new System.Drawing.Point(15, 139);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(373, 65);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Step 2: Normalization and Feature Selection";
            // 
            // buttonApplyStep2
            // 
            this.buttonApplyStep2.Location = new System.Drawing.Point(207, 29);
            this.buttonApplyStep2.Name = "buttonApplyStep2";
            this.buttonApplyStep2.Size = new System.Drawing.Size(159, 23);
            this.buttonApplyStep2.TabIndex = 11;
            this.buttonApplyStep2.Text = "Apply";
            this.buttonApplyStep2.UseVisualStyleBackColor = true;
            // 
            // comboBoxNormalization
            // 
            this.comboBoxNormalization.FormattingEnabled = true;
            this.comboBoxNormalization.Items.AddRange(new object[] {
            "Normalization",
            "Z (Recomended)",
            "Horizontal",
            "ln"});
            this.comboBoxNormalization.Location = new System.Drawing.Point(7, 29);
            this.comboBoxNormalization.Name = "comboBoxNormalization";
            this.comboBoxNormalization.Size = new System.Drawing.Size(194, 24);
            this.comboBoxNormalization.TabIndex = 5;
            // 
            // groupBoxPFstep1
            // 
            this.groupBoxPFstep1.Controls.Add(this.textBoxOutputDirectory);
            this.groupBoxPFstep1.Controls.Add(this.buttonOutputDirectoryStep1);
            this.groupBoxPFstep1.Controls.Add(this.textBoxBrowseSparseMatrix);
            this.groupBoxPFstep1.Controls.Add(this.textBoxBrowseIndex);
            this.groupBoxPFstep1.Controls.Add(this.buttonBrowseSparseMatrix);
            this.groupBoxPFstep1.Controls.Add(this.buttonBrowseIndex);
            this.groupBoxPFstep1.Location = new System.Drawing.Point(15, 21);
            this.groupBoxPFstep1.Name = "groupBoxPFstep1";
            this.groupBoxPFstep1.Size = new System.Drawing.Size(373, 112);
            this.groupBoxPFstep1.TabIndex = 0;
            this.groupBoxPFstep1.TabStop = false;
            this.groupBoxPFstep1.Text = "Step 1: Prepare the input";
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(11, 77);
            this.textBoxOutputDirectory.MaxLength = 1000;
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(190, 22);
            this.textBoxOutputDirectory.TabIndex = 7;
            // 
            // buttonOutputDirectoryStep1
            // 
            this.buttonOutputDirectoryStep1.Location = new System.Drawing.Point(207, 77);
            this.buttonOutputDirectoryStep1.Name = "buttonOutputDirectoryStep1";
            this.buttonOutputDirectoryStep1.Size = new System.Drawing.Size(160, 23);
            this.buttonOutputDirectoryStep1.TabIndex = 6;
            this.buttonOutputDirectoryStep1.Text = "Output Directory";
            this.buttonOutputDirectoryStep1.UseVisualStyleBackColor = true;
            // 
            // textBoxBrowseSparseMatrix
            // 
            this.textBoxBrowseSparseMatrix.Location = new System.Drawing.Point(7, 49);
            this.textBoxBrowseSparseMatrix.MaxLength = 1000;
            this.textBoxBrowseSparseMatrix.Name = "textBoxBrowseSparseMatrix";
            this.textBoxBrowseSparseMatrix.Size = new System.Drawing.Size(194, 22);
            this.textBoxBrowseSparseMatrix.TabIndex = 4;
            // 
            // textBoxBrowseIndex
            // 
            this.textBoxBrowseIndex.Location = new System.Drawing.Point(7, 21);
            this.textBoxBrowseIndex.MaxLength = 1000;
            this.textBoxBrowseIndex.Name = "textBoxBrowseIndex";
            this.textBoxBrowseIndex.Size = new System.Drawing.Size(194, 22);
            this.textBoxBrowseIndex.TabIndex = 3;
            this.textBoxBrowseIndex.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonBrowseSparseMatrix
            // 
            this.buttonBrowseSparseMatrix.Location = new System.Drawing.Point(207, 48);
            this.buttonBrowseSparseMatrix.Name = "buttonBrowseSparseMatrix";
            this.buttonBrowseSparseMatrix.Size = new System.Drawing.Size(160, 23);
            this.buttonBrowseSparseMatrix.TabIndex = 1;
            this.buttonBrowseSparseMatrix.Text = "Browse Sparse Matrix";
            this.buttonBrowseSparseMatrix.UseVisualStyleBackColor = true;
            this.buttonBrowseSparseMatrix.Click += new System.EventHandler(this.buttonBrowseSparseMatrix_Click);
            // 
            // buttonBrowseIndex
            // 
            this.buttonBrowseIndex.Location = new System.Drawing.Point(207, 21);
            this.buttonBrowseIndex.Name = "buttonBrowseIndex";
            this.buttonBrowseIndex.Size = new System.Drawing.Size(160, 23);
            this.buttonBrowseIndex.TabIndex = 0;
            this.buttonBrowseIndex.Text = "Browse Index";
            this.buttonBrowseIndex.UseVisualStyleBackColor = true;
            this.buttonBrowseIndex.Click += new System.EventHandler(this.buttonLoadIndex_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewResults);
            this.tabPage2.Controls.Add(this.richTextBoxResults);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(409, 414);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Result Analizer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBoxResults
            // 
            this.richTextBoxResults.Location = new System.Drawing.Point(18, 32);
            this.richTextBoxResults.Name = "richTextBoxResults";
            this.richTextBoxResults.Size = new System.Drawing.Size(370, 154);
            this.richTextBoxResults.TabIndex = 0;
            this.richTextBoxResults.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(409, 414);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Maestro";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.ShowHelp = true;
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Location = new System.Drawing.Point(18, 211);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.RowTemplate.Height = 24;
            this.dataGridViewResults.Size = new System.Drawing.Size(370, 150);
            this.dataGridViewResults.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 532);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "PatternLab";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ionCharge.ResumeLayout(false);
            this.groupBoxClusteringStep1.ResumeLayout(false);
            this.groupBoxClusteringStep1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBoxClusteringStep3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExclamationMark2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExclamationMark1)).EndInit();
            this.groupBoxClusteringStep2.ResumeLayout(false);
            this.groupBoxClusteringStep2.PerformLayout();
            this.tabPageNormalization.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBoxPFstep1.ResumeLayout(false);
            this.groupBoxPFstep1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.RichTextBox richTextBoxResults;
        private System.Windows.Forms.PictureBox pictureBoxExclamationMark2;
        private System.Windows.Forms.PictureBox pictureBoxExclamationMark1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem utilsToolStripMenuItem;
        private System.Windows.Forms.Button buttonClearMemory;
        private System.Windows.Forms.Button buttonSaveLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonDTA;
        private System.Windows.Forms.RadioButton radioButtonMS2;
        private System.Windows.Forms.RadioButton radioButtonFullMSMS;
        private System.Windows.Forms.RadioButton radioButtonPrecursor;
        private System.Windows.Forms.Button buttonWriteSparseMatrix;
        private System.Windows.Forms.CheckBox cBIonCharge1;
        private System.Windows.Forms.CheckBox cBIonCharge3;
        private System.Windows.Forms.CheckBox cBIonCharge2;
        private System.Windows.Forms.TextBox textBoxMIC;
        private System.Windows.Forms.GroupBox groupBoxClusteringStep1;
        private System.Windows.Forms.GroupBox groupBoxClusteringStep2;
        private System.Windows.Forms.Button ButtonOpenFolderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBoxClusteringStep3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPageNormalization;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox ionCharge;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button bWriteIndex;
        private System.Windows.Forms.Button loadB;
        private System.Windows.Forms.Button clusterB;
        private System.Windows.Forms.RichTextBox progressTB;
        private System.Windows.Forms.TextBox clusterMarginTB;
        private System.Windows.Forms.TextBox masterDirectoryTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;



        void WorkBClick(object sender, System.EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "Working.....";
            this.statusStrip1.Update();

            try
            {
                DirectoryInfo di = new DirectoryInfo(this.masterDirectoryTB.Text);
                DirectoryInfo[] directories = di.GetDirectories();

                //Update Log
                this.progressTB.AppendText("Loading MS info to RAM\r\n");
                this.progressTB.AppendText("..Load only precursor info = " + this.radioButtonPrecursor.Checked + "\r\n");

                //Chose if we will parse DTA or MS2
                if (this.radioButtonDTA.Checked)
                {
                    foreach (DirectoryInfo dinfo in directories)
                    {
                        this.progressTB.AppendText("..Processing " + dinfo.Name + "\r\n");
                        this.progressTB.Update();
                        parser.parseDtaDir(dinfo.FullName, acceptableCharges);
                    }
                }
                else
                { //We will Load MS2 files
                    this.progressTB.Text += "..Processing " + di.FullName + "\r\n";
                    FileInfo[] Files = di.GetFiles("*.ms2");
                    this.progressTB.AppendText("..Loading " + Files.Length + " file(s)\r\n");
                    this.progressTB.Update();
                    parser.parseMS2Dir(di.FullName, acceptableCharges);
                }

                //Dump some info to the Log
                this.progressTB.AppendText("Experiments Loaded = " + parser.MsExperiments.Count + "\r\n");
                foreach (msRuns run in parser.MsExperiments)
                {
                    this.progressTB.AppendText(".." + run.RunName + " (" + run.Scans.Count + " scans)" + "\r\n");
                }

                this.toolStripStatusLabel2.Text = "Waiting for work.....";
                this.progressTB.AppendText("Idle\r\n");
                this.groupBoxClusteringStep2.Enabled = true;

            }
            catch
            {
                MessageBox.Show("Some error happened! ", "Error!");
            }

        }

        void ClusterBClick(object sender, System.EventArgs e)
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripStatusLabel2.Text = "Clustering....";
            this.progressTB.Text += "Clustering " + parser.MsExperiments.Count + " experiments\r\n";
            this.progressTB.Update();
            this.statusStrip1.Update();

            try
            {
                cluster = new msmsCluster(parser.MsExperiments, double.Parse(this.clusterMarginTB.Text));
                cluster.cluster(double.Parse(this.clusterMarginTB.Text));
                
                //Enable step 3
                this.pictureBoxExclamationMark1.Image = (System.Drawing.Image)myResources.GetObject("exclamationSmall");
                this.pictureBoxExclamationMark2.Image = (System.Drawing.Image)myResources.GetObject("exclamationSmall");
                this.groupBoxClusteringStep3.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Please parse some stuff to RAM first", "Error!");
            }
            this.toolStripStatusLabel2.Text = "Waiting for work.....";
        }


        void BSaveClusterClick(object sender, EventArgs e)
        {
            this.saveFileDialog1.FileName = "cluster.txt";
            this.saveFileDialog1.ShowDialog();

            this.toolStripStatusLabel2.Text = "Saving to File....";
            try
            {
                this.pictureBoxExclamationMark1.Image = (System.Drawing.Image)myResources.GetObject("checkmark");
                cluster.saveIndex(this.saveFileDialog1.FileName);
                this.progressTB.Text += "Index File created!\r\n";
            }
            catch
            {
                MessageBox.Show("Probably you dont have a clustered structure in the RAM memory", "Error!");
            }
            this.toolStripStatusLabel2.Text = "Index file saved.....";
        }


        void BSparseMatrixClick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "Saving to File....";


            try
            {
                this.pictureBoxExclamationMark2.Image = (System.Drawing.Image)myResources.GetObject("checkmark");
                this.saveFileDialog1.FileName = "sparseMatrix.txt";
                this.saveFileDialog1.ShowDialog();

                sMatrix.getMatrixFromMsmsCluster(cluster);
                sMatrix.saveMatrix(this.saveFileDialog1.FileName);
                this.progressTB.AppendText("Matrix File saved!\r\n");
            }
            catch
            {
                MessageBox.Show("Probably you dont have a clustered structure in the RAM memory", "Error!");
            }
            this.toolStripStatusLabel2.Text = "sparseMatrix saved....";
        }


        void ButtonOpenFolderBrowserDialog1Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.masterDirectoryTB.Text = folderBrowserDialog1.SelectedPath;
                this.masterDirectoryTB.Update();
            }
        }

        void RadioButtonFullMSMSCheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonFullMSMS.Checked)
            {
                this.textBoxMIC.Enabled = true;
                this.label5.Enabled = true;
                this.label6.Enabled = true;
            }
            else
            {
                this.textBoxMIC.Enabled = false;
                this.label5.Enabled = false;
                this.label6.Enabled = false;
            }
        }

        void RadioButtonPrecursorCheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonPrecursor.Checked)
            {
                parser.LoadMZ = true;
            }
            else
            {
                parser.LoadMZ = false;
            }
        }

        void ButtonClearMemoryClick(object sender, EventArgs e)
        {
            parser.dispose();
            this.progressTB.Text += "Memory Cleared\r\n";
            MessageBox.Show("Memory Cleared!", "Memory Cleared!");
            this.groupBoxClusteringStep3.Enabled = false;
            this.groupBoxClusteringStep2.Enabled = false;
        	
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBoxExclamationMark1.Image = (System.Drawing.Image)myResources.GetObject("exclamationBWsmall");
            this.pictureBoxExclamationMark2.Image = (System.Drawing.Image)myResources.GetObject("exclamationBWsmall");
        }



        void AboutToolStripMenuItem1Click(object sender, EventArgs e)
        {
            aboutForm about = new aboutForm();
            about.ShowDialog();
        }


        //The ion Charge buttons

        void setValueForacceptableCharges1()
        {
            if (this.cBIonCharge1.Checked == true)
            {
                acceptableCharges[1] = true;
            }
            else
            {
                acceptableCharges[1] = false;
            }
        }

        void setValueForacceptableCharges2()
        {
            if (this.cBIonCharge2.Checked == true)
            {
                acceptableCharges[2] = true;
            }
            else
            {
                acceptableCharges[2] = false;
            }
        }

        void setValueForacceptableCharges3()
        {
            if (this.cBIonCharge3.Checked == true)
            {
                acceptableCharges[3] = true;
            }
            else
            {
                acceptableCharges[3] = false;
            }
        }

        void CBIonCharge1CheckedChanged(object sender, EventArgs e)
        {
            if (this.cBIonCharge1.Checked == true)
            {
                acceptableCharges[1] = true;
            }
            else
            {
                acceptableCharges[1] = false;
            }
        }

        void CBIonCharge2CheckedChanged(object sender, EventArgs e)
        {
            if (this.cBIonCharge2.Checked == true)
            {
                acceptableCharges[2] = true;
            }
            else
            {
                acceptableCharges[2] = false;
            }
        }

        void CBIonCharge3CheckedChanged(object sender, EventArgs e)
        {
            if (this.cBIonCharge3.Checked == true)
            {
                acceptableCharges[3] = true;
                MessageBox.Show("buttonvalueChanged");
            }
            else
            {
                acceptableCharges[3] = false;
            }
        }

        void ProgressTBTextChanged(object sender, EventArgs e)
        {
            this.progressTB.Focus();
        }

        //----

        void ButtonSaveLogClick(object sender, EventArgs e)
        {
            this.saveFileDialog1.FileName = "log.txt";
            this.saveFileDialog1.ShowDialog();

            this.progressTB.SaveFile(this.saveFileDialog1.FileName);
            
        }

        private GroupBox groupBoxPFstep1;
        private Button buttonBrowseSparseMatrix;
        private Button buttonBrowseIndex;
        private TextBox textBoxBrowseSparseMatrix;
        private TextBox textBoxBrowseIndex;
        private ComboBox comboBoxNormalization;
        private Button buttonOutputDirectoryStep1;
        private GroupBox groupBox4;
        private Button buttonStartStep3;
        private Panel panel1;
        private ComboBox comboBoxFeatureSelection;
        private Button buttonContinueStep3;
        private ToolStripMenuItem conversionToolStripMenuItem;
        private ToolStripMenuItem dTA2MS2ToolStripMenuItem1;
        private TabPage tabPage2;
        private Button buttonApplyStep2;
        private GroupBox groupBox5;
        private ToolStripMenuItem matrixNormalizationToolStripMenuItem;
        private ToolStripMenuItem featureSelectionToolStripMenuItem;
        private ToolStripMenuItem horizontalToolStripMenuItem;
        private ToolStripMenuItem lnToolStripMenuItem;
        private ToolStripMenuItem zToolStripMenuItem;
        private ToolStripMenuItem fSVMToolStripMenuItem;
        private ToolStripMenuItem golubsIndexToolStripMenuItem;
        private ToolStripMenuItem tTestToolStripMenuItem;
        private ToolStripMenuItem sVMRFEToolStripMenuItem;
        private TextBox textBoxOutputDirectory;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private Button buttonStopStep3;
        private DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn rankDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn scoreDataGridViewTextBoxColumn;
        private DataGridView dataGridViewResults;

    }

    //--------------------------------------------
}
