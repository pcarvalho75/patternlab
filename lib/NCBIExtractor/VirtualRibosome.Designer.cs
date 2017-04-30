namespace NCBIExtractor
{
    partial class VirtualRibosome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VirtualRibosome));
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelNoStopCodons = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelNoNt = new System.Windows.Forms.Label();
            this.buttonWork = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonInput = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.textBoxInputFile = new System.Windows.Forms.TextBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxQualityFilters = new System.Windows.Forms.GroupBox();
            this.radioButtonSixFrame = new System.Windows.Forms.RadioButton();
            this.radioButton3Frame = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownMaxNoStopCodons = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMinNoNT = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.checkBoxQualityFilters = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxQualityFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxNoStopCodons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinNoNT)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.Location = new System.Drawing.Point(8, 39);
            this.richTextBoxOutput.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(584, 413);
            this.richTextBoxOutput.TabIndex = 1;
            this.richTextBoxOutput.Text = "";
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(8, 460);
            this.buttonGo.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(584, 28);
            this.buttonGo.TabIndex = 2;
            this.buttonGo.Text = "GO";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInput.Location = new System.Drawing.Point(8, 7);
            this.textBoxInput.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(584, 22);
            this.textBoxInput.TabIndex = 3;
            this.textBoxInput.Text = "atgcccaagctgaatagcgtagaggggttttcatcatttgaggacgatgtataa";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(609, 526);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxQualityFilters);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.buttonWork);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBoxQualityFilters);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(601, 497);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "High throughput tanslator";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelNoStopCodons);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.labelNoNt);
            this.groupBox3.Location = new System.Drawing.Point(35, 365);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(516, 71);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Results";
            // 
            // labelNoStopCodons
            // 
            this.labelNoStopCodons.AutoSize = true;
            this.labelNoStopCodons.Location = new System.Drawing.Point(413, 47);
            this.labelNoStopCodons.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNoStopCodons.Name = "labelNoStopCodons";
            this.labelNoStopCodons.Size = new System.Drawing.Size(16, 17);
            this.labelNoStopCodons.TabIndex = 5;
            this.labelNoStopCodons.Text = "?";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 47);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(374, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "No sequences eliminated due to elevated no stop codons:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(344, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "No sequences eliminated for not satisfyingmin no NT:";
            // 
            // labelNoNt
            // 
            this.labelNoNt.AutoSize = true;
            this.labelNoNt.Location = new System.Drawing.Point(413, 20);
            this.labelNoNt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNoNt.Name = "labelNoNt";
            this.labelNoNt.Size = new System.Drawing.Size(16, 17);
            this.labelNoNt.TabIndex = 1;
            this.labelNoNt.Text = "?";
            // 
            // buttonWork
            // 
            this.buttonWork.Enabled = false;
            this.buttonWork.Location = new System.Drawing.Point(35, 444);
            this.buttonWork.Margin = new System.Windows.Forms.Padding(4);
            this.buttonWork.Name = "buttonWork";
            this.buttonWork.Size = new System.Drawing.Size(516, 28);
            this.buttonWork.TabIndex = 10;
            this.buttonWork.Text = "Go !";
            this.buttonWork.UseVisualStyleBackColor = true;
            this.buttonWork.Click += new System.EventHandler(this.buttonWork_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.buttonInput);
            this.groupBox2.Controls.Add(this.buttonOutput);
            this.groupBox2.Controls.Add(this.textBoxInputFile);
            this.groupBox2.Controls.Add(this.textBoxOutput);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(35, 30);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(516, 123);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Input :";
            // 
            // buttonInput
            // 
            this.buttonInput.Location = new System.Drawing.Point(389, 37);
            this.buttonInput.Margin = new System.Windows.Forms.Padding(4);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(100, 28);
            this.buttonInput.TabIndex = 0;
            this.buttonInput.Text = "Browse";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Location = new System.Drawing.Point(389, 73);
            this.buttonOutput.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(100, 28);
            this.buttonOutput.TabIndex = 5;
            this.buttonOutput.Text = "Browse";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // textBoxInputFile
            // 
            this.textBoxInputFile.Location = new System.Drawing.Point(87, 39);
            this.textBoxInputFile.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxInputFile.Name = "textBoxInputFile";
            this.textBoxInputFile.Size = new System.Drawing.Size(293, 22);
            this.textBoxInputFile.TabIndex = 1;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(87, 75);
            this.textBoxOutput.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(293, 22);
            this.textBoxOutput.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output : ";
            // 
            // groupBoxQualityFilters
            // 
            this.groupBoxQualityFilters.Controls.Add(this.radioButtonSixFrame);
            this.groupBoxQualityFilters.Controls.Add(this.radioButton3Frame);
            this.groupBoxQualityFilters.Controls.Add(this.label6);
            this.groupBoxQualityFilters.Controls.Add(this.numericUpDownMaxNoStopCodons);
            this.groupBoxQualityFilters.Controls.Add(this.label5);
            this.groupBoxQualityFilters.Controls.Add(this.label4);
            this.groupBoxQualityFilters.Controls.Add(this.label3);
            this.groupBoxQualityFilters.Controls.Add(this.numericUpDownMinNoNT);
            this.groupBoxQualityFilters.Location = new System.Drawing.Point(35, 198);
            this.groupBoxQualityFilters.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxQualityFilters.Name = "groupBoxQualityFilters";
            this.groupBoxQualityFilters.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxQualityFilters.Size = new System.Drawing.Size(516, 159);
            this.groupBoxQualityFilters.TabIndex = 8;
            this.groupBoxQualityFilters.TabStop = false;
            this.groupBoxQualityFilters.Text = "Quality filters";
            // 
            // radioButtonSixFrame
            // 
            this.radioButtonSixFrame.AutoSize = true;
            this.radioButtonSixFrame.Location = new System.Drawing.Point(221, 110);
            this.radioButtonSixFrame.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonSixFrame.Name = "radioButtonSixFrame";
            this.radioButtonSixFrame.Size = new System.Drawing.Size(151, 21);
            this.radioButtonSixFrame.TabIndex = 13;
            this.radioButtonSixFrame.TabStop = true;
            this.radioButtonSixFrame.Text = "6 Frame translation";
            this.radioButtonSixFrame.UseVisualStyleBackColor = true;
            // 
            // radioButton3Frame
            // 
            this.radioButton3Frame.AutoSize = true;
            this.radioButton3Frame.Location = new System.Drawing.Point(19, 110);
            this.radioButton3Frame.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton3Frame.Name = "radioButton3Frame";
            this.radioButton3Frame.Size = new System.Drawing.Size(151, 21);
            this.radioButton3Frame.TabIndex = 12;
            this.radioButton3Frame.TabStop = true;
            this.radioButton3Frame.Text = "3 Frame translation";
            this.radioButton3Frame.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(404, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "stop codons";
            // 
            // numericUpDownMaxNoStopCodons
            // 
            this.numericUpDownMaxNoStopCodons.Location = new System.Drawing.Point(299, 66);
            this.numericUpDownMaxNoStopCodons.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownMaxNoStopCodons.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownMaxNoStopCodons.Name = "numericUpDownMaxNoStopCodons";
            this.numericUpDownMaxNoStopCodons.Size = new System.Drawing.Size(60, 22);
            this.numericUpDownMaxNoStopCodons.TabIndex = 10;
            this.numericUpDownMaxNoStopCodons.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 71);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(234, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Eliminate sequences with more than";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "nt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Eliminate sequences with less than";
            // 
            // numericUpDownMinNoNT
            // 
            this.numericUpDownMinNoNT.Location = new System.Drawing.Point(299, 30);
            this.numericUpDownMinNoNT.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownMinNoNT.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownMinNoNT.Name = "numericUpDownMinNoNT";
            this.numericUpDownMinNoNT.Size = new System.Drawing.Size(60, 22);
            this.numericUpDownMinNoNT.TabIndex = 6;
            this.numericUpDownMinNoNT.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxInput);
            this.tabPage2.Controls.Add(this.richTextBoxOutput);
            this.tabPage2.Controls.Add(this.buttonGo);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(601, 497);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Translate single sequence";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBoxQualityFilters
            // 
            this.checkBoxQualityFilters.AutoSize = true;
            this.checkBoxQualityFilters.Checked = true;
            this.checkBoxQualityFilters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxQualityFilters.Location = new System.Drawing.Point(35, 170);
            this.checkBoxQualityFilters.Name = "checkBoxQualityFilters";
            this.checkBoxQualityFilters.Size = new System.Drawing.Size(155, 21);
            this.checkBoxQualityFilters.TabIndex = 12;
            this.checkBoxQualityFilters.Text = "Apply Quality Filters";
            this.checkBoxQualityFilters.UseVisualStyleBackColor = true;
            this.checkBoxQualityFilters.CheckedChanged += new System.EventHandler(this.checkBoxQualityFilters_CheckedChanged);
            // 
            // VirtualRibosome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 526);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VirtualRibosome";
            this.Text = "Virtual Ribosome";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxQualityFilters.ResumeLayout(false);
            this.groupBoxQualityFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxNoStopCodons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinNoNT)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonWork;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.TextBox textBoxInputFile;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxQualityFilters;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxNoStopCodons;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownMinNoNT;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelNoStopCodons;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelNoNt;
        private System.Windows.Forms.RadioButton radioButtonSixFrame;
        private System.Windows.Forms.RadioButton radioButton3Frame;
        private System.Windows.Forms.CheckBox checkBoxQualityFilters;
    }
}

