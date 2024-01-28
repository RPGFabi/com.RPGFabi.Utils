using RPGFabi_Utils.Math;
using UnityEngine;

namespace RPGFabi_Utils.World
{
    public static class Worldconversions
    {
        public static Vector2 GetScreenPositionFromWorldPosition(Vector3 position, Camera camera)
        {
            return camera.WorldToScreenPoint(position);
        }

        #region WorldToGrid
        public static Vector2Int GetGridIndexFromWorldPos_XY(Vector3 pos, Vector3 cellSize, bool offsetByHalfCell = false)
        {
            if(offsetByHalfCell)
            {
                pos -= cellSize / 2f;
            }

            return Vectormath.FloorVectorToInt(new Vector2(pos.x, pos.y));
        }
        public static Vector2Int GetGridIndexFromWorldPos_XZ(Vector3 pos, Vector3 cellSize, bool offsetByHalfCell = false)
        {
            if (offsetByHalfCell)
            {
                pos -= cellSize / 2f;
            }

            return Vectormath.FloorVectorToInt(new Vector2(pos.x, pos.z));
        }
        public static Vector2Int GetGridIndexFromWorldPos_YZ(Vector3 pos, Vector3 cellSize, bool offsetByHalfCell = false)
        {
            if (offsetByHalfCell)
            {
                pos -= cellSize / 2f;
            }

            return Vectormath.FloorVectorToInt(new Vector2(pos.y, pos.z));
        }
        #endregion

    }
}
