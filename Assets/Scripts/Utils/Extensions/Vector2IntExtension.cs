using UnityEngine;

namespace Utils.Extensions
{
    public static class Vector2IntExtension
    {
        public static Vector3Int ToVector3Int(this Vector2Int vector2Int)
        {
            return new Vector3Int(vector2Int.x, vector2Int.y, 0);
        }
    }
}