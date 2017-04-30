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
            this.cometWrapper1 = new CometWrapper.CometWrapper();
            this.SuspendLayout();
            // 
            // cometWrapper1
            // 
            this.cometWrapper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cometWrapper1.Location = new System.Drawing.Point(0, 0);
            this.cometWrapper1.Name = "cometWrapper1";
            this.cometWrapper1.Size = new System.Drawing.Size(763, 499);
            this.cometWrapper1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 499);
            this.Controls.Add(this.cometWrapper1);
            this.Name = "Form1";
            this.Text = "Comet PSM";
            this.ResumeLayout(false);

        }

        #endregion

        private CometWrapper.CometWrapper cometWrapper1;
    }
}

