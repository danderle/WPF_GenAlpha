using GenAlpha.Core;

namespace GenAlpha
{
    /// <summary>
    /// Locates view model from the ioc for use in binding in xaml files
    /// </summary>
    public class ViewModelLocator
    {
        #region Public Properties

        /// <summary>
        /// Singleton instance of the locator
        /// </summary>
        public static ViewModelLocator Instance { get; set; } = new();

        /// <summary>
        /// The application view model
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => DI.Service<ApplicationViewModel>();

        #endregion
    }
}
