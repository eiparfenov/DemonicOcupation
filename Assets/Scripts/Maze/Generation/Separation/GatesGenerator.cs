using System;
using System.Linq;
using Shared.Sides;
using UnityEngine;
using Utils.Extensions;
using Utils.WeightRandom;

namespace Maze.Generation.Separation
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Separators/GatesGenerator", fileName = "TwoSidesSeparator")]
    public class GatesGenerator: ScriptableObject
    {
        [SerializeField] private GateGenerationRule[] generationRules;

        public void AddGates(Cell bottomLeft, Cell topRight, Direction dir)
        {
            var start = Mathf.Max(dir.MagnitudeCross(bottomLeft.BottomLeft), dir.MagnitudeCross(topRight.BottomLeft));
            var end = Mathf.Min(dir.MagnitudeCross(bottomLeft.TopRight), dir.MagnitudeCross(topRight.TopRight));
            var gateInfo = GetGatesInfo(end - start);
            var gatesPoses = RandomExtension.RandomGates(start, end, gateInfo.lenght, gateInfo.gatesCount);
            foreach (var gatesPose in gatesPoses)
            {
                var gate = new Gate
                {
                    Length = gateInfo.lenght,
                    TopRight = dir.Project(bottomLeft.TopRight) + dir.VectorCross() * gatesPose,
                    Direction = dir
                };
                bottomLeft.Gates.Add(gate);
                topRight.Gates.Add(gate);
            }
        }

        private (int lenght, int gatesCount) GetGatesInfo(int sideLength)
        {
            var generationRule = generationRules
                .Where(rule => rule.GateLength > sideLength)
                .ItemWithMin(rule => rule.GateLength);
            if (generationRule == null)
            {
                generationRule = generationRules.ItemWithMax(rule => rule.MaxSize);
            }

            var gateLength = generationRule.GateLength;
            var gatesCount = generationRule.GateRules.GetRandomByWeight().GatesCount;
            return (gateLength, gatesCount);
        }
    }

    [Serializable]
    public class GateGenerationRule
    {
        [field: SerializeField] public int MaxSize { get; private set; }
        [field: SerializeField] public int GateLength { get; private set; }
        [field: SerializeField] public GateGenerationRulePart[] GateRules { get; private set; }
    }

    [Serializable]
    public class GateGenerationRulePart: IRandomWeightItem
    {
        [field: SerializeField] public int GatesCount { get; private set; }
        [field: SerializeField] public float Weight { get; private set; }
    }
}