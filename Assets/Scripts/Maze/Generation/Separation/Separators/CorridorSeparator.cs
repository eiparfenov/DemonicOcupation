using System.Collections.Generic;
using System.Linq;
using Maze.Generation.Separation.GatesGenerators;
using Shared.Sides;
using UnityEngine;
using Utils.Extensions;

namespace Maze.Generation.Separation.Separators
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Separators/CorridorSeparator")]
    public class CorridorSeparator: SeparatorBase
    {
        [SerializeField] private int corridorAdditionalWidth;
        [SerializeField] private int minSideLength;
        [SerializeField] private GatesGeneratorBase gatesGenerator;
        public override List<Cell> Separate(Cell parentCell)
        {
            // Finds all gates
            var possibleCorridors = parentCell.Gates
                .Select(gate => (
                    gate.Direction.MagnitudeCross(gate.BottomLeft) - corridorAdditionalWidth,
                    gate.Direction.MagnitudeCross(gate.TopRight) + corridorAdditionalWidth,
                    gate.Direction)
                ).ToList();
            // Checks gates opposite
            possibleCorridors = possibleCorridors
                .Where(corridor => !parentCell.Gates
                    .Any(gate =>
                        {
                            if (gate.Direction != corridor.Direction) return false;

                            var corridorHigh = corridor.Item2;
                            var corridorLow = corridor.Item1;
                            var gateLow = gate.Direction.MagnitudeCross(gate.BottomLeft);
                            var gateHigh = gate.Direction.MagnitudeCross(gate.TopRight);
                            var result = gateLow < corridorHigh && corridorHigh < gateHigh ||
                                   gateLow < corridorLow && corridorLow < gateHigh;
                            return result;
                        }
                    )
                )
                .ToList();
            
            if (!possibleCorridors.Any()) return default;
            
            // Selects closest to center corridor
            var cellCenter = parentCell.Center;
            var corridorInfo = possibleCorridors.ItemWithMin(corridor => Mathf.Abs((corridor.Item1 + corridor.Item2) / 2 - corridor.Direction.MagnitudeCross(cellCenter)));
            
            // The direction of corridor
            var direction = corridorInfo.Direction;
            
            // Creates corridor cell
            var corridorCell = Cell.FromCoords(
                direction.Project(parentCell.BottomLeft) + direction.VectorCross() * corridorInfo.Item1,
                direction.Project(parentCell.TopRight) + direction.VectorCross() * corridorInfo.Item2,
                parentCell);

            // Creates side cells
            var blCells = SeparateAlongCorridor(parentCell.BottomLeft,
                direction.Project(parentCell.TopRight) + direction.ProjectCross(corridorCell.BottomLeft),
                direction, parentCell, false, corridorCell);
            var trCells = SeparateAlongCorridor(
                direction.Project(parentCell.BottomLeft) + direction.ProjectCross(corridorCell.TopRight),
                parentCell.TopRight,
                direction, parentCell, true, corridorCell);

            var result = new List<Cell>() { corridorCell };
            result.AddRange(blCells);
            result.AddRange(trCells);
            return result;
        }

        private List<Cell> SeparateAlongCorridor(Vector2Int bottomLeft, Vector2Int topRight, Direction direction, Cell parentCell, bool up, Cell corridor)
        {
            var size = topRight - bottomLeft;
            var roomsCount = Mathf.RoundToInt((float)direction.Magnitude(size) / direction.MagnitudeCross(size));
            var roomLength = (float)direction.Magnitude(size) / roomsCount;

            var possiblePoses =
                parentCell.PossibleSeparationPoses(up ? direction.Cross().TopRightSide() : direction.Cross().BottomLeftSide(), 0);
            var result = new List<Cell>();
            for (int i = 0; i < roomsCount; i++)
            {
                var sepLow = (direction.Magnitude(bottomLeft) + Mathf.RoundToInt(i * roomLength));
                var sepHigh = (direction.Magnitude(bottomLeft) + Mathf.RoundToInt((i + 1) * roomLength));

                sepLow = possiblePoses.ItemWithMin(x => Mathf.Abs(sepLow - x));
                sepHigh = possiblePoses.ItemWithMin(x => Mathf.Abs(sepHigh - x));
                if (i + 1 == roomsCount) sepHigh = direction.Magnitude(parentCell.TopRight);
                
                var cell =Cell.FromCoords(
                    direction.ProjectCross(bottomLeft) + direction.Vector() * sepLow,
                    direction.ProjectCross(topRight) + direction.Vector() * sepHigh,
                    parentCell);

                if (up)
                {
                    gatesGenerator.AddGates(corridor, cell, direction.Cross());
                }
                else
                {
                    gatesGenerator.AddGates(cell, corridor, direction.Cross());
                }
                result.Add(cell);
            }

            return result;
        }
    }
}