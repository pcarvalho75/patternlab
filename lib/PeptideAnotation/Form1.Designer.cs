namespace PeptideAnotation
{
    partial class Form1
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
            this.buttonPeptideAnotation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPeptideSequence1 = new System.Windows.Forms.TextBox();
            this.richTextBoxAnotationAlfa = new System.Windows.Forms.RichTextBox();
            this.groupBoxAlfaPeptide = new System.Windows.Forms.GroupBox();
            this.groupBoxAnotatedPeptide = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownXLPos1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownXLPos2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPeptideSequence2 = new System.Windows.Forms.TextBox();
            this.groupBoxBetaPeptide = new System.Windows.Forms.GroupBox();
            this.richTextBoxAnotationBeta = new System.Windows.Forms.RichTextBox();
            this.peptideAnotator1 = new PeptideAnotation.PeptideAnotator();
            this.groupBoxAlfaPeptide.SuspendLayout();
            this.groupBoxAnotatedPeptide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXLPos1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXLPos2)).BeginInit();
            this.groupBoxBetaPeptide.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPeptideAnotation
            // 
            this.buttonPeptideAnotation.Location = new System.Drawing.Point(301, 282);
            this.buttonPeptideAnotation.Name = "buttonPeptideAnotation";
            this.buttonPeptideAnotation.Size = new System.Drawing.Size(184, 23);
            this.buttonPeptideAnotation.TabIndex = 1;
            this.buttonPeptideAnotation.Text = "Go";
            this.buttonPeptideAnotation.UseVisualStyleBackColor = true;
            this.buttonPeptideAnotation.Click += new System.EventHandler(this.buttonPeptideAnotation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(311, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Alfa Peptide";
            // 
            // textBoxPeptideSequence1
            // 
            this.textBoxPeptideSequence1.Location = new System.Drawing.Point(385, 178);
            this.textBoxPeptideSequence1.Name = "textBoxPeptideSequence1";
            this.textBoxPeptideSequence1.Size = new System.Drawing.Size(100, 20);
            this.textBoxPeptideSequence1.TabIndex = 4;
            this.textBoxPeptideSequence1.Text = "THEPEPTIDE";
            // 
            // richTextBoxAnotationAlfa
            // 
            this.richTextBoxAnotationAlfa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAnotationAlfa.Location = new System.Drawing.Point(3, 16);
            this.richTextBoxAnotationAlfa.Name = "richTextBoxAnotationAlfa";
            this.richTextBoxAnotationAlfa.Size = new System.Drawing.Size(130, 121);
            this.richTextBoxAnotationAlfa.TabIndex = 5;
            this.richTextBoxAnotationAlfa.Text = "b-1\ny-1\nb-2\nb-3\nb-5\ny-3\ny-4\ny-5\ny-6";
            // 
            // groupBoxAlfaPeptide
            // 
            this.groupBoxAlfaPeptide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxAlfaPeptide.Controls.Add(this.richTextBoxAnotationAlfa);
            this.groupBoxAlfaPeptide.Location = new System.Drawing.Point(15, 173);
            this.groupBoxAlfaPeptide.Name = "groupBoxAlfaPeptide";
            this.groupBoxAlfaPeptide.Size = new System.Drawing.Size(136, 140);
            this.groupBoxAlfaPeptide.TabIndex = 6;
            this.groupBoxAlfaPeptide.TabStop = false;
            this.groupBoxAlfaPeptide.Text = "Identified Alfa Peptide";
            // 
            // groupBoxAnotatedPeptide
            // 
            this.groupBoxAnotatedPeptide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAnotatedPeptide.AutoSize = true;
            this.groupBoxAnotatedPeptide.Controls.Add(this.peptideAnotator1);
            this.groupBoxAnotatedPeptide.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAnotatedPeptide.Name = "groupBoxAnotatedPeptide";
            this.groupBoxAnotatedPeptide.Size = new System.Drawing.Size(477, 155);
            this.groupBoxAnotatedPeptide.TabIndex = 7;
            this.groupBoxAnotatedPeptide.TabStop = false;
            this.groupBoxAnotatedPeptide.Text = "Anotated Peptide";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "XL Pos 1:";
            // 
            // numericUpDownXLPos1
            // 
            this.numericUpDownXLPos1.Location = new System.Drawing.Point(385, 230);
            this.numericUpDownXLPos1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownXLPos1.Name = "numericUpDownXLPos1";
            this.numericUpDownXLPos1.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownXLPos1.TabIndex = 9;
            this.numericUpDownXLPos1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDownXLPos2
            // 
            this.numericUpDownXLPos2.Location = new System.Drawing.Point(385, 256);
            this.numericUpDownXLPos2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownXLPos2.Name = "numericUpDownXLPos2";
            this.numericUpDownXLPos2.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownXLPos2.TabIndex = 10;
            this.numericUpDownXLPos2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "XL Pos 2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(311, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Beta Peptide";
            // 
            // textBoxPeptideSequence2
            // 
            this.textBoxPeptideSequence2.Location = new System.Drawing.Point(385, 204);
            this.textBoxPeptideSequence2.Name = "textBoxPeptideSequence2";
            this.textBoxPeptideSequence2.Size = new System.Drawing.Size(100, 20);
            this.textBoxPeptideSequence2.TabIndex = 13;
            this.textBoxPeptideSequence2.Text = "HELLOWORLD";
            // 
            // groupBoxBetaPeptide
            // 
            this.groupBoxBetaPeptide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxBetaPeptide.Controls.Add(this.richTextBoxAnotationBeta);
            this.groupBoxBetaPeptide.Location = new System.Drawing.Point(157, 173);
            this.groupBoxBetaPeptide.Name = "groupBoxBetaPeptide";
            this.groupBoxBetaPeptide.Size = new System.Drawing.Size(138, 140);
            this.groupBoxBetaPeptide.TabIndex = 14;
            this.groupBoxBetaPeptide.TabStop = false;
            this.groupBoxBetaPeptide.Text = "Identified Beta Peptide";
            // 
            // richTextBoxAnotationBeta
            // 
            this.richTextBoxAnotationBeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAnotationBeta.Location = new System.Drawing.Point(3, 16);
            this.richTextBoxAnotationBeta.Name = "richTextBoxAnotationBeta";
            this.richTextBoxAnotationBeta.Size = new System.Drawing.Size(132, 121);
            this.richTextBoxAnotationBeta.TabIndex = 5;
            this.richTextBoxAnotationBeta.Text = "b-2\nb-3\nb-4\ny-1\ny-3\ny-6\ny-7";
            // 
            // peptideAnotator1
            // 
            this.peptideAnotator1.BackColor = System.Drawing.Color.Transparent;
            this.peptideAnotator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peptideAnotator1.Location = new System.Drawing.Point(3, 16);
            this.peptideAnotator1.Name = "peptideAnotator1";
            this.peptideAnotator1.Size = new System.Drawing.Size(471, 136);
            this.peptideAnotator1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 324);
            this.Controls.Add(this.groupBoxBetaPeptide);
            this.Controls.Add(this.textBoxPeptideSequence2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownXLPos2);
            this.Controls.Add(this.numericUpDownXLPos1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBoxAnotatedPeptide);
            this.Controls.Add(this.groupBoxAlfaPeptide);
            this.Controls.Add(this.textBoxPeptideSequence1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPeptideAnotation);
            this.Name = "Form1";
            this.Text = "Peptide Anotator Tabajara";
            this.groupBoxAlfaPeptide.ResumeLayout(false);
            this.groupBoxAnotatedPeptide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXLPos1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXLPos2)).EndInit();
            this.groupBoxBetaPeptide.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPeptideAnotation;
        private PeptideAnotator peptideAnotator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPeptideSequence1;
        private System.Windows.Forms.RichTextBox richTextBoxAnotationAlfa;
        private System.Windows.Forms.GroupBox groupBoxAlfaPeptide;
        private System.Windows.Forms.GroupBox groupBoxAnotatedPeptide;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownXLPos1;
        private System.Windows.Forms.NumericUpDown numericUpDownXLPos2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPeptideSequence2;
        private System.Windows.Forms.GroupBox groupBoxBetaPeptide;
        private System.Windows.Forms.RichTextBox richTextBoxAnotationBeta;
    }
}

