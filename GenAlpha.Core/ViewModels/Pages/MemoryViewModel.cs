using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="MemoryPage"/>
    /// </summary>
    public class MemoryViewModel : BaseViewModel
    {
        #region Private Fields

        private readonly string lowercase = "abcdefghijklmnopqrstuvwxyz";

        private readonly string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly string numbers = "1234567890";

        private readonly string specialCharacters = "!&?()#%=-_";

        private string culturLanguage = "en-EN";

        private readonly List<char> randomCardValues = new();

        private readonly List<char> usedChars = new();

        #endregion

        #region Properties

        /// <summary>
        /// Flag to let us know if the game has ended
        /// </summary>
        public bool GameOver { get; set; }
        
        /// <summary>
        /// A flag to let us know if we can reveal another card
        /// </summary>
        public bool CanReveal => CardsRevealed < 2;

        /// <summary>
        /// The winner score
        /// </summary>
        public int WinnerScore { get; set; }

        /// <summary>
        /// The current players turn
        /// </summary>
        public PlayerTurn CurrentPlayer { get; set; } = PlayerTurn.Player1;

        /// <summary>
        /// The winner
        /// </summary>
        public PlayerTurn Winner { get; set; } = PlayerTurn.Player1;

        /// <summary>
        /// A counter for the number of cards revealed
        /// </summary>
        public int CardsRevealed { get; set; }

        /// <summary>
        /// Rows for the Memmory field
        /// </summary>
        public int NumberOfRows { get; set; }

        /// <summary>
        /// Columns for the memory field
        /// </summary>
        public int NumberOfColumns { get; set; } = 0;

        /// <summary>
        /// A counter for the current revealed cards
        /// </summary>
        public static int RevealedCounter { get; private set; }

        /// <summary>
        /// The side menu view model
        /// </summary>
        public SideMenuViewModel SideMenu { get; set; } = new SideMenuViewModel();

        /// <summary>
        /// The list of all the memory cards
        /// </summary>
        public ObservableCollection<MemoryCardButtonViewModel> MemoryCards { get; set; } = new ObservableCollection<MemoryCardButtonViewModel>();

        /// <summary>
        /// The list of playing players
        /// </summary>
        public ObservableCollection<Player> Players { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command for counting the revealed cards
        /// </summary>
        public ICommand CardRevealedCommand { get; set; }

        /// <summary>
        /// The command to restart the game
        /// </summary>
        public ICommand RestartGameCommand { get; set; }

        /// <summary>
        /// The command to show/hidew the side menu
        /// </summary>
        public ICommand ToggleSideMenuCommand { get; set; }
        
        /// <summary>
        /// The command to go back to the game selection menu
        /// </summary>
        public ICommand ToGameSelectionCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MemoryViewModel()
        {
            InitializeCommands();
            InitializeProperties();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Creates new memory cards and restarts game
        /// </summary>
        private void RestartGame()
        {
            GameOver = false;
            usedChars.Clear();
            randomCardValues.Clear();
            MemoryCards.Clear();
            ResetPlayers();
            GetGameSettings();
            CreateMemoryCards();
            ShuffleMemoryCards();
        }

        /// <summary>
        /// The command method to show / hide the side menu
        /// </summary>
        private void ToggleSideMenu()
        {
            SideMenu.ShowSideMenu = !SideMenu.ShowSideMenu;
            if (!SideMenu.ShowSideMenu)
            {
                RestartGame();
            }
        }

        /// <summary>
        /// Switches the page to return to the game selection page
        /// </summary>
        private async void GoToGameSelctionAsync()
        {
            DI.Service<ApplicationViewModel>().GoToPage(ApplicationPage.GameSelection);

            await Task.Delay(1);
        }

        #endregion

        #region Card Actions

        /// <summary>
        /// Resets the revealed counter and sets all card reveald flags to false
        /// </summary>
        private void ResetRevealed()
        {
            var matchList = new List<bool>();
            foreach (var card in MemoryCards)
            {
                card.IsRevealed = false;
                if(card.IsMatched)
                {
                    matchList.Add(true);
                }
            }
            RevealedCounter = 0;
            if(matchList.Count == MemoryCards.Count)
            {
                SetWinnerScore();
                GameOver = true;
            }
        }

        /// <summary>
        /// Increases the revealed counter
        /// </summary>
        private void CardRevealed()
        {
            RevealedCounter++;
        }

        /// <summary>
        /// Checks for matching cards if 2 cards are currently revealed
        /// </summary>
        private void CheckForMatch()
        {
            if (RevealedCounter == 2)
            {
                var list = new List<MemoryCardButtonViewModel>();
                foreach (var card in MemoryCards)
                {
                    if (card.IsRevealed && !card.IsAnimating)
                    {
                        //if card revealed flag is set and not currently animating add to revealed list
                        list.Add(card);
                    }
                }

                //if 2 cards are revealed check if match or not
                if (list.Count == 2)
                {
                    var card1 = list[0];
                    var card2 = list[1];

                    if (card1.Content.Equals(card2.Content))
                    {
                        IncreaseScore();
                        //Match animation
                        card1.Match();
                        card2.Match();
                    }
                    else
                    {
                        NextPlayer();
                        //no match animation
                        card1.NoMatch();
                        card2.NoMatch();
                    }
                }
            }
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Initialize all the commands
        /// </summary>
        private void InitializeCommands()
        {
            CardRevealedCommand = new RelayCommand(() => CardsRevealed++);
            RestartGameCommand = new RelayCommand(RestartGame);
            ToggleSideMenuCommand = new RelayCommand(ToggleSideMenu);
            ToGameSelectionCommand = new RelayCommand(GoToGameSelctionAsync);
        }

        /// <summary>
        /// Initalize all the properties
        /// </summary>
        private void InitializeProperties()
        {
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Players", SettingTypes.Increment, 1));
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Number of cards", SettingTypes.Increment, 16));
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Use lowercase letters", SettingTypes.Toggle) { IsChecked = true });
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Use uppercase letters", SettingTypes.Toggle));
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Use numbers", SettingTypes.Toggle));
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Use special characters", SettingTypes.Toggle));
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Language", SettingTypes.LanguageToggle) { IsChecked = false });

            SideMenu.ShowSideMenu = false;
            RestartGame();
        }

        /// <summary>
        /// Increases the current players score
        /// </summary>
        private void IncreaseScore()
        {
            Players[(int)CurrentPlayer].Score++; ;
        }

        /// <summary>
        /// Gets the current game settings
        /// </summary>
        private void GetGameSettings()
        {
            foreach(var item in SideMenu.SettingsList.SettingItems)
            {
                if (item.Name.Contains("Players"))
                {
                    Players = new ObservableCollection<Player>()
                    {
                        new Player(PlayerTurn.Player1)
                    };
                    CurrentPlayer = PlayerTurn.Player1;
                    if(item.CurrentValue >= 2)
                    {
                        Players.Add(new Player(PlayerTurn.Player2));
                        if(item.CurrentValue == 3)
                        {
                            Players.Add(new Player(PlayerTurn.Player3));
                        }
                    }
                    
                }
                else if (item.Name.Contains("cards"))
                {
                    var numberOfCards = item.CurrentValue;
                    NumberOfRows = 1;
                    NumberOfColumns = 1;
                    while (NumberOfRows * NumberOfColumns != numberOfCards)
                    {
                        if (NumberOfRows == NumberOfColumns)
                        {
                            NumberOfRows++;
                        }
                        else
                        {
                            NumberOfColumns++;
                        }
                    }
                }
                else if (item.Name.Contains("Language"))
                {
                    culturLanguage = item.IsChecked ? "en-EN" : "de-DE";
                }
                else if (item.IsChecked)
                {
                    if (item.Name.Contains("lowercase"))
                    {
                        randomCardValues.AddRange(lowercase);
                    }
                    else if (item.Name.Contains("uppercase"))
                    {
                        randomCardValues.AddRange(uppercase);
                    }
                    else if (item.Name.Contains("numbers"))
                    {
                        randomCardValues.AddRange(numbers);
                    }
                    else if (item.Name.Contains("special"))
                    {
                        randomCardValues.AddRange(specialCharacters);
                    }
                }
            }
        }

        /// <summary>
        /// Fills the memory cards list
        /// </summary>
        private void CreateMemoryCards()
        {
            int count = 0;
            RevealedCounter = 0;

            //Creates cards for every row and column
            while (MemoryCards.Count < (NumberOfRows * NumberOfColumns))
            {
                // gets random char
                var card = new MemoryCardButtonViewModel(GetRandomChar().ToString(), culturLanguage);

                //Hooks into the action calls
                card.CardRevealed = CardRevealed;
                card.ResetRevealed = ResetRevealed;
                card.CheckForMatch = CheckForMatch;
                count++;

                //Creates a matching card
                var match = new MemoryCardButtonViewModel(card);
                match.CardRevealed = CardRevealed;
                match.ResetRevealed = ResetRevealed;
                match.CheckForMatch = CheckForMatch;
                MemoryCards.Add(card);
                MemoryCards.Add(match);
            }
        }

        /// <summary>
        /// Generates random chars and adds them to a used list so none will repeat
        /// </summary>
        /// <returns></returns>
        private char GetRandomChar()
        {
            char randomChar;
            var rand = new Random();
            do
            {
                var num = rand.Next(0, randomCardValues.Count);
                randomChar = randomCardValues[num];
            }
            while (usedChars.Contains(randomChar));

            usedChars.Add(randomChar);
            return randomChar;
        }


        /// <summary>
        /// Shuffles the memory cards
        /// </summary>
        private void ShuffleMemoryCards()
        {
            MemoryCards = new ObservableCollection<MemoryCardButtonViewModel>(MemoryCards.ToList().Shuffle());
        }

        /// <summary>
        /// Sets the next player as current player
        /// </summary>
        private void NextPlayer()
        {
            int index = (int)CurrentPlayer;
            index++;
            if(index >= Players.Count)
            {
                index = 0;
            }
            CurrentPlayer = (PlayerTurn)index;
            foreach(Player player in Players)
            {
                player.CurrentPlayer = player.Position == CurrentPlayer;
            }
        }

        /// <summary>
        /// Resets player scores and current player
        /// </summary>
        private void ResetPlayers()
        {
            CurrentPlayer = PlayerTurn.Player1;
            GetGameSettings();
        }

        /// <summary>
        /// Sets the winner score
        /// </summary>
        private void SetWinnerScore()
        {
            var list = Players.ToList();
            list.Sort((Player a, Player b) => { return a.Score.CompareTo(b.Score); });
            Winner = list.Last().Position;
            WinnerScore = list.Last().Score;
        }
        #endregion

    }
}
