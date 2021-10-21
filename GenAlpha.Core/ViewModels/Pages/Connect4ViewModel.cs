using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="Connect4Page"/>
    /// </summary>
    public class Connect4ViewModel : BaseViewModel
    {
        #region Fields

        private readonly byte[] rgbPlayer1 = { 0xFF, 0x33, 0x33 }; // Red player 1

        private readonly byte[] rgbPlayer2 = { 0xFF, 0xFF, 0x33 }; // Yellow Player 2

        #endregion

        #region Properties

        /// <summary>
        /// Flag to let us know if the game has ended
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// The winner score
        /// </summary>
        public int WinnerScore { get; set; }

        /// <summary>
        /// The current players turn
        /// </summary>
        public static PlayerTurn CurrentPlayer { get; private set; } = PlayerTurn.Player1;

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
            ResetPlayers();
            GetGameSettings();
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
            int col = column;
            bool chipSet = false;

            // Cycles from the bottom row up to check which chip is not set
            for(int i = NumberOfRows-1; i >= 0; i--)
            {
                int index = NumberOfColumns * i + col;

                // true if no player has set this chip
                if (!Field[index].PlayerSet)
                {
                    Field[index].PlayerSet = true;
                    Field[index].RgbHex = CurrentPlayer == PlayerTurn.Player1? rgbPlayer1 : rgbPlayer2;
                    chipSet = true;
                    break;
                }
            }
            // if a chip has been set, switch players
            if(chipSet)
            {
                SwitchPlayer();
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
            CreatePlayers();
            CreateGameField();
            
            RestartGame();
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
        /// Gets the current game settings
        /// </summary>
        private void GetGameSettings()
        {
        }

        /// <summary>
        /// Resets player scores and current player
        /// </summary>
        private void ResetPlayers()
        {
            CurrentPlayer = PlayerTurn.Player1;
            GetGameSettings();
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
        }

        /// <summary>
        /// Switches the players turn
        /// </summary>
        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == PlayerTurn.Player1 ? PlayerTurn.Player2 : PlayerTurn.Player1;
        }

        #endregion

    }
}
