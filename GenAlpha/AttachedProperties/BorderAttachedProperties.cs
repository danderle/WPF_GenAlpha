using System.Windows;
using System.Windows.Input;

namespace GenAlpha
{
    #region Mouse

    /// <summary>
    /// Executes the command on the mouse enter event
    /// </summary>
    public class MouseEnter : BaseAttachedProperties<MouseEnter, ICommand>
    {
        #region Command

        /// <summary>
        /// The command to execute
        /// </summary>
        private ICommand command;

        #endregion

        #region OnValueChanged

        /// <summary>
        /// Sets the command and registers the MouseEnter event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Check if sender is a framework element
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            // save the command
            command = (ICommand)e.NewValue;

            // Register mouse enter event
            element.MouseEnter += Element_MouseEnter;
        }

        #endregion

        #region Mouse enter event

        /// <summary>
        /// The mouse enter event executes the command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            command.Execute(null);
        } 
        #endregion
    }

    /// <summary>
    /// Executes the command on the mouse leave event
    /// </summary>
    public class MouseLeave : BaseAttachedProperties<MouseLeave, ICommand>
    {
        #region Command

        /// <summary>
        /// The command to execute
        /// </summary>
        private ICommand command;

        #endregion

        #region OnValueChanged

        /// <summary>
        /// Sets the command and registers the MouseLeave event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Check if sender is a framework element
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            // save the command
            command = (ICommand)e.NewValue;

            // Register mouse enter and leave events
            element.MouseLeave += Element_MouseLeave;
        }

        #endregion

        #region Mouse enter event

        /// <summary>
        /// The mouse leave event executes the command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_MouseLeave(object sender, MouseEventArgs e)
        {
            command.Execute(null);
        }
        #endregion
    }

    /// <summary>
    /// Executes the command with the mouse position as a parameter
    /// </summary>
    public class MouseMoved : BaseAttachedProperties<MouseMoved, ICommand>
    {
        /// <summary>
        /// The command to execute
        /// </summary>
        private ICommand command;

        /// <summary>
        /// Sets the command and registers the MouseMove event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Check if sender is a framework element
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            // save the command
            command = (ICommand)e.NewValue;

            element.MouseMove += Element_MouseMove;
        }

        /// <summary>
        /// The mouse move event method executes the commmand with the mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            Point mousePosition = e.GetPosition(element);
            command.Execute(mousePosition);
        }
    }

    #endregion

}
