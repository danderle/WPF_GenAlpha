using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GenAlpha.Core
{
    public static class Image
    {
        static Image()
        {
        }

        public static Dictionary<MinesweeperSquareState, object> GetMinesweeperImagePaths()
        {
            return new()
            {
                { MinesweeperSquareState.Zero, Application.Current.FindResource("0") },
                { MinesweeperSquareState.One, Application.Current.FindResource("1") },
                { MinesweeperSquareState.Two, Application.Current.FindResource("2") },
                { MinesweeperSquareState.Three, Application.Current.FindResource("3") },
                { MinesweeperSquareState.Four, Application.Current.FindResource("4") },
                { MinesweeperSquareState.Five, Application.Current.FindResource("5") },
                { MinesweeperSquareState.Six, Application.Current.FindResource("6") },
                { MinesweeperSquareState.Seven, Application.Current.FindResource("7") },
                { MinesweeperSquareState.Eight, Application.Current.FindResource("8") },
                { MinesweeperSquareState.Flag, Application.Current.FindResource("Flag") },
                { MinesweeperSquareState.Bomb, Application.Current.FindResource("Bomb") },
                { MinesweeperSquareState.Unopened, Application.Current.FindResource("Unopened") },
            };
        }
    }
}
