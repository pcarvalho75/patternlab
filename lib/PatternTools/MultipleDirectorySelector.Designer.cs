namespace PatternTools
{
    partial class MultipleDirectorySelector
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
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.splitContainerInput = new System.Windows.Forms.SplitContainer();
            this.buttonAddDirectory = new System.Windows.Forms.Button();
            this.dataGridViewInput = new System.Windows.Forms.DataGridView();
            this.ColumnDirectory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnClassDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInput)).BeginInit();
            this.splitContainerInput.Panel1.SuspendLayout();
            this.splitContainerInput.Panel2.SuspendLayout();
            this.splitContainerInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.splitContainerInput);
            this.groupBoxInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxInput.Location = new System.Drawing.Point(0, 0);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(491, 326);
            this.groupBoxInput.TabIndex = 2;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input :: Directory path";
            // 
            // splitContainerInput
            // 
            this.splitContainerInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerInput.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerInput.Location = new System.Drawing.Point(3, 16);
            this.splitContainerInput.Name = "splitContainerInput";
            this.splitContainerInput.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerInput.Panel1
            // 
            this.splitContainerInput.Panel1.Controls.Add(this.buttonAddDirectory);
            // 
            // splitContainerInput.Panel2
            // 
            this.splitContainerInput.Panel2.Controls.Add(this.dataGridViewInput);
            this.splitContainerInput.Size = new System.Drawing.Size(485, 307);
            this.splitContainerInput.SplitterDistance = 25;
            this.splitContainerInput.TabIndex = 0;
            // 
            // buttonAddDirectory
            // 
            this.buttonAddDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAddDirectory.Location = new System.Drawing.Point(0, 0);
            this.buttonAddDirectory.Name = "buttonAddDirectory";
            this.buttonAddDirectory.Size = new System.Drawing.Size(485, 25);
            this.buttonAddDirectory.TabIndex = 0;
            this.buttonAddDirectory.Text = "Add directory";
            this.buttonAddDirectory.UseVisualStyleBackColor = true;
            this.buttonAddDirectory.Click += new System.EventHandler(this.buttonAddDirectory_Click);
            // 
            // dataGridViewInput
            // 
            this.dataGridViewInput.AllowUserToOrderColumns = true;
            this.dataGridViewInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDirectory,
            this.ColumnClassDescription});
            this.dataGridViewInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewInput.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewInput.Name = "dataGridViewInput";
            this.dataGridViewInput.Size = new System.Drawing.Size(485, 278);
            this.dataGridViewInput.TabIndex = 0;
            this.dataGridViewInput.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInput_CellEndEdit);
            // 
            // ColumnDirectory
            // 
            this.ColumnDirectory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnDirectory.HeaderText = "Directory";
            this.ColumnDirectory.Name = "ColumnDirectory";
            this.ColumnDirectory.Width = 74;
            // 
            // ColumnClassDescription
            // 
            this.ColumnClassDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnClassDescription.HeaderText = "ClassDescription";
            this.ColumnClassDescription.Name = "ColumnClassDescription";
            // 
            // MultipleDirectorySelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxInput);
            this.Name = "MultipleDirectorySelector";
            this.Size = new System.Drawing.Size(491, 326);
            this.groupBoxInput.ResumeLayout(false);
            this.splitContainerInput.Panel1.ResumeLayout(false);
            this.splitContainerInput.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInput)).EndInit();
            this.splitContainerInput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.SplitContainer splitContainerInput;
        private System.Windows.Forms.Button buttonAddDirectory;
        private System.Windows.Forms.DataGridView dataGridViewInput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDirectory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnClassDescription;
    }
}
