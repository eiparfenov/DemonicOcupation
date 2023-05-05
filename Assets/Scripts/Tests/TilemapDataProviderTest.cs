using Maze.Generation.Drawing;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tests
{
    public class TilemapDataProviderTest: MonoBehaviour
    {
        [SerializeField] private TilemapDataProvider tiles;
        [SerializeField] private int rotation;

        [Button()]
        private void Draw()
        {
            var tilemap = GetComponent<Tilemap>();
            foreach (var tileInfo in tiles.GetTiles(rotation))
            {
                tilemap.SetTile(tileInfo.Position, tileInfo.Tile);
            }
        }

        [Button()]
        private void Clear()
        {
            GetComponent<Tilemap>().ClearAllTiles();
        }
    }
}