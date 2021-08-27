﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="TopBarControl"/>
    /// </summary>
    public class TopBarViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// A flag to let us know if to show/hide the side menu
        /// </summary>
        public bool ShowSideMenu { get; set; }

        /// <summary>
        /// The current score
        /// </summary>
        public int Score { get; set; } = 0;

        #endregion

        #region Actions

        public Action ToggelSettingsMenu;

        #endregion

        #region Commands

        /// <summary>
        /// The command to go back to the game selection menu
        /// </summary>
        public ICommand ToGameSelectionCommand { get; set; }

        /// <summary>
        /// The command to show/hidew the side menu
        /// </summary>
        public ICommand ToggleSideMenuCommand { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TopBarViewModel()
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
            ToggelSettingsMenu();
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

        #region Private Helpers


        private void RestartGame()
        {
            
        }

        private void InitializeCommands()
        {
            ToggleSideMenuCommand = new RelayCommand(ToggleSideMenu);
            ToGameSelectionCommand = new RelayCommand(GoToGameSelctionAsync);
        }

        #endregion
    }
}
