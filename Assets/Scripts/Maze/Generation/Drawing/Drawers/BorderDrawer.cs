using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation.Drawing.Drawers
{
    [CreateAssetMenu(menuName = "DemonicOcupation/Maze/Drawers/BorderDrawer", fileName = "BorderDrawer")]
    public class BorderDrawer: BaseDrawer
    {
        [SerializeField] private int borderSize;
        [SerializeField] private TileBase borderTile;

        public override void Draw(Cell cell, Tilemap tilemap)
        {
            foreach (var pos in cell.BorderCells(borderSize))
            {
                tilemap.SetTile(pos.ToVector3Int(), borderTile);
            }
        }
    }
}