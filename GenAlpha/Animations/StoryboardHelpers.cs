using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace GenAlpha
{
    /// <summary>
    /// Animation helpers for the <see cref="Storyboard"/>
    /// </summary>
    public static class StoryboardHelpers
    {
        #region Slide left / right
        
        /// <summary>
        /// Adds a slide in animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="deceleration">The rate of deceleration</param>
        public static void AddSlideInFromRight(this Storyboard storyboard, float seconds, double offset, float deceleration = 0.9f)
        {
            //Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(offset, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = deceleration,
            };

            //Set the target property for animation
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a slide out animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="deceleration">The rate of deceleration</param>
        public static void AddSlideOutToLeft(this Storyboard storyboard, float seconds, double offset, float deceleration = 0.9f)
        {
            //Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, offset, 0),
                DecelerationRatio = deceleration,
            };

            //Set the target property for animation
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }
        #endregion

        #region Fade in / out

        /// <summary>
        /// Adds a fade in to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="deceleration">The rate of deceleration</param>
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            //Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1,
            };

            //Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a fade out to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="deceleration">The rate of deceleration</param>
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {
            //Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0,
            };

            //Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }
        #endregion
    }
}
