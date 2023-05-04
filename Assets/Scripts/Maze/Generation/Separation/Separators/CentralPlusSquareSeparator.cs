using System.Collections.Generic;
using System.Linq;
using Maze.Generation.Separation.GatesGenerators;
using Shared.Sides;
using UnityEngine;
using Utils.Extensions;

namespace Maze.Generation.Separation.Separators
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Separators/CentralPlusSquareSeparator")]
    public class CentralPlusSquareSeparator: SeparatorBase
    {
        [SerializeField] private int minParentCellSize;
        [SerializeField] private int squareSide;
        [SerializeField] private GatesGeneratorBase centerGatesGenerator;
        [SerializeField] private GatesGeneratorBase sideGatesGenerator;

        public override List<Cell> Separate(Cell parentCell)
        {
            var topPoses = parentCell.PossibleSeparationPoses(Side.Top, minParentCellSize);
            var leftPoses = parentCell.PossibleSeparationPoses(Side.Left, minParentCellSize);
            var rightPoses = parentCell.PossibleSeparationPoses(Side.Right, minParentCellSize);
            var bottomPoses = parentCell.PossibleSeparationPoses(Side.Bottom, minParentCellSize);
            var center = parentCell.Center - Vector2Int.one * squareSide / 2;

            // Filters poses, that are filled with gates
            var blPosesX = topPoses.Intersect(bottomPoses).ToList();
            var blPosesY = leftPoses.Intersect(rightPoses).ToList();

            blPosesX = blPosesX.Intersect(blPosesX.Select(x => x - squareSide)).ToList();
            blPosesY = blPosesY.Intersect(blPosesY.Select(y => y - squareSide)).ToList();


            var poseBL = new Vector2Int(blPosesX.ItemWithMin(x => Mathf.Abs(center.x - x)),
                blPosesY.ItemWithMin(y => Mathf.Abs(center.y - y)));
            var poseTR = poseBL + Vector2Int.one * squareSide;
            var parentTR = parentCell.TopRight;
            var parentBL = parentCell.BottomLeft;

            // Creates cells (look at numpad)
            var cell1 = Cell.FromSides(poseBL.x, poseBL.y, parentBL.x, parentBL.y, parentCell);
            var cell2 = Cell.FromSides(poseBL.x, poseTR.y, parentBL.x, poseBL.y, parentCell);
            var cell3 = Cell.FromSides(poseBL.x, parentTR.y, parentBL.x, poseTR.y, parentCell);
            var cell4 = Cell.FromSides(poseTR.x, poseBL.y, poseBL.x, parentBL.y, parentCell);
            var cell5 = Cell.FromSides(poseTR.x, poseTR.y, poseBL.x, poseBL.y, parentCell);
            var cell6 = Cell.FromSides(poseTR.x, parentTR.y, poseBL.x, poseTR.y, parentCell);
            var cell7 = Cell.FromSides(parentTR.x, poseBL.y, poseTR.x, parentBL.y, parentCell);
            var cell8 = Cell.FromSides(parentTR.x, poseTR.y, poseTR.x, poseBL.y, parentCell);
            var cell9 = Cell.FromSides(parentTR.x, parentTR.y, poseTR.x, poseTR.y, parentCell);

            // Adds gates between center
            centerGatesGenerator?.AddGates(cell4, cell5, Direction.Horizontal);
            centerGatesGenerator?.AddGates(cell2, cell5, Direction.Vertical);
            centerGatesGenerator?.AddGates(cell5, cell8, Direction.Vertical);
            centerGatesGenerator?.AddGates(cell5, cell6, Direction.Horizontal);

            // Adds gates to sides
            sideGatesGenerator?.AddGates(cell1 ,cell2, Direction.Horizontal);
            sideGatesGenerator?.AddGates(cell2, cell3, Direction.Horizontal);
            sideGatesGenerator?.AddGates(cell7, cell8, Direction.Horizontal);
            sideGatesGenerator?.AddGates(cell8, cell9, Direction.Horizontal);
            sideGatesGenerator?.AddGates(cell1 ,cell4, Direction.Vertical);
            sideGatesGenerator?.AddGates(cell4, cell7, Direction.Vertical);
            sideGatesGenerator?.AddGates(cell3, cell6, Direction.Vertical);
            sideGatesGenerator?.AddGates(cell6, cell9, Direction.Vertical);

            return new List<Cell>()
            {
                cell1,
                cell2,
                cell3,
                cell4,
                cell5,
                cell6,
                cell7,
                cell8,
                cell9
            };
        }
    }
}