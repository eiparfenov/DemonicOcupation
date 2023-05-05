using System.Collections.Generic;
using Shared.Sides;
using UnityEngine;

namespace Maze
{
    public class Gate
    {
        public Vector2Int TopRight { get; set; }
        public Vector2Int BottomLeft => TopRight - Direction.VectorCross() * Length;
        public Vector2Int Center => TopRight - Direction.VectorCross() * Length / 2;
        public int Length { get; set; }
        public Stack<Cell> TopRightCells { get; set; }
        public Stack<Cell> BottomLeftCells { get; set; }
        /// <summary>
        /// The direction of gate to pass throw
        /// </summary>
        public Direction Direction { get; set; }

        public Gate()
        {
            TopRightCells = new Stack<Cell>();
            BottomLeftCells = new Stack<Cell>();
        }
    }
}