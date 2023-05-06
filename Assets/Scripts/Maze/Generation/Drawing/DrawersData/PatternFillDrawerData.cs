using UnityEngine;

namespace Maze.Generation.Drawing.DrawersData
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Drawers/PatternFillDrawer")]

    public class PatternFillDrawerData: DrawerBaseData
    {
        [field: SerializeField] public TilemapDataProvider Pattern { get; private set; }
    }
}