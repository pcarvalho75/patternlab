namespace NCBIExtractor
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
            this.dbPrepareControl1 = new NCBIExtractor.DBPrepareControl();
            this.SuspendLayout();
            // 
            // dbPrepareControl1
            // 
            this.dbPrepareControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbPrepareControl1.Location = new System.Drawing.Point(0, 0);
            this.dbPrepareControl1.Name = "dbPrepareControl1";
            this.dbPrepareControl1.Size = new System.Drawing.Size(667, 465);
            this.dbPrepareControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 465);
            this.Controls.Add(this.dbPrepareControl1);
            this.Name = "Form1";
            this.Text = "Prepare a search database";
            this.ResumeLayout(false);

        }

        #endregion

        private DBPrepareControl dbPrepareControl1;
    }
}