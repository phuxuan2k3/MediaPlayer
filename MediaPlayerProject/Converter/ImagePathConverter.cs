using System;
using System.Globalization;
using System.Windows.Data;

namespace MediaPlayerProject.Converter
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string relative = (string)value;
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string absolute = $"{folder}/assets/images/{relative}";
            return absolute;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
