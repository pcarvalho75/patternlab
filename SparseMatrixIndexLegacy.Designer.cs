namespace PatternLab
{
    partial class SparseMatrixIndexLegacy
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxIndex = new System.Windows.Forms.TextBox();
            this.textBoxSparseMatrix = new System.Windows.Forms.TextBox();
            this.textBoxProjectDescription = new System.Windows.Forms.TextBox();
            this.buttonBrowseIndex = new System.Windows.Forms.Button();
            this.buttonBrowseSparseMatrix = new System.Windows.Forms.Button();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonConvert);
            this.groupBox1.Controls.Add(this.buttonBrowseSparseMatrix);
            this.groupBox1.Controls.Add(this.buttonBrowseIndex);
            this.groupBox1.Controls.Add(this.textBoxProjectDescription);
            this.groupBox1.Controls.Add(this.textBoxSparseMatrix);
            this.groupBox1.Controls.Add(this.textBoxIndex);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 246);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Convert Index and SparseMatrix files to the new PatternLabProject file";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Index";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "SparseMatrix";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Project description";
            // 
            // textBoxIndex
            // 
            this.textBoxIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIndex.Location = new System.Drawing.Point(130, 41);
            this.textBoxIndex.Name = "textBoxIndex";
            this.textBoxIndex.Size = new System.Drawing.Size(436, 20);
            this.textBoxIndex.TabIndex = 3;
            // 
            // textBoxSparseMatrix
            // 
            this.textBoxSparseMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSparseMatrix.Location = new System.Drawing.Point(130, 81);
            this.textBoxSparseMatrix.Name = "textBoxSparseMatrix";
            this.textBoxSparseMatrix.Size = new System.Drawing.Size(436, 20);
            this.textBoxSparseMatrix.TabIndex = 4;
            // 
            // textBoxProjectDescription
            // 
            this.textBoxProjectDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProjectDescription.Location = new System.Drawing.Point(130, 126);
            this.textBoxProjectDescription.Name = "textBoxProjectDescription";
            this.textBoxProjectDescription.Size = new System.Drawing.Size(436, 20);
            this.textBoxProjectDescription.TabIndex = 5;
            // 
            // buttonBrowseIndex
            // 
            this.buttonBrowseIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseIndex.Location = new System.Drawing.Point(572, 39);
            this.buttonBrowseIndex.Name = "buttonBrowseIndex";
            this.buttonBrowseIndex.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseIndex.TabIndex = 6;
            this.buttonBrowseIndex.Text = "Browse";
            this.buttonBrowseIndex.UseVisualStyleBackColor = true;
            this.buttonBrowseIndex.Click += new System.EventHandler(this.buttonBrowseIndex_Click);
            // 
            // buttonBrowseSparseMatrix
            // 
            this.buttonBrowseSparseMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseSparseMatrix.Location = new System.Drawing.Point(572, 79);
            this.buttonBrowseSparseMatrix.Name = "buttonBrowseSparseMatrix";
            this.buttonBrowseSparseMatrix.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseSparseMatrix.TabIndex = 7;
            this.buttonBrowseSparseMatrix.Text = "Browse";
            this.buttonBrowseSparseMatrix.UseVisualStyleBackColor = true;
            this.buttonBrowseSparseMatrix.Click += new System.EventHandler(this.buttonBrowseSparseMatrix_Click);
            // 
            // buttonConvert
            // 
            this.buttonConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConvert.Location = new System.Drawing.Point(130, 168);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(517, 23);
            this.buttonConvert.TabIndex = 8;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SparseMatrixIndexLegacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SparseMatrixIndexLegacy";
            this.Size = new System.Drawing.Size(665, 246);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.Button buttonBrowseSparseMatrix;
        private System.Windows.Forms.Button buttonBrowseIndex;
        private System.Windows.Forms.TextBox textBoxProjectDescription;
        private System.Windows.Forms.TextBox textBoxSparseMatrix;
        private System.Windows.Forms.TextBox textBoxIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
