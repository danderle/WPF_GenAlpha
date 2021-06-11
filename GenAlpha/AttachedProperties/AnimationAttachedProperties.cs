using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GenAlpha
{
    /// <summary>
    /// Attached property which animates a control in and out
    /// </summary>
    public class SlideAndFadeInOutFromRightProperty : BaseAttachedProperties<SlideAndFadeInOutFromRightProperty, bool>
    {
        public override async void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as UserControl;

            if (control == null)
                return;

            var newValue = (bool)e.NewValue;

            if(newValue)
            {
                await SlideInFromRight(control, 0.5f);
            }
            else
            {
                await SlideOutToRight(control, 0.5f);
            }
        }

        /// <summary>
        /// Slides a control in from the right
        /// </summary>
        /// <param name="page"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task SlideInFromRight(UserControl control, float seconds)
        {
            //Create storyboard
            var sb = new Storyboard();

            //Add slide from right animation
            sb.AddSlideInFromRight(seconds, control.MinWidth);

            //start animating
            sb.Begin(control);

            //Make page visible
            control.Visibility = Visibility.Visible;

            //Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }

        /// <summary>
        /// Slides a control out to the right
        /// </summary>
        /// <param name="page"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static async Task SlideOutToRight(UserControl control, float seconds)
        {
            //Create storyboard
            var sb = new Storyboard();

            //Add slide to right animation
            sb.AddSlideOutToRight(seconds, control.ActualWidth);

            //start animating
            sb.Begin(control);

            //Wait for it to finish
            await Task.Delay((int)(seconds * 1000));

            //Make control collapsed
            control.Visibility = Visibility.Collapsed;
        }
    }
}
