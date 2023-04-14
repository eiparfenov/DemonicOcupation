using UnityEngine;

namespace Maze.Generation.Separation.Separators
{
    public abstract class BaseSeparator: ScriptableObject
    {
        public abstract Cell[] Separate(Cell cell);
    }
}