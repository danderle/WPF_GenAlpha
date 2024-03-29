﻿namespace GenAlpha.Core
{
    /// <summary>
    /// The design model for design time binding
    /// </summary>
    public class MinesweeperDesignModel: MinesweeperViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the desing model
        /// </summary>
        public static MinesweeperDesignModel Instance { get; set; } = new();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MinesweeperDesignModel()
        {

        }

        #endregion
    }
}
