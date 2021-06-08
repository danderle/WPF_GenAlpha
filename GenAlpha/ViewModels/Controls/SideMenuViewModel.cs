namespace GenAlpha
{
    /// <summary>
    /// The view model for the side menu control
    /// </summary>
    public class SideMenuViewModel : BaseViewModel
    {
        #region Properties

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
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Use lowercase letters", SettingTypes.Toggle));
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Use uppercase letters", SettingTypes.Toggle));
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Use numbers", SettingTypes.Toggle));
            SettingsList.SettingItems.Add(new SettingsListItemViewModel("Use special characters", SettingTypes.Toggle));
        }

        #endregion
    }
}
