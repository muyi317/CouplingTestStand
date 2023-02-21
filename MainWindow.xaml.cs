using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using NationalInstruments.NetworkVariable;
using InteropAssembly;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using ErrorCompensation;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CouplingTestStand
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 属性与字段
        string testTime = "";
        int m_maxDist = 570000;
        int m_minDist = 40000;

        private Thread readNetworkBuffer;

        CouplingDB couplingDB = new CouplingDB();
        CouplingTestDB couplingTestOption = new CouplingTestDB();
        int index = -1;
        int testIndex = -1;

        CouplingCurrent TestType = new CouplingCurrent();
        public CouplingCurrent CouplingC
        {
            get { return TestType; }
            set { TestType = value; }
        }

        CouplingTestData _couplingTestData = new CouplingTestData();

        Yaskawa yaskawa = new Yaskawa(); 

        //原点复归标志位
        bool originReached = false;

        string m_CurveType = "";

        #endregion
        public MainWindow()
        {
            //m_stopAll.Connect();
            //m_stopAll.WriteData(new NetworkVariableData<Boolean>(true));
            InitializeComponent();
            dbTableUpdate();
            dbTestTableUpdate();

            progressBarTest.Value = 0;
            posMotorSlider.Minimum = m_minDist-40000;
            posMotorSlider.Maximum = m_maxDist;
            posMotorSliderView.Minimum = m_minDist-40000;
            posMotorSliderView.Maximum = m_maxDist;

            posMotorSliderView2.Minimum = m_minDist-40000;
            posMotorSliderView2.Maximum = m_maxDist;

            // 开始PC端数据采集循环
            readNetworkBuffer = new Thread(new ThreadStart(startReadBuffer));
            readNetworkBuffer.IsBackground = true;
            readNetworkBuffer.Start();

            this.DataContext = CouplingC;

            DataContext = this;
            tabAuto.IsEnabled = true;
            tabManual.FontStyle = FontStyles.Italic;
            tabManual.IsEnabled = false;

            #region btn enabled
            btnStartTest.IsEnabled = false;
            btnStopTest.IsEnabled = false;
            #endregion

            _syncContext = SynchronizationContext.Current;

            this.Closing += MainWindow_Closed;

            btnMoveCW.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btnMoveCW_Down), true);
            btnMoveCW.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btnMoveCW_up), true);
            btnMoveCCW.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btnMoveCCW_Down), true);
            btnMoveCCW.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btnMoveCCW_up), true);
            btnRotateCW.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btnRotateCW_Down), true);
            btnRotateCW.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btnRotateCW_Up), true);
            btnRotateCCW.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btnRotateCCW_Down), true);
            btnRotateCCW.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btnRotateCCW_Up), true);

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            m_timerOriginalSearch.Tick += new EventHandler(timerOriginalSearch);
            m_timerOriginalSearch.Interval = TimeSpan.FromSeconds(0.2);

            //链接RT
            ConnectRT();

            //查询原点复归
            ushort a = 0;
            yaskawa.readMB("MB002222", ref a);
            if (a == 0)
            {
                MessageBox.Show("请先完成原点复归");
            }
        }

        #region 数据库页面
        private void textMovePos_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(textMovePos.Text) <= 100& Convert.ToDouble(textMovePos.Text) >= 0)
                {
                    if (this.IsLoaded & textMovePos.Text != "")
                    {
                        posMotorSlider.Value = Convert.ToDouble(textMovePos.Text);
                    }
                }
                else
                {
                    //if (Convert.ToDouble(textMovePos.Text) <= 0)
                    //{
                        //textMovePos.Text = "0";
                    //}
                    //if (Convert.ToDouble(textMovePos.Text) >= 100)
                    //{
                        //textMovePos.Text = "100";
                    //}
                }
            }
            catch (FormatException)
            {

            }
        }

        private void posMotorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textMovePos.Text = Convert.ToString(posMotorSlider.Value);
        }

        private void dbTableUpdate()
        {
            dataGridCoupling.Items.Clear();
            couplingDB.Select();
            for (int i = 0; i<couplingDB.couplingTypes.Count; i++)
            {
                dataGridCoupling.Items.Add(new { couplingId = couplingDB.couplingTypes[i] });
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (index == -1)
            {
                MessageBox.Show("请选择联轴器型号");
            }
            else
            {
                couplingDB.Delete(couplingDB.couplingInf);
                dbTableUpdate();
                index = -1;
                couplingType.Text = "";
                torqueRated.Text = "";
                maxSpeed.Text = "";
                torqueCons.Text = "";
                axialCons.Text = "";
                momentIne.Text = "";
                mass.Text = "";
            }
        }

        private void dataGridCoupling_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridCoupling.SelectedItem != null)
            {
                string a = dataGridCoupling.SelectedItem.ToString();
                string[] b = a.Split(' ');

                couplingDB.SelectCouplingType(b[3]);
                couplingType.Text = couplingDB.couplingInf["couplingId"];              
                torqueRated.Text = couplingDB.couplingInf["torqueRated"];
                maxSpeed.Text = couplingDB.couplingInf["maxSpeed"];
                torqueCons.Text = couplingDB.couplingInf["torqueCons"];
                axialCons.Text = couplingDB.couplingInf["axialCons"];
                momentIne.Text = couplingDB.couplingInf["momentIne"];
                mass.Text = couplingDB.couplingInf["mass"];

                TestType.CouplingId = couplingType.Text;
                TestType.TorqueRated = torqueRated.Text;
                TestType.MaxSpeed = maxSpeed.Text;
                TestType.TorqueCons = torqueCons.Text;
                TestType.AxialCons = axialCons.Text;
                TestType.MomentIne = momentIne.Text;
                TestType.Mass = mass.Text;
                index = 1;
                try
                {
                    helixView.viewModel.LoadModels(couplingType.Text);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }               
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            couplingDB.Add(TestType);
            dbTableUpdate();
        }

        private void applyTest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(CouplingC.CouplingId);
            textTestCoupling.Text = CouplingC.CouplingId;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            couplingDB.Update(TestType);
        }
        #endregion

        #region 测试数据库页面
        private void dataGridCouplingTestSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridCouplingTestSet.SelectedItem != null)
            {
                string a = dataGridCouplingTestSet.SelectedItem.ToString();
                string[] b = a.Split(' ', ',');
                
                //Console.WriteLine(a);
                //Console.WriteLine(b[20]);

                CouplingTesting.testStiffness = b[28];

                testTime = "today";
                couplingTestOption.SelectCouplingType(b[3]);
                textTime.Text = b[11] + " " + b[12];
                textCouplingType.Text = b[7];
                textMaxTorque.Text = b[20];

                m_CurveType = b[16];

                if (b[16] == "静态刚度")
                {
                    try
                    {
                        CouplingTesting.angelDir = "D:\\MeasuredData\\" + couplingTestOption.couplingInf["dataDir"].Split('a')[3] + "StaticAngelData.dat";
                        CouplingTesting.torqueDir = "D:\\MeasuredData\\" + couplingTestOption.couplingInf["dataDir"].Split('a')[3] + "StaticTorqueData.dat";
                    }
                    catch (System.Collections.Generic.KeyNotFoundException)
                    {
                        
                    } 
                }
                if(b[16] == "动态刚度")
                {
                    try
                    {
                        CouplingTesting.angelDir = "D:\\MeasuredData\\" + couplingTestOption.couplingInf["dataDir"].Split('a')[3] + "DynamicAngelData.dat";
                        CouplingTesting.torqueDir = "D:\\MeasuredData\\" + couplingTestOption.couplingInf["dataDir"].Split('a')[3] + "DynamicTorqueData.dat";
                    }
                    catch (System.Collections.Generic.KeyNotFoundException)
                    {

                    }
                }
                testIndex = 1;
            }
        }

        private void dbTestTableUpdate()
        {
            dataGridCouplingTestSet.Items.Clear();
            couplingTestOption.Select();
            for (int i = 0; i < couplingTestOption.couplingTypes.Count; i++)
            {
                dataGridCouplingTestSet.Items.Add(new
                {
                    id = couplingTestOption.id[i],
                    couplingId = couplingTestOption.couplingTypes[i],
                    mesTime = couplingTestOption.mesTime[i],
                    testItem = couplingTestOption.testItem[i],
                    testTorque = couplingTestOption.testTorque[i],
                    testFrequency = couplingTestOption.testFrequency[i],
                    testStiffness = couplingTestOption.testStiffness[i],
                    torqueRated = couplingTestOption.torqueRated[i],
                    maxSpeed = couplingTestOption.maxSpeed[i],
                    torqueCons = couplingTestOption.torqueCons[i],
                    axialCons = couplingTestOption.axialCons[i],
                    momentIne = couplingTestOption.momentIne[i],
                    mass = couplingTestOption.mass[i]
                });
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dbTestTableUpdate();
        }

        private void btnTestDelete_Click(object sender, RoutedEventArgs e)
        {
            if (testIndex == -1)
            {
                MessageBox.Show("请选择项目");
            }
            else
            {
                File.Delete(CouplingTesting.angelDir);
                File.Delete(CouplingTesting.torqueDir);

                couplingTestOption.Delete(couplingTestOption.couplingInf);

                dbTestTableUpdate();
                testIndex = -1;
            }
        }
        #endregion

        #region 开始测试页面
        private static NetworkVariableReader<double> _readerTorque;
        private static NetworkVariableReader<double> _readerAngel;
        private const string NetTorqueData = @"\\localhost\CouplingTestStand\torqueData";
        private const string NetAngelData = @"\\localhost\CouplingTestStand\angelData";
        private static NetworkVariableWriter<Boolean> stopNetLoop;
        private const string NVStop = @"\\localhost\CouplingTestStand\stop";
        private double AngelData;
        private double TorqueData;

        private NetworkVariableWriter<String> _torqueFileName = 
            new NetworkVariableWriter<String>(@"\\localhost\CouplingTestStand\torqueFileName");
        private NetworkVariableWriter<String> _angelFileName =
            new NetworkVariableWriter<String>(@"\\localhost\CouplingTestStand\angelFileName");
        private NetworkVariableWriter<Boolean> _logFile = new NetworkVariableWriter<Boolean>(@"\\localhost\CouplingTestStand\logFile");
        private NetworkVariableWriter<Boolean> _setZero = new NetworkVariableWriter<Boolean>(@"\\localhost\CouplingTestStand\setZeroPC");
        private NetworkVariableWriter<Boolean> m_stopAll = new NetworkVariableWriter<Boolean>(@"\\localhost\CouplingTestStand\stopAllThread");

        private Thread threadAquire;
        private Thread threadCurve;
        private Thread threadTest;

        public static bool stopLoop = false;
        SynchronizationContext _syncContext = null;

        #region 按键逻辑
        private void CreateReader()
        {
            _readerTorque = new NetworkVariableReader<double>(NetTorqueData);
            _readerTorque.PropertyChanged += OnPropertyChanged;
            _readerAngel = new NetworkVariableReader<double>(NetAngelData);
            _readerAngel.PropertyChanged += OnPropertyChanged;
            stopNetLoop = new NetworkVariableWriter<Boolean>(NVStop);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConnectionStatus")
            {
                textStatus.Text = _readerTorque.ConnectionStatus.ToString();
                btnStartTest.IsEnabled = (_readerTorque.ConnectionStatus == ConnectionStatus.Connected);
                btnConn.IsEnabled = !(_readerTorque.ConnectionStatus == ConnectionStatus.Connected);
            }
        }

        private void btnConn_Click(object sender, RoutedEventArgs e)
        {
            ConnectRT();
        }

        private void ConnectRT()
        {
            if (_readerTorque == null)
            {
                CreateReader();
            }
            _readerTorque.Connect();
            _readerAngel.Connect();
            threadAquire = new Thread(new ThreadStart(threadStartAquire));
            threadCurve = new Thread(new ThreadStart(threadUpdateCurve));
            threadAquire.IsBackground = true;
            threadCurve.IsBackground = true;
            threadAquire.Start();
            threadCurve.Start();
            //textStatus.Text = _reader.ReadData().GetValue().ToString();
        }

        private void threadStartAquire()
        {
            InteropAssembly.LabVIEWExports.readNetworkBuffer();
            Thread.Sleep(10);
        }

        private void threadUpdateCurve()
        {
            while (true)
            {
                try
                {
                    _syncContext.Post(angelCurveUpdate, _readerAngel.ReadData().GetValue());
                    _syncContext.Post(torqueCurveUpdate, _readerTorque.ReadData().GetValue());     //子线程中通过UI线程上下文更新UI 
                    Thread.Sleep(10);
                }
                catch (TimeoutException)
                {
                    
                }
            }
        }

        private void angelCurveUpdate(object d)
        {
            if (Data.arrayAngelData.Count <= 300)
            {
                AngelData = (double)d;
                Data.arrayAngelData.Enqueue((double)d);
            }
            else
            {
                AngelData = (double)d;
                Data.arrayAngelData.Enqueue((double)d);
                Data.arrayAngelData.Dequeue();
            }
           
        }

        private void torqueCurveUpdate(object d)
        {
            if (Data.arrayTorqueData.Count <= 300)
            {
                TorqueData = (double)d;
                Data.arrayTorqueData.Enqueue((double)d);
            }
            else
            {
                TorqueData = (double)d;
                Data.arrayTorqueData.Enqueue((double)d);
                Data.arrayTorqueData.Dequeue();
            }
        }

        private void btnStopTest_Click(object sender, RoutedEventArgs e)
        {
            threadTest.Abort();
            stopNetLoop.Connect();
            stopNetLoop.WriteData(new NetworkVariableData<bool>(true));
            btnStartTest.IsEnabled = true;
            btnStopTest.IsEnabled = false;
            tabStiffnessCurve.FontStyle = FontStyles.Normal;
            tabStiffnessCurve.IsEnabled = true;
            checkBoxStaticStiffness.IsEnabled = true;
            textBoxStaticStiffness.IsEnabled = true;
            checkBoxDynamicStiffness.IsEnabled = true;
            textBoxFrequency.IsEnabled = true;
            textBoxDynamicStiffness.IsEnabled = true;
            switchManualAuto.IsEnabled = true;   //切换手自动按键启用
            applyTest.IsEnabled = true;          //添加测试轴承按键启用
            tabCouplingTestSet.IsEnabled = true;
            tabCouplingTestSet.FontStyle = FontStyles.Normal;

            yaskawa.writeMB("MB000025", "0");
            Thread.Sleep(50);
            yaskawa.writeMB("MB000024", "0");

            _logFile.Connect();
            _logFile.WriteData(new NetworkVariableData<Boolean>(false));
        }

        private void btnStartTest_Click(object sender, RoutedEventArgs e)
        {
            if (textTestCoupling.Text == "")
            {
                MessageBox.Show("请选择具体型号");
                return;
            }
            if (textBoxStaticStiffness.Text.Contains(".") || textBoxDynamicStiffness.Text.Contains(".") || textBoxFrequency.Text.Contains("."))
            {
                MessageBox.Show("请输入整数");
                return;
            }

            //检查原点是否复归
            if (!originReached)
            {
                MessageBox.Show("请先进行原点复归操作");
                return;
            }

            //控件启用设置
            btnStartTest.IsEnabled = false;
            btnStopTest.IsEnabled = true;
            tabStiffnessCurve.FontStyle = FontStyles.Italic;
            tabStiffnessCurve.IsEnabled = false;
            checkBoxStaticStiffness.IsEnabled = false;
            textBoxStaticStiffness.IsEnabled = false;
            checkBoxDynamicStiffness.IsEnabled = false;
            textBoxFrequency.IsEnabled = false;
            textBoxDynamicStiffness.IsEnabled = false;
            switchManualAuto.IsEnabled = false;  //切换手自动按键禁用
            applyTest.IsEnabled = false;         //添加测试轴承按键禁用
            tabCouplingTestSet.IsEnabled = false;
            tabCouplingTestSet.FontStyle = FontStyles.Italic;

            testTime = DateTime.Now.ToString("yyyyMMddHHmmss");  //当前测试时间

            _couplingTestData.couplingId = CouplingC.CouplingId;
            _couplingTestData.torqueRated = CouplingC.TorqueRated;
            _couplingTestData.maxSpeed = CouplingC.MaxSpeed;
            _couplingTestData.torqueCons = CouplingC.TorqueCons;
            _couplingTestData.axialCons = CouplingC.AxialCons;
            _couplingTestData.momentIne = CouplingC.MomentIne;
            _couplingTestData.mass = CouplingC.Mass;
            _couplingTestData.dataDir = testTime;

            //开始测试
            threadTest = new Thread(new ThreadStart(startTest));
            threadTest.IsBackground = true;
            threadTest.Start();
            progressBarTest.Value = 0;
            m_divideProgress = Convert.ToInt32(checkBoxStaticStiffness.IsChecked) + Convert.ToInt32(checkBoxDynamicStiffness.IsChecked);
        }

        #endregion

        #region 测试流程
        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 步骤：
         * 1、静态刚度测试
         * 2、动态刚度测试
        +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        int m_divideProgress = 1;

        private void startTest()
        {
            bool? a = false;
            bool? b = false;
            this.Dispatcher.Invoke(new Action(() =>
            {
                a = checkBoxStaticStiffness.IsChecked;
                b = checkBoxDynamicStiffness.IsChecked;
            }));

            if (a == true)
            {
                Thread.Sleep(1000);
                StaticStiffnessTest(testTime);
            }
            if (b == true)
            {
                Thread.Sleep(1000);
                DynamicStiffnessTest(testTime);
            }

            this.Dispatcher.Invoke(new Action(() =>
            {
                    progressBarTest.Value = 100;
                MessageBox.Show("测试完成");

                switchManualAuto.IsEnabled = true;   //切换手自动按键启用
                applyTest.IsEnabled = true;          //添加测试轴承按键启用
                btnStartTest.IsEnabled = true;
                btnStopTest.IsEnabled = false;
                tabStiffnessCurve.FontStyle = FontStyles.Normal;
                tabStiffnessCurve.IsEnabled = true;
                checkBoxStaticStiffness.IsEnabled = true;
                textBoxStaticStiffness.IsEnabled = true;
                checkBoxDynamicStiffness.IsEnabled = true;
                textBoxFrequency.IsEnabled = true;
                textBoxDynamicStiffness.IsEnabled = true;
                tabCouplingTestSet.IsEnabled = true;
                tabCouplingTestSet.FontStyle = FontStyles.Normal;
                progressBarTest.Value = 0;
                dbTestTableUpdate();
            }));         
        }

        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 静态刚度测试：
         * 1、定位力矩
         * 2、角度对中
         * 3、旋转电机使能
         * 4、记录数据
         * 5、延时等待一圈的时间
         * 6、停止记录数据
         * 7、存储数据库
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        private void StaticStiffnessTest(string testTime)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (Convert.ToInt32(textBoxStaticStiffness.Text) > 200 || Convert.ToInt32(textBoxStaticStiffness.Text) < 0)
                {
                    MessageBox.Show("静态测试力矩值应设定为0-200");
                    return;
                }
                currentAction.Text = "静态测试移动到设定力矩。。";
            }));

            object sender = new object();
            RoutedEventArgs e = new RoutedEventArgs();

            Thread.Sleep(1000);

            // 1、定位力矩
            this.Dispatcher.Invoke(new Action(() =>
            {
                textTargetTorque.Text = textBoxStaticStiffness.Text;
            }));

            BtnSearchTorque_Click(sender, e);

            this.Dispatcher.Invoke(new Action(() =>
            {
                currentAction.Text = "静态测试移动完成";
                btnStartTest.IsEnabled = false;
                btnStopTest.IsEnabled = true;
            }));

            Thread.Sleep(2000);

            this.Dispatcher.Invoke(new Action(() =>
            {
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress; //进度条数值
            }));
            Thread.Sleep(1000);

            // 2、角度对中
            //error = AngelCalibration();
            //if (error == 1)
            //{
                //MessageBox.Show("角度校准错误，未找到力矩零点");
                //return;
            //}

            // 3、旋转电机使能
            this.Dispatcher.Invoke(new Action(() =>
            {
                currentAction.Text = "静态测试开始加载";
            }));
                
            Thread.Sleep(1000);

            this.Dispatcher.Invoke(new Action(() =>
            {
                textRotateSpeed.Text = "4";
                btnRotateCW_Down(sender, e);
            }));
                
            Thread.Sleep(7500);

            this.Dispatcher.Invoke(new Action(() =>
            {
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress;  //进度条数值
            }));

            // 4、记录数据
            this.Dispatcher.Invoke(new Action(() =>
            {
                currentAction.Text = "静态测试数据记录中。。";
                CouplingTesting.angelDir = "D:\\MeasuredData\\" + testTime + "StaticAngelData.dat";
                CouplingTesting.torqueDir = "D:\\MeasuredData\\" + testTime + "StaticTorqueData.dat";
                CouplingC.DataDir = @"D:\MeasuredData\" + testTime;

                _torqueFileName.Connect();
                _torqueFileName.WriteData(new NetworkVariableData<String>(testTime + "StaticTorqueData.dat"));

                _angelFileName.Connect();
                _angelFileName.WriteData(new NetworkVariableData<String>(testTime + "StaticAngelData.dat"));

                _logFile.Connect();
                _logFile.WriteData(new NetworkVariableData<Boolean>(true));
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress;  //进度条数值
            }));              

            // 5、延时等待二圈的时间
            Thread.Sleep(31000);
            this.Dispatcher.Invoke(new Action(() =>
            {
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress; //进度条数值
            }));

            // 6、停止记录数据
            this.Dispatcher.Invoke(new Action(() =>
            {
                _logFile.Connect();
                _logFile.WriteData(new NetworkVariableData<Boolean>(false));
                btnRotateCW_Up(sender, e);
                currentAction.Text = "静态测试数据记录完成";
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress;  //进度条数值
            }));

            // 8、数据补偿
            this.Dispatcher.Invoke(new Action(() =>
            {
                currentAction.Text = "静态测试数据补偿。。";
            }));
            double a = 0;
            ErrorCompensation.LabVIEWExports.ErrorCompensationRelese("D:\\MeasuredData\\" + testTime + "StaticAngelData.dat", "D:\\MeasuredData\\" + testTime + "StaticTorqueData.dat", 0, out a);
            CouplingC.TestStiffness = Convert.ToString(a);

            // 7、存储数据库
            this.Dispatcher.Invoke(new Action(() =>
            {
                CouplingC.TestItem = "静态刚度";
                CouplingC.TestTorque = textBoxStaticStiffness.Text;
                CouplingC.TestFrequency = "0";
            }));
            Thread.Sleep(1000);
            this.Dispatcher.Invoke(new Action(() =>
            {
                couplingTestOption.Add(CouplingC);
            }));

            this.Dispatcher.Invoke(new Action(() =>
            {
                Thread.Sleep(500);
                dbTestTableUpdate();
            }));
        }

        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 动态刚度测试：
         * 1、定位力矩
         * 2、角度对中
         * 3、旋转电机使能
         * 4、记录数据
         * 5、延时等待一圈的时间
         * 6、停止记录数据
         * 7、存数据库
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        private void DynamicStiffnessTest(string testTime)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (Convert.ToInt32(textBoxDynamicStiffness.Text) > 200 || Convert.ToInt32(textBoxDynamicStiffness.Text) < 0)
                {
                    MessageBox.Show("动态测试力矩值应设定为0-200");
                    return;
                }
                if (Convert.ToInt32(textBoxFrequency.Text) > 20 || Convert.ToInt32(textBoxFrequency.Text) < 0)
                {
                    MessageBox.Show("频率值应设定为0-20");
                    return;
                }
            }));
            
            object sender = new object();
            RoutedEventArgs e = new RoutedEventArgs();

            this.Dispatcher.Invoke(new Action(() =>
            {
                currentAction.Text = "动态测试移动到设定力矩。。";
            }));
            Thread.Sleep(1000);

            // 1、定位力矩
            this.Dispatcher.Invoke(new Action(() =>
            {
                textTargetTorque.Text = textBoxDynamicStiffness.Text;
            }));

            BtnSearchTorque_Click(sender, e);

            this.Dispatcher.Invoke(new Action(() =>
            {
                currentAction.Text = "动态测试移动完成";
            }));

            Thread.Sleep(2000);

            this.Dispatcher.Invoke(new Action(() =>
            {               
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress; //进度条数值
            }));

            // 2、角度对中
            //error = AngelCalibration();
            //if (error == 1)
            //{
                //MessageBox.Show("角度校准错误，未找到力矩零点");
                //return;
            //}

            // 3、旋转电机使能
            this.Dispatcher.Invoke(new Action(() =>
            {
                currentAction.Text = "动态测试开始加载";
            }));

            Thread.Sleep(1000);

            this.Dispatcher.Invoke(new Action(() =>
            {
                textRotateSpeed.Text = Convert.ToString(Convert.ToInt32(textBoxFrequency.Text) * 60);
                btnRotateCW_Down(sender, e);
            }));

            Thread.Sleep(5000);

            this.Dispatcher.Invoke(new Action(() =>
            {
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress;  //进度条数值
            }));

            // 4、记录数据
            this.Dispatcher.Invoke(new Action(() =>
            {
                currentAction.Text = "动态测试数据记录中。。";
                CouplingTesting.angelDir = "D:\\MeasuredData\\" + testTime + "DynamicAngelData.dat";
                CouplingTesting.torqueDir = "D:\\MeasuredData\\" + testTime + "DynamicTorqueData.dat";
                CouplingC.DataDir = "D:\\MeasuredData\\" + testTime;

                _torqueFileName.Connect();
                _torqueFileName.WriteData(new NetworkVariableData<String>(testTime + "DynamicTorqueData.dat"));

                _angelFileName.Connect();
                _angelFileName.WriteData(new NetworkVariableData<String>(testTime + "DynamicAngelData.dat"));

                _logFile.Connect();
                _logFile.WriteData(new NetworkVariableData<Boolean>(true));
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress;  //进度条数值
            }));

            // 5、延时等待三圈的时间
            string frequency = "1";
            this.Dispatcher.Invoke(new Action(() =>
            {
                frequency = textBoxFrequency.Text;
            }));

            Thread.Sleep(6000 / Convert.ToInt32(frequency));

            this.Dispatcher.Invoke(new Action(() =>
            {
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress;  //进度条数值
            }));

            // 6、停止记录数据
            this.Dispatcher.Invoke(new Action(() =>
            {
                _logFile.Connect();
                _logFile.WriteData(new NetworkVariableData<Boolean>(false));
                btnRotateCW_Up(sender, e);
                currentAction.Text = "动态测试数据记录完成";
                progressBarTest.Value = progressBarTest.Value + 20 / m_divideProgress;  //进度条数值
            }));

            // 8、数据补偿
            this.Dispatcher.Invoke(new Action(() =>
            {
                currentAction.Text = "动态测试数据补偿。。";
            }));

            double a = 0;
            ErrorCompensation.LabVIEWExports.ErrorCompensationRelese("D:\\MeasuredData\\" + testTime + "DynamicAngelData.dat", "D:\\MeasuredData\\" + testTime + "DynamicTorqueData.dat", Convert.ToInt32(frequency), out a);
            CouplingC.TestStiffness = Convert.ToString(a);

            // 7、存储数据库
            this.Dispatcher.Invoke(new Action(() =>
            {
                CouplingC.TestItem = "动态刚度";
                CouplingC.TestTorque = textBoxDynamicStiffness.Text;
                CouplingC.TestFrequency = textBoxFrequency.Text;
            }));
            Thread.Sleep(1000);
            this.Dispatcher.Invoke(new Action(() =>
            {
                couplingTestOption.Add(CouplingC);
            }));

            this.Dispatcher.Invoke(new Action(() =>
            {
                Thread.Sleep(500);
                dbTestTableUpdate();              
            }));
        }

        #endregion
        #endregion

        #region 手动调试页面
        private void btnMMDistanceSet_Click(object sender, RoutedEventArgs e)
        {
            if(textMovePos.Text != "")
            {
                if (btnMMDistanceSet.Value == true)
                {
                    yaskawa.writeML("MB000060", "0"); //模式标志位
                    Thread.Sleep(50);
                    yaskawa.writeML("MW00017", textMoveSpeed.Text);
                    Thread.Sleep(50);
                    yaskawa.writeML("ML00070", textMovePos.Text);
                    Thread.Sleep(50);
                    yaskawa.writeMB("MB000024", "1"); //移动电机使能
                    Thread.Sleep(50);
                }
                else
                {
                    yaskawa.writeMB("MB000024", "0");
                }
            }
            else
            {
                MessageBox.Show("请填写位置");
            }
        }
        private void btnMoveCW_Down(object sender, RoutedEventArgs e)
        {
            if (textMoveSpeed.Text != "")
            {
                yaskawa.writeML("MB000060", "1"); //模式标志位
                Thread.Sleep(20);
                yaskawa.writeML("MW00017", textMoveSpeed.Text);
                Thread.Sleep(20);
                yaskawa.writeMB("MB000024", "1");
                Thread.Sleep(200);
                yaskawa.writeMB("MB000120", "1");
            }
            else
            {
                MessageBox.Show("请填写速度");
            }
        }
        private void btnMoveCW_up(object sender, RoutedEventArgs e)
        {
            yaskawa.writeMB("MB000120", "0");
            Thread.Sleep(20);
            yaskawa.writeMB("MB000024", "0");
        }
        private void btnMoveCCW_Down(object sender, RoutedEventArgs e)
        {
            if (textMoveSpeed.Text != "")
            {
                yaskawa.writeML("MB000060", "1"); //模式标志位
                Thread.Sleep(20);
                yaskawa.writeML("MW00017", textMoveSpeed.Text);
                Thread.Sleep(20);
                yaskawa.writeMB("MB000024", "1");
                Thread.Sleep(200);
                yaskawa.writeMB("MB000121", "1");
                m_timerOriginalSearch.Start();
            }
            else
            {
                MessageBox.Show("请填写速度");
            }
        }

        DispatcherTimer m_timerOriginalSearch = new DispatcherTimer();
        void timerOriginalSearch(object sender, EventArgs e)
        {
            ushort a = 0;
            yaskawa.readMB("MB002222", ref a);
            if (a == 1)
            {
                yaskawa.writeMB("MB000121", "0");
                Thread.Sleep(20);
                yaskawa.writeMB("MB000024", "0");
                MessageBox.Show("原点复归完成");
                originReached = true;
                m_timerOriginalSearch.Stop();
            }
        }
        private void btnMoveCCW_up(object sender, RoutedEventArgs e)
        {
            yaskawa.writeMB("MB000121", "0");
            Thread.Sleep(20);
            yaskawa.writeMB("MB000024", "0");
            m_timerOriginalSearch.Stop();
        }
        private void btnRotateCW_Down(object sender, RoutedEventArgs e)
        {
            if (textRotateSpeed.Text != "")
            {
                yaskawa.writeML("MB000061", "1"); //模式标志位
                Thread.Sleep(20); 
                yaskawa.writeML("MW00023", textRotateSpeed.Text);
                Thread.Sleep(20);
                yaskawa.writeMB("MB000025", "1");
                Thread.Sleep(200);
                yaskawa.writeMB("MB000122", "1");
            }
            else
            {
                MessageBox.Show("请填写速度");
            }
        }
        private void btnRotateCW_Up(object sender, RoutedEventArgs e)
        {
            yaskawa.writeMB("MB000122", "0");
            Thread.Sleep(20);
            yaskawa.writeMB("MB000025", "0");
        }

        private void btnRotateCCW_Down(object sender, RoutedEventArgs e)
        {
            if (textRotateSpeed.Text != "")
            {
                yaskawa.writeML("MW00023", textRotateSpeed.Text);
                Thread.Sleep(20);
                yaskawa.writeMB("MB000025", "1");
                Thread.Sleep(200);
                yaskawa.writeMB("MB000123", "1");
            }
            else
            {
                MessageBox.Show("请填写速度");
            }
        }
        private void btnRotateCCW_Up(object sender, RoutedEventArgs e)
        {
            yaskawa.writeMB("MB000123", "0");
            Thread.Sleep(20);
            yaskawa.writeMB("MB000025", "0");
        }

        private void switchRotatePosSet_ValueChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if (swithRotatePosSet.Value == true)
            {
                if (textRotatePos.Text != "")
                {
                    yaskawa.writeML("MB000061", "0"); //模式标志位
                    Thread.Sleep(50);
                    yaskawa.writeML("MW00023", textRotateSpeed.Text);
                    Thread.Sleep(50);

                    int setPos;
                    if (_posAddValue < 0)
                    {
                        setPos = _posAddValue - (1000 - Convert.ToInt32(textRotatePos.Text));
                    }
                    else
                    {
                        setPos = Convert.ToInt32(textRotatePos.Text) + _posAddValue;
                    }
                    yaskawa.writeML("ML00080", Convert.ToString(setPos));
                    Thread.Sleep(50);
                    yaskawa.writeMB("MB000025", "1");
                    Thread.Sleep(50);
                }
                else
                {
                    MessageBox.Show("请填写位置");
                }
            }
            if(swithRotatePosSet.Value == false)
            {
                ushort a = 1;
                yaskawa.readMB("MB000066", ref a); //运行中

                yaskawa.writeML("OW8088", "0");
                Thread.Sleep(50);
                yaskawa.writeMB("MB000025", "0");
            }
        }

        private void btnRotatePosSet_Clicked(object sender, RoutedEventArgs ee)
        {
            yaskawa.writeML("OL8090", textRotateSpeed.Text);
            Thread.Sleep(50);
            yaskawa.writeML("OW8088", "1");
            Thread.Sleep(50);
            int setPos;
            if (_posAddValue < 0)
            {
                setPos = _posAddValue - (1000 - Convert.ToInt32(textRotatePos.Text));
            }
            else
            {
                setPos = Convert.ToInt32(textRotatePos.Text) + _posAddValue;
            }
            yaskawa.writeML("OL809C", Convert.ToString(setPos));
        }

        //伺服反馈定义
        private Int32 moveSpeed = 0x00000000;
        private Int32 rotateSpeed = 0x00000000;
        private Int32 movePos = 0x00000000;
        private Int32 rotatePos = 0x00000000;
        private int _residue;
        private int _posAddValue;
        private DispatcherTimer timer;

        //监视伺服反馈
        private void moniter(object sender, EventArgs e)
        {
            yaskawa.readML("IL8040", ref moveSpeed);
            textMoveSpeedReal.Text = Convert.ToString(moveSpeed);
            yaskawa.readML("IL8016", ref movePos);
            textMovePosReal.Text = Convert.ToString(movePos);
            yaskawa.readML("IL80C0", ref rotateSpeed);
            textRotateSpeedReal.Text = Convert.ToString(rotateSpeed);
            textRotateSpeedAutoReal.Text = Convert.ToString(rotateSpeed);
            
            yaskawa.readML("IL8096", ref rotatePos);
            _residue = rotatePos % 1000;
            if(_residue < 0)
            {
                textRotatePosReal.Text = Convert.ToString(_residue+1000);
            }
            else
            {
                textRotatePosReal.Text = Convert.ToString(_residue);
            }
            _posAddValue = rotatePos - _residue;
            textRotatePosAutoReal.Text = Convert.ToString(rotatePos);
        }

        // 设置角度中点点击事件，通过旋转重置光栅中点位置，使光栅正负与角度传感器一致
        private void btnSetAngelMid_Click(object sender, RoutedEventArgs e)
        {
            AngelCalibration();
        }

        // 安装联轴器时保持轴系处于加载中间位置
        private void btnKeepAngelMid_Click(object sender, RoutedEventArgs e)
        {
            KeepAngelMid();
        }
        #endregion

        #region 其他公共文件
        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);
        }

        #endregion

        #region 窗口关闭逻辑
        void MainWindow_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定退出吗？", "询问", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //关闭窗口
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
                //unityhost.Form1_FormClosed();
                m_stopAll.Connect();
                m_stopAll.WriteData(new NetworkVariableData<Boolean>(true));
            }
            //不关闭窗口
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }
        #endregion

        #region 刚度曲线页面
        // 更新刚度曲线
        private void btnSSC_Click(object sender, RoutedEventArgs e)
        {
            if (testTime == "" | m_CurveType == "")
            {
                MessageBox.Show("未查找到数据文件。");
            }
            else
            {
                if (m_CurveType == "动态刚度")
                {
                    xyGraph.RefreshDynamicCurve();
                }
                else
                {
                    xyGraph.RefreshStiffnessCurve();
                }            
                textStiffness.Text = CouplingTesting.testStiffness;
            }
        }

        private void switchManualSaveData_ValueChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if (switchManualSaveData.Value == true)
            {
                CouplingTesting.angelDir = "D:\\MeasuredData\\angelData.dat";
                CouplingTesting.torqueDir = "D:\\MeasuredData\\torqueData.dat";

                _torqueFileName.Connect();
                _torqueFileName.WriteData(new NetworkVariableData<String>("torqueData.dat"));

                _angelFileName.Connect();
                _angelFileName.WriteData(new NetworkVariableData<String>("angelData.dat"));

                _logFile.Connect();
                _logFile.WriteData(new NetworkVariableData<Boolean>(true));
            }
            else
            {
                _logFile.Connect();
                _logFile.WriteData(new NetworkVariableData<Boolean>(false));
            }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            CouplingTesting.angelDir = "D:\\MeasuredData\\angelData.dat";
            CouplingTesting.torqueDir = "D:\\MeasuredData\\torqueData.dat";
            xyGraph.RefreshCurve();
        }

        private void switchManualAuto_ValueChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if(switchManualAuto.Value == true)
            {
                tabAuto.IsEnabled = false;
                tabAuto.FontStyle = FontStyles.Italic;
                tabManual.IsEnabled = true;
                tabManual.FontStyle = FontStyles.Normal;
                yaskawa.writeMB("MB000052", "0");
                Thread.Sleep(20);
                yaskawa.writeMB("MB000051", "1");
            }
            else
            {
                tabAuto.IsEnabled = true;
                tabAuto.FontStyle = FontStyles.Normal;
                tabManual.IsEnabled = false;
                tabManual.FontStyle = FontStyles.Italic;
                yaskawa.writeMB("MB000051", "1");
                Thread.Sleep(20);
                yaskawa.writeMB("MB000052", "0");
            }
        }

        #endregion

        #region 杂项功能模块
        //查找设定力矩点，二分法
        private void BtnSearchTorque_Click(object sender, RoutedEventArgs e)
        {
            int maxDist = m_maxDist;
            int minDist = m_minDist;
            int midDist = (maxDist + minDist) / 2;
            double targetTorque = 0;
            this.Dispatcher.Invoke(new Action(() =>
            {
                targetTorque = Convert.ToDouble(textTargetTorque.Text);
            }));

            for (int i = 0; i < 6; i++)                                             //i控制力矩找中点的时间
            {
                midDist = (maxDist + minDist) / 2;

                this.Dispatcher.Invoke(new Action(() =>
                {
                    textMovePos.Text = Convert.ToString(midDist);
                    textMoveSpeed.Text = "3000";
                    btnMMDistanceSet.Value = true;
                    btnMMDistanceSet_Click(sender, e);
                }));

                Thread.Sleep(100);

                while (true)
                {
                    ushort a = 1;
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        yaskawa.readMB("MB000055", ref a); //运行中
                    }));

                    if (a == 0)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }

                this.Dispatcher.Invoke(new Action(() =>
                {
                    btnMMDistanceSet.Value = false;
                    btnMMDistanceSet_Click(sender, e);
                }));

                Thread.Sleep(1000);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    //旋转电机开始旋转
                    textRotateSpeed.Text = "10";
                    btnRotateCW_Down(sender, e);
                }));

                //判断力矩是否达到

                DateTime startTime = DateTime.Now;
                while (true)
                {
                    //大了往左走
                    if (Math.Abs(_readerTorque.ReadData().GetValue()) > targetTorque)
                    {
                        maxDist = midDist;
                        break;
                    }

                    //小了往右走
                    else
                    {
                        if ((DateTime.Now - startTime).TotalSeconds > 5)
                        {
                            minDist = midDist;
                            break;
                        }
                    }
                }
                Thread.Sleep(500);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    btnRotateCW_Up(sender, e);
                }));
            }

            //循环结束后最后一次移动
            midDist = (maxDist + minDist) / 2;
            this.Dispatcher.Invoke(new Action(() =>
            {
                textMovePos.Text = Convert.ToString(midDist);
                textMoveSpeed.Text = "3000";
                btnMMDistanceSet.Value = true;
                btnMMDistanceSet_Click(sender, e);
            }));

            Thread.Sleep(100);

            while (true)
            {
                ushort a = 1;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    yaskawa.readMB("MB000055", ref a); //运行中
                }));

                if (a == 0)
                {
                    break;
                }
                Thread.Sleep(500);
            }

            this.Dispatcher.Invoke(new Action(() =>
            {
                btnMMDistanceSet.Value = false;
                btnMMDistanceSet_Click(sender, e);
            }));

        }

        //角度调中，使光栅与扭矩传感器正负一致
        private int AngelCalibration()
        {
            int i = 0;

            this.Dispatcher.Invoke(new Action(() =>
            {
                yaskawa.writeML("MB000061", "0"); //模式标志位
                Thread.Sleep(50);
                yaskawa.writeML("MW00023", "10");  //设置速度
                Thread.Sleep(50);
                yaskawa.writeML("ML00080", Convert.ToString(rotatePos));
                Thread.Sleep(50);
                yaskawa.writeMB("MB000025", "1");
                Thread.Sleep(50);
            }));

            int recentPos = rotatePos;
            int searchPos;
            int error = 0;

            while (true)
            {
                searchPos = recentPos + i;
                i = i + 2;

                this.Dispatcher.Invoke(new Action(() =>
                {
                    yaskawa.writeML("OL8090", "10");
                }));

                Thread.Sleep(20);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    yaskawa.writeML("OW8088", "1");
                }));

                Thread.Sleep(20);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    yaskawa.writeML("OL809C", Convert.ToString(searchPos));
                }));

                Thread.Sleep(200);

                if (Math.Abs(_readerTorque.ReadData().GetValue()) < 2)
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        //设置零点
                        _setZero.Connect();
                        _setZero.WriteData(new NetworkVariableData<Boolean>(true));
                    }));
                    Thread.Sleep(1000);

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        _setZero.Connect();
                        _setZero.WriteData(new NetworkVariableData<Boolean>(false));
                    }));
                    error = 0;
                    break;
                }

                if (i > 1000)
                {
                    MessageBox.Show("未找到编码器零点");
                    error = 1;
                    break;
                }
            }

            //断使能
            ushort a = 1;
            this.Dispatcher.Invoke(new Action(() =>
            {
                yaskawa.readMB("MB000066", ref a); //运行中
                Thread.Sleep(50);
                yaskawa.writeML("OW8088", "0");
                Thread.Sleep(50);
                yaskawa.writeMB("MB000025", "0");
            }));

            return error;
        }

        //安装联轴器时，定位编码器中点位置并保持
        private void KeepAngelMid()
        {
            // 1、旋转电机使能
            yaskawa.writeML("MB000061", "0"); //模式标志位
            Thread.Sleep(50);
            yaskawa.writeML("MW00023", "10");  //设置速度
            Thread.Sleep(50);
            yaskawa.writeML("ML00080", Convert.ToString(rotatePos));
            Thread.Sleep(50);
            yaskawa.writeMB("MB000025", "1");
            Thread.Sleep(50);

            // 2、
            int recentPos = rotatePos;
            int searchPos;
            int i = 0;
            double angel1 = _readerAngel.ReadData().GetValue();
            double angel2 = _readerAngel.ReadData().GetValue();


            Int32 extremePoint = 0x00000000;

            this.Dispatcher.Invoke(new Action(() =>
            {
                yaskawa.writeML("OL8090", "15");
            }));

            Thread.Sleep(20);

            this.Dispatcher.Invoke(new Action(() =>
            {
                yaskawa.writeML("OW8088", "1");
            }));

            Thread.Sleep(20);

            recentPos = recentPos + 2;

            this.Dispatcher.Invoke(new Action(() =>
            {
                yaskawa.writeML("OL809C", Convert.ToString(recentPos));
            }));

            Thread.Sleep(3000);

            angel2 = _readerAngel.ReadData().GetValue();

            if (angel1 > angel2)
            {
                while (true)
                {
                    searchPos = recentPos + i;
                    i = i + 2;

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        yaskawa.writeML("OL8090", "15");
                    }));

                    Thread.Sleep(20);

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        yaskawa.writeML("OW8088", "1");
                    }));

                    Thread.Sleep(20);

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        yaskawa.writeML("OL809C", Convert.ToString(searchPos));
                    }));

                    Thread.Sleep(200);

                    angel1 = angel2;
                    angel2 = _readerAngel.ReadData().GetValue();

                    if (angel2 > angel1)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            yaskawa.readML("IL8096", ref extremePoint);   //记录电机编码器位置
                            Thread.Sleep(200);
                            yaskawa.writeML("OL8090", "30");
                            Thread.Sleep(20);
                            yaskawa.writeML("OW8088", "1");
                            Thread.Sleep(20);
                            yaskawa.writeML("OL809C", Convert.ToString(extremePoint - 260));
                        }));
                        Thread.Sleep(3000);

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            //设置零点
                            _setZero.Connect();
                            _setZero.WriteData(new NetworkVariableData<Boolean>(true));
                        }));
                        Thread.Sleep(1000);

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            _setZero.Connect();
                            _setZero.WriteData(new NetworkVariableData<Boolean>(false));
                        }));

                        MessageBox.Show("已经到达轴系中点，请安装联轴器后点击确定");   //程序中断等待轴承安装

                        break;
                    }


                    if (i > 1000)
                    {
                        MessageBox.Show("未找到编码器零点");
                        break;
                    }
                }
            }
            else
            {
                while (true)
                {
                    searchPos = recentPos + i;
                    i = i + 2;

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        yaskawa.writeML("OL8090", "15");
                    }));

                    Thread.Sleep(20);

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        yaskawa.writeML("OW8088", "1");
                    }));

                    Thread.Sleep(20);

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        yaskawa.writeML("OL809C", Convert.ToString(searchPos));
                    }));

                    Thread.Sleep(200);

                    angel1 = angel2;
                    angel2 = _readerAngel.ReadData().GetValue();

                    if (angel2 < angel1)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            yaskawa.readML("IL8096", ref extremePoint);   //记录电机编码器位置
                            Thread.Sleep(200);
                            yaskawa.writeML("OL8090", "30");
                            Thread.Sleep(20);
                            yaskawa.writeML("OW8088", "1");
                            Thread.Sleep(20);
                            yaskawa.writeML("OL809C", Convert.ToString(extremePoint - 260));
                        }));
                        Thread.Sleep(3000);

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            //设置零点
                            _setZero.Connect();
                            _setZero.WriteData(new NetworkVariableData<Boolean>(true));
                        }));
                        Thread.Sleep(1000);

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            _setZero.Connect();
                            _setZero.WriteData(new NetworkVariableData<Boolean>(false));
                        }));

                        MessageBox.Show("已经到达轴系中点，请安装联轴器后点击确定");   //程序中断等待轴承安装

                        break;
                    }


                    if (i > 1000)
                    {
                        MessageBox.Show("未找到编码器零点");
                        break;
                    }
                }
            }

            // 5、安装完成断使能
            yaskawa.writeML("OW8088", "0");
            Thread.Sleep(50);
            yaskawa.writeMB("MB000025", "0");
        }

        //打开PC端网络流
        private void startReadBuffer()
        {
            InteropAssembly.LabVIEWExports.readNetworkBuffer();
        }

        private void textMovePosReal_Changed(object sender, RoutedEventArgs e)
        {
            posMotorSliderView.Value = Convert.ToInt32(textMovePosReal.Text);
            posMotorSliderView2.Value = Convert.ToInt32(textMovePosReal.Text);
        }
        #endregion

        // 主窗口启动事件
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //实例化对象
            timer = new DispatcherTimer();
            //设置触发时间
            timer.Interval = TimeSpan.FromMilliseconds(300);
            //设置触发事件
            timer.Tick += moniter;
            //启动
            timer.Start();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            
            Bitmap bit = new Bitmap(xyGraph.Width+140, xyGraph.Height+20);//实例化一个和窗体一样大的bitmap
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
            g.CopyFromScreen(Convert.ToInt32(this.Left+10), Convert.ToInt32(this.Top+70), 0, 0, new System.Drawing.Size(xyGraph.Width+140, xyGraph.Height+20));//保存整个窗体为图片
                                                                                           //g.CopyFromScreen(panel游戏区 .PointToScreen(Point.Empty), Point.Empty, panel游戏区.Size);//只保存某个控件（这里是panel游戏区）
            bit.Save("CurveTemp.jpg");//默认保存格式为PNG，保存成jpg格式质量不是很好



            WordTool.WordTemplateReplace($"{WordTool.WordTemplateDirectory}报告.docx",
                    $"{WordTool.WordOutputDirectory}检测报告--{DateTime.Now.ToString("yyyymmdd-HHmmssffff")}.docx",
                    new Dictionary<string, string>()
                    {
                        //["CouplingType"] = "2021",
                        //["TestTime"] = "01",
                        //["MaxTorque"] = "28",

                        ["Department"] = textDepartment.Text,
                        ["Customer"] = textCustomer.Text,
                        ["BatchNum"] = textBatchNum.Text,
                        ["CouplingType"] = CouplingC.CouplingId,
                        ["Temp"] = textTemp.Text,
                        ["Num"] = textNum.Text,
                        ["Standard"] = textStandard.Text,
                        ["Humidity"] = textHumidity.Text,
                        ["Shape"] = textShape.Text,
                        ["Diameter"] = textDiameter.Text,
                        ["Area"] = textArea.Text,
                        ["MaxTorque"] = CouplingTesting.testStiffness,
                        ["Tester"] = textTester.Text,
                        ["TestTime"] = textTime.Text,
                        ["Assessor"] = textAssessor.Text,
                    },
                    new Dictionary<string, WordImg>()
                    {
                        ["picture"] = new WordImg(WordTool.TestImagePath, 480, 280),
                    });
        }
    }
}