using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using GenAlpha.Core;

namespace GenAlpha
{
    /// <summary>
    /// Converts a <see cref="Connect4ChipStates"/>to Color
    /// </summary>
    public class ChipStateToColorConverter : BaseValueConverter<ChipStateToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Connect4ChipStates chipState = (Connect4ChipStates)value;
            switch(chipState)
            {
                case Connect4ChipStates.StartUnselected:
                    return new SolidColorBrush((Color)Application.Current.FindResource("LightGray"));
                case Connect4ChipStates.Player1:
                    return new SolidColorBrush((Color)Application.Current.FindResource("Player1Red"));
                case Connect4ChipStates.Player2:
                    return new SolidColorBrush((Color)Application.Current.FindResource("Player2Yellow"));
                case Connect4ChipStates.GameOverUnselected:
                    return new SolidColorBrush((Color)Application.Current.FindResource("VeryDarkBlue"));
                default:
                    Debugger.Break();
                    return new SolidColorBrush((Color)Application.Current.FindResource("LightGray"));
            }

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
