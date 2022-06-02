using Svg2Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Svg2Xaml
{
    /// <summary>
    /// SvgToImageConverter，AIStudio扩展
    /// </summary>
    public class SvgToImageConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null && parameter == null) return null;
            string _path = string.Empty;
            string _fill = string.Empty;

            if (value != null)
            {
                _path = value.ToString();
                if (parameter != null)
                {
                    _fill = parameter.ToString();
                }
            }
            else if (parameter != null)
            {
                _path = parameter.ToString();
            }
            using (FileStream stream = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                SvgReaderOptions options = new SvgReaderOptions();
                options.IgnoreEffects = false;
                if (!string.IsNullOrEmpty(_fill))
                {
                    options.Fill = _fill;
                }
                return SvgReader.Load(stream, options);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
