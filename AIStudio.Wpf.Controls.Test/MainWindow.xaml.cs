using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using AIStudio.Wpf.Controls.Helper;

namespace AIStudio.Wpf.Controls.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [STAThread]
        [LoaderOptimization(LoaderOptimization.MultiDomainHost)]
        private void menuPrism_Click(object sender, RoutedEventArgs e)
        {
#if NETFRAMEWORK
            var domain = AppDomain.CreateDomain("AIStudio.Wpf.PrismRegions.Demo");

            domain.DoCallBack(new CrossAppDomainDelegate(() => {
                Thread thread = new Thread(() => {
                    AIStudio.Wpf.PrismRegions.Demo.App app = new PrismRegions.Demo.App();
                    app.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
                    app.Run();
                });
                thread.Name = AppDomain.CurrentDomain.FriendlyName;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }));
#else
            System.Diagnostics.Process.Start("PrismRegions.Demo/AIStudio.Wpf.PrismRegions.Demo.exe");
#endif
        }
     
    }
}
