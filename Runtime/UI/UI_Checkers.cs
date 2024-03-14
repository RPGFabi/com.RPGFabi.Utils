using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPGFabi_Utils.UI
{
    public class UI_Checkers : MonoBehaviour
    {
        public static bool CheckIfMouseIsOverUIWhichBlocksRaycast() => EventSystem.current.IsPointerOverGameObject();
    }
}
