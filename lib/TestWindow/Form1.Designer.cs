namespace TestWindow
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
            this.dtaselectSimpleParser1 = new DTAParserClass.dtaselectSimpleParser();
            this.SuspendLayout();
            // 
            // dtaselectSimpleParser1
            // 
            this.dtaselectSimpleParser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtaselectSimpleParser1.Location = new System.Drawing.Point(0, 0);
            this.dtaselectSimpleParser1.Margin = new System.Windows.Forms.Padding(2);
            this.dtaselectSimpleParser1.Name = "dtaselectSimpleParser1";
            this.dtaselectSimpleParser1.Size = new System.Drawing.Size(674, 443);
            this.dtaselectSimpleParser1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 443);
            this.Controls.Add(this.dtaselectSimpleParser1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private DTAParserClass.dtaselectSimpleParser dtaselectSimpleParser1;
    }
}

