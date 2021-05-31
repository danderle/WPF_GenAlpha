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
        #region Properties

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

        #region Card Actions

        /// <summary>
        /// Resets the revealed counter and sets all card reveald flags to false
        /// </summary>
        private void ResetRevealed()
        {
            foreach (var card in MemoryCards)
            {
                card.IsRevealed = false;
            }
            RevealedCounter = 0;
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
                //Right now gets a letter in increasing order
                card.Content = Convert.ToString(Convert.ToChar('a' + count));
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
