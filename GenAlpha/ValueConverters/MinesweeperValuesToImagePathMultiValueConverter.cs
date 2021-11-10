using GenAlpha.Core;
using System;
using System.Globalization;

namespace GenAlpha
{
    /// <summary>
    /// Converts a bool and a <see cref="MinesweeperValues"/>to an image path
    /// </summary>
    public class MinesweeperValuesToImagePathMultiValueConverter : BaseMultiValueConverter<MinesweeperValuesToImagePathMultiValueConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            MinesweeperValues faceValue = (MinesweeperValues)values[1];
            if (values[0] is bool && (bool)values[0])
            {
                return MinesweeperViewModel.MinesweeperImagePaths[faceValue];
            }
            else
            {
                switch (faceValue)
                {
                    case MinesweeperValues.Flag:
                        return MinesweeperViewModel.MinesweeperImagePaths[faceValue];
                    default:
                        return MinesweeperViewModel.MinesweeperImagePaths[MinesweeperValues.Unopened];
                }
            }
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
