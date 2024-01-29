using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPGFabi_Utils.World
{
    class WorldText
    {
        /// <summary>
        /// Returns the Default Font by Unity
        /// </summary>
        /// <returns></returns>
        public static Font GetDefaultFont()
        {
            return Resources.GetBuiltinResource<Font>("Arial.ttf");
        }

        public static TextMesh CreateWorldText(string text, Vector3 position, Quaternion rotation, Font font, int fontSize, Color color, TextAnchor anchor, TextAlignment alignment)
        {
            return CreateWorldText(null, text, position, rotation, font, fontSize, color, anchor, alignment);
        }


        /// <summary>
        /// Creates an Text in the World
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="font"></param>
        /// <param name="fonzSize"></param>
        /// <param name="color"></param>
        /// <param name="textAnchor"></param>
        /// <param name="textAlignment"></param>
        /// <returns></returns>
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 position, Quaternion rotation, Font font, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment)
        {
            GameObject gameObject = new GameObject("World Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent);
            transform.localPosition = position;
            transform.localRotation = rotation;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.font = font;
            textMesh.text = text;
            return textMesh;
        }
    }
}
