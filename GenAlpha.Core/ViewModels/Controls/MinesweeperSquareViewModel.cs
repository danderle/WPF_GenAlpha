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

        #endregion

        #region Properties

        /// <summary>
        /// Flag to let us know if it has been clicked
        /// </summary>
        public bool IsClicked { get; set; }

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
        public MinesweeperSquareState SqaureState { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command for clicking
        /// </summary>
        public ICommand ClickCommand { get; set; }

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
            SqaureState = state;
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
            SquareClicked(Column);
        }

        #endregion

        #region Private Helpers Methods

        /// <summary>
        /// Initializes all the commands
        /// </summary>
        private void InitializeCommands()
        {
            ClickCommand = new RelayCommand(Click);
        }

        #endregion
    }
}
