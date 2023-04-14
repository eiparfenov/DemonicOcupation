using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation.Drawing.Drawers
{
    [CreateAssetMenu(menuName = "DemonicOcupation/Maze/Drawers/FillDrawer", fileName = "FillDrawer")]
    public class FillDrawer: BaseDrawer
    {
        [Tooltip("Tile to fill")]
        [SerializeField] private TileBase tile;
        
        public override void Draw(Cell cell, Tilemap tilemap)
        {
            foreach (var pos in cell.AllCells())
            {
                tilemap.SetTile(pos.ToVector3Int(), tile);
            }
        }
    }
}