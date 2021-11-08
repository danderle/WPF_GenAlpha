using System.Collections.Generic;
using System.Windows;

namespace GenAlpha.Core
{
    /// <summary>
    /// Static class handles the image paths
    /// </summary>
    public static class Image
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        static Image()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a dictionary containing the Minewsweeper
        /// </summary>
        /// <returns></returns>
        public static Dictionary<MinesweeperValues, object> GetMinesweeperImagePaths()
        {
            return new()
            {
                { MinesweeperValues.Zero, Application.Current.FindResource("0") },
                { MinesweeperValues.One, Application.Current.FindResource("1") },
                { MinesweeperValues.Two, Application.Current.FindResource("2") },
                { MinesweeperValues.Three, Application.Current.FindResource("3") },
                { MinesweeperValues.Four, Application.Current.FindResource("4") },
                { MinesweeperValues.Five, Application.Current.FindResource("5") },
                { MinesweeperValues.Six, Application.Current.FindResource("6") },
                { MinesweeperValues.Seven, Application.Current.FindResource("7") },
                { MinesweeperValues.Eight, Application.Current.FindResource("8") },
                { MinesweeperValues.Flag, Application.Current.FindResource("Flag") },
                { MinesweeperValues.Bomb, Application.Current.FindResource("Bomb") },
                { MinesweeperValues.Unopened, Application.Current.FindResource("Unopened") },
            };
        }
        #endregion
    }
}
