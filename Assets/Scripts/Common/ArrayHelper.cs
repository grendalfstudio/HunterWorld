using System;

namespace Common
{
    public static class ArrayHelper
    {
        public static T GetRandomElement<T>(this T[] array)
        {
            var rnd = new Random();
            var num = rnd.Next(array.Length);
            return array[num];
        }
    }
}