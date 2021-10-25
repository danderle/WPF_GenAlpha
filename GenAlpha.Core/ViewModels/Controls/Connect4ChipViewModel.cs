using System;
using System.Windows;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the connect4 buttons
    /// </summary>
    public class Connect4ChipViewModel : BaseViewModel
    {
        #region Fields

        private PlayerTurn player = PlayerTurn.None;

        #endregion

        #region Properties

        /// <summary>
        /// Flag for letting us know if its set by a player
        /// </summary>
        public bool PlayerSet { get; set; }

        /// <summary>
        /// Flag to let us know if it has been clicked
        /// </summary>
        public bool IsClicked { get; set; }

        /// <summary>
        /// The index of the button
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// The column in which this button is in
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// The row in which this button is on
        /// </summary>
        public int Row { get; }

        public Thickness Margin { get; set; } = new Thickness(5);

        /// <summary>
        /// The current chip state
        /// </summary>
        public Connect4ChipStates ChipState { get; set; }

        public PlayerTurn Player
        {
            get => player;
            set
            {
                player = value;
                if (player == PlayerTurn.Player1)
                {
                    ChipState = Connect4ChipStates.Player1;
                }
                else if (player == PlayerTurn.Player2)
                {
                    ChipState = Connect4ChipStates.Player2;
                }
            }
        }

        #region Actions

        /// <summary>
        /// Trigger action when chip is clicked
        /// </summary>
        public Action<int> ChipClicked;

        #endregion

        #endregion

        #region Commands

        /// <summary>
        /// The command for clicking
        /// </summary>
        public ICommand ClickCommand { get; set; }

        /// <summary>
        /// The command when animation is finished
        /// </summary>
        public ICommand FinishedAnimatingCommand { get; set; }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Connect4ChipViewModel(int row, int col, int index)
        {
            Row = row;
            Column = col;
            Index = index;
            InitializeCommands();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// The click command method
        /// </summary>
        private void Click()
        {
            ChipClicked(Column);
        }

        /// <summary>
        /// The finished animation command
        /// </summary>
        private void FinishedAnimating()
        {
        }

        #endregion

        #region Private Helpers Methods

        /// <summary>
        /// Initializes all the commands
        /// </summary>
        private void InitializeCommands()
        {
            ClickCommand = new RelayCommand(Click);
            FinishedAnimatingCommand = new RelayCommand(FinishedAnimating);
        }

        #endregion
    }
}
