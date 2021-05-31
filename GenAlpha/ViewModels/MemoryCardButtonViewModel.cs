using System;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The view model for the memory card buttons
    /// </summary>
    public class MemoryCardButtonViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Flag for letting us know if this card has been matched
        /// </summary>
        public bool IsMatched { get; set; } = false;

        /// <summary>
        /// Flag for letting us know if this card is revealed
        /// </summary>
        public bool IsRevealed { get; set; } = false;

        /// <summary>
        /// Flag for letting us know if this cards is currently animating
        /// </summary>
        public bool IsAnimating { get; set; } = false;

        /// <summary>
        /// The content for this cards
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The current animation to run on value changed
        /// </summary>
        public ButtonAnimationTypes Animation { get; set; } = ButtonAnimationTypes.None;

        #endregion

        #region MyRegion

        /// <summary>
        /// The action to execute when revealing
        /// </summary>
        public Action CardRevealed;

        /// <summary>
        /// The action to execute when reseting the revealed
        /// </summary>
        public Action ResetRevealed;

        /// <summary>
        /// The action to execute when checking for a possible match
        /// </summary>
        public Action CheckForMatch;

        #endregion

        #region Commands

        /// <summary>
        /// The command for clicking
        /// </summary>
        public ICommand ClickCommand { get; set; }

        /// <summary>
        /// The command when animation is finished
        /// </summary>
        public ICommand FinishedAnimatingCommand { get; set; }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MemoryCardButtonViewModel()
        {
            InitializeCommands();
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public MemoryCardButtonViewModel(MemoryCardButtonViewModel src)
            : base()
        {
            Content = src.Content;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// The click command method
        /// </summary>
        private void Click()
        {
            //Check if we can reveal
            if (MemoryViewModel.RevealedCounter < 2 && !IsMatched && !IsRevealed)
            {
                IsRevealed = true;
                CardRevealed();
                IsAnimating = true;
                Animation = ButtonAnimationTypes.Reveal;
            }
        }

        /// <summary>
        /// The finished animation command
        /// </summary>
        private void FinishedAnimating()
        {
            //gets the finished animation and set animating to false
            var finishedAnimation = Animation;
            IsAnimating = false;

            switch (Animation)
            {
                //If we revealed check for a match
                case ButtonAnimationTypes.Reveal:
                    CheckForMatch();
                    break;
                //if there was a match set the match flag and reset the revealed
                case ButtonAnimationTypes.Match:
                    IsMatched = true;
                    ResetRevealed();
                    break;
                //if there was no match just reset the revealed
                case ButtonAnimationTypes.NoMatch:
                    ResetRevealed();
                    break;
            }
            Animation = ButtonAnimationTypes.None;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The starts the match animation
        /// </summary>
        public void Match()
        {
            IsAnimating = true;
            Animation = ButtonAnimationTypes.Match;
        }

        /// <summary>
        /// Starts thhe no match animation
        /// </summary>
        public void NoMatch()
        {
            IsAnimating = true;
            Animation = ButtonAnimationTypes.NoMatch;
        }

        #endregion

        #region Private Helpers Methods

        private void InitializeCommands()
        {
            ClickCommand = new RelayCommand(Click);
            FinishedAnimatingCommand = new RelayCommand(FinishedAnimating);
        }

        #endregion
    }
}
