namespace XQuantPairWiseAnalyzer
{
    partial class UserControlPairAnalyzer
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
            this.groupBoxPairAnalyzer = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMinPeptideFold = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownMinMS1COunt = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxOnlyUniquePeptides = new System.Windows.Forms.CheckBox();
            this.checkBoxInvertClasses = new System.Windows.Forms.CheckBox();
            this.buttonProteinReport = new System.Windows.Forms.Button();
            this.checkBoxNormalize = new System.Windows.Forms.CheckBox();
            this.buttonRunAnalysis = new System.Windows.Forms.Button();
            this.groupBoxClasses = new System.Windows.Forms.GroupBox();
            this.checkedListBoxClasses = new System.Windows.Forms.CheckedListBox();
            this.tabControlAnalysis = new System.Windows.Forms.TabControl();
            this.tabPagePeptideAnalysis = new System.Windows.Forms.TabPage();
            this.dataGridViewPeptideAnalysis = new System.Windows.Forms.DataGridView();
            this.tabPageProteinAnalysis = new System.Windows.Forms.TabPage();
            this.richTextBoxProteinAndPeptideReport = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBoxProteinReport = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xICCoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pairedAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePairedAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxPairAnalyzer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinPeptideFold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinMS1COunt)).BeginInit();
            this.groupBoxClasses.SuspendLayout();
            this.tabControlAnalysis.SuspendLayout();
            this.tabPagePeptideAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPeptideAnalysis)).BeginInit();
            this.tabPageProteinAnalysis.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxPairAnalyzer
            // 
            this.groupBoxPairAnalyzer.Controls.Add(this.splitContainer1);
            this.groupBoxPairAnalyzer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPairAnalyzer.Location = new System.Drawing.Point(0, 24);
            this.groupBoxPairAnalyzer.Name = "groupBoxPairAnalyzer";
            this.groupBoxPairAnalyzer.Size = new System.Drawing.Size(634, 517);
            this.groupBoxPairAnalyzer.TabIndex = 0;
            this.groupBoxPairAnalyzer.TabStop = false;
            this.groupBoxPairAnalyzer.Text = "Pair Analyzer";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownMinPeptideFold);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownMinMS1COunt);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxOnlyUniquePeptides);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxInvertClasses);
            this.splitContainer1.Panel1.Controls.Add(this.buttonProteinReport);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxNormalize);
            this.splitContainer1.Panel1.Controls.Add(this.buttonRunAnalysis);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxClasses);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlAnalysis);
            this.splitContainer1.Size = new System.Drawing.Size(628, 498);
            this.splitContainer1.SplitterDistance = 144;
            this.splitContainer1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(480, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Weights are attributed as the square root of the number of conditions in which th" +
    "e peptide was found";
            // 
            // numericUpDownMinPeptideFold
            // 
            this.numericUpDownMinPeptideFold.DecimalPlaces = 1;
            this.numericUpDownMinPeptideFold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMinPeptideFold.Location = new System.Drawing.Point(428, 42);
            this.numericUpDownMinPeptideFold.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownMinPeptideFold.Name = "numericUpDownMinPeptideFold";
            this.numericUpDownMinPeptideFold.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownMinPeptideFold.TabIndex = 10;
            this.numericUpDownMinPeptideFold.Value = new decimal(new int[] {
            12,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Min Peptide Fold";
            // 
            // numericUpDownMinMS1COunt
            // 
            this.numericUpDownMinMS1COunt.Location = new System.Drawing.Point(290, 42);
            this.numericUpDownMinMS1COunt.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownMinMS1COunt.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownMinMS1COunt.Name = "numericUpDownMinMS1COunt";
            this.numericUpDownMinMS1COunt.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownMinMS1COunt.TabIndex = 8;
            this.numericUpDownMinMS1COunt.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Min MS1 Count";
            // 
            // checkBoxOnlyUniquePeptides
            // 
            this.checkBoxOnlyUniquePeptides.AutoSize = true;
            this.checkBoxOnlyUniquePeptides.Location = new System.Drawing.Point(447, 11);
            this.checkBoxOnlyUniquePeptides.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxOnlyUniquePeptides.Name = "checkBoxOnlyUniquePeptides";
            this.checkBoxOnlyUniquePeptides.Size = new System.Drawing.Size(128, 17);
            this.checkBoxOnlyUniquePeptides.TabIndex = 6;
            this.checkBoxOnlyUniquePeptides.Text = "Only Unique Peptides";
            this.checkBoxOnlyUniquePeptides.UseVisualStyleBackColor = true;
            // 
            // checkBoxInvertClasses
            // 
            this.checkBoxInvertClasses.AutoSize = true;
            this.checkBoxInvertClasses.Checked = true;
            this.checkBoxInvertClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxInvertClasses.Location = new System.Drawing.Point(209, 11);
            this.checkBoxInvertClasses.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxInvertClasses.Name = "checkBoxInvertClasses";
            this.checkBoxInvertClasses.Size = new System.Drawing.Size(92, 17);
            this.checkBoxInvertClasses.TabIndex = 5;
            this.checkBoxInvertClasses.Text = "Invert Classes";
            this.checkBoxInvertClasses.UseVisualStyleBackColor = true;
            // 
            // buttonProteinReport
            // 
            this.buttonProteinReport.Enabled = false;
            this.buttonProteinReport.Location = new System.Drawing.Point(304, 71);
            this.buttonProteinReport.Name = "buttonProteinReport";
            this.buttonProteinReport.Size = new System.Drawing.Size(143, 23);
            this.buttonProteinReport.TabIndex = 4;
            this.buttonProteinReport.Text = "Generate Protein Report";
            this.buttonProteinReport.UseVisualStyleBackColor = true;
            this.buttonProteinReport.Click += new System.EventHandler(this.buttonProteinReport_Click);
            // 
            // checkBoxNormalize
            // 
            this.checkBoxNormalize.AutoSize = true;
            this.checkBoxNormalize.Checked = true;
            this.checkBoxNormalize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNormalize.Location = new System.Drawing.Point(304, 11);
            this.checkBoxNormalize.Name = "checkBoxNormalize";
            this.checkBoxNormalize.Size = new System.Drawing.Size(141, 17);
            this.checkBoxNormalize.TabIndex = 3;
            this.checkBoxNormalize.Text = "Normalize quants by TIC";
            this.checkBoxNormalize.UseVisualStyleBackColor = true;
            // 
            // buttonRunAnalysis
            // 
            this.buttonRunAnalysis.Enabled = false;
            this.buttonRunAnalysis.Location = new System.Drawing.Point(205, 71);
            this.buttonRunAnalysis.Name = "buttonRunAnalysis";
            this.buttonRunAnalysis.Size = new System.Drawing.Size(96, 23);
            this.buttonRunAnalysis.TabIndex = 2;
            this.buttonRunAnalysis.Text = "Run Core Analysis";
            this.buttonRunAnalysis.UseVisualStyleBackColor = true;
            this.buttonRunAnalysis.Click += new System.EventHandler(this.buttonRunAnalysis_Click);
            // 
            // groupBoxClasses
            // 
            this.groupBoxClasses.Controls.Add(this.checkedListBoxClasses);
            this.groupBoxClasses.Location = new System.Drawing.Point(3, 3);
            this.groupBoxClasses.Name = "groupBoxClasses";
            this.groupBoxClasses.Size = new System.Drawing.Size(200, 90);
            this.groupBoxClasses.TabIndex = 1;
            this.groupBoxClasses.TabStop = false;
            this.groupBoxClasses.Text = "Class Labels";
            // 
            // checkedListBoxClasses
            // 
            this.checkedListBoxClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxClasses.FormattingEnabled = true;
            this.checkedListBoxClasses.Location = new System.Drawing.Point(3, 16);
            this.checkedListBoxClasses.Name = "checkedListBoxClasses";
            this.checkedListBoxClasses.Size = new System.Drawing.Size(194, 71);
            this.checkedListBoxClasses.TabIndex = 0;
            // 
            // tabControlAnalysis
            // 
            this.tabControlAnalysis.Controls.Add(this.tabPagePeptideAnalysis);
            this.tabControlAnalysis.Controls.Add(this.tabPageProteinAnalysis);
            this.tabControlAnalysis.Controls.Add(this.tabPage1);
            this.tabControlAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAnalysis.Location = new System.Drawing.Point(0, 0);
            this.tabControlAnalysis.Name = "tabControlAnalysis";
            this.tabControlAnalysis.SelectedIndex = 0;
            this.tabControlAnalysis.Size = new System.Drawing.Size(628, 350);
            this.tabControlAnalysis.TabIndex = 0;
            // 
            // tabPagePeptideAnalysis
            // 
            this.tabPagePeptideAnalysis.Controls.Add(this.dataGridViewPeptideAnalysis);
            this.tabPagePeptideAnalysis.Location = new System.Drawing.Point(4, 22);
            this.tabPagePeptideAnalysis.Name = "tabPagePeptideAnalysis";
            this.tabPagePeptideAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePeptideAnalysis.Size = new System.Drawing.Size(620, 324);
            this.tabPagePeptideAnalysis.TabIndex = 0;
            this.tabPagePeptideAnalysis.Text = "Peptide Analysis";
            this.tabPagePeptideAnalysis.UseVisualStyleBackColor = true;
            // 
            // dataGridViewPeptideAnalysis
            // 
            this.dataGridViewPeptideAnalysis.AllowUserToAddRows = false;
            this.dataGridViewPeptideAnalysis.AllowUserToDeleteRows = false;
            this.dataGridViewPeptideAnalysis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPeptideAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPeptideAnalysis.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewPeptideAnalysis.Name = "dataGridViewPeptideAnalysis";
            this.dataGridViewPeptideAnalysis.ReadOnly = true;
            this.dataGridViewPeptideAnalysis.Size = new System.Drawing.Size(614, 318);
            this.dataGridViewPeptideAnalysis.TabIndex = 0;
            // 
            // tabPageProteinAnalysis
            // 
            this.tabPageProteinAnalysis.Controls.Add(this.richTextBoxProteinAndPeptideReport);
            this.tabPageProteinAnalysis.Location = new System.Drawing.Point(4, 22);
            this.tabPageProteinAnalysis.Name = "tabPageProteinAnalysis";
            this.tabPageProteinAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProteinAnalysis.Size = new System.Drawing.Size(620, 324);
            this.tabPageProteinAnalysis.TabIndex = 1;
            this.tabPageProteinAnalysis.Text = "Peptide Mapping Report";
            this.tabPageProteinAnalysis.UseVisualStyleBackColor = true;
            // 
            // richTextBoxProteinAndPeptideReport
            // 
            this.richTextBoxProteinAndPeptideReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxProteinAndPeptideReport.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxProteinAndPeptideReport.Name = "richTextBoxProteinAndPeptideReport";
            this.richTextBoxProteinAndPeptideReport.Size = new System.Drawing.Size(614, 318);
            this.richTextBoxProteinAndPeptideReport.TabIndex = 0;
            this.richTextBoxProteinAndPeptideReport.Text = "";
            this.richTextBoxProteinAndPeptideReport.WordWrap = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBoxProteinReport);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(620, 324);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Protein Report";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBoxProteinReport
            // 
            this.richTextBoxProteinReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxProteinReport.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxProteinReport.Name = "richTextBoxProteinReport";
            this.richTextBoxProteinReport.Size = new System.Drawing.Size(614, 318);
            this.richTextBoxProteinReport.TabIndex = 0;
            this.richTextBoxProteinReport.Text = "";
            this.richTextBoxProteinReport.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem1,
            this.savePairedAnalysisToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem1
            // 
            this.loadToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xICCoreToolStripMenuItem,
            this.pairedAnalysisToolStripMenuItem});
            this.loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
            this.loadToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem1.Text = "Load";
            // 
            // xICCoreToolStripMenuItem
            // 
            this.xICCoreToolStripMenuItem.Name = "xICCoreToolStripMenuItem";
            this.xICCoreToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.xICCoreToolStripMenuItem.Text = "XIC Core";
            this.xICCoreToolStripMenuItem.Click += new System.EventHandler(this.xICCoreToolStripMenuItem_Click);
            // 
            // pairedAnalysisToolStripMenuItem
            // 
            this.pairedAnalysisToolStripMenuItem.Name = "pairedAnalysisToolStripMenuItem";
            this.pairedAnalysisToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.pairedAnalysisToolStripMenuItem.Text = "Paired Analysis";
            this.pairedAnalysisToolStripMenuItem.Click += new System.EventHandler(this.pairedAnalysisToolStripMenuItem_Click);
            // 
            // savePairedAnalysisToolStripMenuItem
            // 
            this.savePairedAnalysisToolStripMenuItem.Name = "savePairedAnalysisToolStripMenuItem";
            this.savePairedAnalysisToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.savePairedAnalysisToolStripMenuItem.Text = "Save Paired Analysis";
            this.savePairedAnalysisToolStripMenuItem.Click += new System.EventHandler(this.savePairedAnalysisToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // UserControlPairAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxPairAnalyzer);
            this.Controls.Add(this.menuStrip1);
            this.Name = "UserControlPairAnalyzer";
            this.Size = new System.Drawing.Size(634, 541);
            this.groupBoxPairAnalyzer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinPeptideFold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinMS1COunt)).EndInit();
            this.groupBoxClasses.ResumeLayout(false);
            this.tabControlAnalysis.ResumeLayout(false);
            this.tabPagePeptideAnalysis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPeptideAnalysis)).EndInit();
            this.tabPageProteinAnalysis.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPairAnalyzer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckedListBox checkedListBoxClasses;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxClasses;
        private System.Windows.Forms.Button buttonRunAnalysis;
        private System.Windows.Forms.TabControl tabControlAnalysis;
        private System.Windows.Forms.TabPage tabPagePeptideAnalysis;
        private System.Windows.Forms.DataGridView dataGridViewPeptideAnalysis;
        private System.Windows.Forms.TabPage tabPageProteinAnalysis;
        private System.Windows.Forms.CheckBox checkBoxNormalize;
        private System.Windows.Forms.Button buttonProteinReport;
        private System.Windows.Forms.RichTextBox richTextBoxProteinAndPeptideReport;
        private System.Windows.Forms.ToolStripMenuItem savePairedAnalysisToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richTextBoxProteinReport;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xICCoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pairedAnalysisToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxInvertClasses;
        private System.Windows.Forms.NumericUpDown numericUpDownMinPeptideFold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownMinMS1COunt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxOnlyUniquePeptides;
        private System.Windows.Forms.Label label3;
    }
}
