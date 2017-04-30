namespace TrendQuest
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
            this.trendFinderControl1 = new TrendQuest.TrendQuestControl();
            this.SuspendLayout();
            // 
            // trendFinderControl1
            // 
            this.trendFinderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trendFinderControl1.Location = new System.Drawing.Point(0, 0);
            this.trendFinderControl1.Name = "trendFinderControl1";
            this.trendFinderControl1.Size = new System.Drawing.Size(740, 498);
            this.trendFinderControl1.TabIndex = 0;
            this.trendFinderControl1.Load += new System.EventHandler(this.trendFinderControl1_Load);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 498);
            this.Controls.Add(this.trendFinderControl1);
            this.Name = "MainForm";
            this.Text = "PatternLab\'s TrendQuest";
            this.ResumeLayout(false);

        }

        #endregion

        private TrendQuestControl trendFinderControl1;
    }
}

