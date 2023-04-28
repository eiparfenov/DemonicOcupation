using System;
using UnityEngine;

namespace Shared.Sides
{
    public static class DirectionExtension
    {
        public static Vector2Int Vector(this Direction direction)
        {
            return direction switch
            {
                Direction.Horizontal => Vector2Int.right,
                Direction.Vertical => Vector2Int.up,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Vector2Int VectorCross(this Direction direction)
        {
            return direction switch
            {
                Direction.Horizontal => Vector2Int.up,
                Direction.Vertical => Vector2Int.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static int Magnitude(this Direction direction, Vector2Int vector)
        {
            return direction switch
            {
                Direction.Horizontal => vector.x,
                Direction.Vertical => vector.y,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static int MagnitudeCross(this Direction direction, Vector2Int vector)
        {
            return direction switch
            {
                Direction.Horizontal => vector.y,
                Direction.Vertical => vector.x,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Vector2Int Project(this Direction direction, Vector2Int vector)
        {
            switch (direction)
            {
                case Direction.Horizontal:
                    vector.y = 0;
                    break;
                case Direction.Vertical:
                    vector.x = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            return vector;
        }

        public static Vector2Int ProjectCross(this Direction direction, Vector2Int vector)
        {
            switch (direction)
            {
                case Direction.Horizontal:
                    vector.x = 0;
                    break;
                case Direction.Vertical:
                    vector.y = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            return vector;
        }

        public static Direction Cross(this Direction direction)
        {
            return direction switch
            {
                Direction.Horizontal => Direction.Vertical,
                Direction.Vertical => Direction.Horizontal,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Side TopRightSide(this Direction direction)
        {
            return direction switch
            {
                Direction.Horizontal => Side.Right,
                Direction.Vertical => Side.Top,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        public static Side BottomLeftSide(this Direction direction)
        {
            return direction switch
            {
                Direction.Horizontal => Side.Left,
                Direction.Vertical => Side.Bottom,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}