using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
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
        /// <param name="keepMargin">Wether toi keep the element at the same width during animation</param>
        public static void AddSlideInFromRight(this Storyboard storyboard, float seconds, double offset, float deceleration = 0.9f, bool keepMargin = true)
        {
            //Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
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
        /// <param name="keepMargin">Wether toi keep the element at the same width during animation</param>
        public static void AddSlideOutToLeft(this Storyboard storyboard, float seconds, double offset, float deceleration = 0.9f, bool keepMargin = true)
        {
            //Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, keepMargin ? offset : 0, 0),
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
        /// <param name="keepMargin">Wether toi keep the element at the same width during animation</param>
        public static void AddSlideOutToRight(this Storyboard storyboard, float seconds, double offset, float deceleration = 0.9f, bool keepMargin = true)
        {
            //Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
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
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            //Create the fade in opacity
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
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {
            //Create the fade out opacity
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

        #region Rotation

        /// <summary>
        /// Adds a fade out to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddRotation(this Storyboard storyboard, float seconds, float begin = 0, int transformGroupChild = -1)
        {
            //Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 720,
                BeginTime = TimeSpan.FromSeconds(begin)
            };

            if (transformGroupChild > -1)
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath($"RenderTransform.Children[{transformGroupChild}].Angle"));
            }
            else
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            }

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }

        #endregion

        #region Scale X / Y

        /// <summary>
        /// Adds a scale y out to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddScaleYout(this Storyboard storyboard, float seconds, float begin = 0, int transformGroupChild = -1)
        {
            //Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0,
                BeginTime = TimeSpan.FromSeconds(begin)
            };

            if (transformGroupChild > -1)
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath($"RenderTransform.Children[{transformGroupChild}].ScaleY"));
            }
            else
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
            }

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a scale x out to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddScaleXout(this Storyboard storyboard, float seconds, float begin = 0, int transformGroupChild = -1)
        {
            //Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0,
                BeginTime = TimeSpan.FromSeconds(begin)
            };

            if(transformGroupChild > -1)
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath($"RenderTransform.Children[{transformGroupChild}].ScaleX"));
            }
            else
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            }

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a scale x out to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddScaleXCover(this Storyboard storyboard, float seconds, float begin = 0, int transformGroupChild = -1)
        {
            //Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = -1,
                BeginTime = TimeSpan.FromSeconds(begin)
            };

            if (transformGroupChild > -1)
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath($"RenderTransform.Children[{transformGroupChild}].ScaleX"));
            }
            else
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            }

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a scale x out to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddScaleUncover(this Storyboard storyboard, float seconds, float begin = 0, int transformGroupChild = -1)
        {
            //Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = -1,
                To = 1,
                BeginTime = TimeSpan.FromSeconds(begin)
            };

            if (transformGroupChild > -1)
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath($"RenderTransform.Children[{transformGroupChild}].ScaleX"));
            }
            else
            {
                //Set the target property name
                Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            }

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }
        #endregion

        #region Color

        /// <summary>
        /// Adds a scale x out to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddForegroundColor(this Storyboard storyboard,Color from, Color to, float seconds, float begin = 0)
        {
            //Create the margin animate from right
            var animation = new ColorAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = from,
                To = to,
                BeginTime = TimeSpan.FromSeconds(begin)
            };

            //Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Foreground.Color"));

            //Add to the storyboard
            storyboard.Children.Add(animation);
        }

        #endregion
    }
}
