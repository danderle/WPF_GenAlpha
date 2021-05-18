using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GenAlpha
{
    /// <summary>
    /// Shuffles all the items in the grid to new positions
    /// </summary>
    public class ShuffleGridProperty : BaseAttachedProperties<ShuffleGridProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //Get the caller
            var grid = sender as Grid;

            //Make sure it is a grid
            if (grid == null)
                return;

            if(!grid.IsLoaded)
            {
                grid.Loaded += Grid_Loaded;
                return;
            }

            //true to shuffle
            if((bool)e.NewValue)
            {
                var list = new List<Button>();
                int count = 0;
                foreach(var item in grid.Children)
                {
                    var button = item as Button;
                    button.Tag = count;
                    list.Add(button);
                    count++;
                }
                grid.Children.Clear();
                foreach (var button in list)
                {
                    grid.Children.Add(button);
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            var list = new List<Button>();
            int count = 0;
            var cards = (ResourceDictionary)Application.Current.Resources["MemoryCards"];
            if(cards.Count * 2 == grid.Children.Count)
            {
                int gridIndex = 0;
                foreach(DictionaryEntry card in cards)
                {
                    var key = card.Key;
                    var value = card.Key;
                    var button = (Button)grid.Children[gridIndex];
                    button.Tag = key.ToString().Replace("FontAwesome", "");
                    button.Content = Application.Current.Resources[key];
                    list.Add(button);
                    gridIndex++;
                    button = (Button)grid.Children[gridIndex];
                    button.Tag = key.ToString().Replace("FontAwesome", "");
                    button.Content = Application.Current.Resources[key];
                    list.Add(button);
                    gridIndex++;
                }
            }

            Shuffle(list);

            
            grid.Children.Clear();
            int row = 0;
            int column = 0;
            foreach (var button in list)
            {
                Grid.SetColumn(button, column);
                Grid.SetRow(button, row);
                column++;
                if(column >= grid.ColumnDefinitions.Count)
                {
                    column = 0;
                    row++;
                }
                grid.Children.Add(button);
            }
        }

        private void Shuffle<T>(IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;

            for (int i = list.Count - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);

                T value = list[rnd];
                list[rnd] = list[i];
                list[i] = value;
            }
        }
    }
}
