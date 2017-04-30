namespace SEProcessor
{
    partial class SaveMS2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveMS2));
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxZ1 = new System.Windows.Forms.CheckBox();
            this.checkBoxZ2 = new System.Windows.Forms.CheckBox();
            this.checkBoxZ3 = new System.Windows.Forms.CheckBox();
            this.checkBoxZ4andUP = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxZ1
            // 
            this.checkBoxZ1.AutoSize = true;
            this.checkBoxZ1.Location = new System.Drawing.Point(6, 33);
            this.checkBoxZ1.Name = "checkBoxZ1";
            this.checkBoxZ1.Size = new System.Drawing.Size(38, 17);
            this.checkBoxZ1.TabIndex = 1;
            this.checkBoxZ1.Text = "+1";
            this.checkBoxZ1.UseVisualStyleBackColor = true;
            // 
            // checkBoxZ2
            // 
            this.checkBoxZ2.AutoSize = true;
            this.checkBoxZ2.Checked = true;
            this.checkBoxZ2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxZ2.Location = new System.Drawing.Point(51, 33);
            this.checkBoxZ2.Name = "checkBoxZ2";
            this.checkBoxZ2.Size = new System.Drawing.Size(38, 17);
            this.checkBoxZ2.TabIndex = 2;
            this.checkBoxZ2.Text = "+2";
            this.checkBoxZ2.UseVisualStyleBackColor = true;
            // 
            // checkBoxZ3
            // 
            this.checkBoxZ3.AutoSize = true;
            this.checkBoxZ3.Checked = true;
            this.checkBoxZ3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxZ3.Location = new System.Drawing.Point(96, 33);
            this.checkBoxZ3.Name = "checkBoxZ3";
            this.checkBoxZ3.Size = new System.Drawing.Size(38, 17);
            this.checkBoxZ3.TabIndex = 3;
            this.checkBoxZ3.Text = "+3";
            this.checkBoxZ3.UseVisualStyleBackColor = true;
            // 
            // checkBoxZ4andUP
            // 
            this.checkBoxZ4andUP.AutoSize = true;
            this.checkBoxZ4andUP.Checked = true;
            this.checkBoxZ4andUP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxZ4andUP.Location = new System.Drawing.Point(141, 33);
            this.checkBoxZ4andUP.Name = "checkBoxZ4andUP";
            this.checkBoxZ4andUP.Size = new System.Drawing.Size(44, 17);
            this.checkBoxZ4andUP.TabIndex = 4;
            this.checkBoxZ4andUP.Text = ">+3";
            this.checkBoxZ4andUP.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxZ1);
            this.groupBox1.Controls.Add(this.checkBoxZ4andUP);
            this.groupBox1.Controls.Add(this.checkBoxZ2);
            this.groupBox1.Controls.Add(this.checkBoxZ3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 73);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Charge Filter";
            // 
            // SaveMS2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 135);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveMS2";
            this.Text = "Save MS2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxZ1;
        private System.Windows.Forms.CheckBox checkBoxZ2;
        private System.Windows.Forms.CheckBox checkBoxZ3;
        private System.Windows.Forms.CheckBox checkBoxZ4andUP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}