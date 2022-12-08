﻿using ProductShop.Cookie;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ProductShop.Converters
{
    public class PermissionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is Permission.Permission permission == false)
                return null;

            return Session.Instance.User.HasPermission(permission) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
