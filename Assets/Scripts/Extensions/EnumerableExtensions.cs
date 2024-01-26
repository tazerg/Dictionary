using System;
using System.Collections.Generic;
using System.Linq;

namespace JHI.Dict.Extensions
{
    public static class EnumerableExtensions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.RandomElementUsing(new Random());
        }

        public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
        {
            var enumerableList = enumerable.ToList();
            var index = rand.Next(0, enumerableList.Count);
            return enumerableList.ElementAt(index);
        }
    }
}