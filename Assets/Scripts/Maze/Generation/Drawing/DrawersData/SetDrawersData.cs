using UnityEngine;

namespace Maze.Generation.Drawing.DrawersData
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Drawers/DrawerSet")]
    public class DrawersSet: DrawerBaseData
    {
        [field: SerializeField] public DrawerBaseData[] DrawersToApply { get; private set; }
        
    }
}