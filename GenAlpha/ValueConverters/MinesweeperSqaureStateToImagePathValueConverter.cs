using GenAlpha.Core;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;

namespace GenAlpha
{
    /// <summary>
    /// Converts a <see cref="MinesweeperSqaureState"/>to an image path
    /// </summary>
    public class MinesweeperSqaureStateToImagePathValueConverter : BaseValueConverter<MinesweeperSqaureStateToImagePathValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MinesweeperSquareState state = (MinesweeperSquareState)value;
            switch(state)
            {
                case MinesweeperSquareState.Zero:
                    return Application.Current.FindResource("0");
                case MinesweeperSquareState.One:
                    return Application.Current.FindResource("1");
                case MinesweeperSquareState.Two:
                    return Application.Current.FindResource("2");
                case MinesweeperSquareState.Three:
                    return Application.Current.FindResource("3");
                case MinesweeperSquareState.Four:
                    return Application.Current.FindResource("4");
                case MinesweeperSquareState.Five:
                    return Application.Current.FindResource("5");
                case MinesweeperSquareState.Six:
                    return Application.Current.FindResource("6");
                case MinesweeperSquareState.Seven:
                    return Application.Current.FindResource("7");
                case MinesweeperSquareState.Eight:
                    return Application.Current.FindResource("8");
                case MinesweeperSquareState.Flag:
                    return Application.Current.FindResource("Flag");
                case MinesweeperSquareState.Bomb:
                    return Application.Current.FindResource("Bomb");
                case MinesweeperSquareState.Unopened:
                    return Application.Current.FindResource("Unopened");
                default:
                    Debugger.Break();
                    return Application.Current.FindResource("Unopened");
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
