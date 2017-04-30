namespace SEProcessor.Result
{
    partial class SequenceExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SequenceExplorer));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.sequenceViewer1 = new SEProcessor.Result.SequenceViewer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.globalProteinCoverage1 = new ProteinCoverageViewer.GlobalProteinCoverage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupGraph1 = new SEProcessor.Result.GroupGraph();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(743, 507);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.elementHost1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(735, 481);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Fasta Coverage";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(3, 3);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(729, 475);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.sequenceViewer1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.globalProteinCoverage1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(735, 481);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Protein Coverage";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // globalProteinCoverage1
            // 
            this.globalProteinCoverage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.globalProteinCoverage1.Location = new System.Drawing.Point(3, 3);
            this.globalProteinCoverage1.Name = "globalProteinCoverage1";
            this.globalProteinCoverage1.Size = new System.Drawing.Size(729, 475);
            this.globalProteinCoverage1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupGraph1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(735, 481);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Group view";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupGraph1
            // 
            this.groupGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupGraph1.g = null;
            this.groupGraph1.Location = new System.Drawing.Point(0, 0);
            this.groupGraph1.Name = "groupGraph1";
            this.groupGraph1.Size = new System.Drawing.Size(735, 481);
            this.groupGraph1.TabIndex = 0;
            // 
            // SequenceExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 507);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SequenceExplorer";
            this.Text = "Sequence Explorer";
            this.Load += new System.EventHandler(this.ProteinCoverage3_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private SequenceViewer sequenceViewer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private ProteinCoverageViewer.GlobalProteinCoverage globalProteinCoverage1;
        private GroupGraph groupGraph1;
    }
}