using System;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The view model for a settings list item
    /// </summary>
    public class SettingsListItemViewModel : BaseViewModel
    {
        #region Private Fields

        private const int MIN_PLAYERS = 1;
        
        private const int MAX_PLAYERS = 3;
        
        private readonly int[] CARDS = { 2, 4, 12, 16, 20, 30, 36 };

        #endregion

        #region Properties

        /// <summary>
        /// Flag for letting us know if a checkbox is checked
        /// </summary>
        public bool IsChecked { get; set; } = false;

        /// <summary>
        /// The Setting name/description text
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The items current value
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// The type of setting for this item
        /// </summary>
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
        public SettingsListItemViewModel(string name, SettingTypes settingType, int currentValue = 0)
        {
            Name = name;
            SettingType = settingType;
            CurrentValue = currentValue;
            InitializeCommands();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Decreases the items current value
        /// </summary>
        private void Decrease()
        {
            if (Name.Contains("Players"))
            {
                CurrentValue--;
                CurrentValue = CurrentValue < MIN_PLAYERS ? MIN_PLAYERS : CurrentValue;
            }
            else if (Name.Contains("Number of cards"))
            {
                int index = Array.FindIndex(CARDS, x => x == CurrentValue);
                index = --index >= 0 ? index : ++index;
                CurrentValue = CARDS[index];
            }
        }

        /// <summary>
        /// Increases the items current value
        /// </summary>
        private void Increase()
        {
            if (Name.Contains("Players"))
            {
                CurrentValue++;
                CurrentValue = CurrentValue > MAX_PLAYERS ? MAX_PLAYERS : CurrentValue;
            }
            else if (Name.Contains("Number of cards"))
            {
                int index = Array.FindIndex(CARDS, x => x == CurrentValue);
                index = ++index >= CARDS.Length ? --index : index;
                CurrentValue = CARDS[index];
            }

        }

        #endregion

        #region Private Helpers Methods

        private void InitializeCommands()
        {
            IncreaseCommand = new RelayCommand(Increase);
            DecreaseCommand = new RelayCommand(Decrease);
        }

        #endregion
    }
}
