using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace LoadingScene
{
    [Serializable]
    public class GameSettings
    {
        public string PlayerName { get; set; } = "Player";

        public int BulletsCount { get; set; } = 0;

        public int DeersGroupsCount { get; set; } = 0;

        public Tuple<int, int> DeersOnGroupCount { get; set; } = new Tuple<int, int>(0, 0);

        public int WolfsCount { get; set; } = 0;

        public int HaresCount { get; set; } = 0;
    }
}

