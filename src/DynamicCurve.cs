using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CouplingTestStand
{
    public partial class DynamicCurve : UserControl
    {
        public DynamicCurve()
        {
            InitializeComponent();

            InitAngelChart();

            InitTorqueChart();

            this.timer1.Start();
        }

        private void InitAngelChart()
        {
            //定义图表区域
            this.angelCurve.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("C1");
            this.angelCurve.ChartAreas.Add(chartArea1);
            //定义存储和显示点的容器
            this.angelCurve.Series.Clear();
            Series series1 = new Series("A");
            series1.ChartArea = "C1";
            this.angelCurve.Series.Add(series1);
            //设置图表显示样式
            this.angelCurve.ChartAreas[0].AxisY.Minimum = -2;
            this.angelCurve.ChartAreas[0].AxisY.Maximum = 2;
            this.angelCurve.ChartAreas[0].AxisX.Interval = 50;
            this.angelCurve.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.angelCurve.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //设置标题
            this.angelCurve.Titles.Clear();
            //设置图表显示样式
            this.angelCurve.Series[0].Color = Color.Blue;
            this.angelCurve.Series[0].ChartType = SeriesChartType.Line;
            this.angelCurve.Series[0].Points.Clear();
        }

        private void InitTorqueChart()
        {
            //定义图表区域
            this.torqueCurve.ChartAreas.Clear();
            ChartArea chartArea2 = new ChartArea("C1");
            this.torqueCurve.ChartAreas.Add(chartArea2);
            //定义存储和显示点的容器
            this.torqueCurve.Series.Clear();
            Series series1 = new Series("T");
            series1.ChartArea = "C1";
            this.torqueCurve.Series.Add(series1);
            //设置图表显示样式
            this.torqueCurve.ChartAreas[0].AxisY.Minimum = -300;
            this.torqueCurve.ChartAreas[0].AxisY.Maximum = 300;
            this.torqueCurve.ChartAreas[0].AxisX.Interval = 50;
            this.torqueCurve.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.torqueCurve.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //设置标题
            this.torqueCurve.Titles.Clear();
            //设置图表显示样式
            this.torqueCurve.Series[0].Color = Color.Blue;
            this.torqueCurve.Series[0].ChartType = SeriesChartType.Line;
            this.torqueCurve.Series[0].Points.Clear();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.angelCurve.Series[0].Points.Clear();
            for (int i = 0; i < Data.arrayAngelData.Count; i++)
            {
                this.angelCurve.Series[0].Points.AddXY((i + 1), Data.arrayAngelData.ElementAt(i));
            }

            this.torqueCurve.Series[0].Points.Clear();
            for (int i = 0; i < Data.arrayTorqueData.Count; i++)
            {
                this.torqueCurve.Series[0].Points.AddXY((i + 1), Data.arrayTorqueData.ElementAt(i));
            }
        }
    }
}
