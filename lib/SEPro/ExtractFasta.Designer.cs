namespace SEProcessor
{
    partial class ExtractFasta
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
            this.buttonGo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBoxFastaIDs = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(12, 180);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(389, 23);
            this.buttonGo.TabIndex = 0;
            this.buttonGo.Text = "GO";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBoxFastaIDs);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 162);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter the fasta IDs you wish to extract (1 per line)";
            // 
            // richTextBoxFastaIDs
            // 
            this.richTextBoxFastaIDs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxFastaIDs.Location = new System.Drawing.Point(3, 16);
            this.richTextBoxFastaIDs.Name = "richTextBoxFastaIDs";
            this.richTextBoxFastaIDs.Size = new System.Drawing.Size(383, 143);
            this.richTextBoxFastaIDs.TabIndex = 0;
            this.richTextBoxFastaIDs.Text = "";
            // 
            // ExtractFasta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 215);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonGo);
            this.Name = "ExtractFasta";
            this.Text = "ExtractFasta";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBoxFastaIDs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}