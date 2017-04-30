namespace GO
{
    partial class SearchAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchAll));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxFDR = new System.Windows.Forms.CheckBox();
            this.numericUpDownHyperGeometricPValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFDRAlfa = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinimumNumberOfTerms = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinimumDepth = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonWork = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHyperGeometricPValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFDRAlfa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumNumberOfTerms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Hypergeometric p-value cutoff";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Min. no. if IDs mapped to term";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Minimum Depth";
            // 
            // checkBoxFDR
            // 
            this.checkBoxFDR.AutoSize = true;
            this.checkBoxFDR.Location = new System.Drawing.Point(60, 60);
            this.checkBoxFDR.Name = "checkBoxFDR";
            this.checkBoxFDR.Size = new System.Drawing.Size(95, 17);
            this.checkBoxFDR.TabIndex = 13;
            this.checkBoxFDR.Text = "Apply BH-FDR";
            this.checkBoxFDR.UseVisualStyleBackColor = true;
            this.checkBoxFDR.CheckedChanged += new System.EventHandler(this.checkBoxFDR_CheckedChanged);
            // 
            // numericUpDownHyperGeometricPValue
            // 
            this.numericUpDownHyperGeometricPValue.DecimalPlaces = 4;
            this.numericUpDownHyperGeometricPValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownHyperGeometricPValue.Location = new System.Drawing.Point(212, 33);
            this.numericUpDownHyperGeometricPValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHyperGeometricPValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownHyperGeometricPValue.Name = "numericUpDownHyperGeometricPValue";
            this.numericUpDownHyperGeometricPValue.Size = new System.Drawing.Size(72, 20);
            this.numericUpDownHyperGeometricPValue.TabIndex = 14;
            this.numericUpDownHyperGeometricPValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // numericUpDownFDRAlfa
            // 
            this.numericUpDownFDRAlfa.DecimalPlaces = 2;
            this.numericUpDownFDRAlfa.Enabled = false;
            this.numericUpDownFDRAlfa.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownFDRAlfa.Location = new System.Drawing.Point(212, 59);
            this.numericUpDownFDRAlfa.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFDRAlfa.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownFDRAlfa.Name = "numericUpDownFDRAlfa";
            this.numericUpDownFDRAlfa.Size = new System.Drawing.Size(72, 20);
            this.numericUpDownFDRAlfa.TabIndex = 15;
            this.numericUpDownFDRAlfa.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // numericUpDownMinimumNumberOfTerms
            // 
            this.numericUpDownMinimumNumberOfTerms.Location = new System.Drawing.Point(211, 85);
            this.numericUpDownMinimumNumberOfTerms.Name = "numericUpDownMinimumNumberOfTerms";
            this.numericUpDownMinimumNumberOfTerms.Size = new System.Drawing.Size(72, 20);
            this.numericUpDownMinimumNumberOfTerms.TabIndex = 16;
            this.numericUpDownMinimumNumberOfTerms.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numericUpDownMinimumDepth
            // 
            this.numericUpDownMinimumDepth.Location = new System.Drawing.Point(211, 111);
            this.numericUpDownMinimumDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinimumDepth.Name = "numericUpDownMinimumDepth";
            this.numericUpDownMinimumDepth.Size = new System.Drawing.Size(72, 20);
            this.numericUpDownMinimumDepth.TabIndex = 17;
            this.numericUpDownMinimumDepth.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(60, 137);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonWork
            // 
            this.buttonWork.Location = new System.Drawing.Point(141, 137);
            this.buttonWork.Name = "buttonWork";
            this.buttonWork.Size = new System.Drawing.Size(142, 23);
            this.buttonWork.TabIndex = 19;
            this.buttonWork.Text = "Work";
            this.buttonWork.UseVisualStyleBackColor = true;
            this.buttonWork.Click += new System.EventHandler(this.buttonWork_Click);
            // 
            // SearchAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 208);
            this.Controls.Add(this.buttonWork);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.numericUpDownMinimumDepth);
            this.Controls.Add(this.numericUpDownMinimumNumberOfTerms);
            this.Controls.Add(this.numericUpDownFDRAlfa);
            this.Controls.Add(this.numericUpDownHyperGeometricPValue);
            this.Controls.Add(this.checkBoxFDR);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SearchAll";
            this.Text = "Automatic mode - search all";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHyperGeometricPValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFDRAlfa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumNumberOfTerms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumDepth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxFDR;
        private System.Windows.Forms.NumericUpDown numericUpDownHyperGeometricPValue;
        private System.Windows.Forms.NumericUpDown numericUpDownFDRAlfa;
        private System.Windows.Forms.NumericUpDown numericUpDownMinimumNumberOfTerms;
        private System.Windows.Forms.NumericUpDown numericUpDownMinimumDepth;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonWork;
    }
}