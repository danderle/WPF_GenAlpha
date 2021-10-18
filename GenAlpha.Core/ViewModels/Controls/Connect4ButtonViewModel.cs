using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the connect4 buttons
    /// </summary>
    public class Connect4ButtonViewModel : BaseViewModel
    {
        #region Fields

        private byte[] rgbPlayer1 = { 0xFF, 0x33, 0x33 }; // Red player 1
        private byte[] rgbPlayer2 = { 0xFF, 0xFF, 0x33 }; // Yellow Player 2

        #endregion

        #region Properties

        /// <summary>
        /// Flag for letting us know if its still available
        /// </summary>
        public bool Available { get; set; } = true;

        /// <summary>
        /// Flag for letting us know if this cards is currently animating
        /// </summary>
        public bool IsAnimating { get; set; } = false;

        /// <summary>
        /// The current animation to run on value changed
        /// </summary>
        public ButtonAnimationTypes Animation { get; set; } = ButtonAnimationTypes.None;

        /// <summary>
        /// The rgb in bytes
        /// </summary>
        public byte[] RgbHex { get; set; }

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
        public Connect4ButtonViewModel()
        {
            InitializeCommands();
            InitializeProperties();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// The click command method
        /// </summary>
        private void Click()
        {
            RgbHex = Connect4ViewModel.CurrentPlayer == PlayerTurn.Player1 ? rgbPlayer1 : rgbPlayer2;
            Available = false;
        }

        /// <summary>
        /// The finished animation command
        /// </summary>
        private void FinishedAnimating()
        {
        }

        #endregion

        #region Public Methods


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

        /// <summary>
        /// Initializes all the properties
        /// </summary>
        private void InitializeProperties()
        {
        }
            
        #endregion
    }
}
