using System;
using System.Globalization;
using System.Windows.Data;

namespace ProductShop.Converters
{
    public class ProductCountToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal count == false)
                return null;

            return count > 0 ? $"Количество: {count}" : "Нет в наличии";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
