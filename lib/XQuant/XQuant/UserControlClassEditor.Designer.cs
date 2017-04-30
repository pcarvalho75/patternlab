namespace XQuant
{
    partial class UserControlClassEditor
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
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.dataGridViewQuantPckgs = new System.Windows.Forms.DataGridView();
            this.groupBoxMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuantPckgs)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.dataGridViewQuantPckgs);
            this.groupBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMain.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(565, 312);
            this.groupBoxMain.TabIndex = 0;
            this.groupBoxMain.TabStop = false;
            this.groupBoxMain.Text = "Class Editor";
            // 
            // dataGridViewQuantPckgs
            // 
            this.dataGridViewQuantPckgs.AllowUserToAddRows = false;
            this.dataGridViewQuantPckgs.AllowUserToDeleteRows = false;
            this.dataGridViewQuantPckgs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewQuantPckgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewQuantPckgs.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewQuantPckgs.Name = "dataGridViewQuantPckgs";
            this.dataGridViewQuantPckgs.Size = new System.Drawing.Size(559, 293);
            this.dataGridViewQuantPckgs.TabIndex = 0;
            this.dataGridViewQuantPckgs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewQuantPckgs_CellContentClick);
            // 
            // UserControlClassEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMain);
            this.Name = "UserControlClassEditor";
            this.Size = new System.Drawing.Size(565, 312);
            this.groupBoxMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuantPckgs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.DataGridView dataGridViewQuantPckgs;
    }
}
