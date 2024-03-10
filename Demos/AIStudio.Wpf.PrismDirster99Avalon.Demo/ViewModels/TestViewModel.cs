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
    public class TestViewModel : DockWindowViewModel, INavigationAware
    {
        private int _pageViews;
        public int PageViews
        {
            get { return _pageViews; }
            set { SetProperty(ref _pageViews, value); }
        }

        public TestViewModel()
        {
            Title = "Test";
            MaxTabItemNumber = 3;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            PageViews++;
        }
    }
}
