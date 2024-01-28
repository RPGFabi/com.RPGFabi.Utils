using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFabi_Utils.Math
{
    public class Vectormath
    {
        public static Vector3 FloorVector(Vector3 a)
        {
            return new Vector3(
                Mathf.Floor(a.x),
                Mathf.Floor(a.y),
                Mathf.Floor(a.z)
                );
        }
        public static Vector3Int FloorVectorToInt(Vector3 a)
        {
            return new Vector3Int(
                Mathf.FloorToInt(a.x),
                Mathf.FloorToInt(a.y),
                Mathf.FloorToInt(a.z)
                );
        }
        public static Vector2 FloorVector(Vector2 a)
        {
            return new Vector2(
                Mathf.Floor(a.x),
                Mathf.Floor(a.y)
                );
        }
        public static Vector2Int FloorVectorToInt(Vector2 a)
        {
            return new Vector2Int(
                Mathf.FloorToInt(a.x),
                Mathf.FloorToInt(a.y)
                );
        }

    }
}
