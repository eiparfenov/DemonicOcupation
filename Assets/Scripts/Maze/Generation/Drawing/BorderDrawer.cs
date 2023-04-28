using System.Collections.Generic;
using Maze.Generation.Separation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation.Drawing
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Generation/Drawers/BorderDrawer")]
    public class BorderDrawer: DrawerBase
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