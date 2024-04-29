using System;
using System.Globalization;
using Xamarin.Forms;

namespace DoToo.Converters
{
    public class FilterTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value
                ? "Filter: " + (string)Application.Current.Resources["AllLabel"]
                : "Filter: " + (string)Application.Current.Resources["ActiveLabel"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
