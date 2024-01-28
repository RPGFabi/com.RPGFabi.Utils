using System.Collections.Generic;
using UnityEngine;

namespace RPGFabi_Utils.Language
{
    [System.Serializable]
    public class Language
    {
        public string languageName;
        public string[] translations;
        //public string imageToken;

        public int GetLookupCount() => translations.Length;
        //public string GetImageToken() => imageToken;

    #if UNITY_EDITOR
        public void AddKey(string key)
        {
            string[] temp = new string[GetLookupCount() + 1];
            for (int i = 0; i < GetLookupCount(); i++)
            {
                temp[i] = translations[i];
            }
            temp[GetLookupCount() + 1] = key;
        }
     #endif

        public string GetValueByIndex(int index)
        { 
            if (GetLookupCount() < index) return "";
            return      translations[index];
        }

        public Language()
        {
            translations = new string[0];
        }

        public Language(int id = 0) => languageName = "New Language " + id;

        public Language(string[]values)
        {
            int maxCount = values.Length;

            languageName = values[0].Replace("\r","");
            //imageToken = values[1].Replace("\r", "");

            translations = new string[maxCount - 2];
            for (int i = 2; i < maxCount; i++)
            {
                    translations[i-2] = values[i].Replace("\r", "");
            }
        }
    }
}