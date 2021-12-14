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
        /// The radius of the spy circle
        /// </summary>
        public double Radius { get; set; } = 10;

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

        /// <summary>
        /// The mouse enter command
        /// </summary>
        public ICommand MouseEnterCommand { get; set; }

        /// <summary>
        /// The mouse leave command
        /// </summary>
        public ICommand MouseLeaveCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AnimalGuessViewModel()
        {
            MouseLeaveCommand = new RelayCommand(MouseLeave);
            MouseEnterCommand = new RelayCommand(MouseEnter);
            MouseMoveCommand = new RelayParameterizedCommand(MouseMoved);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Sets the current mouse position
        /// </summary>
        /// <param name="obj"></param>
        private void MouseMoved(object obj)
        {
            string position = obj.ToString();
            var coo = position.Split(";");
            MousePosition = new Point(Convert.ToDouble(coo[0]), Convert.ToDouble(coo[1]));
        }

        /// <summary>
        /// On entering set the radius
        /// </summary>
        private void MouseEnter()
        {
            Radius = 20;
        }

        /// <summary>
        /// On leave set the radius to zero
        /// </summary>
        private void MouseLeave()
        {
            Radius = 0;
        }

        #endregion
    }
}
