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
using System.IO;

namespace CouplingTestStand
{
    public partial class StiffnessCurve : UserControl
    {
        private Queue<double> angelQueue = new Queue<double>();
        private Queue<double> torqueQueue = new Queue<double>();
        private double m_tMax = 0;
        private double m_tMin = 0;
        private double m_aMax = 0;
        private double m_aMin = 0;
        private int m_tMaxIndex = 0;
        private int m_tMinIndex = 0;
        private int m_aMaxIndex = 0;
        private int m_aMinIndex = 0;



        public StiffnessCurve()
        {
            InitializeComponent();
            InitChart();
        }

        private void InitChart()
        {
            //定义图表区域
            this.Stiffness.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("C1");
            this.Stiffness.ChartAreas.Add(chartArea1);
            //定义存储和显示点的容器
            this.Stiffness.Series.Clear();
            Series series1 = new Series("S");
            series1.ChartArea = "C1";
            this.Stiffness.Series.Add(series1);
            //设置图表显示样式
            //this.Stiffness.ChartAreas[0].AxisY.Minimum = -200;
            //this.Stiffness.ChartAreas[0].AxisY.Maximum = 200;
            //this.Stiffness.ChartAreas[0].AxisX.Interval = 0.05;
            this.Stiffness.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.Stiffness.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.Stiffness.ChartAreas[0].AxisX.Title = "角度（度）";
            this.Stiffness.ChartAreas[0].AxisY.Title = "力矩（牛米）";
            this.Stiffness.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            this.Stiffness.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
            this.Stiffness.ChartAreas[0].AxisX.MinorGrid.LineColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Stiffness.ChartAreas[0].AxisY.MinorGrid.LineColor = System.Drawing.Color.FromArgb(240, 240, 240);
            //设置标题
            this.Stiffness.Titles.Clear();

            //设置图表显示样式
            this.Stiffness.Series[0].Color = Color.Blue;
            this.Stiffness.Series[0].ChartType = SeriesChartType.Line;
        }

        //更新测试刚度曲线
        public void RefreshCurve()
        {
            UpdateQueueValue();
            this.Stiffness.Series[0].Points.Clear();
            int count = 0;
            for (int i = 0; i < angelQueue.Count; i++)
            {
                try
                {
                    if(angelQueue.ElementAt(i) < 0 != angelQueue.ElementAt(i+1) < 0)
                    {
                        count = count + 1;
                    }
                    if (count > 2 & count < 5)
                    {
                        this.Stiffness.Series[0].Points.AddXY(angelQueue.ElementAt(i), torqueQueue.ElementAt(i));
                    }
                    if (count > 5)
                    {
                        break;
                    }
                }
                catch(ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            setAxisMinMax();
        }

        //更新静态刚度曲线
        public void RefreshStiffnessCurve()
        {
            UpdateQueueValue();
            this.Stiffness.Series[0].Points.Clear();
            m_tMax = 0;
            m_tMin = 0;
            m_aMax = 0;
            m_aMin = 0;
            m_tMaxIndex = 0;
            m_tMinIndex = 0;
            m_aMaxIndex = 0;
            m_aMinIndex = 0;
            for (int i = 0; i < angelQueue.Count; i = i+10)
            {
                try
                {
                    this.Stiffness.Series[0].Points.AddXY(angelQueue.ElementAt(i), torqueQueue.ElementAt(i));

                    if (angelQueue.ElementAt(i) > m_aMax)
                    {
                        m_aMax = angelQueue.ElementAt(i);
                        m_aMaxIndex = i/10;
                    }
                    if (angelQueue.ElementAt(i) < m_aMin)
                    {
                        m_aMin = angelQueue.ElementAt(i);
                        m_aMinIndex = i/10;
                    }

                    if (torqueQueue.ElementAt(i) > m_tMax)
                    {
                        m_tMax = torqueQueue.ElementAt(i);
                        m_tMaxIndex = i/10;
                    }
                    if (torqueQueue.ElementAt(i) < m_tMin)
                    {
                        m_tMin = torqueQueue.ElementAt(i);
                        m_tMinIndex = i/10;
                    }

                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            setAxisMinMax();
        }

        //更新动态刚度曲线
        public void RefreshDynamicCurve()
        {
            UpdateQueueValue();
            this.Stiffness.Series[0].Points.Clear();
            m_tMax = 0;
            m_tMin = 0;
            m_aMax = 0;
            m_aMin = 0;
            m_tMaxIndex = 0;
            m_tMinIndex = 0;
            m_aMaxIndex = 0;
            m_aMinIndex = 0;

            for (int i = 0; i < angelQueue.Count; i++)
            {
                try
                {
                    this.Stiffness.Series[0].Points.AddXY(angelQueue.ElementAt(i), torqueQueue.ElementAt(i));

                    if (angelQueue.ElementAt(i) > m_aMax)
                    {
                        m_aMax = angelQueue.ElementAt(i);
                        m_aMaxIndex = i;
                    }
                    if (angelQueue.ElementAt(i) < m_aMin)
                    {
                        m_aMin = angelQueue.ElementAt(i);
                        m_aMinIndex = i;
                    }

                    if (torqueQueue.ElementAt(i) > m_tMax)
                    {
                        m_tMax = torqueQueue.ElementAt(i);
                        m_tMaxIndex = i;
                    }
                    if (torqueQueue.ElementAt(i) < m_tMin)
                    {
                        m_tMin = torqueQueue.ElementAt(i);
                        m_tMinIndex = i;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            setAxisMinMax();
        }

        private void UpdateQueueValue()
        {
            readData(ref angelQueue, CouplingTesting.angelDir);
            readData(ref torqueQueue, CouplingTesting.torqueDir);
        }

        private void readData(ref Queue<double> data, string dir)
        {
            data.Clear();
            BinaryReader br;
            try
            {
                br = new BinaryReader(File.Open(dir,FileMode.Open));
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Cannot open file.");
                return;
            }
            try
            {
                while (true)
                {
                    double d = br.ReadDouble();
                    data.Enqueue(d);
                }
            }
            catch (EndOfStreamException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("已经读到末尾");
            }
            br.Close();
        }


        private void setAxisMinMax()
        {
            double min = 0;
            min = angelQueue.Min();
            Console.WriteLine(min);
            min = Convert.ToDouble(Convert.ToDouble(min).ToString("0.00"));
            min = min - 0.01;
            this.Stiffness.ChartAreas[0].AxisX.Minimum = min;

            double max = 0;
            max = angelQueue.Max();
            Console.WriteLine(max);
            max = Convert.ToDouble(Convert.ToDouble(max).ToString("0.00"));
            max = max + 0.01;
            this.Stiffness.ChartAreas[0].AxisX.Maximum = max;

            markPoint(m_aMaxIndex, "最大角度： " + m_aMax + " 度");
            markPoint(m_aMinIndex, "最小角度： " + m_aMin + " 度");
            markPoint(m_tMaxIndex, "最大力矩： " + m_tMax + " 牛米");
            markPoint(m_tMinIndex, "最小力矩： " + m_tMin + " 牛米");

        }

        private void markPoint(int i, string label)
        {
            this.Stiffness.Series[0].Points[i].MarkerStyle = MarkerStyle.Diamond;
            this.Stiffness.Series[0].Points[i].MarkerColor = Color.Red;
            this.Stiffness.Series[0].Points[i].MarkerBorderWidth = 3;
            this.Stiffness.Series[0].Points[i].MarkerSize = 10;
            this.Stiffness.Series[0].Points[i].Label = label;
            this.Stiffness.Series[0].Points[i].IsValueShownAsLabel = true;

        }
    }
}
