using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation.Drawing
{
    public class TilemapDataProvider: MonoBehaviour
    {
        [SerializeField] private List<TileInfo> tiles;
        [field: SerializeField] public Vector2Int Size { get; private set; }
        
        [Button("Bake")]
        private void ScanTiles()
        {
            tiles = new List<TileInfo>();
            var tilemapsData = GetComponentsInChildren<Tilemap>()
                .Select(tilemap => (tilemap, tilemap.GetComponent<TilemapRenderer>().sortingOrder));
            
            foreach ((Tilemap tilemap, int sortingOrder) tilemapData in tilemapsData)
            {
                var tilemap = tilemapData.tilemap;
                var layer = tilemapData.sortingOrder;
                tilemap.CompressBounds();
                var bounds = tilemap.cellBounds;
                for (int x = bounds.xMin; x < bounds.xMax; x++)
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    var cellPosition = new Vector3Int(x, y, 0);
                    var tile = tilemap.GetTile(cellPosition);
                    if(tile == null) continue;
                    tiles.Add(new TileInfo(tile, layer, (Vector2Int) cellPosition));
                }
            }

            Size = new Vector2Int(
                tiles.Max(ti => ti.Position.x) - tiles.Min(ti => ti.Position.x) + 1,
                tiles.Max(ti => ti.Position.y) - tiles.Min(ti => ti.Position.y) + 1
                );
        }

        /// <summary>
        /// Returns stored tiles
        /// </summary>
        /// <param name="rotation"> count of 90' rotations around clock</param>
        /// <returns></returns>
        public IEnumerable<TileInfo> GetTiles(int rotation = 0)
        {
            rotation = (rotation % 4 + 4) % 4;
            foreach (var tileInfo in tiles)
            {
                yield return rotation switch
                {
                    0 => new TileInfo(tileInfo.Tile, tileInfo.Layer, tileInfo.Position),
                    1 => new TileInfo(tileInfo.Tile, tileInfo.Layer,
                        new Vector2Int(tileInfo.Position.y, -tileInfo.Position.x) + Vector2Int.down),
                    2 => new TileInfo(tileInfo.Tile, tileInfo.Layer,
                        new Vector2Int(-tileInfo.Position.x, -tileInfo.Position.y) - Vector2Int.one),
                    3 => new TileInfo(tileInfo.Tile, tileInfo.Layer,
                        new Vector2Int(-tileInfo.Position.y, tileInfo.Position.x) + Vector2Int.left),
                    _ => throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null)
                };
            }
        }
    }

    [Serializable]
    public struct TileInfo
    {
        [field: SerializeField] public TileBase Tile { get; private set; }
        [field: SerializeField] public int Layer { get; private set; }
        [field: SerializeField] public Vector2Int Position { get; private set; }

        public TileInfo(TileBase tile, int layer, Vector2Int position)
        {
            Tile = tile;
            Layer = layer;
            Position = position;
        }
    }
}