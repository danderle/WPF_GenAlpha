using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The view model for the <see cref="GameSelectionPage"/>
    /// </summary>
    public class GameSelectionViewModel : BaseViewModel
    {
        #region Private Members

        #endregion

        #region Properties

        public List<string> Games { get; set; } = new List<string>();

        #endregion

        #region Commands

        /// <summary>
        /// 
        /// </summary>
        public ICommand ClickCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameSelectionViewModel()
        {
            Games.Add("Memory Game");
            Games.Add("Keyboard Shooter");
            Games.Add("Game 3");
            Games.Add("Game 4");
            Games.Add("Game 5");

            ClickCommand = new RelayCommand(Click);
        }

        private void Click()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Helpers

        #endregion
    }
}
