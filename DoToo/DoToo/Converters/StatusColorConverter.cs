using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DoToo.Converters
{
    
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value 
                ? (Color)Application.Current.Resources["CompletedColor"] 
                : (Color)Application.Current.Resources["ActiveColor"];
        }

        //Note: The ConvertBack method is only used for controls that return data from plain text
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
