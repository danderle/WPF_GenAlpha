using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GenAlpha
{
    /// <summary>
    /// Shuffles all the items in the <see cref="MemoryPage"/> grid to new positions
    /// </summary>
    public class ShuffleMemoryGridProperty : BaseAttachedProperties<ShuffleMemoryGridProperty, bool>
    {
        #region Events

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //Get the caller
            var grid = sender as Grid;

            //Make sure it is a grid
            if (grid == null)
                return;

            //if not loaded hook into loaded event
            if (!grid.IsLoaded)
            {
                grid.Loaded += Grid_Loaded;
                return;
            }

            //unhook if already loaded
            grid.Loaded -= Grid_Loaded;

            //true to shuffle
            if ((bool)e.NewValue)
            {
                var list = GetListWithAllMemoryButtons(grid);
                Shuffle(list);

                grid.Children.Clear();
                FillGridFromList(grid, list);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            var list = GetListWithAllMemoryButtons(grid);
            Shuffle(list);

            grid.Children.Clear();
            FillGridFromList(grid, list);
        }

        #endregion


        #region Private Helpers

        private void FillGridFromList(Grid grid, List<Button> list)
        {
            int row = 0;
            int column = 0;
            foreach (var button in list)
            {
                Grid.SetColumn(button, column);
                Grid.SetRow(button, row);
                column++;
                if (column >= grid.ColumnDefinitions.Count)
                {
                    column = 0;
                    row++;
                }
                grid.Children.Add(button);
            }
        }

        private List<Button> GetListWithAllMemoryButtons(Grid grid)
        {
            var list = new List<Button>();
            var cards = (ResourceDictionary)Application.Current.Resources["MemoryCards"];
            if (cards.Count * 2 == grid.Children.Count)
            {
                int gridIndex = 0;
                foreach (DictionaryEntry card in cards)
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
            return list;
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

        #endregion
    }
}
