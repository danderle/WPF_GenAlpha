using System;
using System.Globalization;
using System.Windows;

namespace GenAlpha
{
    /// <summary>
    /// Converts the a bool to visibility
    /// </summary>
    public class BoolToVisibilityValueConverter : BaseValueConverter<BoolToVisibilityValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = (bool)value;
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
