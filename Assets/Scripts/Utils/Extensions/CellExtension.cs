using System.Collections.Generic;
using System.Linq;
using Maze.Generation;
using Shared.Sides;

namespace Utils.Extensions
{
    public static class CellExtension
    {
        public static IEnumerable<Cell> AllFinishCells(this Cell cell)
        {
            if (cell.IsFinish)
            {
                yield return cell;
            }
            else
            {
                foreach (var subCell in cell.SubCells)
                {
                    foreach (var finishCell in subCell.AllFinishCells())
                    {
                        yield return finishCell;
                    }
                }
            }
        }

        public static void AddGatesToChildren(this Cell parent, Cell[] cells)
        {
            foreach (var cell in cells)
            {
                cell.Gates.AddRange(parent.Gates.Where(gate => 
                    cell.BottomLeft.x <= gate.BottomLeft.x &&
                    cell.BottomLeft.y <= gate.BottomLeft.y &&
                    gate.TopRight.x <= cell.TopRight.x &&
                    gate.TopRight.y <= cell.TopRight.y));
            }
        }

        public static IEnumerable<int> PossiblePosesForSeparation(this Cell cell, Side side, bool checkOpposite = false, int offset = 0)
        {
            var dir = side.CorrespondingDirection().Cross();
            var gatesToCheck = cell.Gates.Where(gate => gate.Side == side || (checkOpposite && gate.Side == side.Opposite())).ToArray();
            for (int i = dir.Magnitude(cell.BottomLeft) + offset; i < dir.Magnitude(cell.TopRight) - offset; i++)
            {
                if (gatesToCheck.All(gate => i < dir.Magnitude(gate.BottomLeft) || dir.Magnitude(gate.TopRight) < i))
                {
                    yield return i;
                }
            }
        }
    }   
}