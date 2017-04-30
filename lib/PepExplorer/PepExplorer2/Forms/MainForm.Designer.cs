namespace PepExplorer2.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.programMainControl1 = new PepExplorer2.Forms.ProgramMainControl();
            this.SuspendLayout();
            // 
            // programMainControl1
            // 
            this.programMainControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programMainControl1.Location = new System.Drawing.Point(0, 0);
            this.programMainControl1.Name = "programMainControl1";
            this.programMainControl1.Size = new System.Drawing.Size(701, 343);
            this.programMainControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 343);
            this.Controls.Add(this.programMainControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(717, 357);
            this.Name = "MainForm";
            this.Text = "PepExplorer 2.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ProgramMainControl programMainControl1;
    }
}