using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Sides;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.Extensions;
using Random = UnityEngine.Random;

namespace Maze.Generation.Separation
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Separators/TwoSideSeparator", fileName = "TwoSidesSeparator")]
    public class TwoSidesSeparator: SeparatorBase
    {
        [SerializeField] private int minCellSize;
        [SerializeField] private float minCellPart;
        [SerializeField] private GatesGenerator gatesGenerator;
        public override List<Cell> Separate(Cell parentCell)
        {
            // Select direction
            var separationDirection = Random.value > (float)parentCell.Size.x / (float)(parentCell.Size.y + parentCell.Size.x) 
                ? Direction.Vertical
                : Direction.Horizontal;

            var minResultRoomSize = Mathf.Max(minCellSize,
                Mathf.RoundToInt(minCellPart * separationDirection.Magnitude(parentCell.Size)));

            // Choose poses to separate
            var separationPoses = separationDirection == Direction.Horizontal
                ? parentCell.PossibleSeparationPoses(Side.Bottom, minResultRoomSize)
                    .Intersect(parentCell.PossibleSeparationPoses(Side.Top, minResultRoomSize))
                    .ToList()
                : parentCell.PossibleSeparationPoses(Side.Left, minResultRoomSize)
                    .Intersect(parentCell.PossibleSeparationPoses(Side.Right, minResultRoomSize))
                    .ToList();
            
            // If no poses to separate try an other direction
            if (!separationPoses.Any())
            {
                separationDirection = separationDirection.Cross();
                separationPoses = separationDirection == Direction.Horizontal
                ? parentCell.PossibleSeparationPoses(Side.Left, minResultRoomSize)
                    .Intersect(parentCell.PossibleSeparationPoses(Side.Right, minResultRoomSize))
                    .ToList()
                : parentCell.PossibleSeparationPoses(Side.Top, minResultRoomSize)
                    .Intersect(parentCell.PossibleSeparationPoses(Side.Bottom, minResultRoomSize))
                    .ToList();
            }

            // If still no poses it is not separable
            if (!separationPoses.Any())
            {
                return null;
            }
            Debug.Log(separationPoses.Count);
            
            // Select separator
            var separator = separationPoses.RandomOrDefault();
            // The bottom left cell
            var cellBL = Cell.FromCoords(
                parentCell.BottomLeft, 
                separationDirection.ProjectCross(parentCell.TopRight) + separationDirection.Vector() * separator,
                parentCell);
            // The top right cell
            var cellTR = Cell.FromCoords(
                separationDirection.ProjectCross(parentCell.BottomLeft) + separationDirection.Vector() * separator,
                parentCell.TopRight,
                parentCell);
            gatesGenerator.AddGates(cellBL, cellTR, separationDirection);
            return new List<Cell>() { cellBL, cellTR };
        }
    }
}