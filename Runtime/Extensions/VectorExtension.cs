using System;
using UnityEngine;

namespace Spectra.Extensions
{
    public static class VectorExtension
    {
        //Vector2 => Vector3 & Vector4 : Vector2Int
        //Vector2Int => Vector2 : Vector3Int & Vector3
        //Vector3 => Vector2 & Vector4 : Vector3Int
        //Vector3Int => Vector3 : Vector2Int & Vector2
        //Vector4 => Vector2 & Vector3

        public static Vector2Int RoundToInt(this Vector2 vector) => Vector2Int.RoundToInt(vector);
        public static Vector2Int FloorToInt(this Vector2 vector) => Vector2Int.FloorToInt(vector);
        public static Vector2Int CeilToInt(this Vector2 vector) => Vector2Int.CeilToInt(vector);

        public static Vector3Int RoundToInt(this Vector3 vector) => Vector3Int.RoundToInt(vector);
        public static Vector3Int FloorToInt(this Vector3 vector) => Vector3Int.FloorToInt(vector);
        public static Vector3Int CeilToInt(this Vector3 vector) => Vector3Int.CeilToInt(vector);

        public static Vector2Int ToVector2Int(this Vector3Int vector) => new Vector2Int(vector.x, vector.y);
        public static Vector3Int ToVector3Int(this Vector2Int vector) => new Vector3Int(vector.x, vector.y, 0);

        public static Vector2 ToVector2(this Vector3Int vector) => new Vector2Int(vector.x, vector.y);
        public static Vector3 ToVector3(this Vector2Int vector) => new Vector3Int(vector.x, vector.y, 0);

        public static Vector2[] ToVector2(this Vector3[] vector) => Array.ConvertAll(vector, x => (Vector2)x);
        public static Vector3[] ToVector3(this Vector2[] vector) => Array.ConvertAll(vector, x => (Vector3)x);

        public static Vector2Int Abs(this Vector2Int vector) => new Vector2Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        public static Vector3Int Abs(this Vector3Int vector) => new Vector3Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));

        /// <summary> Same as new Vector2Int(-1, 1) </summary>
        public static Vector2Int TopLeft { get => new Vector2Int(-1, 1); }
        /// <summary> Same as new Vector2Int(1, 1) </summary>
        public static Vector2Int TopRight { get => new Vector2Int(1, 1); }
        /// <summary> Same as new Vector2Int(-1, -1) </summary>
        public static Vector2Int BottomLeft { get => new Vector2Int(-1, -1); }
        /// <summary> Same as new Vector2Int(1, -1) </summary>
        public static Vector2Int BottomRight { get => new Vector2Int(1, -1); }
    }
}