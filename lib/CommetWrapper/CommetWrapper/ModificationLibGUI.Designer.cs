namespace CometWrapper
{
    partial class ModificationLibGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModificationLibGUI));
            this.buttonDone = new System.Windows.Forms.Button();
            this.groupBoxModifications = new System.Windows.Forms.GroupBox();
            this.dataGridViewModifications = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMassShift = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResidues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.groupBoxModifications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModifications)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(294, 227);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(291, 23);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Add selected row to my search.xml";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // groupBoxModifications
            // 
            this.groupBoxModifications.Controls.Add(this.dataGridViewModifications);
            this.groupBoxModifications.Location = new System.Drawing.Point(12, 12);
            this.groupBoxModifications.Name = "groupBoxModifications";
            this.groupBoxModifications.Size = new System.Drawing.Size(573, 209);
            this.groupBoxModifications.TabIndex = 1;
            this.groupBoxModifications.TabStop = false;
            this.groupBoxModifications.Text = "Select a modification";
            // 
            // dataGridViewModifications
            // 
            this.dataGridViewModifications.AllowUserToOrderColumns = true;
            this.dataGridViewModifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModifications.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnMassShift,
            this.ColumnResidues});
            this.dataGridViewModifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewModifications.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewModifications.Name = "dataGridViewModifications";
            this.dataGridViewModifications.Size = new System.Drawing.Size(567, 190);
            this.dataGridViewModifications.TabIndex = 0;
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            // 
            // ColumnMassShift
            // 
            this.ColumnMassShift.HeaderText = "MassShift";
            this.ColumnMassShift.Name = "ColumnMassShift";
            // 
            // ColumnResidues
            // 
            this.ColumnResidues.HeaderText = "Residues";
            this.ColumnResidues.Name = "ColumnResidues";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(15, 227);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(273, 23);
            this.buttonUpdate.TabIndex = 2;
            this.buttonUpdate.Text = "Update my Lib";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // ModificationLibGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 262);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.groupBoxModifications);
            this.Controls.Add(this.buttonDone);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModificationLibGUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ModificationLib";
            this.groupBoxModifications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModifications)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.GroupBox groupBoxModifications;
        private System.Windows.Forms.DataGridView dataGridViewModifications;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMassShift;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResidues;
    }
}