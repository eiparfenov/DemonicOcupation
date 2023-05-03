using System.Collections.Generic;
using System.Linq;
using Shared.Sides;
using UnityEngine;

namespace Maze.Generation.Separation
{
    public class CentralSquareSeparator: SeparatorBase
    {
        [SerializeField] private int minParentCellSize;
        [SerializeField] private int squareSide;
        public override List<Cell> Separate(Cell parentCell)
        {
            var topPoses = parentCell.PossibleSeparationPoses(Side.Top, minParentCellSize)
                .OrderBy(pos => Mathf.Abs(Direction.Horizontal.Magnitude(parentCell.Center) - pos));
            var leftPoses = parentCell.PossibleSeparationPoses(Side.Left, minParentCellSize)
                .OrderBy(pos => Mathf.Abs(Direction.Vertical.Magnitude(parentCell.Center) - pos));
            
            var rightPoses = parentCell.PossibleSeparationPoses(Side.Right, minParentCellSize);
            var bottomPoses = parentCell.PossibleSeparationPoses(Side.Bottom, minParentCellSize);
            var center = parentCell.Center - Vector2Int.one * squareSide / 2;
            var possibleBLPoses = leftPoses
                .SelectMany(x => topPoses
                    .Select(y => new Vector2Int(x, y)))
                .OrderBy(v => (center - v).magnitude);
            foreach (var blPose in possibleBLPoses)
            {
                
            }
                
            
            return null;
        }
    }
}