using System;
using System.Windows;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The view model for the <see cref="MemoryPage"/>
    /// </summary>
    public class MemoryViewModel : BaseViewModel
    {
        #region Private Members


        #endregion

        #region Properties

        /// <summary>
        /// A flag to let us know if we can reveal another card
        /// </summary>
        public bool CanReveal => CardsRevealed < 2;

        /// <summary>
        /// A counter for the number of cards revealed
        /// </summary>
        public int CardsRevealed { get; set; } = 0;

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
            CardRevealedCommand = new RelayCommand(() => CardsRevealed++);
            ResetCardsRevealedCommand = new RelayCommand(() => CardsRevealed = 0);
        }

        #endregion

    }
}
