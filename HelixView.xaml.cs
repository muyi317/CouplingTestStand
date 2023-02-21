using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Helix3D
{
    /// <summary>
    /// HelixView.xaml 的交互逻辑
    /// </summary>
    public partial class HelixView : UserControl
    {
        public HelixView()
        {
            InitializeComponent();
        }
        private void BatchedMeshGeometryModel3D_Mouse3DDown(object sender, HelixToolkit.Wpf.SharpDX.MouseDown3DEventArgs e)
        {
            viewModel.SelectedGeometry = e.HitTestResult.Geometry;
        }
    }
}
