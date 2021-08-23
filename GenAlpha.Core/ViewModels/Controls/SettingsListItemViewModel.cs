using System;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for a settings list item
    /// </summary>
    public class SettingsListItemViewModel : BaseViewModel
    {
        #region Private Fields

        private readonly int MIN_PLAYERS = 1;
        private readonly int MAX_PLAYERS = 3;

        private readonly int MIN_WORD_LENGTH = 1;
        private readonly int MAX_WORD_LENGTH = 20;

        private readonly int MIN_WORD_SPAWN_INTERVAL = 1000;
        private readonly int MAX_WORD_SPAWN_INTERVAL = 5000;

        private readonly int MIN_WORD_SPEED = 1;
        private readonly int MAX_WORD_SPEED = 20;

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
            else if (Name.Contains("Word length"))
            {
                CurrentValue--;
                CurrentValue = CurrentValue > MIN_WORD_LENGTH ? CurrentValue : 1;
            }
            else if (Name.Contains("Use spawn"))
            {
                CurrentValue--;
                CurrentValue = CurrentValue <= 0 ? 0 : 1;
            }
            else if (Name.Contains("Word spawn time"))
            {
                CurrentValue -= 100;
                CurrentValue = CurrentValue < MIN_WORD_SPAWN_INTERVAL ? MIN_WORD_SPAWN_INTERVAL : CurrentValue;
            }
            else if (Name.Contains("Word speed"))
            {
                CurrentValue -= 1;
                CurrentValue = CurrentValue < MIN_WORD_SPEED ? MIN_WORD_SPEED : CurrentValue;
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
            else if(Name.Contains("Word length"))
            {
                CurrentValue++;
                CurrentValue = CurrentValue > MAX_WORD_LENGTH ? MAX_WORD_LENGTH : CurrentValue;
            }
            else if (Name.Contains("Use spawn"))
            {
                CurrentValue++;
                CurrentValue = CurrentValue >= 1 ? 1 : 0;
            }
            else if (Name.Contains("Word spawn time"))
            {
                CurrentValue +=100;
                CurrentValue = CurrentValue > MAX_WORD_SPAWN_INTERVAL ? MAX_WORD_SPAWN_INTERVAL : CurrentValue;
            }
            else if (Name.Contains("Word speed"))
            {
                CurrentValue += 1;
                CurrentValue = CurrentValue > MAX_WORD_SPEED ? MAX_WORD_SPEED : CurrentValue;
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
