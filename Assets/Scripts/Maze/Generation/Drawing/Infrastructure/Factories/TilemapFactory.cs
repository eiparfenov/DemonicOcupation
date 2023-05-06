using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Maze.Generation.Drawing.Infrastructure.Factories
{
    public class TilemapFactory:IFactory<int, Tilemap>
    {
        public class PlaceholderFactory : PlaceholderFactory<int, Tilemap> { }

        private readonly DiContainer _container;
        private readonly GameObject _tilemapPref;
        private readonly Transform _parentForTilemaps;

        [Inject]
        public TilemapFactory(DiContainer container, GameObject tilemapPref, Transform parentForTilemaps)
        {
            _container = container;
            _tilemapPref = tilemapPref;
            _parentForTilemaps = parentForTilemaps;
        }
        
        public Tilemap Create(int sortingOrder)
        {
            var tilemap = _container.InstantiatePrefabForComponent<Tilemap>(_tilemapPref);
            var renderer = tilemap.GetComponent<TilemapRenderer>();
            tilemap.transform.SetParent(_parentForTilemaps);
            renderer.sortingOrder = sortingOrder;
            return tilemap;
        }
    }
}