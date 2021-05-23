using System.Windows;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The view model for the <see cref="GameSelectionPage"/>
    /// </summary>
    public class GameSelectionViewModel : BaseViewModel
    {
        #region Private Members

        

        #endregion

        #region Properties

        

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to open the system menu the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameSelectionViewModel()
        {
            
        }

        #endregion

       
        #region Private Helpers

        #endregion
    }
}
