namespace GenAlpha
{
    /// <summary>
    /// The design model for design time binding
    /// </summary>
    public class GameSelectionDesignModel : GameSelectionViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the desing model
        /// </summary>
        public static GameSelectionDesignModel Instance { get; set; } = new();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameSelectionDesignModel()
        {
        }

        #endregion
    }
}
