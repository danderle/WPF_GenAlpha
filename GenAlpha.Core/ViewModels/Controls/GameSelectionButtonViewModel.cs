using System.Windows.Input;

namespace GenAlpha.Core
{

    /// <summary>
    /// The view model for the game selection buttons
    /// </summary>
    public class GameSelectionButtonViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// The content for this button
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// The game page this button represents
        /// </summary>
        public ApplicationPage GamePage { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command for clicking
        /// </summary>
        public ICommand ClickCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameSelectionButtonViewModel()
        {
            InitializeCommands();
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public GameSelectionButtonViewModel(string content, ApplicationPage gamePage)
        {
            InitializeCommands();
            Content = content;
            GamePage = gamePage;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// The click command method
        /// </summary>
        private void Click()
        {
            IoC.Get<ApplicationViewModel>().GoToPage(GamePage);
        }

        #endregion

        #region Private Helpers Methods

        private void InitializeCommands()
        {
            ClickCommand = new RelayCommand(Click);
        }

        #endregion
    }
}
