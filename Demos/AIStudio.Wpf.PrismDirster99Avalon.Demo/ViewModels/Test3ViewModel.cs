using AIStudio.Wpf.AvalonDockPrism.Avalon;
using AIStudio.Wpf.PrismAvalonExtensions.DockStrategies;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStudio.Wpf.AvalonDockPrism.ViewModels
{
    public class Test3ViewModel : DockWindowViewModel, INavigationAware
    {
        private int _pageViews;
        public int PageViews
        {
            get
            {
                return _pageViews;
            }
            set
            {
                SetProperty(ref _pageViews, value);
            }
        }

        public Test3ViewModel()
        {
            Title = "Test3";
            CanClose = false;
            DockStrategy = new SideDockStrategy() { Side = DockSide.Left };
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            PageViews++;
        }

        public DockStrategy DockStrategy
        {
            get; set;
        }
    }
}
