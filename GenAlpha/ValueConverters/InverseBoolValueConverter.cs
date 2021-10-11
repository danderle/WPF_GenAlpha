﻿using System;
using System.Globalization;

namespace GenAlpha
{
    /// <summary>
    /// Inverts a bool value
    /// </summary>
    public class InverseBoolValueConverter : BaseValueConverter<InverseBoolValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
