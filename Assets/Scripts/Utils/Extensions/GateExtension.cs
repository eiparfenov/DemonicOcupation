using System.Collections.Generic;
using Maze.Generation;
using Maze.Generation.Separation;
using UnityEngine;

namespace Utils.Extensions
{
    public static class GateExtension
    {
        public static IEnumerable<Vector2Int> Border(this Gate gate, int length, int side)
        {
            for (int i = 0; i < length; i++)
            {
                yield return gate.BottomLeft + 
                             gate.Side.InverseDirection() * (-i - gate.Side.Correction()) + 
                             gate.Side.CorrespondingDirection().VectorCross() * side;
                yield return gate.TopRight + 
                             gate.Side.InverseDirection() * (-i - gate.Side.Correction()) - 
                             gate.Side.CorrespondingDirection().VectorCross() * (side + 1);
            }
        }
    }
}