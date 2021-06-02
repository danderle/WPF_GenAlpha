using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The view model for the <see cref="MemoryPage"/>
    /// </summary>
    public class MemoryViewModel : BaseViewModel
    {
        #region Private Fields

        private string chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private List<char> usedChars = new List<char>();

        #endregion

        #region Properties

        public bool GameOver { get; set; } = false;

        /// <summary>
        /// A flag to let us know if we can reveal another card
        /// </summary>
        public bool CanReveal => CardsRevealed < 2;

        /// <summary>
        /// A counter for the number of cards revealed
        /// </summary>
        public int CardsRevealed { get; set; } = 0;

        /// <summary>
        /// Rows for the Memmory field
        /// </summary>
        public int NumberOfRows { get; set; } = 4;

        /// <summary>
        /// Columns for the memory field
        /// </summary>
        public int NumberOfColumns { get; set; } = 4;

        /// <summary>
        /// A counter for the current revealed cards
        /// </summary>
        public static int RevealedCounter { get; private set; } = 0;

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
            MemoryCards.Clear();
            CreateMemoryCards();
            ShuffleMemoryCards();
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
                        //Match animation
                        card1.Match();
                        card2.Match();
                    }
                    else
                    {
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
        }

        /// <summary>
        /// Initalize all the properties
        /// </summary>
        private void InitializeProperties()
        {
            CreateMemoryCards();
            ShuffleMemoryCards();
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
                var num = rand.Next(0, chars.Length);
                randomChar = chars[num];
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

        #endregion

    }
}
