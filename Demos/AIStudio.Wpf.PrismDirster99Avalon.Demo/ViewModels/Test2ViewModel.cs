using AIStudio.Wpf.AvalonDockPrism.Avalon;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStudio.Wpf.AvalonDockPrism.ViewModels
{
    public class Test2ViewModel : DockWindowViewModel, INavigationAware
    {

        private int _pageViews;
        public int PageViews
        {
            get { return _pageViews; }
            set { SetProperty(ref _pageViews, value); }
        }
        IRegionManager _regionManager;

        public Test2ViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Title = "Test2";
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            PageViews++;
        }


    }
}
