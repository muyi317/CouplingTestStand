namespace CouplingTestStand
{
    partial class StiffnessCurve
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Stiffness = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Stiffness)).BeginInit();
            this.SuspendLayout();
            // 
            // Stiffness
            // 
            chartArea1.Name = "ChartArea1";
            this.Stiffness.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Stiffness.Legends.Add(legend1);
            this.Stiffness.Location = new System.Drawing.Point(3, 3);
            this.Stiffness.Margin = new System.Windows.Forms.Padding(5);
            this.Stiffness.Name = "Stiffness";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Stiffness.Series.Add(series1);
            this.Stiffness.Size = new System.Drawing.Size(811, 343);
            this.Stiffness.TabIndex = 0;
            this.Stiffness.Text = "StiffnessCurve";
            // 
            // StiffnessCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Stiffness);
            this.Name = "StiffnessCurve";
            this.Size = new System.Drawing.Size(881, 351);
            ((System.ComponentModel.ISupportInitialize)(this.Stiffness)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Stiffness;
    }
}
