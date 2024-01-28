using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RPGFabi_Utils.Language
{
    public class LanguageDisplay : MonoBehaviour
    {
        [SerializeField] GameObject buttonPrefab;
        Transform parent;
        int lastActive = 0;

        private void Awake()
        {
            parent = buttonPrefab.transform.parent;
            buttonPrefab.SetActive(true);
        }

        private void Start()
        {
            GetAndDisplayLanguageImages();
            LanguageHandler.languageChanged += GetAndDisplayLanguageImages;
        }

        private void OnDestroy()
        {
            LanguageHandler.languageChanged -= GetAndDisplayLanguageImages;
        }

        private void GetAndDisplayLanguageImages()
        {
            // Clear Childs
            for (int i = transform.childCount; i >1 ; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            Texture2D[] imgs = LanguageHandler.instance.GetLanguageImages();

            float width = ((RectTransform)buttonPrefab.transform).rect.width;
            for (int i = 0; i < imgs.Length; i++)
            {
                GameObject newButton = Instantiate(buttonPrefab, parent);
                newButton.transform.GetChild(1).GetComponent<RawImage>().texture = imgs[i];

                int x = new int();
                x = i;
                newButton.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { LanguageHandler.instance.SetLanguage(x); });
                newButton.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { EnableButtonHighlight(x); });

                newButton.transform.localPosition = new Vector3(i * (width + 20), buttonPrefab.transform.localPosition.y, 0);
                newButton.SetActive(true);

            }

            // Get Index of current language
            EnableButtonHighlight(LanguageHandler.currentLanguageIndex);
        }

        void EnableButtonHighlight(int i)
        {
            if (i >= transform.childCount || transform.childCount == 1) return;

            transform.GetChild(i + 1).GetChild(0).gameObject.SetActive(true);
            if(lastActive != 0)
            {
                transform.GetChild(lastActive).GetChild(0).gameObject.SetActive(false);
            }
            lastActive = i + 1;
        }

    }
}