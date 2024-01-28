using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPGFabi_Utils.Language
{
    public class LanguageGetter : MonoBehaviour
    {
        [SerializeField] string key;
        TMP_Text tMP_Text;
        Text text;
        bool handlerAdded = false;

        private void Awake()
        {
            GetTextfields();

            if (!handlerAdded)
            {
                LanguageHandler.languageChanged += GetLanguages;
                handlerAdded = true;
            }
        }

        private void Start()
        {
            GetLanguages();
        }

        private void OnDestroy()
        {
            if (handlerAdded)
            {
                LanguageHandler.languageChanged -= GetLanguages;
                handlerAdded = false;
            }
        }

        private void OnValidate()
        {
            // Replace all Spaces
            if (!string.IsNullOrEmpty(key))
            {
                key = key.Replace(" ", "");
            }

            GetTextfields();
        }

        private void GetTextfields()
        {
            tMP_Text = GetComponent<TMP_Text>();
            text = GetComponent<Text>();

            if (tMP_Text != null)
            {
                tMP_Text.text = key;
            }
            if (text != null)
            {
                text.text = key;
            }
        }

        void GetLanguages()
        {
                string value = LanguageHandler.instance.GetTranslationByKey(key);
            
                if (tMP_Text != null)
                {
                    tMP_Text.text = value;
                }
                if (text != null)
                {
                    text.text = value;
                }
        }

        public void UpDateKey(string newKey){
            key = newKey;
            GetLanguages();
        }
    }
}