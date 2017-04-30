namespace XQuant
{
    partial class Parameters
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
            this.groupBoxXQuantParams = new System.Windows.Forms.GroupBox();
            this.numericUpDownXICPPM = new System.Windows.Forms.NumericUpDown();
            this.textBoxAcceptableZ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxXQuantParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXICPPM)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxXQuantParams
            // 
            this.groupBoxXQuantParams.Controls.Add(this.numericUpDownXICPPM);
            this.groupBoxXQuantParams.Controls.Add(this.textBoxAcceptableZ);
            this.groupBoxXQuantParams.Controls.Add(this.label5);
            this.groupBoxXQuantParams.Controls.Add(this.label1);
            this.groupBoxXQuantParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxXQuantParams.Location = new System.Drawing.Point(0, 0);
            this.groupBoxXQuantParams.Name = "groupBoxXQuantParams";
            this.groupBoxXQuantParams.Size = new System.Drawing.Size(440, 142);
            this.groupBoxXQuantParams.TabIndex = 0;
            this.groupBoxXQuantParams.TabStop = false;
            this.groupBoxXQuantParams.Text = "XQuant Params";
            // 
            // numericUpDownXICPPM
            // 
            this.numericUpDownXICPPM.Location = new System.Drawing.Point(260, 31);
            this.numericUpDownXICPPM.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDownXICPPM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownXICPPM.Name = "numericUpDownXICPPM";
            this.numericUpDownXICPPM.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownXICPPM.TabIndex = 12;
            this.numericUpDownXICPPM.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // textBoxAcceptableZ
            // 
            this.textBoxAcceptableZ.Location = new System.Drawing.Point(141, 31);
            this.textBoxAcceptableZ.Name = "textBoxAcceptableZ";
            this.textBoxAcceptableZ.Size = new System.Drawing.Size(62, 20);
            this.textBoxAcceptableZ.TabIndex = 8;
            this.textBoxAcceptableZ.Text = "2, 3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Acceptable Charge States";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "XIC ppm";
            // 
            // Parameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxXQuantParams);
            this.Name = "Parameters";
            this.Size = new System.Drawing.Size(440, 142);
            this.groupBoxXQuantParams.ResumeLayout(false);
            this.groupBoxXQuantParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXICPPM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxXQuantParams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAcceptableZ;
        private System.Windows.Forms.NumericUpDown numericUpDownXICPPM;
    }
}
