namespace XDScoreWindow
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
            this.xdScoreControl1 = new XDScore.XDScoreControl();
            this.SuspendLayout();
            // 
            // xdScoreControl1
            // 
            this.xdScoreControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xdScoreControl1.Location = new System.Drawing.Point(0, 0);
            this.xdScoreControl1.Name = "xdScoreControl1";
            this.xdScoreControl1.Size = new System.Drawing.Size(937, 588);
            this.xdScoreControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 588);
            this.Controls.Add(this.xdScoreControl1);
            this.Name = "Form1";
            this.Text = "XD Score Window";
            this.ResumeLayout(false);

        }

        #endregion

        private XDScore.XDScoreControl xdScoreControl1;
    }
}

