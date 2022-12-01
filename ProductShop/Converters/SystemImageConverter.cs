using ProductShop.Connection;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ProductShop.Converters
{
    public class SystemImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string == false)
                return null;

            return DatabaseContext.Entities.SystemImage.Local.FirstOrDefault(image => image.Name == parameter as string).BitmapSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
