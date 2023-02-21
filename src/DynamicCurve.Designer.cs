namespace CouplingTestStand
{
    partial class DynamicCurve
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.angelCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.torqueCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.angelCurve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.torqueCurve)).BeginInit();
            this.SuspendLayout();
            // 
            // angelCurve
            // 
            chartArea1.Name = "ChartArea1";
            this.angelCurve.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.angelCurve.Legends.Add(legend1);
            this.angelCurve.Location = new System.Drawing.Point(3, 3);
            this.angelCurve.Name = "angelCurve";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.angelCurve.Series.Add(series1);
            this.angelCurve.Size = new System.Drawing.Size(435, 250);
            this.angelCurve.TabIndex = 0;
            this.angelCurve.Text = "angelCurve";
            // 
            // torqueCurve
            // 
            chartArea2.Name = "ChartArea1";
            this.torqueCurve.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.torqueCurve.Legends.Add(legend2);
            this.torqueCurve.Location = new System.Drawing.Point(444, 3);
            this.torqueCurve.Name = "torqueCurve";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.torqueCurve.Series.Add(series2);
            this.torqueCurve.Size = new System.Drawing.Size(435, 250);
            this.torqueCurve.TabIndex = 1;
            this.torqueCurve.Text = "torqueCurve";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // DynamicCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.torqueCurve);
            this.Controls.Add(this.angelCurve);
            this.Name = "DynamicCurve";
            this.Size = new System.Drawing.Size(1140, 420);
            ((System.ComponentModel.ISupportInitialize)(this.angelCurve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.torqueCurve)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart angelCurve;
        private System.Windows.Forms.DataVisualization.Charting.Chart torqueCurve;
        private System.Windows.Forms.Timer timer1;
    }
}
