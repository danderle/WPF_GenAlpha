﻿namespace GenAlpha.Core
{
    /// <summary>
    /// This class represents the falling text across the screen
    /// </summary>
    public class FallingText
    {
        #region Constants

        private const int DEFAULT_SPEED = 1;

        #endregion

        #region Properties

        /// <summary>
        /// Flag to let us knwow if this text is targeted
        /// </summary>
        public bool Targeted { get; set; }

        /// <summary>
        /// The actual text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The displayed text
        /// </summary>
        public string DisplayedText { get; set; }

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
            DisplayedText = text;
            Xposition = xSartPosition;
            Yposition = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the text at its spedd
        /// </summary>
        public void Move()
        {
            Yposition += DEFAULT_SPEED;
        } 
        #endregion
    }
}