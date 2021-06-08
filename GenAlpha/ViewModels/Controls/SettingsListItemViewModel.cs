using System;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The view model for a settings list item
    /// </summary>
    public class SettingsListItemViewModel : BaseViewModel
    {
        #region Properties

        public bool IsChecked { get; set; } = true;

        public string SettingName { get; set; }

        public int IncrementValue { get; set; }

        public SettingTypes SettingType { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command for increasing the increment value
        /// </summary>
        public ICommand IncreaseCommand { get; set; }

        /// <summary>
        /// The command to decrease the increment value
        /// </summary>
        public ICommand DecreaseCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsListItemViewModel()
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public SettingsListItemViewModel(string settingName, SettingTypes settingType, int incrementValue = 0)
        {
            SettingName = settingName;
            SettingType = settingType;
            IncrementValue = incrementValue;
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
            IncreaseCommand = new RelayCommand(() => IncrementValue++);
            DecreaseCommand = new RelayCommand(() => IncrementValue--);
        }

        #endregion
    }
}
