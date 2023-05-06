using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maze.Generation.Drawing.DrawersData
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Drawers/BorderDrawer")]
    public class BorderDrawerData: DrawerBaseData
    {
        [field: SerializeField] public TileBase BorderTile { get; private set; }
        [field: SerializeField] public int BorderSize { get; private set; }
        [field: SerializeField] public int Layer { get; private set; }
    }
}