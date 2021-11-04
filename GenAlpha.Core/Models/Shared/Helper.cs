using System;
using System.Collections.Generic;

namespace GenAlpha.Core
{
    /// <summary>
    /// Static helper class for repeating algorithims
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Shuffles a list of a generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Shuffles an array of a generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T[] Shuffle<T>(this T[] array)
        {
            Random random = new Random();

            for (int i = array.Length - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);
                var value = array[rnd];

                array[rnd] = array[i];
                array[i] = value;
            }
            return array;
        }
    }
}
