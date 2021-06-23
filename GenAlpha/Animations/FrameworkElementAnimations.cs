using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace GenAlpha
{
    /// <summary>
    /// Helpers to animate framework elements in specific ways
    /// </summary>
    public static class FrameworkElementAnimations
    {
        /// <summary>
        /// Slides a control in from the right
        /// </summary>
        /// <param name="element"></param>
        /// <param name="seconds"></param>
        /// <param name="keepMargin">Wether toi keep the element at the same width during animation</param>
        /// <returns></returns>
        public static async Task SlideInFromRightAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            //Create storyboard
            var sb = new Storyboard();

            //Add slide from right animation
            sb.AddSlideInFromRight(seconds, element.ActualWidth, keepMargin: keepMargin);

            //start animating
            sb.Begin(element);

            //Make element visible
            element.Visibility = Visibility.Visible;

            //Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }

        /// <summary>
        /// Slides a control out to the right
        /// </summary>
        /// <param name="element"></param>
        /// <param name="seconds"></param>
        /// <param name="keepMargin">Wether toi keep the element at the same width during animation</param>
        /// <returns></returns>
        public static async Task SlideOutToRightAsync(this FrameworkElement element, float seconds, bool keepMargin = true)
        {
            //Create storyboard
            var sb = new Storyboard();

            //Add slide to right animation
            sb.AddSlideOutToRight(seconds, element.MinWidth, keepMargin: keepMargin);

            //start animating
            sb.Begin(element);

            //Make element visible
            element.Visibility = Visibility.Visible;

            //Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }
    }
}
