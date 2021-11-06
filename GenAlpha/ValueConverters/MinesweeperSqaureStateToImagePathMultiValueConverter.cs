using GenAlpha.Core;
using System;
using System.Globalization;

namespace GenAlpha
{
    /// <summary>
    /// Converts a bool and a <see cref="MinesweeperSqaureState"/>to an image path
    /// </summary>
    public class MinesweeperSqaureStateToImagePathMultiValueConverter : BaseMultiValueConverter<MinesweeperSqaureStateToImagePathMultiValueConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            MinesweeperSquareState state = (MinesweeperSquareState)values[1];
            if (values[0] is bool && (bool)values[0])
            {
                return MinesweeperViewModel.MinesweeperImagePaths[state];
            }
            else
            {
                switch (state)
                {
                    case MinesweeperSquareState.Flag:
                        return MinesweeperViewModel.MinesweeperImagePaths[state];
                    default:
                        return MinesweeperViewModel.MinesweeperImagePaths[MinesweeperSquareState.Unopened];
                }
            }
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
