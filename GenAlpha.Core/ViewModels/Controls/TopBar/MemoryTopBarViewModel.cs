﻿using System;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="TopBarControl"/>
    /// </summary>
    public class MemoryTopBarViewModel : BaseTopBarViewModel
    {
        #region Properties

        /// <summary>
        /// A flag to let us know if to show/hide the side menu
        /// </summary>
        public bool ShowSideMenu { get; set; }

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
        public MemoryTopBarViewModel(Action toggleSideMenu)
        {
            ToggelSideMenu = toggleSideMenu;
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

        #region Private Helpers

        private void InitializeCommands()
        {
            ToggleSideMenuCommand = new RelayCommand(ToggleSideMenu);
        }

        #endregion
    }
}
