using Shared.Sides;
using UnityEngine;

namespace Maze.Generation.Separation.GatesGenerators
{
    public abstract class GatesGeneratorBase: ScriptableObject
    {
        public abstract void AddGates(Cell bottomLeft, Cell topRight, Direction dir);
    }
}