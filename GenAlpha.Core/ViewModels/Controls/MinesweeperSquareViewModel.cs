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
        private readonly Action<int> SquareClicked;

        private MinesweeperSquareState hiddenState;

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
        /// The index of the button
        /// </summary>
        public int Index { get; }

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
        public MinesweeperSquareViewModel(int row, int col, int index, MinesweeperSquareState state, Action<int> squareClicked)
        {
            Row = row;
            Column = col;
            Index = index;
            SquareState = state;
            SquareClicked = squareClicked;
            InitializeCommands();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// The click command method
        /// </summary>
        private void Click()
        {
            IsRevealed = true;
            SquareClicked(Column);
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
                    SquareState = hiddenState;
                }
                else
                {
                    hiddenState = SquareState;
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
