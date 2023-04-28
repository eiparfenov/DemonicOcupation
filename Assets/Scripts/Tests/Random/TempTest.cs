using System.Linq;
using Maze;
using NaughtyAttributes;
using Shared.Sides;
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
            var cell = Cell.FromCoords(new Vector2Int(-5, -5), new Vector2Int(10, 5), null);
            Debug.Log(string.Join(" ", cell.PossibleSeparationPoses(Side.Bottom,10)));
        }

        [Button()]
        private void Test2()
        {
            var res = RandomExtension.RandomSeparations(length, gatesCount);
            print(string.Join(", ", res));
        }
    }
}