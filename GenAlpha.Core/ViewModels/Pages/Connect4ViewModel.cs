using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="Connect4Page"/>
    /// </summary>
    public class Connect4ViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// increases the margin of the losers chip which will shrink the losers chips
        /// </summary>
        private readonly Thickness loserChipsMargin = new Thickness(30);

        /// <summary>
        /// Flag to let us know if a chip is falling. If a chip is falling we cannot add another
        /// </summary>
        private bool chipIsFalling;

        #endregion

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
        /// The current players turn
        /// </summary>
        public PlayerTurn CurrentPlayer { get; private set; } = PlayerTurn.Player1;

        /// <summary>
        /// The winner
        /// </summary>
        public PlayerTurn Winner { get; set; } = PlayerTurn.Player1;

        /// <summary>
        /// Rows for the connect4 field
        /// </summary>
        public int NumberOfRows => 6;

        /// <summary>
        /// Columns for the connect4 field
        /// </summary>
        public int NumberOfColumns => 7;

        /// <summary>
        /// The side menu view model
        /// </summary>
        public SideMenuViewModel SideMenu { get; set; } = new SideMenuViewModel();

        /// <summary>
        /// The list of the connect4 field
        /// </summary>
        public ObservableCollection<Connect4ChipViewModel> Field { get; set; } = new ObservableCollection<Connect4ChipViewModel>();

        /// <summary>
        /// The list of playing players
        /// </summary>
        public ObservableCollection<Player> Players { get; set; }

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
        public Connect4ViewModel()
        {
            InitializeCommands();
            InitializeProperties();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Creates new memory cards and restarts game
        /// </summary>
        private void RestartGame()
        {
            GameOver = false;
            Moves = 0;
            CreatePlayers();
            ResetField();
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
        /// The action method which is triggered when a chip has been clicked
        /// </summary>
        /// <param name="column"></param>
        private async void ChipClicked(int column)
        {
            // cannot add another chip while previous is still falling or gameover
            if (!chipIsFalling && !GameOver)
            {
                chipIsFalling = true;
                int col = column;
                bool chipSet = false;
                // Cycles from the bottom row up to check which chip is not set
                for (int row = NumberOfRows - 1; row >= 0; row--)
                {
                    int index = (NumberOfColumns * row) + col;
                    // true if no player has set this chip
                    if (Field[index].Player == PlayerTurn.None)
                    {
                        await DropChip(column, row);
                        Field[index].Player = CurrentPlayer;
                        chipSet = true;
                        CheckForWin(row, col);
                        break;
                    }
                }
                // if a chip has been set, switch players
                if (chipSet)
                {
                    Moves++;
                    SwitchPlayer();
                }
                chipIsFalling = false;
            }
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Toggles the color of each chip in the selected column, to simulate a falling chip
        /// </summary>
        /// <param name="column"></param>
        /// <param name="lastRowIndex"></param>
        /// <returns></returns>
        private async Task DropChip(int column, int lastRowIndex)
        {
            for (int row = 0; row < lastRowIndex; row++)
            {
                int index = (NumberOfColumns * row) + column;
                var chip = Field[index];
                chip.Player = CurrentPlayer;
                await Task.Delay(Connect4ChipViewModel.FLASH_TIME_MILLISECONDS);
                chip.Player = PlayerTurn.None;
            }
        }

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
            CreatePlayers();
            CreateGameField();
        }

        /// <summary>
        /// Creates all the chips inside the field and sets the column, row and index of the chip
        /// </summary>
        private void CreateGameField()
        {
            Field = new ObservableCollection<Connect4ChipViewModel>();
            int row = 0;
            int col = 0;
            for (int i = 0; i < NumberOfRows * NumberOfColumns; i++)
            {
                if (col >= NumberOfColumns)
                {
                    col = 0;
                    row++;
                }
                var index = NumberOfColumns * row + col;
                var chip = new Connect4ChipViewModel(row, col, index);
                chip.ChipClicked = ChipClicked;
                Field.Add(chip);
                col++;
            }
        }

        /// <summary>
        /// Checks all possible directions for a connect 4
        /// </summary>
        /// <param name="lastRow"></param>
        /// <param name="lastCol"></param>
        private void CheckForWin(int lastRow, int lastCol)
        {
            bool win = CheckRow(lastRow, lastCol);
            if (!win)
            {
                win = CheckColumn(lastRow, lastCol);
            }
            if (!win)
            {
                win = CheckDiagonal(lastRow, lastCol);
            }
            if(win)
            {
                Winner = CurrentPlayer;
                Moves /= 2;
            }
            GameOver = win;
            if(GameOver)
            {
                SetGameOverField();
            }
        }

        /// <summary>
        /// Changes the unselected chips to the background color and shrinks the losing chips by increasing the margin
        /// </summary>
        private void SetGameOverField()
        {
            foreach(Connect4ChipViewModel chip in Field)
            {
                if(chip.ChipState == Connect4ChipStates.StartUnselected)
                {
                    chip.ChipState = Connect4ChipStates.GameOverUnselected;
                }
                else if(chip.Player != Winner)
                {
                    chip.Margin = loserChipsMargin;
                }
            }
        }

        /// <summary>
        /// Checks the column of last dropped chip for a connect 4
        /// </summary>
        /// <param name="lastRow">row of last dropped chip</param>
        /// <param name="lastCol">column of last dropped chip</param>
        /// <returns>true if connect 4</returns>
        private bool CheckColumn(int lastRow, int lastCol)
        {
            int count = 0;
            var chipState = Field[(NumberOfColumns * lastRow) + lastCol].ChipState;
            for (int currentRow = 0; currentRow < NumberOfRows; currentRow++)
            {
                CheckForaSingleMatch(chipState, currentRow, lastCol, ref count);
                if (count == 4)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks the row of last dropped chip for a connect 4
        /// </summary>
        /// <param name="lastRow">row of last dropped chip</param>
        /// <param name="lastCol">column of last dropped chip</param>
        /// <returns>true if connect 4</returns>
        private bool CheckRow(int lastRow, int lastCol)
        {
            int count = 0;
            var lastChipState = Field[(NumberOfColumns * lastRow) + lastCol].ChipState;
            for (int currentColumn = 0; currentColumn < NumberOfColumns; currentColumn++)
            {
                CheckForaSingleMatch(lastChipState, lastRow, currentColumn, ref count);
                if (count == 4)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks the falling and rising diagonals for a connect4
        /// </summary>
        /// <param name="lastRow">row of the last dropped chip</param>
        /// <param name="lastCol">column of the last dropped chip</param>
        /// <returns>true if a connect4 found, false otherwise</returns>
        private bool CheckDiagonal(int lastRow, int lastCol)
        {
            bool connect4 = CheckRisingDiagonal(lastRow, lastCol);
            if (!connect4)
            {
                connect4 = CheckFallingDiagonal(lastRow, lastCol);
            }
            return connect4;
        }

        /// <summary>
        /// Checks the falling diagonal
        /// </summary>
        /// <param name="lastRow">row of the last dropped chip</param>
        /// <param name="lastCol">column of the last dropped chip</param>
        /// <returns>true if a connect4 found, false otherwise</returns>
        private bool CheckFallingDiagonal(int lastRow, int lastCol)
        {
            List<Connect4ChipStates> diagonalList = new();
            // adds all the chips on diagonal toward the row = 0
            for (int row = lastRow, column = lastCol; row >= 0 && column >= 0; row--, column--)
            {
                Connect4ChipStates chipState = Field[(NumberOfColumns * row) + column].ChipState;
                diagonalList.Add(chipState);
            }
            diagonalList.Reverse();
            // adds all the chips on the diagonal towards the last row
            for (int row = lastRow + 1, column = lastCol + 1; row < NumberOfRows && column < NumberOfColumns; row++, column++)
            {
                Connect4ChipStates chipState = Field[(NumberOfColumns * row) + column].ChipState;
                diagonalList.Add(chipState);
            }
            Connect4ChipStates lastChipState = Field[(NumberOfColumns * lastRow) + lastCol].ChipState;

            bool connect4 = CheckListForConnect4(lastChipState, diagonalList);
            return connect4;
        }

        /// <summary>
        /// Checks the rising diagonal
        /// </summary>
        /// <param name="lastRow">row of the last dropped chip</param>
        /// <param name="lastCol">column of the last dropped chip</param>
        /// <returns>true if a connect4 found, false otherwise</returns>
        private bool CheckRisingDiagonal(int lastRow, int lastCol)
        {
            List<Connect4ChipStates> diagonalList = new();
            // adds all the chips on diagonal toward the row = 0
            for (int row = lastRow, column = lastCol; row >= 0 && column < NumberOfColumns; row--, column++)
            {
                Connect4ChipStates chipState = Field[(NumberOfColumns * row) + column].ChipState;
                diagonalList.Add(chipState);
            }
            diagonalList.Reverse();
            // adds all the chips on the diagonal towards the last row
            for (int row = lastRow + 1, column = lastCol - 1; row < NumberOfRows && column >= 0; row++, column--)
            {
                Connect4ChipStates chipState = Field[(NumberOfColumns * row) + column].ChipState;
                diagonalList.Add(chipState);
            }
            Connect4ChipStates lastChipState = Field[(NumberOfColumns * lastRow) + lastCol].ChipState;

            bool connect4 = CheckListForConnect4(lastChipState, diagonalList);
            return connect4;
        }

        /// <summary>
        /// Checks the list of added chips for a connect 4
        /// </summary>
        /// <param name="lastChipState"></param>
        /// <param name="diagonalList"></param>
        /// <returns></returns>
        private bool CheckListForConnect4(Connect4ChipStates lastChipState, List<Connect4ChipStates> diagonalList)
        {
            int count = 0;
            foreach (Connect4ChipStates chipState in diagonalList)
            {
                if (chipState == lastChipState)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 4)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Comapres the given rgb with the current row and column
        /// </summary>
        /// <param name="lastChipState">the <see cref="Connect4ChipStates"/> to match</param>
        /// <param name="row">the current row</param>
        /// <param name="column">the current column</param>
        /// <param name="count">the current count</param>
        private void CheckForaSingleMatch(Connect4ChipStates lastChipState, int row, int column, ref int count)
        {
            var chipState = Field[(NumberOfColumns * row) + column].ChipState;
            if (lastChipState == chipState)
            {
                count++;
            }
            else
            {
                count = 0;
            }
        }

        /// <summary>
        /// Gets the current game settings
        /// </summary>
        private void ResetField()
        {
            CreateGameField();
        }

        /// <summary>
        /// Creates the players
        /// </summary>
        private void CreatePlayers()
        {
            Players = new ObservableCollection<Player>()
            {
                new Player(PlayerTurn.Player1),
                new Player(PlayerTurn.Player2),
            };
            CurrentPlayer = PlayerTurn.Player1;
        }

        /// <summary>
        /// Switches the players turn
        /// </summary>
        private void SwitchPlayer()
        {
            if (!GameOver)
            {
                CurrentPlayer = CurrentPlayer == PlayerTurn.Player1 ? PlayerTurn.Player2 : PlayerTurn.Player1;
                foreach (Player player in Players)
                {
                    player.CurrentPlayer = CurrentPlayer == player.Position;
                }
            }
        }

        #endregion
    }
}
