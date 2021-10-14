namespace GenAlpha.Core
{
    /// <summary>
    /// Player class used for games with multiple players
    /// </summary>
    public class Player : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Flag to know if its the currentPlayer
        /// </summary>
        public bool CurrentPlayer { get; set; }

        /// <summary>
        /// The score of this player
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The position of this player
        /// </summary>
        public PlayerTurn Position { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The Overloaded constructor
        /// </summary>
        /// <param name="position"></param>
        public Player(PlayerTurn position)
        {
            Position = position;
            CurrentPlayer = Position == PlayerTurn.Player1;
        }

        #endregion
    }
}
