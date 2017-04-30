namespace ProteinCoverageViewer
{
    partial class GlobalProteinCoverage
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.splitContainerAllPeptideLabels = new System.Windows.Forms.SplitContainer();
            this.buttonDomains = new System.Windows.Forms.Button();
            this.checkBoxAllPeptideLabels = new System.Windows.Forms.CheckBox();
            this.chartCoverage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAllPeptideLabels)).BeginInit();
            this.splitContainerAllPeptideLabels.Panel1.SuspendLayout();
            this.splitContainerAllPeptideLabels.Panel2.SuspendLayout();
            this.splitContainerAllPeptideLabels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCoverage)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerAllPeptideLabels
            // 
            this.splitContainerAllPeptideLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerAllPeptideLabels.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerAllPeptideLabels.Location = new System.Drawing.Point(0, 0);
            this.splitContainerAllPeptideLabels.Name = "splitContainerAllPeptideLabels";
            this.splitContainerAllPeptideLabels.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerAllPeptideLabels.Panel1
            // 
            this.splitContainerAllPeptideLabels.Panel1.Controls.Add(this.buttonDomains);
            this.splitContainerAllPeptideLabels.Panel1.Controls.Add(this.checkBoxAllPeptideLabels);
            // 
            // splitContainerAllPeptideLabels.Panel2
            // 
            this.splitContainerAllPeptideLabels.Panel2.Controls.Add(this.chartCoverage);
            this.splitContainerAllPeptideLabels.Size = new System.Drawing.Size(624, 306);
            this.splitContainerAllPeptideLabels.SplitterDistance = 29;
            this.splitContainerAllPeptideLabels.TabIndex = 1;
            // 
            // buttonDomains
            // 
            this.buttonDomains.Location = new System.Drawing.Point(114, 3);
            this.buttonDomains.Name = "buttonDomains";
            this.buttonDomains.Size = new System.Drawing.Size(198, 23);
            this.buttonDomains.TabIndex = 1;
            this.buttonDomains.Text = "Infer Domains through FioCloud ";
            this.buttonDomains.UseVisualStyleBackColor = true;
            this.buttonDomains.Click += new System.EventHandler(this.buttonDomains_Click);
            // 
            // checkBoxAllPeptideLabels
            // 
            this.checkBoxAllPeptideLabels.AutoSize = true;
            this.checkBoxAllPeptideLabels.Checked = true;
            this.checkBoxAllPeptideLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllPeptideLabels.Location = new System.Drawing.Point(3, 7);
            this.checkBoxAllPeptideLabels.Name = "checkBoxAllPeptideLabels";
            this.checkBoxAllPeptideLabels.Size = new System.Drawing.Size(105, 17);
            this.checkBoxAllPeptideLabels.TabIndex = 0;
            this.checkBoxAllPeptideLabels.Text = "All peptide labels";
            this.checkBoxAllPeptideLabels.UseVisualStyleBackColor = true;
            this.checkBoxAllPeptideLabels.CheckedChanged += new System.EventHandler(this.checkBoxAllPeptideLabels_CheckedChanged);
            // 
            // chartCoverage
            // 
            this.chartCoverage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(223)))), ((int)(((byte)(193)))));
            this.chartCoverage.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.chartCoverage.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
            this.chartCoverage.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartCoverage.BorderlineWidth = 2;
            this.chartCoverage.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea1.Area3DStyle.Inclination = 15;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.Perspective = 10;
            chartArea1.Area3DStyle.Rotation = 10;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.LabelStyle.IsEndLabelVisible = false;
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BackColor = System.Drawing.Color.OldLace;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "Default";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 80F;
            chartArea1.Position.Width = 87F;
            chartArea1.Position.X = 5F;
            chartArea1.Position.Y = 6F;
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.chartCoverage.ChartAreas.Add(chartArea1);
            this.chartCoverage.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            legend1.IsTextAutoFit = false;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend1.Name = "Default";
            legend1.Position.Auto = false;
            legend1.Position.Height = 9.054053F;
            legend1.Position.Width = 36.26765F;
            legend1.Position.X = 54.46494F;
            legend1.Position.Y = 85F;
            this.chartCoverage.Legends.Add(legend1);
            this.chartCoverage.Location = new System.Drawing.Point(0, 0);
            this.chartCoverage.Name = "chartCoverage";
            series1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series1.ChartArea = "Default";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(65)))), ((int)(((byte)(140)))), ((int)(((byte)(240)))));
            series1.CustomProperties = "PointWidth=0.4";
            series1.Legend = "Default";
            series1.Name = "Protein";
            series1.YValuesPerPoint = 2;
            series2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series2.ChartArea = "Default";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(180)))), ((int)(((byte)(65)))));
            series2.CustomProperties = "DrawSideBySide=false, PointWidth=0.4";
            series2.Legend = "Default";
            series2.Name = "Coverage";
            series2.YValuesPerPoint = 2;
            series3.ChartArea = "Default";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series3.CustomProperties = "PointWidth=0.4";
            series3.Legend = "Default";
            series3.Name = "Domains";
            series3.YValuesPerPoint = 2;
            this.chartCoverage.Series.Add(series1);
            this.chartCoverage.Series.Add(series2);
            this.chartCoverage.Series.Add(series3);
            this.chartCoverage.Size = new System.Drawing.Size(624, 273);
            this.chartCoverage.TabIndex = 1;
            title1.Name = "Title1";
            this.chartCoverage.Titles.Add(title1);
            // 
            // GlobalProteinCoverage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerAllPeptideLabels);
            this.Name = "GlobalProteinCoverage";
            this.Size = new System.Drawing.Size(624, 306);
            this.splitContainerAllPeptideLabels.Panel1.ResumeLayout(false);
            this.splitContainerAllPeptideLabels.Panel1.PerformLayout();
            this.splitContainerAllPeptideLabels.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAllPeptideLabels)).EndInit();
            this.splitContainerAllPeptideLabels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartCoverage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerAllPeptideLabels;
        private System.Windows.Forms.CheckBox checkBoxAllPeptideLabels;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCoverage;
        private System.Windows.Forms.Button buttonDomains;
    }
}
