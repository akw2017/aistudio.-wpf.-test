using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using AIStudio.Wpf.AvalonDockPrism.Views;
using Prism.Ioc;
using AIStudio.Wpf.AvalonDockPrism.Avalon;
using System.Windows.Threading;
using System;
using System.Windows.Controls;

namespace AIStudio.Wpf.AvalonDockPrism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainerExtension _container;

        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }


        public MainWindowViewModel(IRegionManager regionManager,  IContainerExtension container)
        {
            _regionManager = regionManager;
            _container = container;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }

    }
}
