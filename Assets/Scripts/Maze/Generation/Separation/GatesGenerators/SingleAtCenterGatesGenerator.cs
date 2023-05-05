using Shared.Sides;
using UnityEngine;

namespace Maze.Generation.Separation.GatesGenerators
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/GatesGenerators/SingleAtCenterGatesGenerator")]
    public class SingleAtCenterGatesGenerator: GatesGeneratorBase
    {
        [SerializeField] private int gateLenght;
        public override void AddGates(Cell bottomLeft, Cell topRight, Direction dir)
        {
            var start = Mathf.Max(dir.MagnitudeCross(bottomLeft.BottomLeft), dir.MagnitudeCross(topRight.BottomLeft));
            var end = Mathf.Min(dir.MagnitudeCross(bottomLeft.TopRight), dir.MagnitudeCross(topRight.TopRight));
            var tr = Mathf.RoundToInt((start + end + gateLenght) / 2f);
            var gate = new Gate()
            {
                TopRight = dir.VectorCross() * tr + dir.Project(bottomLeft.TopRight),
                Length = gateLenght,
                Direction = dir,
            };
            bottomLeft.Gates.Add(gate);
            topRight.Gates.Add(gate);
        }
    }
}