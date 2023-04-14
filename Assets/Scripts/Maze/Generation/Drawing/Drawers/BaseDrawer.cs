using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maze.Generation.Drawing.Drawers
{
    public abstract class BaseDrawer: ScriptableObject
    {
        public abstract void Draw(Cell cell, Tilemap tilemap);
    }
}