using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maze.Generation.Drawing
{
    public abstract class DrawerBase: ScriptableObject
    {
        public abstract void Draw(Cell cell, Dictionary<int , Tilemap> tilemaps);

        protected Tilemap GetTilemap(Dictionary<int, Tilemap> tilemaps, int layer)
        {
            if (tilemaps.ContainsKey(layer)) return tilemaps[layer];
            
            Debug.unityLogger.LogError(this.GetType().Name, $"No tilemap on layer {layer}");
            return default;
        }
    }
}