using Maze.Generation.Drawing.Drawers;
using Maze.Generation.Separation;
using Maze.Generation.Separation.Separators;
using UnityEngine;

namespace Maze.Generation
{
    [CreateAssetMenu(fileName = "GeneratorSettings", menuName = "DemonicOcupation/Maze/GeneratorSettings", order = 0)]
    public class GeneratorSettings : ScriptableObject
    {
        [field: SerializeField] public Vector2Int InitialSize { get; private set; }
        [field: SerializeField] public BaseSeparator Separator { get; private set; }
        [field: SerializeField] public BaseDrawer[] DrawersToTest { get; private set; }
    }
}