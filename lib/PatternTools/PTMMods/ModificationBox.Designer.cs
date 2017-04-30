namespace PatternTools.PTMMods
{
    partial class ModificationBox
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
            this.groupBoxModificationBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewPTMs = new System.Windows.Forms.DataGridView();
            this.ColumnActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnPTM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDeltaMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIsVariable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnOnlyNTerminus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnOnlyCTerminus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.buttonLoadSoftwareDefaults = new System.Windows.Forms.Button();
            this.buttonUpdateLib = new System.Windows.Forms.Button();
            this.groupBoxModificationBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPTMs)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxModificationBox
            // 
            this.groupBoxModificationBox.Controls.Add(this.splitContainer1);
            this.groupBoxModificationBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxModificationBox.Location = new System.Drawing.Point(0, 0);
            this.groupBoxModificationBox.Name = "groupBoxModificationBox";
            this.groupBoxModificationBox.Size = new System.Drawing.Size(594, 153);
            this.groupBoxModificationBox.TabIndex = 0;
            this.groupBoxModificationBox.TabStop = false;
            this.groupBoxModificationBox.Text = "Selected Modifications";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewPTMs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonLoadSoftwareDefaults);
            this.splitContainer1.Panel2.Controls.Add(this.buttonUpdateLib);
            this.splitContainer1.Size = new System.Drawing.Size(588, 134);
            this.splitContainer1.SplitterDistance = 105;
            this.splitContainer1.TabIndex = 1;
            // 
            // dataGridViewPTMs
            // 
            this.dataGridViewPTMs.AllowUserToOrderColumns = true;
            this.dataGridViewPTMs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPTMs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnActive,
            this.ColumnPTM,
            this.ColumnAA,
            this.ColumnDeltaMass,
            this.ColumnIsVariable,
            this.ColumnOnlyNTerminus,
            this.ColumnOnlyCTerminus});
            this.dataGridViewPTMs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPTMs.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPTMs.Name = "dataGridViewPTMs";
            this.dataGridViewPTMs.RowHeadersVisible = false;
            this.dataGridViewPTMs.Size = new System.Drawing.Size(588, 105);
            this.dataGridViewPTMs.TabIndex = 0;
            this.dataGridViewPTMs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPTMs_CellContentClick);
            // 
            // ColumnActive
            // 
            this.ColumnActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnActive.HeaderText = "Active";
            this.ColumnActive.Name = "ColumnActive";
            this.ColumnActive.Width = 43;
            // 
            // ColumnPTM
            // 
            this.ColumnPTM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnPTM.HeaderText = "PTM Name";
            this.ColumnPTM.Name = "ColumnPTM";
            // 
            // ColumnAA
            // 
            this.ColumnAA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnAA.HeaderText = "AminoAcid";
            this.ColumnAA.Name = "ColumnAA";
            this.ColumnAA.Width = 82;
            // 
            // ColumnDeltaMass
            // 
            this.ColumnDeltaMass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnDeltaMass.HeaderText = "DeltaMass";
            this.ColumnDeltaMass.Name = "ColumnDeltaMass";
            this.ColumnDeltaMass.Width = 82;
            // 
            // ColumnIsVariable
            // 
            this.ColumnIsVariable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnIsVariable.HeaderText = "Is Variable";
            this.ColumnIsVariable.Name = "ColumnIsVariable";
            this.ColumnIsVariable.Width = 56;
            // 
            // ColumnOnlyNTerminus
            // 
            this.ColumnOnlyNTerminus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnOnlyNTerminus.HeaderText = "Only N Terminus";
            this.ColumnOnlyNTerminus.Name = "ColumnOnlyNTerminus";
            this.ColumnOnlyNTerminus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnOnlyNTerminus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnOnlyNTerminus.Width = 101;
            // 
            // ColumnOnlyCTerminus
            // 
            this.ColumnOnlyCTerminus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnOnlyCTerminus.HeaderText = "Only C Terminus";
            this.ColumnOnlyCTerminus.Name = "ColumnOnlyCTerminus";
            this.ColumnOnlyCTerminus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnOnlyCTerminus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // buttonLoadSoftwareDefaults
            // 
            this.buttonLoadSoftwareDefaults.Location = new System.Drawing.Point(181, 2);
            this.buttonLoadSoftwareDefaults.Name = "buttonLoadSoftwareDefaults";
            this.buttonLoadSoftwareDefaults.Size = new System.Drawing.Size(175, 23);
            this.buttonLoadSoftwareDefaults.TabIndex = 1;
            this.buttonLoadSoftwareDefaults.Text = "Load Software Defaults";
            this.buttonLoadSoftwareDefaults.UseVisualStyleBackColor = true;
            this.buttonLoadSoftwareDefaults.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonUpdateLib
            // 
            this.buttonUpdateLib.Location = new System.Drawing.Point(0, 2);
            this.buttonUpdateLib.Name = "buttonUpdateLib";
            this.buttonUpdateLib.Size = new System.Drawing.Size(175, 23);
            this.buttonUpdateLib.TabIndex = 0;
            this.buttonUpdateLib.Text = "Update Default Lib";
            this.buttonUpdateLib.UseVisualStyleBackColor = true;
            this.buttonUpdateLib.Click += new System.EventHandler(this.buttonUpdateLib_Click);
            // 
            // ModificationBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxModificationBox);
            this.Name = "ModificationBox";
            this.Size = new System.Drawing.Size(594, 153);
            this.groupBoxModificationBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPTMs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxModificationBox;
        private System.Windows.Forms.DataGridView dataGridViewPTMs;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonUpdateLib;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPTM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDeltaMass;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnIsVariable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnOnlyNTerminus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnOnlyCTerminus;
        private System.Windows.Forms.Button buttonLoadSoftwareDefaults;
    }
}
