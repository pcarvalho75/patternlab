namespace XQuant
{
    partial class XICToy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XICToy));
            this.coreXtractor1 = new XQuant.CoreXtractor();
            this.SuspendLayout();
            // 
            // coreXtractor1
            // 
            this.coreXtractor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coreXtractor1.Location = new System.Drawing.Point(0, 0);
            this.coreXtractor1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.coreXtractor1.Name = "coreXtractor1";
            this.coreXtractor1.Size = new System.Drawing.Size(706, 598);
            this.coreXtractor1.TabIndex = 0;
            // 
            // XICToy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 598);
            this.Controls.Add(this.coreXtractor1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XICToy";
            this.Text = "XICToy";
            this.ResumeLayout(false);

        }

        #endregion

        private CoreXtractor coreXtractor1;
    }
}