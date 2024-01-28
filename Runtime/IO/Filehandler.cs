using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RPGFabi_Utils.IO
{
    public static class Filehandler
    {
        public static void ShowExplorer(string itemPath)
        {
            itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
            System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
        }

        public static void CopyFilesToPath(List<string> paths, string targetPath)
        {
            foreach (string path in paths)
            {
                // Copy File to target
                if (!File.Exists(path)) continue;
                File.Copy(path, targetPath);
            }
        }

    }
}