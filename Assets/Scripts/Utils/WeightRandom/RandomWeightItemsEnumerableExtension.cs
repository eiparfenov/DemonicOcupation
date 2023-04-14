using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.WeightRandom
{
    public static class RandomWeightItemsEnumerableExtension
    {
        public static T GetRandomByWeight<T>(this IEnumerable<T> source) where T : IRandomWeightItem
        {
            var arr = source.ToArray();
            var totalWeight = arr.Sum(x => x.Weight);
            var targetWeight = Random.Range(0, totalWeight);
            int i = 0;
            while (arr[i].Weight < targetWeight)
            {
                targetWeight -= arr[i].Weight;
                i++;
            }

            return arr[i];
        }
    }
}