using System;
using System.Timers;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="TopBarControl"/>
    /// </summary>
    public class MinesweeperTopBarViewModel : BaseTopBarViewModel
    {
        #region Fields

        private const int TIMER_INTERVAL = 1000;

        private Timer stopwatchTimer = new Timer(TIMER_INTERVAL);

        #endregion

        #region Properties

        /// <summary>
        /// A flag to let us know if to show/hide the side menu
        /// </summary>
        public bool ShowSideMenu { get; set; }

        /// <summary>
        /// Shows the number of potential bombs
        /// </summary>
        public int RemainingBombs { get; set; }

        /// <summary>
        /// Displays the current GameState
        /// </summary>
        public string GameState { get; set; } = "Ready";

        /// <summary>
        /// Elapsed time in seconds
        /// </summary>
        public int ElapsedTime { get; set; } = 0;

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
        public MinesweeperTopBarViewModel(Action toggleSideMenu)
        {
            ToggelSideMenu = toggleSideMenu;
            stopwatchTimer.Elapsed += StopwatchTimer_Elapsed;
            InitializeCommands();
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

        #region Event Methods

        /// <summary>
        /// The elapsed stopwatch timer method to add a second to the <see cref="ElapsedTime"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopwatchTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ElapsedTime += 1;
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Sets the game state and starts the timer
        /// </summary>
        public void StartGame()
        {
            GameState = "Go!";
            StartTimer();
        }

        /// <summary>
        /// Resets the top bar values
        /// </summary>
        /// <param name="numberOfBombs"></param>
        public void ResetGameScore(int numberOfBombs)
        {
            GameState = "Ready";
            ElapsedTime = 0;
            RemainingBombs = numberOfBombs;
        }

        /// <summary>
        /// Sets the winner game state and stops the timer
        /// </summary>
        public void StopGameWinner()
        {
            GameState = "Winner";
            StopTimer();
        }

        /// <summary>
        /// Sets the game over game state and stops the timer
        /// </summary>
        public void StopGameOver()
        {
            GameState = "Game Over";
            StopTimer();
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Initializes the commmands
        /// </summary>
        private void InitializeCommands()
        {
            ToggleSideMenuCommand = new RelayCommand(ToggleSideMenu);
        }

        /// <summary>
        /// Stops the stopwatchTimer
        /// </summary>
        private void StopTimer()
        {
            stopwatchTimer.Stop();
        }

        /// <summary>
        /// Starts stopwatch timer
        /// </summary>
        private void StartTimer()
        {
            stopwatchTimer.Start();
        }

        #endregion
    }
}
