using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="MinesweeperPage"/>
    /// </summary>
    public class MinesweeperViewModel : BaseViewModel
    {
        #region Fields

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
        /// Creates new memory cards and restarts game
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
        /// The action method which is triggered when a chip has been clicked
        /// </summary>
        /// <param name="column"></param>
        private void ChipClicked(int column)
        {
            // cannot add another chip while previous is still falling or gameover
            if (!GameOver)
            {
                int col = column;
                bool chipSet = false;
                // Cycles from the bottom row up to check which chip is not set
                for (int row = NumberOfRows - 1; row >= 0; row--)
                {
                    int index = (NumberOfColumns * row) + col;
                    // true if no player has set this chip
                    if (Field[index].Player == PlayerTurn.None)
                    {
                        Field[index].Player = CurrentPlayer;
                        chipSet = true;
                        break;
                    }
                }
                // if a chip has been set, switch players
                if (chipSet)
                {
                    Moves++;
                }
            }
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

        #endregion
    }
}
