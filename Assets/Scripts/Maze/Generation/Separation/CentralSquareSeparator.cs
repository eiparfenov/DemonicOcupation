using System.Collections.Generic;
using System.Linq;
using Shared.Sides;
using UnityEngine;
using Utils.Extensions;

namespace Maze.Generation.Separation
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Separators/CentralSquareSeparator")]
    public class CentralSquareSeparator: SeparatorBase
    {
        [SerializeField] private int minParentCellSize;
        [SerializeField] private int squareSide;
        [SerializeField] private GatesGenerator centerGatesGenerator;
        [SerializeField] private GatesGenerator sideGatesGenerator;
        public override List<Cell> Separate(Cell parentCell)
        {
            var topPoses = parentCell.PossibleSeparationPoses(Side.Top, minParentCellSize);
            var leftPoses = parentCell.PossibleSeparationPoses(Side.Left, minParentCellSize);
            var rightPoses = parentCell.PossibleSeparationPoses(Side.Right, minParentCellSize);
            var bottomPoses = parentCell.PossibleSeparationPoses(Side.Bottom, minParentCellSize);
            var center = parentCell.Center - Vector2Int.one * squareSide / 2;

            // Filters poses, that are filled with gates
            var blPosesX = topPoses.Intersect(bottomPoses.Select(x => x - squareSide));
            var blPosesY = leftPoses.Intersect(rightPoses.Select(y => y - squareSide));

            
            var poseBL = new Vector2Int(blPosesX.ItemWithMin(x => Mathf.Abs(center.x - x)),
                blPosesY.ItemWithMin(y => Mathf.Abs(center.y - y)));
            var poseTR = poseBL + Vector2Int.one * squareSide;
            var parentTR = parentCell.TopRight;
            var parentBL = parentCell.BottomLeft;

            // Creates cells
            var centerCell = Cell.FromCoords(poseBL, poseTR, parentCell);
            var cellTR = Cell.FromSides(parentTR.y, parentTR.x, poseTR.y, poseBL.x, parentCell);
            var cellTL = Cell.FromSides(parentTR.y, poseBL.x, poseBL.y, parentBL.x, parentCell);
            var cellBL = Cell.FromSides(poseBL.y, poseTR.x, parentBL.y, parentBL.x, parentCell);
            var cellBR = Cell.FromSides(poseTR.y, parentTR.x, parentBL.y, poseTR.x, parentCell);

            // Adds gates between center
            centerGatesGenerator.AddGates(centerCell, cellTR, Direction.Vertical);
            centerGatesGenerator.AddGates(centerCell, cellBR, Direction.Horizontal);
            centerGatesGenerator.AddGates(cellTL, centerCell, Direction.Horizontal);
            centerGatesGenerator.AddGates(cellBL, centerCell, Direction.Vertical);
            
            // Adds gates to sides
            sideGatesGenerator.AddGates(cellTL, cellTR, Direction.Horizontal);
            sideGatesGenerator.AddGates(cellBL, cellBR, Direction.Horizontal);
            sideGatesGenerator.AddGates(cellBR, cellTR, Direction.Vertical);
            sideGatesGenerator.AddGates(cellBL, cellTL, Direction.Vertical);
            
            return new List<Cell>() { centerCell, cellTR, cellTL, cellBR, cellBL };
        }
    }
}