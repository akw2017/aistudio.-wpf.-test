using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AIStudio.Wpf.Svg2XamlExtension
{
    public class PackSvg : Control
    {
        public static Lazy<IDictionary<Tuple<string, string>, string>> DataIndex;
        static PackSvg()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackSvg), new FrameworkPropertyMetadata(typeof(PackSvg)));

            if (DataIndex == null)
                DataIndex = new Lazy<IDictionary<Tuple<string, string>, string>>(PackSvgDataFactory.Create);
        }

        public PackSvg()
        {
            //读取资源字典文件
            ResourceDictionary rd = new ResourceDictionary();
            rd.Source = new Uri("/AIStudio.Wpf.Svg2XamlExtension;component/Control/PackSvg.xaml", UriKind.Relative);
            if (!this.Resources.MergedDictionaries.Any(p => p.Source == rd.Source))
            {
                this.Resources.MergedDictionaries.Add(rd);
            }
            //获取样式
            this.Style = this.FindResource("DefaultPackSvgStyle") as Style;

        }

        public static readonly DependencyProperty KindProperty
           = DependencyProperty.Register(nameof(Kind), typeof(string), typeof(PackSvg), new PropertyMetadata(default(string), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PackSvg)dependencyObject).UpdateData();
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public string Kind
        {
            get { return (string)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public static readonly DependencyProperty FillProperty
           = DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(PackSvg), new PropertyMetadata(default(Brush), FillPropertyChangedCallback));

        private static void FillPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PackSvg)dependencyObject).UpdateData();
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty PathProperty
            = DependencyProperty.Register(nameof(Path), typeof(string), typeof(PackSvg), new PropertyMetadata(""));

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary>
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        private static readonly DependencyProperty ThemeProperty
          = DependencyProperty.Register(nameof(Theme), typeof(string), typeof(PackSvg), new PropertyMetadata("outline"));

        /// <summary>
        /// fill,outline,twotone,default=outline  <see cref="Theme"/>. 
        /// </summary>
        public string Theme
        {
            get { return (string)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateData();
        }


        internal void UpdateData()
        {
            if (string.IsNullOrEmpty(Kind)) return;
            string path = null;
            if (DataIndex.Value != null)
            {
                if (!string.IsNullOrEmpty(Theme))
                    DataIndex.Value.TryGetValue(new Tuple<string, string>(Theme, Kind), out path);
                if (string.IsNullOrEmpty(path))
                    DataIndex.Value.TryGetValue(new Tuple<string, string>("", Kind), out path);
            }

            Path = path;
        }
    }
}
