using Languages;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Languages
{
    public class LanguageGetter_Dropdown : MonoBehaviour
    {
        [SerializeField] string[] keys;
        TMP_Dropdown tmp_drop;
        Dropdown drop;

        private void Awake()
        {
            LanguageHandler.languageChanged += GetLanguages;
        }
        private void Start()
        {
            GetLanguages();
        }

        private void OnDestroy()
        {
            LanguageHandler.languageChanged -= GetLanguages;
        }

        private void OnValidate()
        {
            // Replace all Spaces
            for (int i = 0; i < keys.Length; i++)
            {
                if (!string.IsNullOrEmpty(keys[i]))
                {
                    keys[i] = keys[i].Replace(" ", "");
                }
            }

            drop = GetComponent<Dropdown>();
            tmp_drop = GetComponent<TMPro.TMP_Dropdown>();

            if (drop != null)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if(drop.options.Count >= keys.Length){
                        drop.options[i].text = keys[i];
                    }
                }
            }
            if (tmp_drop != null)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if (tmp_drop.options.Count >= keys.Length)
                    {
                        tmp_drop.options[i].text = keys[i];
                    }
                }
            }

        }

        void GetLanguages()
        {

            for (int i = 0; i < keys.Length; i++)
            {
                string value = LanguageHandler.instance.GetTranslationByKey(keys[i]);

                if (drop != null)
                {
                    if (drop.options.Count >= i)
                    {
                        drop.options[i].text = value;
                    }
                }

                if (tmp_drop != null)
                {
                    if (tmp_drop.options.Count >= i)
                    {
                        tmp_drop.options[i].text = value;
                    }
                }
            }
        }

    }
}