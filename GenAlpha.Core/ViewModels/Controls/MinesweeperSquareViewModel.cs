using System;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the Minesweeper buttons
    /// </summary>
    public class MinesweeperSquareViewModel : BaseViewModel
    {
        #region Actions

        /// <summary>
        /// Trigger action when a square is clicked
        /// </summary>
        private readonly Action<int, int> SquareClicked;

        /// <summary>
        /// Trigger action when a bomb is revealed
        /// </summary>
        private readonly Action BombRevealed;

        /// <summary>
        /// The item which is flagged
        /// </summary>
        private MinesweeperSquareState flaggedState;

        #endregion

        #region Properties

        /// <summary>
        /// Flag to let us know if it has been clicked
        /// </summary>
        public bool IsClicked { get; set; }

        /// <summary>
        /// Flag to let us know if the sqaure is revealed
        /// </summary>
        public bool IsRevealed { get; set; }

        /// <summary>
        /// The column in which this button is in
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// The row in which this button is on
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// The state of the square
        /// </summary>
        public MinesweeperSquareState SquareState { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command for clicking
        /// </summary>
        public ICommand ClickCommand { get; set; }

        /// <summary>
        /// The command for right clicking
        /// </summary>
        public ICommand RightClickCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MinesweeperSquareViewModel(int row, int col, MinesweeperSquareState state, Action<int, int> squareClicked, Action bombRevealed)
        {
            Row = row;
            Column = col;
            SquareState = state;
            SquareClicked = squareClicked;
            BombRevealed = bombRevealed;
            InitializeCommands();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// The click command method
        /// </summary>
        private void Click()
        {
            if (SquareState != MinesweeperSquareState.Flag && !IsRevealed)
            {
                IsRevealed = true;
                if (SquareState == MinesweeperSquareState.Bomb)
                {
                    BombRevealed();
                }
                else
                {
                    SquareClicked(Row, Column);
                }
            }
        }

        /// <summary>
        /// The right click command
        /// </summary>
        private void RightClick()
        {
            if(!IsRevealed)
            {
                if (SquareState == MinesweeperSquareState.Flag)
                {
                    SquareState = flaggedState;
                }
                else
                {
                    flaggedState = SquareState;
                    SquareState = MinesweeperSquareState.Flag;
                }
            }
        }

        #endregion

        #region Private Helpers Methods

        /// <summary>
        /// Initializes all the commands
        /// </summary>
        private void InitializeCommands()
        {
            ClickCommand = new RelayCommand(Click);
            RightClickCommand = new RelayCommand(RightClick);
        }

        #endregion
    }
}
