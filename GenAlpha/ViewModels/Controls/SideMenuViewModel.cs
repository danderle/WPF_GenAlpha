using System;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The view model for the side menu control
    /// </summary>
    public class SideMenuViewModel : BaseViewModel
    {
        #region Properties

        public SettingsListItemViewModel SettingItem1 { get; set; }
        public SettingsListItemViewModel SettingItem2 { get; set; }

        #endregion

        #region Commands

        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuViewModel()
        {
            SettingItem1 = new SettingsListItemViewModel("Players", SettingTypes.Increment, 1);
            SettingItem2 = new SettingsListItemViewModel("Use lowercase letters", SettingTypes.Toggle);
            InitializeCommands();
        }

        #endregion

        #region Command Methods


        #endregion

        #region Public Methods

        #endregion

        #region Private Helpers Methods

        private void InitializeCommands()
        {
        }

        #endregion
    }
}
