using Maze.Generation.Separation;
using Shared.Sides;
using UnityEngine;
using Utils.Extensions;

namespace Maze.Generation
{
    public class Gate
    {
        public Vector2Int BottomLeft { get; private set; }
        public Vector2Int TopRight { get; private set; }
        public Side Side { get; private set; }

        public Gate(Vector2Int bottomLeft, Vector2Int topRight, Side side)
        {
            BottomLeft = bottomLeft;
            TopRight = topRight;
            Side = side;
        }

        /// <summary>
        /// Creates gates between two cells
        /// </summary>
        /// <param name="cbl">bottom or left cell</param>
        /// <param name="ctr">top or right cell</param>
        /// <param name="direction">the pass direction</param>
        /// <param name="rules">rules</param>
        public static void AddGates(Cell cbl, Cell ctr, Direction direction, GateCreationRules rules)
        {
            var start = Mathf.Max(direction.MagnitudeCross(cbl.BottomLeft), direction.MagnitudeCross(ctr.BottomLeft));
            var end = Mathf.Min(direction.MagnitudeCross(cbl.TopRight), direction.MagnitudeCross(ctr.TopRight));
            var rule = rules.CreationRuleForSize(end - start);
            var gates = RandomExtension.RandomGates(start, end, rule.GateSize, rule.MinGatesCount, rule.MaxGatesCount);
            foreach (var gate in gates)
            {
                cbl.Gates.Add(new Gate(
                    gate.Item1 * direction.VectorCross() + direction.Project(cbl.TopRight),
                    gate.Item2 * direction.VectorCross() + direction.Project(cbl.TopRight),
                    direction.BottomLeftSide()
                    ));
                ctr.Gates.Add(new Gate(
                    gate.Item1 * direction.VectorCross() + direction.Project(cbl.TopRight),
                    gate.Item2 * direction.VectorCross() + direction.Project(cbl.TopRight),
                    direction.TopRightSide()
                    ));
            }
        }
    }
}