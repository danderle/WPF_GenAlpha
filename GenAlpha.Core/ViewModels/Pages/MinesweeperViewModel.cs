using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="MinesweeperPage"/>
    /// </summary>
    public class MinesweeperViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Flag to let us know if the game has ended
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// The winning moves
        /// </summary>
        public int Moves { get; set; }

        /// <summary>
        /// The winner
        /// </summary>
        public PlayerTurn Winner { get; set; } = PlayerTurn.Player1;

        /// <summary>
        /// Rows for the minesweeper field
        /// </summary>
        public int NumberOfRows { get; private set; } = 20;

        /// <summary>
        /// Columns for the minesweeper field
        /// </summary>
        public int NumberOfColumns { get; private set; } = 20;

        /// <summary>
        /// The number of bombs
        /// </summary>
        public int NumberOfBombs { get; private set; } = 10;

        /// <summary>
        /// The side menu view model
        /// </summary>
        public SideMenuViewModel SideMenu { get; set; } = new SideMenuViewModel();

        /// <summary>
        /// The list of the minesweeper field
        /// </summary>
        public ObservableCollection<MinesweeperSquareViewModel> Field { get; set; }

        /// <summary>
        /// A dictionary holding the image paths according to the square states
        /// </summary>
        public static Dictionary<MinesweeperSquareState, object> MinesweeperImagePaths => Image.GetMinesweeperImagePaths();

        #endregion

        #region Commands

        /// <summary>
        /// The command to restart the game
        /// </summary>
        public ICommand RestartGameCommand { get; set; }

        /// <summary>
        /// The command to show/hidew the side menu
        /// </summary>
        public ICommand ToggleSideMenuCommand { get; set; }
        
        /// <summary>
        /// The command to go back to the game selection menu
        /// </summary>
        public ICommand ToGameSelectionCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MinesweeperViewModel()
        {
            InitializeCommands();
            InitializeProperties();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Resets the game properties
        /// </summary>
        private void RestartGame()
        {
            GameOver = false;
            Moves = 0;
        }

        /// <summary>
        /// The command method to show / hide the side menu
        /// </summary>
        private void ToggleSideMenu()
        {
            SideMenu.ShowSideMenu = !SideMenu.ShowSideMenu;
            if (!SideMenu.ShowSideMenu)
            {
                RestartGame();
            }
        }

        /// <summary>
        /// Switches the page to return to the game selection page
        /// </summary>
        private async void GoToGameSelctionAsync()
        {
            DI.Service<ApplicationViewModel>().GoToPage(ApplicationPage.GameSelection);

            await Task.Delay(1);
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// The action method which is triggered when a square has been clicked
        /// </summary>
        /// <param name="column"></param>
        private void SquareClicked(int column)
        {
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Initialize all the commands
        /// </summary>
        private void InitializeCommands()
        {
            RestartGameCommand = new RelayCommand(RestartGame);
            ToggleSideMenuCommand = new RelayCommand(ToggleSideMenu);
            ToGameSelectionCommand = new RelayCommand(GoToGameSelctionAsync);
        }

        /// <summary>
        /// Initalize all the properties
        /// </summary>
        private void InitializeProperties()
        {
            SideMenu.ShowSideMenu = false;
            CreateGameField();
        }

        /// <summary>
        /// Creates all the squares inside the field and sets the column, row and index of the chip
        /// </summary>
        private void CreateGameField()
        {
            bool[] bombIndexes = GetRandomBombPositions();

            Field = new ObservableCollection<MinesweeperSquareViewModel>();
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
                MinesweeperSquareViewModel square = new(row, col, index, bombIndexes[i]? MinesweeperSquareState.Bomb : MinesweeperSquareState.Unopened, SquareClicked);
                Field.Add(square);
                col++;
            }

            SetSquareValues();
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
                    var index = (NumberOfColumns * row) + column;
                    if (Field[index].SquareState != MinesweeperSquareState.Bomb)
                    {
                        int count = GetCountOfSurroundingBombs(row, column);
                        Field[index].SquareState = (MinesweeperSquareState)count;
                    }
                }
            }
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
                    if (row >= 0 && row < NumberOfRows)
                    {
                        if (column >= 0 && column < NumberOfColumns)
                        {
                            int index = (NumberOfColumns * row) + column;
                            count += Field[index].SquareState == MinesweeperSquareState.Bomb ? 1 : 0;
                        }
                    }
                }
            }
            return count;
        }

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
        #endregion
    }
}
