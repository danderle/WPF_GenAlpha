using System.Windows;
using System.Windows.Controls;

namespace GenAlpha
{
    public class IsRevealedProperty : BaseAttachedProperties<IsRevealedProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            if((bool)e.NewValue)
            {
                var parent = button.Parent;
                CheckRevealedCardsMatchProperty.CheckIfCardsMatch(parent);
            }
        }

        public static bool IsRevealed(Button button)
        {
            return GetValue(button);
        }
        
        public static void Reset(DependencyObject button)
        {
            SetValue(button, false);
        }
    }
}
