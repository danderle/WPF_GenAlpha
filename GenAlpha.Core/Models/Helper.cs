using System;
using System.Collections.Generic;

namespace GenAlpha.Core
{
    public static class Helper
    {

        public static List<T> Shuffle<T>(this List<T> list)
        {
            Random random = new Random();

            for (int i = list.Count - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);
                var value = list[rnd];

                list[rnd] = list[i];
                list[i] = value;
            }
            return list;
        }
    }
}
