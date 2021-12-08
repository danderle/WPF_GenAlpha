using System.Threading.Tasks;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// A base class for the top bar
    /// </summary>
    public class BaseTopBarViewModel : BaseViewModel
    {
        #region Commands

        /// <summary>
        /// The command to go back to the game selection menu
        /// </summary>
        public ICommand ToGameSelectionCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseTopBarViewModel()
        {
            InitializeCommands();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Switches the page to return to the game selection page
        /// </summary>
        private async void GoToGameSelctionAsync()
        {
            DI.Service<ApplicationViewModel>().GoToPage(ApplicationPage.GameSelection);

            await Task.Delay(1);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the commands
        /// </summary>
        private void InitializeCommands()
        {
            ToGameSelectionCommand = new RelayCommand(GoToGameSelctionAsync);
        }

        #endregion
    }
}
