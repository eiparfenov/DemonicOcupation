using System;
using NaughtyAttributes;
using Shared.Sides;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Extensions;
using Random = UnityEngine.Random;

namespace Maze.Generation.Separation.Separators
{
    [CreateAssetMenu(menuName = "DemonicOcupation/Maze/Generation/Separators/TwoParts", fileName = "TwoParts")]
    public class TwoPartsSeparator: BaseSeparator
    {
        [SerializeField] private GateCreationRules gateCreationRules;
        [SerializeField] private SizingRules minRoomSize;
        
        [SerializeField] private float horizontalSeparationProbability;
        [SerializeField] private float verticalSeparationProbability;
        [SerializeField] private float scaleProbabilityMultiplier;
        
        [Foldout("Tags")][SerializeField] private string leftTag;
        [Foldout("Tags")][SerializeField] private string rightTag;
        [Foldout("Tags")][SerializeField] private string topTag;
        [Foldout("Tags")][SerializeField] private string bottomTag;
        
        public override Cell[] Separate(Cell cell)
        {
            var hor = horizontalSeparationProbability + (float)cell.Size.x / (float)cell.Size.y * scaleProbabilityMultiplier;
            var ver = verticalSeparationProbability + (float)cell.Size.y / (float)cell.Size.x * scaleProbabilityMultiplier;
            hor = hor / (hor + ver);
            var direction = hor < Random.value ? Direction.Horizontal : Direction.Vertical;

            var separationPlace = cell.PossiblePosesForSeparation(
                direction.Cross().TopRightSide(), 
                true, 
                minRoomSize.Apply(direction.Magnitude(cell.Size)))
                .RandomOrDefault();
            var cells = new Cell[]
            {
                new Cell(cell.BottomLeft,
                    direction.Vector() * separationPlace + direction.ProjectCross(cell.TopRight),
                    cell,
                    direction switch
                    {
                        Direction.Horizontal => leftTag,
                        Direction.Vertical => bottomTag,
                        _ => throw new ArgumentOutOfRangeException()
                    }),
                new Cell(direction.Vector() * separationPlace + direction.ProjectCross(cell.BottomLeft),
                    cell.TopRight,
                    cell,
                    direction switch
                    {
                        Direction.Horizontal => rightTag,
                        Direction.Vertical => topTag,
                        _ => throw new ArgumentOutOfRangeException()
                    })
            };
            Gate.AddGates(cells[0], cells[1], direction, gateCreationRules);
            return cells;
        }
    }
}