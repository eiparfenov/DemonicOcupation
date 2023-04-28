using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Sides;
using UnityEngine;

namespace Maze
{
    public class Cell
    {
        public Vector2Int TopRight { get; private set; }
        public Vector2Int BottomLeft { get; private set; }
        public Vector2Int Size => TopRight - BottomLeft;
        public List<Gate> Gates { get; }
        public List<Cell> Children { get; }

        private Cell(Cell parent)
        {
            Children = new List<Cell>();
            if (parent != null)
            {
                Gates = parent.Gates
                    .Where(gate => gate.TopRight.x == TopRight.x ||
                                   gate.TopRight.y == TopRight.y ||
                                   gate.TopRight.x == BottomLeft.x ||
                                   gate.TopRight.y == BottomLeft.y || true)
                    .ToList();
                parent.Children.Add(this);
            }
            else
            {
                Gates = new List<Gate>();
            }
        }
        /// <summary>
        /// Finds all poses where there are no gates
        /// </summary>
        /// <param name="side">side to be separated</param>
        /// <param name="roomOffset"> distance from room wall to clothes separtion pos</param>
        /// <returns></returns>
        public List<int> PossibleSeparationPoses(Side side, int roomOffset)
        {
            if (side.CorrespondingDirection().MagnitudeCross(BottomLeft) + roomOffset >=
                side.CorrespondingDirection().MagnitudeCross(Size) - 2 * roomOffset) return new List<int>();
            
            
            return Enumerable.Range(side.CorrespondingDirection().MagnitudeCross(BottomLeft) + roomOffset,
                    side.CorrespondingDirection().MagnitudeCross(Size) - 2 * roomOffset)
                .Where(pos => !GatesOnSide(side)
                    .Any(gate => side.CorrespondingDirection().MagnitudeCross(gate.TopRight) > pos &&
                                 pos >= side.CorrespondingDirection().MagnitudeCross(gate.TopRight) + gate.Length))
                .ToList();
        }

        public static Cell FromCoords(Vector2Int bottomLeft, Vector2Int topRight, Cell parent)
        {
            var result = new Cell(parent)
            {
                BottomLeft = bottomLeft,
                TopRight = topRight
            };
            return result;
        }
        private List<Gate> GatesOnSide(Side side)
        {
            return side switch
            {
                Side.Left => Gates
                    .Where(gate => gate.Direction == Direction.Horizontal && 
                                   gate.TopRight.x == BottomLeft.x)
                    .ToList(),
                Side.Top => Gates
                    .Where(gate => gate.Direction == Direction.Vertical 
                                   && gate.TopRight.y == TopRight.y)
                    .ToList(),
                Side.Right => Gates
                    .Where(gate => gate.Direction == Direction.Horizontal &&
                                   gate.TopRight.x == TopRight.x)
                    .ToList(),
                Side.Bottom => Gates
                    .Where(gate => gate.Direction == Direction.Vertical &&
                                   gate.TopRight.y == BottomLeft.y)
                    .ToList(),
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
            
        }
    }
}