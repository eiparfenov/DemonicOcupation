using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.Extensions;

namespace Maze.Generation.Separation
{
    [CreateAssetMenu(menuName = "DemonicOcupation/Maze/Generation/Separation/GatesCreationRules", fileName = nameof(GateCreationRules))]
    public class GateCreationRules: ScriptableObject
    {
        [Serializable]
        public class GateCreationRule
        {
            [field: SerializeField] public int MaxSize { get; private set; }
            [field: SerializeField] public int MaxGatesCount { get; private set; }
            [field: SerializeField] public int MinGatesCount { get; private set; }
            [field: SerializeField] public int GateSize { get; private set; }
        }
        [SerializeField] private GateCreationRule[] creationRules;

        public GateCreationRule CreationRuleForSize(int length)
        {
            return creationRules
                .Where(rule => length < rule.MaxSize)
                .ItemWithMin(rule => rule.MaxSize);
        }
    }
}