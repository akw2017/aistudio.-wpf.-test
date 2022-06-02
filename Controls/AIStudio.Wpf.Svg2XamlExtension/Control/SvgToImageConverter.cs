using Svg2Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AIStudio.Wpf.Svg2XamlExtension
{
    public class SvgToImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 1) return DependencyProperty.UnsetValue;   

            string _path = values[0] as string;
            if (string.IsNullOrEmpty(_path))
            {
                return DependencyProperty.UnsetValue;
            }

            string _fill = string.Empty;
            if (values.Length > 1 && values[1] != null)
            {
                _fill = values[1].ToString();
            }
         
            try
            {
                using (FileStream stream = new FileStream(_path, FileMode.Open, FileAccess.Read))
                {
                    SvgReaderOptions options = new SvgReaderOptions();
                    options.IgnoreEffects = false;
                    if (!string.IsNullOrEmpty(_fill) && _fill != "#00FFFFFF")
                    {
                        options.Fill = _fill;
                    }
                    return SvgReader.Load(stream, options);
                }
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
