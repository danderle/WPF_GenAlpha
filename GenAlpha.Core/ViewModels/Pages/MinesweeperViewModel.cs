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
            bool[] bombIndexs = new bool[NumberOfRows * NumberOfColumns];
            for(int i = 0; i < NumberOfBombs; i++)
            {
                bombIndexs[i] = true;
            }
            bombIndexs = bombIndexs.Shuffle();

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
                var index = (NumberOfColumns * row) + col;
                var square = new MinesweeperSquareViewModel(row, col, index, bombIndexs[i]? MinesweeperSquareState.Bomb : MinesweeperSquareState.Unopened, SquareClicked);
                Field.Add(square);
                col++;
            }
        }

        #endregion
    }
}
