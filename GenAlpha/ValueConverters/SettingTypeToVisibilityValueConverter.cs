using System;
using System.Globalization;
using System.Windows;

namespace GenAlpha
{
    /// <summary>
    /// Converts a <see cref="SettingTypes"/>to visibility
    /// </summary>
    public class SettingTypeToVisibilityValueConverter : BaseValueConverter<SettingTypeToVisibilityValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var setting = value.ToString();
            var converterParameter = (string)parameter;
            return setting == converterParameter ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
