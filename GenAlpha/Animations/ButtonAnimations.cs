using System;
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
        public static async Task SpinAndScaleOutAsync(Button button1, Button button2, float seconds)
        {
            //Create storyboard
            var sb = new Storyboard();

            //scales x out animation
            sb.AddScaleXout(seconds, 2, 0);

            //scales y out animation
            sb.AddScaleYout(seconds, 2, 0);

            //rotate animation
            sb.AddRotation(seconds, 2, 1);

            var groupTransform = new TransformGroup();
            var scale = new ScaleTransform();
            var rotate = new RotateTransform();
            groupTransform.Children.Add(scale);
            groupTransform.Children.Add(rotate);
            
            //Sets the transforms to the button
            button1.RenderTransform = groupTransform;
            button2.RenderTransform = groupTransform;

            //start animating
            sb.Begin(button1);
            sb.Begin(button2);

            //Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }

        public static async Task FlipAndColorAsync(Button button1, Button button2, float seconds, float beginTime)
        {
            //Create storyboard
            var sb = new Storyboard();

            //scales x out animation
            sb.AddScaleXCover(seconds, beginTime);

            //makes font transparent
            sb.AddForegroundColor(Colors.Transparent, seconds/2f, beginTime);

            //Sets the transforms to the button
            var scale = new ScaleTransform();
            button1.RenderTransform = scale;
            button2.RenderTransform = scale;

            //start animating
            sb.Begin(button1);
            sb.Begin(button2);

            //Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }
    }
}
