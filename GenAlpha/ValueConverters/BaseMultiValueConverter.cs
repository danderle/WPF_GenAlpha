using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace GenAlpha
{
    /// <summary>
    /// A base multi value converter that allows direct xaml usage
    /// </summary>
    /// <typeparam name="T">The type if this value converter</typeparam>
    public abstract class BaseMultiValueConverter<T> : MarkupExtension, IMultiValueConverter
        where T : class, new()
    {
        #region Private Members

        /// <summary>
        /// A single static instance of this value converter
        /// </summary>
        private static T mConverter = null;

        /// <summary>
        /// Provide a static instance of the value converter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new T());
        }

        #endregion

        #region Value Converter Methods

        /// <summary>
        /// The method to convert multiple types to another
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// The method to convert a value back to its source types
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture);

        #endregion
    }
}
