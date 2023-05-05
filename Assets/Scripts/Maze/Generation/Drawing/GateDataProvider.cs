using UnityEngine;

namespace Maze.Generation.Drawing
{
    public class GateDataProvider: TilemapDataProvider
    {
        [SerializeField] private int gateSize;
        public int GateSize => gateSize;
    }
}