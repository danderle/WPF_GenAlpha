using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="TopBarControl"/>
    /// </summary>
    public class Connect4TopBarViewModel : BaseTopBarViewModel
    {
        #region Properties

        /// <summary>
        /// A flag to let us know if to show/hide the side menu
        /// </summary>
        public bool ShowSideMenu { get; set; }

        /// <summary>
        /// The list of playing players
        /// </summary>
        public ObservableCollection<Player> Players { get; set; }

        #endregion

        #region Actions

        /// <summary>
        /// Action to toggle the side menu
        /// </summary>
        public Action ToggelSideMenu;

        #endregion

        #region Commands

        /// <summary>
        /// The command to show/hidew the side menu
        /// </summary>
        public ICommand ToggleSideMenuCommand { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Connect4TopBarViewModel(Action toggleSideMenu)
        {
            ToggelSideMenu = toggleSideMenu;
            InitializeCommands();
            CreatePlayers();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// The command method to show / hide the side menu
        /// </summary>
        private void ToggleSideMenu()
        {
            ToggelSideMenu();
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Initializes the commands
        /// </summary>
        private void InitializeCommands()
        {
            ToggleSideMenuCommand = new RelayCommand(ToggleSideMenu);
        }

        /// <summary>
        /// Creates the players
        /// </summary>
        public PlayerTurn CreatePlayers()
        {
            Players = new ObservableCollection<Player>()
            {
                new Player(PlayerTurn.Player1),
                new Player(PlayerTurn.Player2),
            };
            return PlayerTurn.Player1;
        }

        /// <summary>
        /// Sets the current player in players list
        /// </summary>
        /// <param name="currentPlayer"></param>
        public void SetCurrentPlayer(PlayerTurn currentPlayer)
        {
            foreach (Player player in Players)
            {
                player.CurrentPlayer = currentPlayer == player.Position;
            }
        }

        #endregion
    }
}
