using System;
using System.Linq;
using UnityEngine;

namespace Maze.Generation.Separation
{
    [Serializable]
    public class SizingRules
    {
        [Serializable]
        public class SizingRule
        {
            [field: SerializeField] public SizingRuleType RuleType { get; private set; }
            [field: SerializeField] public int Value { get; private set; }

            public int Apply(int source)
            {
                return RuleType switch
                {
                    SizingRuleType.Tiles => Value,
                    SizingRuleType.PercentsOfRoom => Value * source / 100,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public enum SizingRuleType
        {
            Tiles, PercentsOfRoom
        }

        public enum SizingRulesType
        {
            Max, Min
        }

        [SerializeField] private SizingRulesType rulesType;
        [SerializeField] private SizingRule[] sizingRules;

        public int Apply(int source)
        {
            return rulesType switch
            {
                SizingRulesType.Max => sizingRules.Max(rule => rule.Apply(source)),
                SizingRulesType.Min => sizingRules.Min(rule => rule.Apply(source)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}