using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace GenAlpha
{
    /// <summary>
    /// Converts a <see cref="byte[]"/>to Color
    /// </summary>
    public class RGBhexToColorConverter : BaseValueConverter<RGBhexToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var array = (byte[])value;
            if (array == null || array.Length != 3)
                return new SolidColorBrush((Color)Application.Current.FindResource("LightGray"));

            return new SolidColorBrush(Color.FromRgb(array[0], array[1], array[2]));
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
