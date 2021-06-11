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

        /// <summary>
        /// The single instance of the design model
        /// </summary>
        public static SettingsListItemDesignModel Instance { get; set; } = new SettingsListItemDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsListItemDesignModel()
        {
            Name = "Players";
            SettingType = SettingTypes.Increment;
            CurrentValue = 2;
        }

        #endregion
    }
}
