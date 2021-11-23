using System.Collections.Generic;
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
        /// Shows the number of potential bombs
        /// </summary>
        public int RemainingBombs { get; set; }

        /// <summary>
        /// The winner
        /// </summary>
        public PlayerTurn Winner { get; set; } = PlayerTurn.Player1;

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
            Field.Reset();
        }

        /// <summary>
        /// The command method to show / hide the side menu
        /// </summary>
        private void ToggleSideMenu()
        {
            SideMenu.ShowSideMenu = !SideMenu.ShowSideMenu;
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
        /// The action method to let us know we have revealed a bomb
        /// </summary>
        private void BombRevealed()
        {
            GameOver = true;
        }

        /// <summary>
        /// The action method to let us know we have marked a potential bomb
        /// </summary>
        private void BombMarked(bool flagSet)
        {
            RemainingBombs += flagSet ? -1 : 1;
        }

        /// <summary>
        /// Set up the field size and number of bombs
        /// </summary>
        private void SetGameSettings(int rows, int columns, int bombs)
        {
            Field = new MinesweeperFieldViewModel(rows, columns, bombs, BombRevealed, BombMarked);
            RemainingBombs = Field.NumberOfBombs;
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
            SideMenu = new MinesweeperSideMenuViewModel(SetGameSettings);
        }

        #endregion
    }
}
