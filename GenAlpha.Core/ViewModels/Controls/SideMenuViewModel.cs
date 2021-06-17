namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the side menu control
    /// </summary>
    public class SideMenuViewModel : BaseViewModel
    {
        #region Private Fields

        private bool _showSideMenu = false;

        #endregion

        #region Properties

        /// <summary>
        /// A flag to let us know if to show/hide the side menu
        /// </summary>
        public bool ShowSideMenu
        {
            get => _showSideMenu;
            set
            {
                _showSideMenu = value;
                //if closing
                if(!_showSideMenu)
                {
                    CheckForOneCheckBoxChecked();
                }
            }
        }

        /// <summary>
        /// The setting list view model
        /// </summary>
        public SettingsListViewModel SettingsList { get; set; } = new SettingsListViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuViewModel()
        {
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Players", SettingTypes.Increment, 1));
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Number of cards", SettingTypes.Increment, 16));
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Use lowercase letters", SettingTypes.Toggle) { IsChecked = true });
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Use uppercase letters", SettingTypes.Toggle));
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Use numbers", SettingTypes.Toggle));
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Use special characters", SettingTypes.Toggle));
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// A check to verify that at least one toggle switch is active, if not than use lowercase as default
        /// </summary>
        private void CheckForOneCheckBoxChecked()
        {
            bool oneChecked = false;
            //check if one setting is checked
            foreach (var item in SettingsList.SettingItems)
            {
                if (item.IsChecked)
                {
                    oneChecked = true;
                    break;
                }
            }
            //if none are checked, check the lowercase
            if (!oneChecked)
            {
                foreach (var item in SettingsList.SettingItems)
                {
                    if (item.Name.Contains("lowercase"))
                    {
                        item.IsChecked = true;
                        break;
                    }
                }
            }
        }

        #endregion
    }
}
