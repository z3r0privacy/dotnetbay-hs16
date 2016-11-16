using System;
using System.Globalization;
using System.Windows.Data;

namespace DotNetBay.WPF.Converter
{
    public class BooleanToStatusTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (bool)value;
            return status ? "Open" : "Closed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (string)value;
            return status == "Open";
        }
    }
}
