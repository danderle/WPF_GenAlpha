using System.Windows;
using System.Windows.Input;

namespace GenAlpha
{
    #region MouseMoved

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
        /// Sets the command and registers the MouseEnter and MouseLeave events
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
            element.MouseEnter += Element_MouseEnter;
            element.MouseLeave += Element_MouseLeave;
        }

        /// <summary>
        /// The event method on mouse leave, unhooks the mouse move event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            element.MouseMove -= Element_MouseMove;
        }

        /// <summary>
        /// The mouse enter event method hooks into the mouse move event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            element.MouseMove += Element_MouseMove;
        }

        /// <summary>
        /// The mouse move event method gets the current mouse position and executes the commmand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            Point mousePosition = e.GetPosition(sender as FrameworkElement);
            command.Execute(mousePosition);
        }
    }

    #endregion
}
