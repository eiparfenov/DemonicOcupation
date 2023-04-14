using System;
using System.Diagnostics;
using Shared.Sides;
using UnityEngine;

namespace Utils.Extensions
{
    public static class SideExtension
    {
        public static Side Opposite(this Side side)
        {
            return side switch
            {
                Side.Left => Side.Right,
                Side.Up => Side.Bottom,
                Side.Right => Side.Left,
                Side.Bottom => Side.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }

        public static Vector2Int Direction(this Side side)
        {
            return side switch
            {
                Side.Left => Vector2Int.left,
                Side.Up => Vector2Int.up,
                Side.Right => Vector2Int.right,
                Side.Bottom => Vector2Int.down,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }

        public static Vector2Int InverseDirection(this Side side)
        {
            return -side.Direction();
        }

        public static int Correction(this Side side)
        {
            return side switch
            {
                Side.Left => 1,
                Side.Up => 0,
                Side.Right => 0,
                Side.Bottom => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }

        public static Direction CorrespondingDirection(this Side side)
        {
            return side switch
            {
                Side.Left => Shared.Sides.Direction.Horizontal,
                Side.Up => Shared.Sides.Direction.Vertical,
                Side.Right => Shared.Sides.Direction.Horizontal,
                Side.Bottom => Shared.Sides.Direction.Vertical,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }
    }
}