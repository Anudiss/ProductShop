using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ProductShop.Converters
{
    public class SolidColorBrushToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush == false)
                return null;

            return brush.Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
