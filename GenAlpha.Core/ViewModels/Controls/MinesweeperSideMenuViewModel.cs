using System;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the side menu control
    /// </summary>
    public class MinesweeperSideMenuViewModel : BaseViewModel
    {
        #region Constants

        private const int BEGINNER_COLUMNS = 9;
        private const int BEGINNER_ROWS = 9;
        private const int BEGINNER_BOMBS = 10;
        private const int ADVANCED_COLUMNS = 16;
        private const int ADVANCED_ROWS = 16;
        private const int ADVANCED_BOMBS = 40;
        private const int EXPERT_COLUMNS = 30;
        private const int EXPERT_ROWS = 16;
        private const int EXPERT_BOMBS = 99;
        
        #endregion

        #region Properties

        /// <summary>
        /// Flag to let us know if the side menu is showing
        /// </summary>
        public bool ShowSideMenu { get; set; }

        /// <summary>
        /// User defined number of rows
        /// </summary>
        public int CustomRows { private get; set; } = 40;

        /// <summary>
        /// User defined number of columns
        /// </summary>
        public int CustomColumns { private get; set; } = 40;

        /// <summary>
        /// User defined number of bombs
        /// </summary>
        public int CustomBombs { private get; set; } = 150;

        #endregion

        #region Action

        private readonly Action<int, int, int> setGame;

        #endregion

        #region Commands

        /// <summary>
        /// The command to set the beginner
        /// </summary>
        public ICommand BeginnerCommand { get; set; }

        /// <summary>
        /// The command to set advanced
        /// </summary>
        public ICommand AdvancedCommand { get; set; }

        /// <summary>
        /// The command to set expert
        /// </summary>
        public ICommand ExpertCommand { get; set; }

        /// <summary>
        /// The command to set the user defined settings
        /// </summary>
        public ICommand SaveCustomCommand { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MinesweeperSideMenuViewModel(Action<int, int, int> settGameSetting)
        {
            InitializeCommands();
            setGame = settGameSetting;
            Beginner();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Sets the beginner settings
        /// </summary>
        private void Beginner()
        {
            setGame(BEGINNER_ROWS, BEGINNER_COLUMNS, BEGINNER_BOMBS);
            ShowSideMenu = false;
        }

        /// <summary>
        /// Sets the advanced settings
        /// </summary>
        private void Advanced()
        {
            setGame(ADVANCED_ROWS, ADVANCED_COLUMNS, ADVANCED_BOMBS);
            ShowSideMenu = false;
        }

        /// <summary>
        /// Sets the expert settings
        /// </summary>
        private void Expert()
        {
            setGame(EXPERT_ROWS, EXPERT_COLUMNS, EXPERT_BOMBS);
            ShowSideMenu = false;
        }

        /// <summary>
        /// Sets the user defined settings
        /// </summary>
        private void SaveCustom()
        {
            setGame(CustomRows, CustomColumns, CustomBombs);
            ShowSideMenu = false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the commands
        /// </summary>
        private void InitializeCommands()
        {
            BeginnerCommand = new RelayCommand(Beginner);
            AdvancedCommand = new RelayCommand(Advanced);
            ExpertCommand = new RelayCommand(Expert);
            SaveCustomCommand = new RelayCommand(SaveCustom);
        }

        #endregion
    }
}
