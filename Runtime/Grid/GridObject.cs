using System.Collections;
using UnityEngine;

namespace RPGFabi_Utils.Grid
{
    public abstract class GridObject : MonoBehaviour
    {
        Vector2Int[] gridIndexes;

        public void InitGridObject(Vector2Int[] indexes) => gridIndexes = indexes;
        public Vector2Int[] GetOccupiedIndexes() => gridIndexes;

        protected virtual void OnGridObjectDeleted() { }

        public void DeleteGridObject()
        {
            OnGridObjectDeleted();
            Destroy(gameObject);
        }
    }
}