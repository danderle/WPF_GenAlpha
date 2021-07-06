namespace GenAlpha.Core
{
    /// <summary>
    /// This class represents the falling text across the screen
    /// </summary>
    public class FallingText
    {
        #region Properties

        /// <summary>
        /// The actual text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The x position of the text
        /// </summary>
        public int Xposition { get; private set; }

        /// <summary>
        /// The y position of the text
        /// </summary>
        public int Yposition { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="text">The actual text</param>
        /// <param name="xSartPosition">The x start position</param>
        public FallingText(string text, int xSartPosition)
        {
            Text = text;
            Xposition = xSartPosition;
            Yposition = 0;
        }

        #endregion
    }
}
