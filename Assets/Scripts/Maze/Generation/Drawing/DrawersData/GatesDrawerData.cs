using UnityEngine;

namespace Maze.Generation.Drawing.DrawersData
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Drawers/GatesDrawer")]
    public class GatesDrawerData: DrawerBaseData
    {
        [field: SerializeField] public  GateDataProvider GateTiles { get; private set; }
    }
}