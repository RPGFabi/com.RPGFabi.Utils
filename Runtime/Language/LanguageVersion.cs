using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFabi_Utils.Language
{
    [System.Serializable]
    public class LanguageVersion
    {
        public int version;

        public LanguageVersion() => new LanguageVersion(-1);

        public LanguageVersion(int i) => version = i;
    }
}