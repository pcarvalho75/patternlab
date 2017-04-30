namespace SEProQ.Acetylation
{
    partial class AcetylationQuant
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMS1SEProDirectory = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonGO = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMediumMarker = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxHeavyMarker = new System.Windows.Forms.TextBox();
            this.numericUpDownSearchSpace = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSearchSpace)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "MS1 & Sepro Directory";
            // 
            // textBoxMS1SEProDirectory
            // 
            this.textBoxMS1SEProDirectory.Location = new System.Drawing.Point(97, 13);
            this.textBoxMS1SEProDirectory.Name = "textBoxMS1SEProDirectory";
            this.textBoxMS1SEProDirectory.Size = new System.Drawing.Size(205, 20);
            this.textBoxMS1SEProDirectory.TabIndex = 3;
            this.textBoxMS1SEProDirectory.Text = "C:\\Users\\pcarvalho\\Desktop\\Andres\\1";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(308, 11);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 5;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonGO
            // 
            this.buttonGO.Location = new System.Drawing.Point(20, 110);
            this.buttonGO.Name = "buttonGO";
            this.buttonGO.Size = new System.Drawing.Size(363, 23);
            this.buttonGO.TabIndex = 6;
            this.buttonGO.Text = "GO";
            this.buttonGO.UseVisualStyleBackColor = true;
            this.buttonGO.Click += new System.EventHandler(this.buttonGO_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Medium Marker";
            // 
            // textBoxMediumMarker
            // 
            this.textBoxMediumMarker.Location = new System.Drawing.Point(97, 49);
            this.textBoxMediumMarker.Name = "textBoxMediumMarker";
            this.textBoxMediumMarker.Size = new System.Drawing.Size(100, 20);
            this.textBoxMediumMarker.TabIndex = 8;
            this.textBoxMediumMarker.Text = "42.0098";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(203, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Heavy Marker";
            // 
            // textBoxHeavyMarker
            // 
            this.textBoxHeavyMarker.Location = new System.Drawing.Point(283, 49);
            this.textBoxHeavyMarker.Name = "textBoxHeavyMarker";
            this.textBoxHeavyMarker.Size = new System.Drawing.Size(100, 20);
            this.textBoxHeavyMarker.TabIndex = 10;
            this.textBoxHeavyMarker.Text = "45.0261";
            // 
            // numericUpDownSearchSpace
            // 
            this.numericUpDownSearchSpace.Location = new System.Drawing.Point(97, 75);
            this.numericUpDownSearchSpace.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownSearchSpace.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSearchSpace.Name = "numericUpDownSearchSpace";
            this.numericUpDownSearchSpace.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownSearchSpace.TabIndex = 11;
            this.numericUpDownSearchSpace.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Search Space";
            // 
            // AcetylationQuant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownSearchSpace);
            this.Controls.Add(this.textBoxHeavyMarker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxMediumMarker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonGO);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxMS1SEProDirectory);
            this.Controls.Add(this.label2);
            this.Name = "AcetylationQuant";
            this.Size = new System.Drawing.Size(405, 150);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSearchSpace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMS1SEProDirectory;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonGO;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMediumMarker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxHeavyMarker;
        private System.Windows.Forms.NumericUpDown numericUpDownSearchSpace;
        private System.Windows.Forms.Label label5;
    }
}
