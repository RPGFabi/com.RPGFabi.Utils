using B83.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Files
{
    public class DragandDropHandler : MonoBehaviour
    {
        public static DragandDropHandler instance;

        public delegate void FilesDropped(List<string> paths, Vector2 dropPosition);
        public static event FilesDropped OnFilesDropped;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            UnityDragAndDropHook.InstallHook();
            UnityDragAndDropHook.OnDroppedFiles += HandleDroppedFiles;
            Debug.developerConsoleVisible = true;
        }


        private void OnDisable()
        {
            UnityDragAndDropHook.OnDroppedFiles -= HandleDroppedFiles;
        }



        private void HandleDroppedFiles(List<string> aPathNames, POINT aDropPoint)
        {
            // Only run when Event can be passed on
            if (OnFilesDropped != null)
            {
                string files = $"Dropped {aPathNames.Count} Files at {aDropPoint.x}:{aDropPoint.y}.\n";

                foreach (string file in aPathNames)
                {
                    files += $"\n- {file}";
                }

                Debug.LogError(files);
                OnFilesDropped.Invoke(aPathNames, new Vector2(aDropPoint.x, aDropPoint.y));
            }
        }


    }
}