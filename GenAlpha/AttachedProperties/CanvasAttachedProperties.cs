using GenAlpha.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GenAlpha
{
    /// <summary>
    /// Attached property which is the rgb value for the foreground color
    /// </summary>
    public class SpawnItem : BaseAttachedProperties<SpawnItem, bool>
    {
        private object fallingTexts = new();

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            var canvas = sender as Canvas;
            if (canvas == null)
                return;

            canvas.Focus();

            var childrenCount = canvas.Children.Count;
            if (childrenCount > 2)
            {
                canvas.Children.RemoveRange(2, childrenCount - 1);
            }

            var texts = TextObjects.Get(canvas);
            if (texts == null || texts.Count == 0)
                return;

            lock (fallingTexts)
            {
                TextObjects.AddTextsToCanvas(texts, canvas);
            }
        }
    }

    #region TextObjects

    /// <summary>
    /// Gets the FallingTexts items
    /// </summary>
    public class TextObjects : BaseAttachedProperties<TextObjects, List<FallingText>>
    {
        public static List<FallingText> Get(DependencyObject d)
        {
            return GetValue(d);
        }

        public static void AddTextsToCanvas(List<FallingText> texts, Canvas canvas)
        {
            foreach (var text in texts)
            {
                var textBlock = new TextBlock();
                textBlock.FontSize = (double)Application.Current.FindResource("FontSizeXLarge");
                textBlock.Text = text.DisplayedText;
                if(text.Targeted)
                {
                    textBlock.Foreground = (Brush)Application.Current.FindResource("GoldBrush");
                }
                Canvas.SetLeft(textBlock, text.Xposition);
                Canvas.SetTop(textBlock, text.Yposition);
                canvas.Children.Add(textBlock);
            }
        }
    }

    #endregion

    #region BulletObjects

    /// <summary>
    /// Gets the bullet objects
    /// </summary>
    public class BulletObjects : BaseAttachedProperties<TextObjects, List<Bullet>>
    {
        public static List<Bullet> Get(DependencyObject d)
        {
            return GetValue(d);
        }

        public static void AddBulletsToCanvas(List<Bullet> bullets, Canvas canvas)
        {
            foreach (var bullet in bullets)
            {
                var ellipse = new Ellipse();
                ellipse.Width = 10;
                ellipse.Height = 10;
                ellipse.Fill = new SolidColorBrush((Color)Application.Current.FindResource("Gray"));
                Canvas.SetLeft(ellipse, bullet.Xposition);
                Canvas.SetTop(ellipse, bullet.Yposition);
                canvas.Children.Add(ellipse);
            }
        }
    }

    #endregion

    #region ItemsMoved

    /// <summary>
    /// Lets us know when to update falling texts and bullets
    /// </summary>
    public class ItemsMoved : BaseAttachedProperties<ItemsMoved, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var canvas = sender as Canvas;
            if (canvas == null)
                return;

            var childrenCount = canvas.Children.Count;
            if (childrenCount > 2)
            {
                canvas.Children.RemoveRange(2, childrenCount-1);
            }

            var texts = TextObjects.Get(canvas);
            if (texts == null || texts.Count == 0)
                return;

            TextObjects.AddTextsToCanvas(texts, canvas);

            var bullets = BulletObjects.Get(canvas);
            if (bullets == null || bullets.Count == 0)
                return;

            BulletObjects.AddBulletsToCanvas(bullets, canvas);
        }
    }

    #endregion

    #region KeyboardInput

    /// <summary>
    /// Executes the command with the key input as a parameter
    /// </summary>
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
            if(e.Key >= Key.A && e.Key <= Key.Z)
            {
                command.Execute(e.Key);
            }
        }
    }

    #endregion
}
