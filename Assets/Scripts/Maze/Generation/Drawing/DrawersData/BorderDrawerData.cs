using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation.Drawing.DrawersData
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Drawers/BorderDrawer")]
    public class BorderDrawer: DrawerBaseData
    {
        [SerializeField] private TileBase borderTile;
        [SerializeField] private int borderSize;
        [SerializeField] private int layer;
        public override void Draw(Cell cell, Dictionary<int, Tilemap> tilemaps)
        {
            var tilemap = GetTilemap(tilemaps, layer);
            for (int x = 0; x < cell.Size.x; x++)
            for (int y = 0; y < borderSize; y++)
            {
                var tileCoords = cell.BottomLeft + new Vector2Int(x, y);
                tilemap.SetTile(tileCoords.ToVector3Int(), borderTile);
                tileCoords = cell.BottomLeft + new Vector2Int(x, cell.Size.y - y - 1);
                tilemap.SetTile(tileCoords.ToVector3Int(), borderTile);
            }
            for (int y = 0; y < cell.Size.y; y++)
            for (int x = 0; x < borderSize; x++)
            {
                var tileCoords = cell.BottomLeft + new Vector2Int(x, y);
                tilemap.SetTile(tileCoords.ToVector3Int(), borderTile);
                tileCoords = cell.BottomLeft + new Vector2Int(cell.Size.x - x - 1, y);
                tilemap.SetTile(tileCoords.ToVector3Int(), borderTile);
            }
        }
    }
}