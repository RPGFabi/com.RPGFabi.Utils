using System.Collections.Generic;

namespace RPGFabi_Utils.Language
{
    [System.Serializable]
    public class LanguageDatabase
    {
        public LanguageVersion version;
        public Language[] languages;
        public string[] translationkeys;

        public bool NewVersionAvailable(int liveVersion)
        {
            return liveVersion > version.version;
        }

        public string[] GetLanguageNames()
        {
            List<string> languageNames = new List<string>();

            foreach (Language lng in languages)
            {
                languageNames.Add(lng.languageName);
            }
            return languageNames.ToArray();
        }

        public string CreateCSVString()
        {
            if (languages.Length == 0) return "";

            List<List<string>> keysAndTranslations = new List<List<string>>();

            // Add Languagename
            List<string> languagenames = new List<string>();
            languagenames.Add("languageName");
            for (int i = 0; i < languages.Length; i++)
            {
                languagenames.Add(languages[i].languageName);
            }
            keysAndTranslations.Add(languagenames);

            // Add Translations
            int maxCount = languages[0].GetLookupCount();
            for (int i = 0; i < maxCount; i++)
            {
                List<string> translations = new List<string>();
                translations.Add(translationkeys[i]);

                for (int e = 0; e < languages.Length; e++)
                {
                    translations.Add(languages[e].GetValueByIndex(i));    
                }
                keysAndTranslations.Add(translations);
            }

            // Convert Translations to data
            string translationdata = "";
            translationdata += $"version;{version.version}\n";

            for (int i = 0; i < keysAndTranslations.Count; i++)
            {
                string row = "";

                for (int e = 0; e < keysAndTranslations[i].Count; e++)
                {
                    row += $"{keysAndTranslations[i][e]}";

                    if (e < keysAndTranslations[i].Count - 1) row += ";";
                }

                if(i < keysAndTranslations.Count -1) row += "\n";

                translationdata += row;
            }

            return translationdata;
        }

        public int GetCount() => languages.Length;

        public LanguageDatabase()
        {
            version = new LanguageVersion();
            languages = new Language[0];
            translationkeys = new string[0];
        }
    }
}