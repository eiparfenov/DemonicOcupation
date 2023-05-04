using Maze.Generation.Drawing;
using Maze.Generation.Separation;
using Maze.Generation.Separation.Separators;
using UnityEngine;

namespace Maze.Generation
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Settings/MazeGeneration")]
    public class MazeGeneratorSettings: ScriptableObject
    {
        [field: SerializeField] public int Size { get; private set; }
        [field: SerializeField] public SeparatorBase ToTest { get; private set; }
        [field: SerializeField] public SeparatorBase ToTest2 { get; private set; }
        [field: SerializeField] public DrawerBase DebugDrawer { get; private set; }
        [field: SerializeField] public DrawerBase DebugGatesDrawer { get; private set; }
        
    }
}