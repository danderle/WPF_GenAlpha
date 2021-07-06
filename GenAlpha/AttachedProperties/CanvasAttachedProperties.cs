using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static GenAlpha.Core.KeyboardShooterViewModel;

namespace GenAlpha
{
    /// <summary>
    /// Attached property which is the rgb value for the foreground color
    /// </summary>
    public class SpawnItem : BaseAttachedProperties<SpawnItem, bool>
    {
        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            var canvas = sender as Canvas;
            if (canvas == null)
                return;

            var items = TextObjects.Get(canvas);
            if (items == null || items.Count == 0)
                return;

            var childrenCount = canvas.Children.Count;
            if (childrenCount > 1)
            {
                canvas.Children.RemoveRange(1, childrenCount - 1);
            }

            foreach (var item in items)
            {
                var textBlock = new TextBlock();
                textBlock.Text = item.Text;
                Canvas.SetLeft(textBlock, item.Xposition);
                Canvas.SetTop(textBlock, item.Yposition);
                canvas.Children.Add(textBlock);
            }
        }
    }

    public class TextObjects : BaseAttachedProperties<TextObjects, List<FallingText>>
    {
        public static List<FallingText> Get(DependencyObject d)
        {
            return GetValue(d);
        }
    }
    
    public class ItemsMoved : BaseAttachedProperties<ItemsMoved, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var canvas = sender as Canvas;
            if (canvas == null)
                return;

            var items = TextObjects.Get(canvas);
            if (items == null || items.Count == 0)
                return;

            var childrenCount = canvas.Children.Count;
            if (childrenCount > 1)
            {
                canvas.Children.RemoveRange(1, childrenCount - 1);
            }

            foreach (var item in items)
            {
                var textBlock = new TextBlock();
                textBlock.Text = item.Text;
                Canvas.SetLeft(textBlock, item.Xposition);
                Canvas.SetTop(textBlock, item.Yposition);
                canvas.Children.Add(textBlock);
            }
        }
    }

    public class KeyboardInput : BaseAttachedProperties<KeyboardInput, ICommand>
    {
        private ICommand command;

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            command = (ICommand)e.NewValue;
            element.PreviewKeyDown += Element_PreviewKeyDown;
        }

        private void Element_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            command.Execute(e.Key);
        }
    }

}
