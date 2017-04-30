namespace SEProcessor.Result
{
    partial class ChargeStateDistribution
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChargeStateDistribution));
            this.chartChargeState = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartChargeState)).BeginInit();
            this.SuspendLayout();
            // 
            // chartChargeState
            // 
            chartArea1.Name = "ChartArea1";
            this.chartChargeState.ChartAreas.Add(chartArea1);
            this.chartChargeState.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartChargeState.Legends.Add(legend1);
            this.chartChargeState.Location = new System.Drawing.Point(0, 0);
            this.chartChargeState.Name = "chartChargeState";
            series1.ChartArea = "ChartArea1";
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Charges";
            this.chartChargeState.Series.Add(series1);
            this.chartChargeState.Size = new System.Drawing.Size(679, 350);
            this.chartChargeState.TabIndex = 0;
            this.chartChargeState.Text = "Charge State Distribution";
            title1.Name = "Title1";
            title1.Text = "Charge State Distribution";
            this.chartChargeState.Titles.Add(title1);
            // 
            // ChargeStateDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 350);
            this.Controls.Add(this.chartChargeState);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChargeStateDistribution";
            this.Text = "Charge State Distribution";
            ((System.ComponentModel.ISupportInitialize)(this.chartChargeState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartChargeState;
    }
}