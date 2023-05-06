using System.Collections.Generic;
using Maze.Generation.Drawing.Infrastructure.Factories;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;
using Zenject;

namespace Maze.Generation.Drawing
{
    public class TilemapsForDrawing
    {
        private readonly Dictionary<int, Tilemap> _tilemaps;
        private readonly TilemapFactory.PlaceholderFactory _tilemapFactory;

        [Inject]
        public TilemapsForDrawing(TilemapFactory.PlaceholderFactory tilemapFactory)
        {
            _tilemaps = new Dictionary<int, Tilemap>();
            _tilemapFactory = tilemapFactory;
        }

        public void SetTile(TileBase tile, Vector2Int position, int layer)
        {
            if (!_tilemaps.ContainsKey(layer)) _tilemaps.Add(layer, _tilemapFactory.Create(layer));
            
            _tilemaps[layer].SetTile(position.ToVector3Int(), tile);
        }

        public void ClearTile(Vector2Int position)
        {
            foreach (var tilemapsValue in _tilemaps.Values)
            {
                tilemapsValue.SetTile(position.ToVector3Int(), null);
            }
        }
    }
}