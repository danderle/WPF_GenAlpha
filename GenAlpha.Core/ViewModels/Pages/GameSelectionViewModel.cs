using System.Collections.Generic;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="GameSelectionPage"/>
    /// </summary>
    public class GameSelectionViewModel : BaseViewModel
    {
        #region Private Members

        #endregion

        #region Properties

        /// <summary>
        /// The list of games available
        /// </summary>
        public List<GameSelectionButtonViewModel> Games { get; set; } = new List<GameSelectionButtonViewModel>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameSelectionViewModel()
        {
            Games.Add(new GameSelectionButtonViewModel("Memory Game", ApplicationPage.Memory));
            Games.Add(new GameSelectionButtonViewModel("Keyboard shooter", ApplicationPage.KeyboardShooter));
            Games.Add(new GameSelectionButtonViewModel("Connect-4", ApplicationPage.Connect4));
            Games.Add(new GameSelectionButtonViewModel("Game 4", ApplicationPage.Memory));
            Games.Add(new GameSelectionButtonViewModel("Game 5", ApplicationPage.Memory));
        }

        #endregion
    }
}
