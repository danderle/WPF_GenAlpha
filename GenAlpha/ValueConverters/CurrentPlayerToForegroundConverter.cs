using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace GenAlpha
{
    /// <summary>
    /// Converts a <see cref="PlayerTurn"/>to Color
    /// </summary>
    public class CurrentPlayerToForegroundConverter : BaseValueConverter<CurrentPlayerToForegroundConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((bool)value)
            {
                return new SolidColorBrush((Color)Application.Current.FindResource("Green"));
            }
            return new SolidColorBrush((Color)Application.Current.FindResource("Gray"));
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
