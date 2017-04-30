namespace PatternLab.Forms
{
    partial class WhatIsNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhatIsNew));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxUpdates = new System.Windows.Forms.GroupBox();
            this.richTextBoxUpdates = new System.Windows.Forms.RichTextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxUpdates.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 1;
            // 
            // groupBoxUpdates
            // 
            this.groupBoxUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxUpdates.Controls.Add(this.richTextBoxUpdates);
            this.groupBoxUpdates.Location = new System.Drawing.Point(18, 20);
            this.groupBoxUpdates.Name = "groupBoxUpdates";
            this.groupBoxUpdates.Size = new System.Drawing.Size(1030, 497);
            this.groupBoxUpdates.TabIndex = 3;
            this.groupBoxUpdates.TabStop = false;
            this.groupBoxUpdates.Text = "Updates";
            // 
            // richTextBoxUpdates
            // 
            this.richTextBoxUpdates.BackColor = System.Drawing.Color.Azure;
            this.richTextBoxUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxUpdates.Location = new System.Drawing.Point(3, 22);
            this.richTextBoxUpdates.Name = "richTextBoxUpdates";
            this.richTextBoxUpdates.Size = new System.Drawing.Size(1024, 472);
            this.richTextBoxUpdates.TabIndex = 0;
            this.richTextBoxUpdates.Text = resources.GetString("richTextBoxUpdates.Text");
            this.richTextBoxUpdates.WordWrap = false;
            this.richTextBoxUpdates.TextChanged += new System.EventHandler(this.richTextBoxUpdates_TextChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(18, 523);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(1030, 40);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // WhatIsNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 575);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxUpdates);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "WhatIsNew";
            this.Text = "What Is New?";
            this.groupBoxUpdates.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxUpdates;
        private System.Windows.Forms.RichTextBox richTextBoxUpdates;
        private System.Windows.Forms.Button buttonOk;
    }
}