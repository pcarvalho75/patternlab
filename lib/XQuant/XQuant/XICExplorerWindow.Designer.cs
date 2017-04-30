namespace XQuant
{
    partial class XICExplorerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XICExplorerWindow));
            this.xicExplorer1 = new XQuant.XICExplorer();
            this.SuspendLayout();
            // 
            // xicExplorer1
            // 
            this.xicExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xicExplorer1.Location = new System.Drawing.Point(0, 0);
            this.xicExplorer1.Name = "xicExplorer1";
            this.xicExplorer1.Size = new System.Drawing.Size(694, 401);
            this.xicExplorer1.TabIndex = 0;
            // 
            // XICExplorerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 401);
            this.Controls.Add(this.xicExplorer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XICExplorerWindow";
            this.Text = "XIC Browser";
            this.ResumeLayout(false);

        }

        #endregion

        private XICExplorer xicExplorer1;
    }
}