using GenAlpha.Core;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GenAlpha
{
    /// <summary>
    /// Attached property which is the rgb value for the foreground color
    /// </summary>
    public class RgbProperty : BaseAttachedProperties<RgbProperty, string>
    {
        public static Color GetColor(DependencyObject d)
        {
            //make sure we have a button
            var button = d as Button;
            if (button == null)
                return Color.FromRgb(0,0,0);

            var rgbString = GetValue(button);
            byte r = Convert.ToByte(rgbString.Substring(0, 2), 16);
            byte g = Convert.ToByte(rgbString.Substring(2, 2), 16);
            byte b = Convert.ToByte(rgbString.Substring(4, 2), 16);
            return Color.FromRgb(r, g, b);
        }
    }

    /// <summary>
    /// Attached property which executes a command when an animation is finished
    /// </summary>
    public class FinishedAnimationProperty : BaseAttachedProperties<FinishedAnimationProperty, ICommand>
    {
        /// <summary>
        /// Executes the set command
        /// </summary>
        /// <param name="d"></param>
        /// <param name="time">The time to wait before executing</param>
        /// <returns></returns>
        public static async Task ExecuteCommand(DependencyObject d, int time)
        {
            //make sure we have a button
            var button = d as Button;
            if (button == null)
                return;

            //wait for the specified time then execute
            await Task.Delay(time * 1000);
            GetValue(d).Execute(d);
        }
    }

    /// <summary>
    /// Attached property for setting and starting an animation of type <see cref="ButtonAnimationTypes"/>
    /// </summary>
    public class IsAnimatingProperty : BaseAttachedProperties<IsAnimatingProperty, ButtonAnimationTypes>
    {
        /// <summary>
        /// Overriden on value changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override async void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //make sure we have a button
            var button = sender as Button;
            if (button == null)
                return;

            //Make sure we want to animate
            var animation = (ButtonAnimationTypes)e.NewValue;
            if (animation == ButtonAnimationTypes.None)
                return;

            //the time to animate and begin
            float seconds = 1f;
            float begin = 1f;
            Color cardColor = RgbProperty.GetColor(button);
            switch (animation)
            {
                case ButtonAnimationTypes.Reveal:
                    Sound.Play(SoundTypes.CardFlip);
                    await ButtonAnimations.Reveal(button, cardColor, seconds);
                    await FinishedAnimationProperty.ExecuteCommand(button, 0);
                    break;
                case ButtonAnimationTypes.Match:
                    await ButtonAnimations.SpinAndScaleOutAsync(button, seconds, begin);
                    Sound.Play(SoundTypes.CardSpinOut);
                    await FinishedAnimationProperty.ExecuteCommand(button, Convert.ToInt16(begin));
                    break;
                case ButtonAnimationTypes.NoMatch:
                    await ButtonAnimations.CoverAsync(button, cardColor, seconds, begin);
                    Sound.Play(SoundTypes.CardFlip);
                    await FinishedAnimationProperty.ExecuteCommand(button, Convert.ToInt16(begin));
                    break;
            }
        }
    }

}
