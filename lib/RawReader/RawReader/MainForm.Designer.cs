namespace RawReader
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxInputDirectory = new System.Windows.Forms.TextBox();
            this.checkBoxMS1 = new System.Windows.Forms.CheckBox();
            this.checkBoxMS2 = new System.Windows.Forms.CheckBox();
            this.buttonGO = new System.Windows.Forms.Button();
            this.groupBoxDeNovoSimplification = new System.Windows.Forms.GroupBox();
            this.numericUpDownSpectraPerFile = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownMaxPeaksPerWindow = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWindowSize = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSpectraCleaner = new System.Windows.Forms.CheckBox();
            this.checkBoxUseThermosMonoIsotopicInference = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxMS3 = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonBrowseForFile = new System.Windows.Forms.Button();
            this.richTextBoxMS = new System.Windows.Forms.RichTextBox();
            this.buttonExtract = new System.Windows.Forms.Button();
            this.numericUpDownSpectrumNumber = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxMGF = new System.Windows.Forms.CheckBox();
            this.groupBoxDeNovoSimplification.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpectraPerFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPeaksPerWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindowSize)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpectrumNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Directory with Orbitrap Raw Files";
            // 
            // textBoxInputDirectory
            // 
            this.textBoxInputDirectory.Location = new System.Drawing.Point(264, 12);
            this.textBoxInputDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxInputDirectory.Name = "textBoxInputDirectory";
            this.textBoxInputDirectory.Size = new System.Drawing.Size(437, 22);
            this.textBoxInputDirectory.TabIndex = 1;
            // 
            // checkBoxMS1
            // 
            this.checkBoxMS1.AutoSize = true;
            this.checkBoxMS1.Location = new System.Drawing.Point(12, 57);
            this.checkBoxMS1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxMS1.Name = "checkBoxMS1";
            this.checkBoxMS1.Size = new System.Drawing.Size(58, 21);
            this.checkBoxMS1.TabIndex = 2;
            this.checkBoxMS1.Text = "MS1";
            this.checkBoxMS1.UseVisualStyleBackColor = true;
            this.checkBoxMS1.CheckedChanged += new System.EventHandler(this.checkBoxMS1_CheckedChanged);
            // 
            // checkBoxMS2
            // 
            this.checkBoxMS2.AutoSize = true;
            this.checkBoxMS2.Checked = true;
            this.checkBoxMS2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMS2.Location = new System.Drawing.Point(84, 57);
            this.checkBoxMS2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxMS2.Name = "checkBoxMS2";
            this.checkBoxMS2.Size = new System.Drawing.Size(58, 21);
            this.checkBoxMS2.TabIndex = 3;
            this.checkBoxMS2.Text = "MS2";
            this.checkBoxMS2.UseVisualStyleBackColor = true;
            this.checkBoxMS2.CheckedChanged += new System.EventHandler(this.checkBoxMS2_CheckedChanged);
            // 
            // buttonGO
            // 
            this.buttonGO.Location = new System.Drawing.Point(12, 196);
            this.buttonGO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGO.Name = "buttonGO";
            this.buttonGO.Size = new System.Drawing.Size(695, 28);
            this.buttonGO.TabIndex = 4;
            this.buttonGO.Text = "GO !";
            this.buttonGO.UseVisualStyleBackColor = true;
            this.buttonGO.Click += new System.EventHandler(this.buttonGO_Click);
            // 
            // groupBoxDeNovoSimplification
            // 
            this.groupBoxDeNovoSimplification.Controls.Add(this.numericUpDownSpectraPerFile);
            this.groupBoxDeNovoSimplification.Controls.Add(this.label4);
            this.groupBoxDeNovoSimplification.Controls.Add(this.label3);
            this.groupBoxDeNovoSimplification.Controls.Add(this.label2);
            this.groupBoxDeNovoSimplification.Controls.Add(this.numericUpDownMaxPeaksPerWindow);
            this.groupBoxDeNovoSimplification.Controls.Add(this.numericUpDownWindowSize);
            this.groupBoxDeNovoSimplification.Enabled = false;
            this.groupBoxDeNovoSimplification.Location = new System.Drawing.Point(8, 113);
            this.groupBoxDeNovoSimplification.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxDeNovoSimplification.Name = "groupBoxDeNovoSimplification";
            this.groupBoxDeNovoSimplification.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxDeNovoSimplification.Size = new System.Drawing.Size(695, 75);
            this.groupBoxDeNovoSimplification.TabIndex = 5;
            this.groupBoxDeNovoSimplification.TabStop = false;
            this.groupBoxDeNovoSimplification.Text = "Spectra cleaner";
            // 
            // numericUpDownSpectraPerFile
            // 
            this.numericUpDownSpectraPerFile.Location = new System.Drawing.Point(568, 30);
            this.numericUpDownSpectraPerFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownSpectraPerFile.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownSpectraPerFile.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSpectraPerFile.Name = "numericUpDownSpectraPerFile";
            this.numericUpDownSpectraPerFile.Size = new System.Drawing.Size(77, 22);
            this.numericUpDownSpectraPerFile.TabIndex = 5;
            this.numericUpDownSpectraPerFile.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(456, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Spectra per file";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Max Peaks / Window";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Window Size";
            // 
            // numericUpDownMaxPeaksPerWindow
            // 
            this.numericUpDownMaxPeaksPerWindow.Location = new System.Drawing.Point(376, 30);
            this.numericUpDownMaxPeaksPerWindow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownMaxPeaksPerWindow.Name = "numericUpDownMaxPeaksPerWindow";
            this.numericUpDownMaxPeaksPerWindow.Size = new System.Drawing.Size(72, 22);
            this.numericUpDownMaxPeaksPerWindow.TabIndex = 1;
            this.numericUpDownMaxPeaksPerWindow.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // numericUpDownWindowSize
            // 
            this.numericUpDownWindowSize.Location = new System.Drawing.Point(140, 30);
            this.numericUpDownWindowSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownWindowSize.Name = "numericUpDownWindowSize";
            this.numericUpDownWindowSize.Size = new System.Drawing.Size(73, 22);
            this.numericUpDownWindowSize.TabIndex = 0;
            this.numericUpDownWindowSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // checkBoxSpectraCleaner
            // 
            this.checkBoxSpectraCleaner.AutoSize = true;
            this.checkBoxSpectraCleaner.Location = new System.Drawing.Point(12, 85);
            this.checkBoxSpectraCleaner.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxSpectraCleaner.Name = "checkBoxSpectraCleaner";
            this.checkBoxSpectraCleaner.Size = new System.Drawing.Size(182, 21);
            this.checkBoxSpectraCleaner.TabIndex = 6;
            this.checkBoxSpectraCleaner.Text = "Activate spectra cleaner";
            this.checkBoxSpectraCleaner.UseVisualStyleBackColor = true;
            this.checkBoxSpectraCleaner.CheckedChanged += new System.EventHandler(this.checkBoxSpectraCleaner_CheckedChanged);
            // 
            // checkBoxUseThermosMonoIsotopicInference
            // 
            this.checkBoxUseThermosMonoIsotopicInference.AutoSize = true;
            this.checkBoxUseThermosMonoIsotopicInference.Checked = true;
            this.checkBoxUseThermosMonoIsotopicInference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseThermosMonoIsotopicInference.Location = new System.Drawing.Point(233, 57);
            this.checkBoxUseThermosMonoIsotopicInference.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxUseThermosMonoIsotopicInference.Name = "checkBoxUseThermosMonoIsotopicInference";
            this.checkBoxUseThermosMonoIsotopicInference.Size = new System.Drawing.Size(268, 21);
            this.checkBoxUseThermosMonoIsotopicInference.TabIndex = 7;
            this.checkBoxUseThermosMonoIsotopicInference.Text = "Use Thermo\'s monoisotopic inference";
            this.checkBoxUseThermosMonoIsotopicInference.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 310);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(757, 28);
            this.progressBar1.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(757, 310);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxMGF);
            this.tabPage1.Controls.Add(this.checkBoxMS3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBoxInputDirectory);
            this.tabPage1.Controls.Add(this.checkBoxMS1);
            this.tabPage1.Controls.Add(this.checkBoxMS2);
            this.tabPage1.Controls.Add(this.buttonGO);
            this.tabPage1.Controls.Add(this.groupBoxDeNovoSimplification);
            this.tabPage1.Controls.Add(this.checkBoxUseThermosMonoIsotopicInference);
            this.tabPage1.Controls.Add(this.checkBoxSpectraCleaner);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(749, 281);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Batch Extract";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxMS3
            // 
            this.checkBoxMS3.AutoSize = true;
            this.checkBoxMS3.Location = new System.Drawing.Point(156, 57);
            this.checkBoxMS3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxMS3.Name = "checkBoxMS3";
            this.checkBoxMS3.Size = new System.Drawing.Size(58, 21);
            this.checkBoxMS3.TabIndex = 8;
            this.checkBoxMS3.Text = "MS3";
            this.checkBoxMS3.UseVisualStyleBackColor = true;
            this.checkBoxMS3.CheckedChanged += new System.EventHandler(this.checkBoxMS3_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonBrowseForFile);
            this.tabPage2.Controls.Add(this.richTextBoxMS);
            this.tabPage2.Controls.Add(this.buttonExtract);
            this.tabPage2.Controls.Add(this.numericUpDownSpectrumNumber);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(749, 281);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Single Spectrum Extract";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseForFile
            // 
            this.buttonBrowseForFile.Location = new System.Drawing.Point(15, 49);
            this.buttonBrowseForFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonBrowseForFile.Name = "buttonBrowseForFile";
            this.buttonBrowseForFile.Size = new System.Drawing.Size(261, 28);
            this.buttonBrowseForFile.TabIndex = 6;
            this.buttonBrowseForFile.Text = "Browse for raw file";
            this.buttonBrowseForFile.UseVisualStyleBackColor = true;
            this.buttonBrowseForFile.Click += new System.EventHandler(this.buttonBrowseForFile_Click);
            // 
            // richTextBoxMS
            // 
            this.richTextBoxMS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.richTextBoxMS.Location = new System.Drawing.Point(284, 17);
            this.richTextBoxMS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBoxMS.Name = "richTextBoxMS";
            this.richTextBoxMS.Size = new System.Drawing.Size(451, 253);
            this.richTextBoxMS.TabIndex = 5;
            this.richTextBoxMS.Text = "";
            // 
            // buttonExtract
            // 
            this.buttonExtract.Enabled = false;
            this.buttonExtract.Location = new System.Drawing.Point(15, 85);
            this.buttonExtract.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExtract.Name = "buttonExtract";
            this.buttonExtract.Size = new System.Drawing.Size(261, 28);
            this.buttonExtract.TabIndex = 3;
            this.buttonExtract.Text = "Extract";
            this.buttonExtract.UseVisualStyleBackColor = true;
            this.buttonExtract.Click += new System.EventHandler(this.buttonExtract_Click);
            // 
            // numericUpDownSpectrumNumber
            // 
            this.numericUpDownSpectrumNumber.Location = new System.Drawing.Point(149, 17);
            this.numericUpDownSpectrumNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownSpectrumNumber.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownSpectrumNumber.Name = "numericUpDownSpectrumNumber";
            this.numericUpDownSpectrumNumber.Size = new System.Drawing.Size(127, 22);
            this.numericUpDownSpectrumNumber.TabIndex = 1;
            this.numericUpDownSpectrumNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Spectrum Number: ";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBoxMGF
            // 
            this.checkBoxMGF.AutoSize = true;
            this.checkBoxMGF.Location = new System.Drawing.Point(508, 57);
            this.checkBoxMGF.Name = "checkBoxMGF";
            this.checkBoxMGF.Size = new System.Drawing.Size(60, 21);
            this.checkBoxMGF.TabIndex = 9;
            this.checkBoxMGF.Text = "MGF";
            this.checkBoxMGF.UseVisualStyleBackColor = true;
            this.checkBoxMGF.CheckedChanged += new System.EventHandler(this.checkBoxMGF_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBar1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Size = new System.Drawing.Size(757, 338);
            this.groupBoxDeNovoSimplification.ResumeLayout(false);
            this.groupBoxDeNovoSimplification.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpectraPerFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPeaksPerWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindowSize)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpectrumNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxInputDirectory;
        private System.Windows.Forms.CheckBox checkBoxMS1;
        private System.Windows.Forms.CheckBox checkBoxMS2;
        private System.Windows.Forms.Button buttonGO;
        private System.Windows.Forms.GroupBox groupBoxDeNovoSimplification;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxPeaksPerWindow;
        private System.Windows.Forms.NumericUpDown numericUpDownWindowSize;
        private System.Windows.Forms.CheckBox checkBoxSpectraCleaner;
        private System.Windows.Forms.NumericUpDown numericUpDownSpectraPerFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxUseThermosMonoIsotopicInference;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBoxMS;
        private System.Windows.Forms.Button buttonExtract;
        private System.Windows.Forms.NumericUpDown numericUpDownSpectrumNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonBrowseForFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkBoxMS3;
        private System.Windows.Forms.CheckBox checkBoxMGF;
    }
}