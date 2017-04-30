namespace XQuantPairWiseAnalyzer
{
    partial class MainForm
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
            this.userControlPairAnalyzer1 = new XQuantPairWiseAnalyzer.UserControlPairAnalyzer();
            this.SuspendLayout();
            // 
            // userControlPairAnalyzer1
            // 
            this.userControlPairAnalyzer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlPairAnalyzer1.Location = new System.Drawing.Point(0, 0);
            this.userControlPairAnalyzer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userControlPairAnalyzer1.Name = "userControlPairAnalyzer1";
            this.userControlPairAnalyzer1.Size = new System.Drawing.Size(785, 484);
            this.userControlPairAnalyzer1.TabIndex = 0;
            this.userControlPairAnalyzer1.Load += new System.EventHandler(this.userControlPairAnalyzer1_Load);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 484);
            this.Controls.Add(this.userControlPairAnalyzer1);
            this.Name = "MainForm";
            this.Text = "XQuant Paired analyzer";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControlPairAnalyzer userControlPairAnalyzer1;
    }
}

