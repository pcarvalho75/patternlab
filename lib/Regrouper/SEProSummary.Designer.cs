namespace Regrouper
{
    partial class SEProSummary
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
            this.richTextBoxSummary = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxSummary
            // 
            this.richTextBoxSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxSummary.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxSummary.Name = "richTextBoxSummary";
            this.richTextBoxSummary.Size = new System.Drawing.Size(743, 261);
            this.richTextBoxSummary.TabIndex = 0;
            this.richTextBoxSummary.Text = "";
            this.richTextBoxSummary.TextChanged += new System.EventHandler(this.richTextBoxSummary_TextChanged);
            // 
            // SEProSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 261);
            this.Controls.Add(this.richTextBoxSummary);
            this.Name = "SEProSummary";
            this.Text = "SEProSummary";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxSummary;
    }
}