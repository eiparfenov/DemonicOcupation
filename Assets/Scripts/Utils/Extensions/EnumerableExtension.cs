using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Utils.Extensions
{
    public static class EnumerableExtension
    {
        public static T RandomOrDefault<T>(this IEnumerable<T> source)
        {
            var arr = source.ToArray();
            if (arr.Length == 0) return default;
            return arr[Random.Range(0, arr.Length)];
        }

        public static T ItemWithMax<T>(this IEnumerable<T> source, Func<T, IComparable> comparer)
        {
            var arr = source as T[] ?? source.ToArray();
            if (arr.Length == 0) return default;

            var item = arr.First();
            var max = comparer(item);

            foreach (var it in arr)
            {
                if (comparer(it).CompareTo(max) > 0)
                {
                    item = it;
                    max = comparer(it);
                }
            }

            return item;
        }
        public static T ItemWithMin<T>(this IEnumerable<T> source, Func<T, IComparable> comparer)
        {
            var arr = source as T[] ?? source.ToArray();
            if (arr.Length == 0) return default;

            var item = arr.First();
            var max = comparer(item);

            foreach (var it in arr)
            {
                if (comparer(it).CompareTo(max) < 0)
                {
                    item = it;
                    max = comparer(it);
                }
            }

            return item;
        }
    }
}