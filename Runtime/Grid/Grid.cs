using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGFabi_Utils.World;

namespace RPGFabi_Utils.Grid
{
    public class GridXY<_GridObject> where _GridObject : GridObject
    {
        [SerializeField] bool showDebug = false;

        public float cellSize { get; private set; }
        public Vector3 origin { get; private set; }
        Dictionary<Vector2Int, _GridObject> grid = new Dictionary<Vector2Int, _GridObject>();

        int displayTextSize = 20;
        Dictionary<Vector2Int, TextMesh> debugTexts = new Dictionary<Vector2Int, TextMesh>();


        public GridXY(float _cellSize, Vector3 _origin, Dictionary<Vector2Int,_GridObject> defaultPositions = null)
        {
            float positiveCellSize = Mathf.Abs(_cellSize);
            if(positiveCellSize > 0)
            {
                cellSize = positiveCellSize;
            }
            else
            {
                cellSize = 1f;
            }

            origin = _origin;

            // Fill grid
            if(defaultPositions != null)
            {
                foreach (var pos in defaultPositions)
                {
                    AddCellToGrid(pos.Key, pos.Value);
                }
            }

        }

        public void RemoveGridObject(Vector2Int index)
        {
            _GridObject obj = GetCellObject(index);

            if (obj == null) return;

            // Remove from all Cells
            foreach (Vector2Int obj_Index in obj.GetOccupiedIndexes())
            {
                SetCellObject(obj_Index, null);
            }

            // Handle Removal from OBJ
            obj.DeleteGridObject();
        }

        public int GetCellCount() => grid.Count;

        #region Debugging

        public void SetDebug(bool b)
        {
            showDebug = b;
        }

        public void SetDebugTextSize(int i)
        {
            displayTextSize = i;
        }
        void Debug_AddCellToDisplay(Vector2Int pos)
        {
            Vector3 center = GetWorldPositionFromCell(pos);
            debugTexts.Add(
                pos,
                WorldText.CreateWorldText(
                    pos.ToString(),
                    center,
                    Quaternion.Euler(90, 0, 0),
                    WorldText.GetDefaultFont(),
                    displayTextSize,
                    Color.white,
                    TextAnchor.MiddleCenter,
                    TextAlignment.Center
                    )
                );

            // Add Surroundings => Check if cell already drawn so no line needed
            float halfCell = cellSize / 2f;
            if (!grid.ContainsKey(pos + Vector2Int.left))
            {
                Debug.DrawLine(
                    new Vector3(center.x - halfCell, center.y, center.z - halfCell),
                    new Vector3(center.x - halfCell, center.y, center.z + halfCell),
                    Color.white,
                    100f
                );
            }
            if (!grid.ContainsKey(pos + Vector2Int.right))
            {
                Debug.DrawLine(
                    new Vector3(center.x + halfCell, center.y, center.z - halfCell),
                    new Vector3(center.x + halfCell, center.y, center.z + halfCell),
                    Color.white,
                    100f
                );
            }
            if (!grid.ContainsKey(pos + Vector2Int.up))
            {
                Debug.DrawLine(
                    new Vector3(center.x - halfCell, center.y, center.z + halfCell),
                    new Vector3(center.x + halfCell, center.y, center.z + halfCell),
                    Color.white,
                    100f
                );
            }
            if (!grid.ContainsKey(pos + Vector2Int.down))
            {
                Debug.DrawLine(
                    new Vector3(center.x - halfCell, center.y, center.z - halfCell),
                    new Vector3(center.x + halfCell, center.y, center.z - halfCell),
                    Color.white,
                    100f
                );
            }
        }
        #endregion

        #region CellHandling
        public _GridObject[] GetAllGridObjects(){
            List<_GridObject> objects = new List<_GridObject>();

            foreach (var index in grid)
            {
                if(!objects.Contains(index.Value)){
                    objects.Add(index.Value);
                }
            }
            return objects.ToArray();

        }

        public void AddCellToGrid(Vector2Int pos, _GridObject obj)
        {
            if(grid.ContainsKey(pos))
            {
                Debug.LogWarning($"Grid already contains Cell at: {pos.x}:{pos.y}");
                return;
            }

            grid.Add(pos, obj);
            if(showDebug)
            {
                Debug_AddCellToDisplay(pos);
            }
        }

        public void RemoveCellFromGrid(Vector2Int[] indexes)
        {
            for (int i = 0; i < indexes.Length; i++)
            {
                RemoveCellFromGrid(indexes[i]);
            }
        }

        public void RemoveCellFromGrid(Vector2Int index)
        {
            if (!grid.ContainsKey(index))
            {
                Debug.LogWarning($"Grid doesn't contain a Cell at: {index.x}:{index.y}");
                return;
            }
            else
            {
                RemoveGridObject(index);
                grid.Remove(index);
            }
        }

        public bool CheckIfCellExists(Vector2Int index)
        {
            if (grid.ContainsKey(index))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region World<->Index
        public Vector3 GetWorldPositionFromCell(Vector2Int index)
        {
            return origin + new Vector3(index.x,0,index.y)*cellSize;
        }

        public Vector2Int GetCellIndexFromWorld(Vector3 pos)
        {
            return new Vector2Int(
                Mathf.RoundToInt((pos.x - origin.x) / cellSize),
                Mathf.RoundToInt((pos.z - origin.z) / cellSize)
            );
        }

        #endregion

        #region CellObject
        public _GridObject GetCellObject(Vector2Int index)
        {
            if(grid.ContainsKey(index))
            {
                return grid[index];
            }
            return null;
        }        

        /// <summary>
        /// Tries to place the gridobject in the cell - returns true if success
        /// </summary>
        /// <param name="index"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool SetCellObject(Vector2Int index, _GridObject obj)
        {
            if(grid.ContainsKey(index))
            {
                grid[index] = obj;
                return true;
            }
            return false;
        }

        public void RemoveCellObject(Vector2Int objIndex)
        {
            if(grid.ContainsKey(objIndex))
            {
                foreach (Vector2Int index in grid[objIndex].GetOccupiedIndexes())
                {
                    grid[index] = null;
                }
                grid[objIndex].DeleteGridObject();
            }
        }
        #endregion
    }
}
