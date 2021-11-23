using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the Minesweeper Field
    /// </summary>
    public class MinesweeperFieldViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Rows for the minesweeper field
        /// </summary>
        public int NumberOfRows { get; private set; }

        /// <summary>
        /// Columns for the minesweeper field
        /// </summary>
        public int NumberOfColumns { get; private set; }

        /// <summary>
        /// The number of bombs
        /// </summary>
        public int NumberOfBombs { get; private set; }

        /// <summary>
        /// The sqaures that make up the field
        /// </summary>
        public ObservableCollection<MinesweeperSquareViewModel> Squares { get; set; } = new ObservableCollection<MinesweeperSquareViewModel>();

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
        public MinesweeperFieldViewModel(int rows, int columns, int bombs, Action bombRevealed, Action<bool> bombMarked)
        {
            NumberOfRows = rows;
            NumberOfColumns = columns;
            NumberOfBombs = bombs;

            int row = 0;
            int col = 0;
            for (int i = 0; i < NumberOfRows * NumberOfColumns; i++)
            {
                if (col >= NumberOfColumns)
                {
                    col = 0;
                    row++;
                }
                int index = (NumberOfColumns * row) + col;
                MinesweeperSquareViewModel square = new(row, col, SquareClicked, bombRevealed, bombMarked);
                Squares.Add(square);
                col++;
            }
            Reset();
        }

        /// <summary>
        /// Resets the field
        /// </summary>
        public void Reset()
        {
            int index = 0;
            bool[] bombIndexes = GetRandomBombPositions();
            foreach (MinesweeperSquareViewModel square in Squares)
            {
                square.IsRevealed = false;
                square.FaceValue = bombIndexes[index] ? MinesweeperValues.Bomb : MinesweeperValues.Zero;
                index++;
            }

            SetSquareValues();
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// The action method which is triggered when a square has been clicked
        /// </summary>
        /// <param name="column"></param>
        private void SquareClicked(int row, int column)
        {
            RevealSurrounding(row, column);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reveals all the bomb positions
        /// </summary>
        public void ShowAllBombs()
        {
            foreach (MinesweeperSquareViewModel square in Squares)
            {
                if (square.FaceValue == MinesweeperValues.Bomb)
                {
                    square.IsRevealed = true;
                }
            }
        }

        #endregion

        #region Command Methods

        #endregion

        #region Private Helpers Methods

        /// <summary>
        /// Creates an array of the size of the field and sets true if a bomb is present
        /// </summary>
        /// <returns></returns>
        private bool[] GetRandomBombPositions()
        {
            // Create array with same size of field
            bool[] bombIndexs = new bool[NumberOfRows * NumberOfColumns];

            // Set the bombs to true
            for (int i = 0; i < NumberOfBombs; i++)
            {
                bombIndexs[i] = true;
            }

            // Shuffle and return
            return bombIndexs.Shuffle();
        }

        /// <summary>
        /// Sets the square state with the number of surrounding bombs
        /// </summary>
        private void SetSquareValues()
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int column = 0; column < NumberOfColumns; column++)
                {
                    MinesweeperSquareViewModel square = GetSquareFromField(row, column);
                    if (square.FaceValue != MinesweeperValues.Bomb)
                    {
                        int count = GetCountOfSurroundingBombs(row, column);
                        square.FaceValue = (MinesweeperValues)count;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a square from the field
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private MinesweeperSquareViewModel GetSquareFromField(int row, int column)
        {
            int index = (NumberOfColumns * row) + column;
            return Squares[index];
        }

        /// <summary>
        /// Returns the count of the number of surrounding bombs
        /// </summary>
        /// <param name="middleRow">current row index</param>
        /// <param name="middleColumn">current column index</param>
        /// <returns></returns>
        private int GetCountOfSurroundingBombs(int middleRow, int middleColumn)
        {
            int count = 0;
            for (int row = middleRow - 1; row <= middleRow + 1; row++)
            {
                for (int column = middleColumn - 1; column <= middleColumn + 1; column++)
                {
                    if (IsValidIndex(row, column))
                    {
                        MinesweeperSquareViewModel square = GetSquareFromField(row, column);
                        count += square.FaceValue == MinesweeperValues.Bomb ? 1 : 0;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Returns true if the row and column make a valid index
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool IsValidIndex(int row, int column)
        {
            if (row >= 0 && row < NumberOfRows)
            {
                if (column >= 0 && column < NumberOfColumns)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Reveals the surrounding squares
        /// </summary>
        /// <param name="middleRow"></param>
        /// <param name="middleColumn"></param>
        private void RevealSurrounding(int middleRow, int middleColumn)
        {
            for (int row = middleRow - 1; row <= middleRow + 1; row++)
            {
                for (int column = middleColumn - 1; column <= middleColumn + 1; column++)
                {
                    if (IsValidIndex(row, column))
                    {
                        MinesweeperSquareViewModel square = GetSquareFromField(row, column);
                        Reveal(row, column, square);
                    }
                }
            }
        }

        /// <summary>
        /// Reveals a sqaure if not already revealed or if a bomb
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="square"></param>
        private void Reveal(int row, int column, MinesweeperSquareViewModel square)
        {
            if (!square.IsRevealed && square.FaceValue != MinesweeperValues.Bomb)
            {
                square.IsRevealed = true;
                if (square.FaceValue == MinesweeperValues.Zero)
                {
                    RevealSurrounding(row, column);
                }
            }
        }

        #endregion
    }
}
