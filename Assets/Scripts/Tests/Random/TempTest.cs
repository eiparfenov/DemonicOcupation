using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using Utils;
using Utils.Extensions;

namespace Tests.Random
{
    public class TempTest: MonoBehaviour
    {
        [SerializeField] private int length;
        [SerializeField] private int gatesCount;
        [SerializeField] private int gatesSize;

        [Button()]
        private void Test()
        {
            var gates = RandomExtension.RandomGates(0, length, gatesSize, gatesCount, gatesCount);
            print(string.Join(", ", gates.Select(x=> $"({x.Item1}, {x.Item2})")));
            var s = "";
            for (int i = 0; i < length; i++)
            {
                if (gates.Any(x => x.Item1 < i && i + 1 < x.Item2)) s += "_";
                else if (gates.Any(x => x.Item1 == i)) s += "\\";
                else if (gates.Any(x => x.Item2 - 1 == i)) s += "/";
                else s += "*";
            }
            print(s);
        }

        [Button()]
        private void Test2()
        {
            var res = RandomExtension.RandomSeparations(length, gatesCount);
            print(string.Join(", ", res));
        }
    }
}