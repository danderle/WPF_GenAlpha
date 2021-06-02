using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GenAlpha
{
    /// <summary>
    /// Helpers to animate buttons in specific ways
    /// </summary>
    public static class ButtonAnimations
    {
        /// <summary>
        /// Spins and scales out the button
        /// </summary>
        /// <param name="page"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static async Task SpinAndScaleOutAsync(Button button, float seconds, float beginTime = 0)
        {
            //Create storyboard
            var sb = new Storyboard();

            //scales x out animation
            sb.AddScaleXout(seconds, beginTime, 0);

            //scales y out animation
            sb.AddScaleYout(seconds, beginTime, 0);

            //rotate animation
            sb.AddRotation(seconds, beginTime, 1);

            var groupTransform = new TransformGroup();
            var scale = new ScaleTransform();
            var rotate = new RotateTransform();
            var origin = new Point(0.5f, 0.5f);
            button.RenderTransformOrigin = origin;
            groupTransform.Children.Add(scale);
            groupTransform.Children.Add(rotate);

            //Sets the transforms to the button
            button.RenderTransform = groupTransform;

            //start animating
            sb.Begin(button);

            //Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }

        /// <summary>
        /// Covers the revealed button animation
        /// </summary>
        /// <param name="button"></param>
        /// <param name="seconds"></param>
        /// <param name="begin"></param>
        /// <returns></returns>
        public static async Task CoverAsync(Button button, Color fromColor, float seconds, float begin)
        {
            //Create storyboard
            var sb = new Storyboard();

            ////scales x out animation
            sb.AddScaleXCover(seconds, begin);

            //makes font transparent
            sb.AddForegroundColor(fromColor, Colors.Transparent, seconds / 2, begin);

            //Sets the transforms to the button
            var scale = new ScaleTransform();
            button.RenderTransform = scale;

            //start animating
            sb.Begin(button);

            //Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }

        /// <summary>
        /// Reveales a covered button animation
        /// </summary>
        /// <param name="button"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static async Task Reveal(Button button, Color ToColor, float seconds)
        {
            //Create storyboard
            var sb = new Storyboard();

            //scales x out animation
            sb.AddScaleUncover(seconds);

            //makes font transparent
            sb.AddForegroundColor(Colors.Transparent, ToColor, seconds / 2f, seconds / 2f);

            //Sets the transforms to the button
            var scale = new ScaleTransform();
            button.RenderTransform = scale;
            button.RenderTransformOrigin = new Point(0.5, 0.5);

            //start animating
            sb.Begin(button);

            //Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }
    }
}
