using System.Collections.Generic;
using Maze.Generation;
using UnityEngine;

namespace Utils.Extensions
{
    public static class CellExtensionPositionsInCell
    {
        public static IEnumerable<Vector2Int> AllCells(this Cell cell)
        {
            for (int x = cell.BottomLeft.x; x < cell.TopRight.x; x++)
            for (int y = cell.BottomLeft.y; y < cell.TopRight.y; y++)
            {
                yield return new Vector2Int(x, y);
            }
        }

        public static IEnumerable<Vector2Int> BorderCells(this Cell cell, int borderSize)
        {
            foreach (var pos in cell.AllCells())
            {
                if (cell.BottomLeft.x > pos.x - borderSize ||
                    cell.TopRight.x < pos.x + borderSize + 1 ||
                    cell.BottomLeft.y > pos.y - borderSize ||
                    cell.TopRight.y < pos.y + borderSize + 1)
                {
                    yield return pos;
                }
            }
        }
    }
}