using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The design time model for a settings items list
    /// </summary>
    public class SettingsListItemDesignModel : SettingsListItemViewModel
    {
        #region Singleton

        public static SettingsListItemDesignModel Instance { get; set; } = new SettingsListItemDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsListItemDesignModel()
        {
            SettingName = "Players";
            SettingType = SettingTypes.Increment;
            IncrementValue = 2;
        }

        #endregion
    }
}
