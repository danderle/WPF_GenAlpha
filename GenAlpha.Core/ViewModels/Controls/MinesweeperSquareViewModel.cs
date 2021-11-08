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
        private MinesweeperValues flaggedState;

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
        public MinesweeperValues Value { get; set; }

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
        public MinesweeperSquareViewModel(int row, int col, MinesweeperValues state, Action<int, int> squareClicked, Action bombRevealed)
        {
            Row = row;
            Column = col;
            Value = state;
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
            if (Value != MinesweeperValues.Flag && !IsRevealed)
            {
                IsRevealed = true;
                if (Value == MinesweeperValues.Bomb)
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
                if (Value == MinesweeperValues.Flag)
                {
                    Value = flaggedState;
                }
                else
                {
                    flaggedState = Value;
                    Value = MinesweeperValues.Flag;
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
