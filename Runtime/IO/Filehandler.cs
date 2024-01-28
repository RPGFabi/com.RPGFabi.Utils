using System.Collections.Generic;
using System.IO;
using SFB;
using UnityEngine;

namespace RPGFabi_Utils.IO
{
    public static class Filehandler
    {
        public static void OpenPathInExplorer(string itemPath)
        {
            itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
            System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
        }


        #region FileBrowser
        public static string GetPathFromFileBrowser(bool save = false)
        {
            return StandaloneFileBrowser.SaveFilePanel(save?"Save File":"Open File", "", "", "");
        }

        public static string GetPathFromFileBrowser(bool save = false, ExtensionFilter[] filter)
        {
            return StandaloneFileBrowser.OpenFilePanel(save ? "Save File" : "Open File", "", filter, false)[0];
        }

        public static string GetPathForImages(bool save = false)
        {
            var extensions = new[] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg")
            };

            return GetPathFromFileBrowser(save, extensions);
        }

        public static string GetPathForTextFiles(bool save = false)
        {
            var extensions = new[] {
                new ExtensionFilter("Text File", "txt", "json")
            };

            return GetPathFromFileBrowser(save, extensions);
        }
        #endregion

        #region WorkingWithFiles
        public static void WriteFile(byte[] data, string path)
        {
            System.IO.File.WriteAllBytes(path, data);
        }
        public static void WriteFile(string data, string path)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
            writer.Write(data);
            writer.Close();
        }

        public static string ReadFileAsString(string path)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(path);
            string data = reader.ReadToEnd();
            reader.Close();
            return data;
        }

        public static byte[] ReadFileAsBytes(string path)
        {
            return System.IO.File.ReadAllBytes(path);
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
        #endregion

        #region Json-Handling
        public static void WriteJSON<T>(T data, string path)
        {
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            string json = JsonUtility.ToJson(data, true);
            WriteFile(json, path);
        }

        public static T ReadJSON<T>(string path)
        {
            string data = ReadFileAsString(path);
            T _data = JsonUtility.FromJson<T>(data);
            return _data;
        }

        #endregion

    }
}