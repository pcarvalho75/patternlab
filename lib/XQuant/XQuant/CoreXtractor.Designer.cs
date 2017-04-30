namespace XQuant
{
    partial class CoreXtractor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxCore = new System.Windows.Forms.GroupBox();
            this.groupBoxXtract = new System.Windows.Forms.GroupBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.buttonXtract = new System.Windows.Forms.Button();
            this.textBoxTheoreticalMass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxScanNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.parameters1 = new XQuant.Parameters();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxCore.SuspendLayout();
            this.groupBoxXtract.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCore
            // 
            this.groupBoxCore.Controls.Add(this.groupBoxXtract);
            this.groupBoxCore.Controls.Add(this.parameters1);
            this.groupBoxCore.Controls.Add(this.buttonLoad);
            this.groupBoxCore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCore.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCore.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxCore.Name = "groupBoxCore";
            this.groupBoxCore.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxCore.Size = new System.Drawing.Size(714, 565);
            this.groupBoxCore.TabIndex = 0;
            this.groupBoxCore.TabStop = false;
            this.groupBoxCore.Text = "Core Xtractor";
            // 
            // groupBoxXtract
            // 
            this.groupBoxXtract.Controls.Add(this.richTextBoxLog);
            this.groupBoxXtract.Controls.Add(this.buttonXtract);
            this.groupBoxXtract.Controls.Add(this.textBoxTheoreticalMass);
            this.groupBoxXtract.Controls.Add(this.label2);
            this.groupBoxXtract.Controls.Add(this.textBoxScanNo);
            this.groupBoxXtract.Controls.Add(this.label1);
            this.groupBoxXtract.Enabled = false;
            this.groupBoxXtract.Location = new System.Drawing.Point(9, 190);
            this.groupBoxXtract.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxXtract.Name = "groupBoxXtract";
            this.groupBoxXtract.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxXtract.Size = new System.Drawing.Size(660, 255);
            this.groupBoxXtract.TabIndex = 2;
            this.groupBoxXtract.TabStop = false;
            this.groupBoxXtract.Text = "Xtract";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(346, 29);
            this.richTextBoxLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(302, 215);
            this.richTextBoxLog.TabIndex = 5;
            this.richTextBoxLog.Text = "";
            // 
            // buttonXtract
            // 
            this.buttonXtract.Location = new System.Drawing.Point(14, 118);
            this.buttonXtract.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonXtract.Name = "buttonXtract";
            this.buttonXtract.Size = new System.Drawing.Size(324, 35);
            this.buttonXtract.TabIndex = 4;
            this.buttonXtract.Text = "Xtract";
            this.buttonXtract.UseVisualStyleBackColor = true;
            this.buttonXtract.Click += new System.EventHandler(this.buttonXtract_Click);
            // 
            // textBoxTheoreticalMass
            // 
            this.textBoxTheoreticalMass.Location = new System.Drawing.Point(176, 29);
            this.textBoxTheoreticalMass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTheoreticalMass.Name = "textBoxTheoreticalMass";
            this.textBoxTheoreticalMass.Size = new System.Drawing.Size(160, 26);
            this.textBoxTheoreticalMass.TabIndex = 3;
            this.textBoxTheoreticalMass.Text = "1657.875541";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "MS2 Scan No Event";
            // 
            // textBoxScanNo
            // 
            this.textBoxScanNo.Location = new System.Drawing.Point(176, 69);
            this.textBoxScanNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxScanNo.Name = "textBoxScanNo";
            this.textBoxScanNo.Size = new System.Drawing.Size(160, 26);
            this.textBoxScanNo.TabIndex = 1;
            this.textBoxScanNo.Text = "28812";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "MH";
            // 
            // parameters1
            // 
            this.parameters1.Location = new System.Drawing.Point(9, 29);
            this.parameters1.Margin = new System.Windows.Forms.Padding(6);
            this.parameters1.Name = "parameters1";
            this.parameters1.Size = new System.Drawing.Size(660, 105);
            this.parameters1.TabIndex = 1;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(8, 145);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(660, 35);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Load .RAW, .ms1, or mzML file";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // CoreXtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCore);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CoreXtractor";
            this.Size = new System.Drawing.Size(714, 565);
            this.groupBoxCore.ResumeLayout(false);
            this.groupBoxXtract.ResumeLayout(false);
            this.groupBoxXtract.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCore;
        private System.Windows.Forms.Button buttonLoad;
        private Parameters parameters1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBoxXtract;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Button buttonXtract;
        private System.Windows.Forms.TextBox textBoxTheoreticalMass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxScanNo;
        private System.Windows.Forms.Label label1;
    }
}
