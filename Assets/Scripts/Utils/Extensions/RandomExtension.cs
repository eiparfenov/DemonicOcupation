using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.Extensions
{
    public static class RandomExtension
    {
        public static bool RandomBool(float probability = .5f) => Random.value < probability;

        public static List<int> RandomGates(int start, int end, int gateSize, int count)
        {
            var separationsLength = end - start - gateSize * count;
            var separations = RandomSeparations(separationsLength, count + 1);
            var result = new int[count];
            var pos = start;
            for (int i = 0; i < count; i++)
            {
                pos += separations[i];
                pos += gateSize;
                result[i] = pos;
            }

            return result.ToList();
        }

        public static List<int> RandomSeparations(int total, int count, float variation = .8f)
        {
            var result = new int[count];
            var max = (int)(total * (1 + variation) / count);
            var min = (int)(total * (1 - variation) / count);
            for (int i = 0; i < count; i++)
            {
                var remains = count - i - 1;
                result[i] = Mathf.RoundToInt(Mathf.Lerp(max, min, Random.value));
                result[i] = Mathf.Clamp(result[i], total - max * remains, total - min * remains);
                total -= result[i];
            }
            return result.ToList();
        }
    }
}