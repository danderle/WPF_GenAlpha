namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="KeyboardShooterPage"/>
    /// </summary>
    public class KeyboardShooterViewModel : BaseViewModel
    {
        #region Private Members

        #endregion

        #region Properties

        /// <summary>
        /// The view model for the top bar
        /// </summary>
        public TopBarViewModel TopBar { get; set; } = new();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public KeyboardShooterViewModel()
        {
        }

        #endregion
    }
}
