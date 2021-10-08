using System;
using System.Windows.Input;

namespace GenAlpha.Core
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
        /// The rgb in hex string format
        /// </summary>
        public string RgbHexString { get; set; } = string.Empty;

        /// <summary>
        /// The content for this cards
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// The current language
        /// </summary>
        public string CultureLanguage { get; set; } = string.Empty;

        /// <summary>
        /// The current animation to run on value changed
        /// </summary>
        public ButtonAnimationTypes Animation { get; set; } = ButtonAnimationTypes.None;

        #endregion

        #region Actions

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
        public MemoryCardButtonViewModel(string content, string culture)
        {
            Content = content;
            CultureLanguage = culture;
            InitializeCommands();
            InitializeProperties();
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public MemoryCardButtonViewModel(MemoryCardButtonViewModel src)
        {
            InitializeCommands();
            Content = src.Content;
            RgbHexString = src.RgbHexString;
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
                    Speech.Speak(Content, CultureLanguage);
                    CheckForMatch();
                    break;
                //if there was a match set the match flag and reset the revealed
                case ButtonAnimationTypes.Match:
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
            IsMatched = true;
            Sound.PlayAsync(SoundTypes.CardMatch);
            Animation = ButtonAnimationTypes.Match;
        }

        /// <summary>
        /// Starts the no match animation
        /// </summary>
        public void NoMatch()
        {
            IsAnimating = true;
            Sound.PlayAsync(SoundTypes.CardNoMatch);
            Animation = ButtonAnimationTypes.NoMatch;
        }

        #endregion

        #region Private Helpers Methods

        /// <summary>
        /// Initializes all the commands
        /// </summary>
        private void InitializeCommands()
        {
            ClickCommand = new RelayCommand(Click);
            FinishedAnimatingCommand = new RelayCommand(FinishedAnimating);
        }

        /// <summary>
        /// Initializes all the properties
        /// </summary>
        private void InitializeProperties()
        {
            var rand = new Random();
            int min = 0;
            int max = 175;
            byte[] rgb = new byte[] { Convert.ToByte(rand.Next(min, max)), Convert.ToByte(rand.Next(min, max)), Convert.ToByte(rand.Next(min, max)) };
            RgbHexString += BitConverter.ToString(rgb).Replace("-", "");
        }
            
        #endregion
    }
}
