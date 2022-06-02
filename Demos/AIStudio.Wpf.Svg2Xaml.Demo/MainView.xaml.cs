using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Svg2Xaml;

namespace AIStudio.Wpf.Svg2Xaml.Demo
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            using (FileStream stream = new FileStream(Image.Tag?.ToString(), FileMode.Open, FileAccess.Read))
            { 
                Image.Source = SvgReader.Load(stream);
            }
        }

    }
}
