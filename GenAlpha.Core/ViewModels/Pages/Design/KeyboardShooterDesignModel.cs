namespace GenAlpha.Core
{
    /// <summary>
    /// The design model for design time binding
    /// </summary>
    public class KeyboardShooterDesignModel : KeyboardShooterViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the desing model
        /// </summary>
        public static KeyboardShooterDesignModel Instance { get; set; } = new();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public KeyboardShooterDesignModel()
        {
        }

        #endregion
    }
}
