using System.Collections.Generic;
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
        /// The top bar for this view model
        /// </summary>
        public MinesweeperTopBarViewModel TopBar { get; set; }

        /// <summary>
        /// The side menu view model
        /// </summary>
        public MinesweeperSideMenuViewModel SideMenu { get; set; }

        /// <summary>
        /// The field view model
        /// </summary>
        public MinesweeperFieldViewModel Field { get; set; }

        /// <summary>
        /// A dictionary holding the image paths according to the square states
        /// </summary>
        public static Dictionary<MinesweeperValues, object> MinesweeperImagePaths => Image.GetMinesweeperImagePaths();

        #endregion

        #region Commands

        /// <summary>
        /// The command to restart the game
        /// </summary>
        public ICommand RestartGameCommand { get; set; }

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
            Field.Reset();
            MinesweeperSquareViewModel.FirstSquareClicked = true;
            TopBar.ResetGameScore(Field.NumberOfBombs);
        }

        /// <summary>
        /// The command method to show / hide the side menu
        /// </summary>
        private void ToggleSideMenu()
        {
            SideMenu.ShowSideMenu = !SideMenu.ShowSideMenu;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// The action method to let us know we have revealed a bomb
        /// </summary>
        private void BombRevealed()
        {
            GameOver = true;
            Field.ShowAllBombs();
            TopBar.StopGameOver();
        }

        /// <summary>
        /// The action method to let us know we have marked a potential bomb
        /// </summary>
        private void BombMarked(bool flagSet)
        {
            TopBar.RemainingBombs += flagSet ? -1 : 1;
        }

        /// <summary>
        /// Starts the game timer after first click
        /// </summary>
        private void StartGameTimer()
        {
            if (!GameOver)
            {
                TopBar.StartGame();
            }
        }

        /// <summary>
        /// Stops the game when won
        /// </summary>
        private void GameIsWon()
        {
            GameOver = true;
            TopBar.StopGameWinner();
        }

        /// <summary>
        /// Set up the field size and number of bombs
        /// </summary>
        private void SetGameSettings(int rows, int columns, int bombs)
        {
            Field = new MinesweeperFieldViewModel(rows, columns, bombs, BombRevealed, BombMarked, StartGameTimer, GameIsWon);
            TopBar.ResetGameScore(Field.NumberOfBombs);
            GameOver = false;
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Initialize all the commands
        /// </summary>
        private void InitializeCommands()
        {
            RestartGameCommand = new RelayCommand(RestartGame);
        }

        /// <summary>
        /// Initalize all the properties
        /// </summary>
        private void InitializeProperties()
        {
            TopBar = new MinesweeperTopBarViewModel(ToggleSideMenu);
            SideMenu = new MinesweeperSideMenuViewModel(SetGameSettings);
        }

        #endregion
    }
}
