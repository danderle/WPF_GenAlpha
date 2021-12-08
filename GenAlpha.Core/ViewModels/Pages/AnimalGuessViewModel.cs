using System;
using System.Windows;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="AnimalGuessPage"/>
    /// </summary>
    public class AnimalGuessViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The current mouse position
        /// </summary>
        public Point MousePosition { get; set; }

        /// <summary>
        /// The top bar for this view model
        /// </summary>
        public BaseTopBarViewModel TopBar { get; set; } = new BaseTopBarViewModel();

        #endregion

        #region Commands

        /// <summary>
        /// The mouse move command
        /// </summary>
        public ICommand MouseMoveCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AnimalGuessViewModel()
        {
            MouseMoveCommand = new RelayParameterizedCommand(MouseMoved);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Seths the current mouse position
        /// </summary>
        /// <param name="obj"></param>
        private void MouseMoved(object obj)
        {
            string position = obj.ToString();
            var coo = position.Split(";");
            MousePosition = new Point(Convert.ToDouble(coo[0]), Convert.ToDouble(coo[1]));
        }

        #endregion
    }
}
