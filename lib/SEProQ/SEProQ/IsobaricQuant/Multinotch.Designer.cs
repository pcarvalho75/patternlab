namespace SEProQ.IsobaricQuant
{
    partial class Multinotch
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
            this.textBoxSeproFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMS3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGo = new System.Windows.Forms.Button();
            this.buttonBrowseSEProFile = new System.Windows.Forms.Button();
            this.buttonBrowseMS3Directory = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxMultiNotch = new System.Windows.Forms.GroupBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxMultiNotch.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxSeproFile
            // 
            this.textBoxSeproFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSeproFile.Location = new System.Drawing.Point(99, 26);
            this.textBoxSeproFile.Name = "textBoxSeproFile";
            this.textBoxSeproFile.Size = new System.Drawing.Size(369, 20);
            this.textBoxSeproFile.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SEPro File:";
            // 
            // textBoxMS3
            // 
            this.textBoxMS3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMS3.Location = new System.Drawing.Point(99, 52);
            this.textBoxMS3.Name = "textBoxMS3";
            this.textBoxMS3.Size = new System.Drawing.Size(369, 20);
            this.textBoxMS3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MS3 Dir:";
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(14, 79);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(535, 23);
            this.buttonGo.TabIndex = 8;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // buttonBrowseSEProFile
            // 
            this.buttonBrowseSEProFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseSEProFile.Location = new System.Drawing.Point(474, 24);
            this.buttonBrowseSEProFile.Name = "buttonBrowseSEProFile";
            this.buttonBrowseSEProFile.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseSEProFile.TabIndex = 11;
            this.buttonBrowseSEProFile.Text = "Browse";
            this.buttonBrowseSEProFile.UseVisualStyleBackColor = true;
            this.buttonBrowseSEProFile.Click += new System.EventHandler(this.buttonBrowseSEProFile_Click);
            // 
            // buttonBrowseMS3Directory
            // 
            this.buttonBrowseMS3Directory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseMS3Directory.Location = new System.Drawing.Point(474, 50);
            this.buttonBrowseMS3Directory.Name = "buttonBrowseMS3Directory";
            this.buttonBrowseMS3Directory.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseMS3Directory.TabIndex = 12;
            this.buttonBrowseMS3Directory.Text = "Browse";
            this.buttonBrowseMS3Directory.UseVisualStyleBackColor = true;
            this.buttonBrowseMS3Directory.Click += new System.EventHandler(this.buttonBrowseMS3Directory_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBoxMultiNotch
            // 
            this.groupBoxMultiNotch.Controls.Add(this.label1);
            this.groupBoxMultiNotch.Controls.Add(this.buttonBrowseMS3Directory);
            this.groupBoxMultiNotch.Controls.Add(this.textBoxSeproFile);
            this.groupBoxMultiNotch.Controls.Add(this.buttonBrowseSEProFile);
            this.groupBoxMultiNotch.Controls.Add(this.textBoxMS3);
            this.groupBoxMultiNotch.Controls.Add(this.buttonGo);
            this.groupBoxMultiNotch.Controls.Add(this.label2);
            this.groupBoxMultiNotch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMultiNotch.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMultiNotch.Name = "groupBoxMultiNotch";
            this.groupBoxMultiNotch.Size = new System.Drawing.Size(572, 121);
            this.groupBoxMultiNotch.TabIndex = 13;
            this.groupBoxMultiNotch.TabStop = false;
            this.groupBoxMultiNotch.Text = "Multinotch";
            // 
            // Multinotch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMultiNotch);
            this.Name = "Multinotch";
            this.Size = new System.Drawing.Size(572, 121);
            this.groupBoxMultiNotch.ResumeLayout(false);
            this.groupBoxMultiNotch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSeproFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMS3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Button buttonBrowseSEProFile;
        private System.Windows.Forms.Button buttonBrowseMS3Directory;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBoxMultiNotch;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
