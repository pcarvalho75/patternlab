namespace GO
{
    partial class ChooseParsing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseParsing));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonNCBiGenePeptFull = new System.Windows.Forms.RadioButton();
            this.numericUpDownGoColumn = new System.Windows.Forms.NumericUpDown();
            this.radioButtonUniProt = new System.Windows.Forms.RadioButton();
            this.numericUpDownMyColumn = new System.Windows.Forms.NumericUpDown();
            this.radioButtonIPI = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonUnstandard = new System.Windows.Forms.RadioButton();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGoColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMyColumn)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonNCBiGenePeptFull);
            this.groupBox1.Controls.Add(this.numericUpDownGoColumn);
            this.groupBox1.Controls.Add(this.radioButtonUniProt);
            this.groupBox1.Controls.Add(this.numericUpDownMyColumn);
            this.groupBox1.Controls.Add(this.radioButtonIPI);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButtonUnstandard);
            this.groupBox1.Controls.Add(this.buttonBrowse);
            this.groupBox1.Controls.Add(this.buttonCancel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(369, 285);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "What type of data do you have?";
            // 
            // radioButtonNCBiGenePeptFull
            // 
            this.radioButtonNCBiGenePeptFull.AutoSize = true;
            this.radioButtonNCBiGenePeptFull.Location = new System.Drawing.Point(24, 63);
            this.radioButtonNCBiGenePeptFull.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonNCBiGenePeptFull.Name = "radioButtonNCBiGenePeptFull";
            this.radioButtonNCBiGenePeptFull.Size = new System.Drawing.Size(160, 21);
            this.radioButtonNCBiGenePeptFull.TabIndex = 14;
            this.radioButtonNCBiGenePeptFull.Text = "NCBi (GenePept full)";
            this.radioButtonNCBiGenePeptFull.UseVisualStyleBackColor = true;
            // 
            // numericUpDownGoColumn
            // 
            this.numericUpDownGoColumn.Enabled = false;
            this.numericUpDownGoColumn.Location = new System.Drawing.Point(257, 194);
            this.numericUpDownGoColumn.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownGoColumn.Name = "numericUpDownGoColumn";
            this.numericUpDownGoColumn.Size = new System.Drawing.Size(84, 22);
            this.numericUpDownGoColumn.TabIndex = 13;
            this.numericUpDownGoColumn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonUniProt
            // 
            this.radioButtonUniProt.AutoSize = true;
            this.radioButtonUniProt.Checked = true;
            this.radioButtonUniProt.Location = new System.Drawing.Point(24, 34);
            this.radioButtonUniProt.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonUniProt.Name = "radioButtonUniProt";
            this.radioButtonUniProt.Size = new System.Drawing.Size(256, 21);
            this.radioButtonUniProt.TabIndex = 4;
            this.radioButtonUniProt.TabStop = true;
            this.radioButtonUniProt.Text = "Uniprot (Downloaded as text format)";
            this.radioButtonUniProt.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMyColumn
            // 
            this.numericUpDownMyColumn.Enabled = false;
            this.numericUpDownMyColumn.Location = new System.Drawing.Point(257, 162);
            this.numericUpDownMyColumn.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownMyColumn.Name = "numericUpDownMyColumn";
            this.numericUpDownMyColumn.Size = new System.Drawing.Size(84, 22);
            this.numericUpDownMyColumn.TabIndex = 12;
            // 
            // radioButtonIPI
            // 
            this.radioButtonIPI.AutoSize = true;
            this.radioButtonIPI.Location = new System.Drawing.Point(24, 92);
            this.radioButtonIPI.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonIPI.Name = "radioButtonIPI";
            this.radioButtonIPI.Size = new System.Drawing.Size(222, 21);
            this.radioButtonIPI.TabIndex = 5;
            this.radioButtonIPI.Text = "International Protein Index (IPI)";
            this.radioButtonIPI.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 194);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "GO Column";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 164);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "My gene column no (starting at 0)";
            // 
            // radioButtonUnstandard
            // 
            this.radioButtonUnstandard.AutoSize = true;
            this.radioButtonUnstandard.Location = new System.Drawing.Point(24, 121);
            this.radioButtonUnstandard.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonUnstandard.Name = "radioButtonUnstandard";
            this.radioButtonUnstandard.Size = new System.Drawing.Size(310, 21);
            this.radioButtonUnstandard.TabIndex = 7;
            this.radioButtonUnstandard.Text = "I have my own tab delimited conversion table";
            this.radioButtonUnstandard.UseVisualStyleBackColor = true;
            this.radioButtonUnstandard.CheckedChanged += new System.EventHandler(this.radioButtonUnstandard_CheckedChanged);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(136, 227);
            this.buttonBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(205, 28);
            this.buttonBrowse.TabIndex = 9;
            this.buttonBrowse.Text = "Browse for conversion file";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(25, 227);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ChooseParsing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 328);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ChooseParsing";
            this.Text = "Choose Parsing";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGoColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMyColumn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonUniProt;
        private System.Windows.Forms.RadioButton radioButtonIPI;
        private System.Windows.Forms.RadioButton radioButtonUnstandard;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownMyColumn;
        private System.Windows.Forms.NumericUpDown numericUpDownGoColumn;
        private System.Windows.Forms.RadioButton radioButtonNCBiGenePeptFull;
    }
}