using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LoadingScene
{
    [Serializable]
    public class GameTexts
    {
        public Dictionary<string, string> Texts { get; set; }
        public List<string> Credits { get; set; }
    }
}