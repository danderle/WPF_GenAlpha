using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="MemoryPage"/>
    /// </summary>
    public class MemoryViewModel : BaseViewModel
    {
        #region Private Fields

        private string lowercase = "abcdefghijklmnopqrstuvwxyz";

        private string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private string numbers = "1234567890";

        private string specialCharacters = "!&?()#%=-_";

        private List<char> randomCardValues = new List<char>();

        private List<char> usedChars = new List<char>();

        #endregion

        #region Properties

        /// <summary>
        /// Flag to let us know if the game has ended
        /// </summary>
        public bool GameOver { get; set; } = false;

        /// <summary>
        /// Flag to let us know if player 2 is playing
        /// </summary>
        public bool Player2 { get; set; } = false;

        /// <summary>
        /// Flag to let us know if player 3 is playing
        /// </summary>
        public bool Player3 { get; set; } = false;

        /// <summary>
        /// A flag to let us know if we can reveal another card
        /// </summary>
        public bool CanReveal => CardsRevealed < 2;

        /// <summary>
        /// The score of player 1
        /// </summary>
        public int ScorePlayer1 { get; set; } = 0;

        /// <summary>
        /// The score of player 2
        /// </summary>
        public int ScorePlayer2 { get; set; } = 0;

        /// <summary>
        /// The score of player 3
        /// </summary>
        public int ScorePlayer3 { get; set; } = 0;

        /// <summary>
        /// The winner score
        /// </summary>
        public int WinnerScore { get; set; } = 0;

        /// <summary>
        /// The current players turn
        /// </summary>
        public PlayerTurn CurrentPlayer { get; set; } = PlayerTurn.Player1;

        /// <summary>
        /// A counter for the number of cards revealed
        /// </summary>
        public int CardsRevealed { get; set; } = 0;

        /// <summary>
        /// Rows for the Memmory field
        /// </summary>
        public int NumberOfRows { get; set; } = 0;

        /// <summary>
        /// Columns for the memory field
        /// </summary>
        public int NumberOfColumns { get; set; } = 0;

        /// <summary>
        /// A counter for the current revealed cards
        /// </summary>
        public static int RevealedCounter { get; private set; } = 0;

        /// <summary>
        /// The side menu view model
        /// </summary>
        public SideMenuViewModel SideMenu { get; set; } = new SideMenuViewModel();

        /// <summary>
        /// The list of all the memory cards
        /// </summary>
        public ObservableCollection<MemoryCardButtonViewModel> MemoryCards { get; set; } = new ObservableCollection<MemoryCardButtonViewModel>();

        #endregion

        #region Commands

        /// <summary>
        /// The command for counting the revealed cards
        /// </summary>
        public ICommand CardRevealedCommand { get; set; }

        /// <summary>
        /// The command to reset the cards revealed
        /// </summary>
        public ICommand ResetCardsRevealedCommand { get; set; }

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
            ResetCardsRevealedCommand = new RelayCommand(() => CardsRevealed = 0);
            RestartGameCommand = new RelayCommand(RestartGame);
            ToggleSideMenuCommand = new RelayCommand(ToggleSideMenu);
            ToGameSelectionCommand = new RelayCommand(() => IoC.Get<ApplicationViewModel>().CurrentPage = ApplicationPage.GameSelection);
        }

        /// <summary>
        /// Initalize all the properties
        /// </summary>
        private void InitializeProperties()
        {
            RestartGame();
        }

        /// <summary>
        /// Increases the current players score
        /// </summary>
        private void IncreaseScore()
        {
            switch (CurrentPlayer)
            {
                case PlayerTurn.Player1:
                    ScorePlayer1++;
                    break;
                case PlayerTurn.Player2:
                    ScorePlayer2++;
                    break;
                case PlayerTurn.Player3:
                    ScorePlayer3++;
                    break;
            }
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
                    switch(item.CurrentValue)
                    {
                        case 1:
                            Player2 = false;
                            break;
                        case 2:
                            Player2 = true;
                            Player3 = false;
                            break;
                        case 3:
                            Player2 = true;
                            Player3 = true;
                            break;
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
            //Creates cards for every row and column
            while (MemoryCards.Count < (NumberOfRows * NumberOfColumns))
            {
                var card = new MemoryCardButtonViewModel();
                //Gets a random char
                card.Content = GetRandomChar().ToString();

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
            Random random = new Random();

            for (int i = MemoryCards.Count - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);
                var value = MemoryCards[rnd];

                MemoryCards[rnd] = MemoryCards[i];
                MemoryCards[i] = value;
            }
        }

        /// <summary>
        /// Sets the next player as current player
        /// </summary>
        private void NextPlayer()
        {
            switch(CurrentPlayer)
            {
                case PlayerTurn.Player1:
                    if(Player2)
                        CurrentPlayer = PlayerTurn.Player2;
                    break;
                case PlayerTurn.Player2:
                    CurrentPlayer = PlayerTurn.Player3;
                    break;
                case PlayerTurn.Player3:
                    CurrentPlayer = PlayerTurn.Player1;
                    break;
            }
        }

        /// <summary>
        /// Resets player scores and current player
        /// </summary>
        private void ResetPlayers()
        {
            CurrentPlayer = PlayerTurn.Player1;
            ScorePlayer1 = 0;
            ScorePlayer2 = 0;
            ScorePlayer3 = 0;
        }

        /// <summary>
        /// Sets the winner score
        /// </summary>
        private void SetWinnerScore()
        {
            switch (CurrentPlayer)
            {
                case PlayerTurn.Player1:
                    WinnerScore = ScorePlayer1;
                    break;
                case PlayerTurn.Player2:
                    WinnerScore = ScorePlayer2;
                    break;
                case PlayerTurn.Player3:
                    WinnerScore = ScorePlayer3;
                    break;
            }
        }
        #endregion

    }
}
