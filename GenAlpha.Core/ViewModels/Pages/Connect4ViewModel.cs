using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="Connect4Page"/>
    /// </summary>
    public class Connect4ViewModel : BaseViewModel
    {
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
        public ObservableCollection<Connect4ButtonViewModel> Field { get; set; } = new ObservableCollection<Connect4ButtonViewModel>();

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
            Field = new ObservableCollection<Connect4ButtonViewModel>();
            for(int i = 0; i < NumberOfRows * NumberOfColumns; i++)
            {
                Field.Add(new Connect4ButtonViewModel());
            }
            
            RestartGame();
        }

        /// <summary>
        /// Gets the current game settings
        /// </summary>
        private void GetGameSettings()
        {
        }

        /// <summary>
        /// Sets the next player as current player
        /// </summary>
        private void NextPlayer()
        {
            int index = (int)CurrentPlayer;
            index++;
            if(index >= Players.Count)
            {
                index = 0;
            }
            CurrentPlayer = (PlayerTurn)index;
            foreach(Player player in Players)
            {
                player.CurrentPlayer = player.Position == CurrentPlayer;
            }
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
        /// Sets the winner score
        /// </summary>
        private void SetWinnerScore()
        {
            var list = Players.ToList();
            list.Sort((Player a, Player b) => { return a.Score.CompareTo(b.Score); });
            Winner = list.Last().Position;
            WinnerScore = list.Last().Score;
        }

        private void CreatePlayers()
        {
            Players = new ObservableCollection<Player>()
            {
                new Player(PlayerTurn.Player1),
                new Player(PlayerTurn.Player2),
            };
        }

        #endregion

    }
}
