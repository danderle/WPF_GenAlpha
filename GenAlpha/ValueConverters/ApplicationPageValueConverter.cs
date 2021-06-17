using GenAlpha.Core;
using System;
using System.Diagnostics;
using System.Globalization;

namespace GenAlpha
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Find the appropriate page
            switch((ApplicationPage)value)
            {
                case ApplicationPage.GameSelection:
                    return new GameSelectionPage();
                case ApplicationPage.Memory:
                    return new MemoryPage();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
