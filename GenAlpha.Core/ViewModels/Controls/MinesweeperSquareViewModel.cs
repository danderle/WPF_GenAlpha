using System;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the Minesweeper buttons
    /// </summary>
    public class MinesweeperSquareViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The item which is flagged
        /// </summary>
        public MinesweeperValues FlaggedState;

        #endregion

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
        /// Trigger action when a bomb is marked
        /// </summary>
        private readonly Action<bool> BombMarked;

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
        /// The value of the square
        /// </summary>
        public MinesweeperValues FaceValue { get; set; } = MinesweeperValues.Unopened;

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
        public MinesweeperSquareViewModel(int row, int col, Action<int, int> squareClicked, Action bombRevealed, Action<bool> bombMarked)
        {
            Row = row;
            Column = col;
            SquareClicked = squareClicked;
            BombRevealed = bombRevealed;
            BombMarked = bombMarked;
            InitializeCommands();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// The click command method
        /// </summary>
        private void Click()
        {
            if (FaceValue != MinesweeperValues.Flag && !IsRevealed)
            {
                IsRevealed = true;
                if (FaceValue == MinesweeperValues.Bomb)
                {
                    BombRevealed();
                }
                else if (FaceValue == MinesweeperValues.Zero)
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
                if (FaceValue == MinesweeperValues.Flag)
                {
                    FaceValue = FlaggedState;
                    BombMarked(false);
                }
                else
                {
                    FlaggedState = FaceValue;
                    FaceValue = MinesweeperValues.Flag;
                    BombMarked(true);
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
