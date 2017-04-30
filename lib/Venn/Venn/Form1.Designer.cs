namespace Venn
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
            this.vennControl1 = new Venn.VennControl();
            this.SuspendLayout();
            // 
            // vennControl1
            // 
            this.vennControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vennControl1.Location = new System.Drawing.Point(0, 0);
            this.vennControl1.Name = "vennControl1";
            this.vennControl1.Size = new System.Drawing.Size(743, 476);
            this.vennControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 476);
            this.Controls.Add(this.vennControl1);
            this.Name = "Form1";
            this.Text = "PatternLab\'s Venn";
            this.ResumeLayout(false);

        }

        #endregion

        private VennControl vennControl1;
    }
}

