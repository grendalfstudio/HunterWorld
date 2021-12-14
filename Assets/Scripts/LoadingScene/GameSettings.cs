using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.LoadingScene
{
    [Serializable]
    public class GameSettings
    {
        public string PlayerName { get; set; } = "Player";

        public int BulletsCount { get; set; } = 0;

        public int DeersGroupsCount { get; set; } = 0;

        public int DeersOnGroupMinCount { get; set; } = 0;
        
        public int DeersOnGroupMaxCount { get; set; } = 0;

        public int WolfsCount { get; set; } = 0;

        public int HaresCount { get; set; } = 0;
    }
}

