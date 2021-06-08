using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The design time model for a settings list
    /// </summary>
    public class SettingsListDesignModel : SettingsListViewModel
    {
        #region Singleton

        public static SettingsListDesignModel Instance { get; set; } = new SettingsListDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsListDesignModel()
        {
            SettingItems = new ObservableCollection<SettingsListItemViewModel>
            {
                new SettingsListItemViewModel("Players", SettingTypes.Increment),
                new SettingsListItemViewModel("Number of memory cards", SettingTypes.Increment),
                new SettingsListItemViewModel("Use lower case letters", SettingTypes.Toggle),
                new SettingsListItemViewModel("Use upper case letters", SettingTypes.Toggle),
                new SettingsListItemViewModel("Use special characters", SettingTypes.Toggle),
            };
        }

        #endregion
    }
}
