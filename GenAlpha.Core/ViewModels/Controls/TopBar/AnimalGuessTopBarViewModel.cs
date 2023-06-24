using System;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="TopBarControl"/>
    /// </summary>
    public class AnimalGuessTopBarViewModel : BaseTopBarViewModel
    {
        #region Fields


        #endregion

        #region Properties

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
        public AnimalGuessTopBarViewModel()
        {
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

        #region Public Methods
        
        

        #endregion

        #region Private Helpers

        /// <summary>
        /// Initializes the commmands
        /// </summary>
        private void InitializeCommands()
        {
            ToggleSideMenuCommand = new RelayCommand(ToggleSideMenu);
        }

        #endregion
    }
}
