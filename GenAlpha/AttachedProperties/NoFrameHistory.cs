using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GenAlpha
{
    /// <summary>
    /// The no frame history attached property for creating a <see cref="Frame"/> that never shows navigation 
    /// and keeps the navigation history empty
    /// </summary>
    public class NoFrameHistory : BaseAttachedProperties<NoFrameHistory, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the frame
            var frame = (sender as Frame);

            // Hide the navigation bar
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            // Clear history on navigate
            frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();
        }
    }
}
