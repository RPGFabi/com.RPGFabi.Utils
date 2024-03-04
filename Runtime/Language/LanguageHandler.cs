using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RPGFabi_Utils.Language
{
    public class LanguageHandler : MonoBehaviour
    {
        const string playerPrefsIndex = "lastSavedLanguageIndex";
        public static LanguageHandler instance;
        
        // Events
        public delegate void OnLanguageChanged();
        public static event OnLanguageChanged languageChanged;

        // Variables
        readonly string languageFilePath = $"{Application.streamingAssetsPath}/languages/translation.csv";
        readonly string languageImagePath = $"{Application.streamingAssetsPath}/languages";

        public LanguageDatabase _languages = new LanguageDatabase();
        public static int currentLanguageIndex = 0;


        public Texture2D[] GetLanguageImages()
        {
            List<Texture2D> languageImages = new List<Texture2D>();

            if (_languages == null) return null;

            for (int i = 0; i < _languages.GetCount(); i++)
            {
                Texture2D image = new Texture2D(2, 2);
                string path = GenerateImagePath(i);

                if (!File.Exists(path))
                {
                    languageImages.Add(image);
                    continue;
                }

                byte[] bytes = File.ReadAllBytes(path);
                image.LoadImage(bytes);

                languageImages.Add(image);
            }
            return languageImages.ToArray();
        }

        private string GenerateImagePath(int languageIndex)
        {
            return $"{languageImagePath}/{_languages.languages[languageIndex].languageName}.png";
        }

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
            DontDestroyOnLoad(gameObject);

            if (File.Exists(languageFilePath))
            {
                // Load Languages from File
                LoadLanguageFile();

                if (_languages.GetCount() > 0)
                {
                    // Load last Saved LNG
                    if (PlayerPrefs.HasKey(playerPrefsIndex))
                    {
                        currentLanguageIndex = PlayerPrefs.GetInt(playerPrefsIndex);
                        SetLanguage(currentLanguageIndex);
                    }
                    else
                    {
                        SetLanguage(0);
                    }
                }
            }
        }


        void LoadLanguageFile()
        {
            // Update
            StreamReader reader = new StreamReader(languageFilePath);
            string data = reader.ReadToEnd();
            reader.Close();

            List<string>[] columns = new List<string>[0];

            string[] rows = data.Split("\n");

            // Get Version
            _languages.version.version = int.Parse(rows[0].Split(";")[1]);


            int maxColumns = int.MinValue;

            for (int rowIndex = 1; rowIndex < rows.Length; rowIndex++)
            {
                string[] cells = rows[rowIndex].Split(";");

                if (rowIndex == 1)
                {
                    maxColumns = cells.Length;
                    columns = new List<string>[maxColumns];

                    // Fill Array
                    for (int arrayIndex = 0; arrayIndex < maxColumns; arrayIndex++)
                    {
                        columns[arrayIndex] = new List<string>();
                    }
                }

                // Add Cell to corresponding collumn
                for (int cellIndex = 0; cellIndex < cells.Length; cellIndex++)
                {
                    if (cellIndex >= maxColumns) break; // No corresponding language in this column

                    string singleCell = cells[cellIndex];

                    columns[cellIndex].Add(singleCell);
                }
            }


            List<Language> temp = new List<Language>();
            columns[0].RemoveAt(0); // Remove "languageName"
            //columns[0].RemoveRange(0,2);  // Remove "languageName" and "imageToken" from Keys
            _languages.translationkeys = columns[0].ToArray();

            for (int i = 1; i < columns.Length; i++)
            {
                temp.Add(new Language(columns[i].ToArray()));
            }

            _languages.languages = temp.ToArray();
        }

        public void SetLanguage(int index){
            if (index >= _languages.GetCount()) return; 

            currentLanguageIndex = index;

            if (languageChanged != null){
                languageChanged.Invoke();
            }

            // Store in PlayerPrefs
            PlayerPrefs.SetInt(playerPrefsIndex, currentLanguageIndex);
            PlayerPrefs.Save();
        }

        public string GetTranslationByKey(string key)
        {
            if (_languages == null || _languages.GetCount() == 0) return key;

            int keyIndex = Array.IndexOf(_languages.translationkeys, key);

            if (keyIndex > -1)
            {
                return _languages.languages[currentLanguageIndex].GetValueByIndex(keyIndex);
            }

            return key;
        }

        public string GetTranslationByKey(string key, object[] variables)
        {
            string text = GetTranslationByKey(key);

            int index = 0;
            while (variables.Length >= index + 1 && text.Contains("{" + index + "}"))
            {
                text = text.Replace("{" + index + "}", variables[index].ToString());
                index++;
            }
            return text;
        }

        public string GetTranslationByKeyAndLanguageIndex(int languageIndex, string key)
        {
            if (_languages == null || _languages.GetCount() <= languageIndex) return key;

            int keyIndex = Array.IndexOf(_languages.translationkeys, key);

            if(languageIndex > -1)
            {
                return _languages.languages[languageIndex].GetValueByIndex(keyIndex);
            }
            return key;
        }

        public bool CheckIfEventHandlerExists(Delegate newHandler)
        {
            if(languageChanged != null)
            {
                foreach(Delegate existingHandler in languageChanged.GetInvocationList())
                {
                    if(existingHandler == newHandler)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}