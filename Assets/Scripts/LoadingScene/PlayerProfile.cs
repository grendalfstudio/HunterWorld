using System;
using Audio;
using Newtonsoft.Json;
using UnityEngine;

namespace LoadingScene
{
    public class PlayerProfile : MonoBehaviour
    {
        private const string DataKey = "GameData";
        public GameSettings GameData { get; set; }
        
        public static PlayerProfile Instance = null;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            GameData = LoadData();
            DontDestroyOnLoad(gameObject);
        }

        public void SaveData()
        {
            var data = JsonConvert.SerializeObject(GameData);
            PlayerPrefs.SetString(DataKey, data);
            PlayerPrefs.Save();
        }

        public GameSettings LoadData()
        {
            if (!PlayerPrefs.HasKey(DataKey))
            {
                return new GameSettings();
            }

            var data = PlayerPrefs.GetString(DataKey);
            return JsonConvert.DeserializeObject<GameSettings>(data);
        }

        private void OnDestroy()
        {
            SaveData();
        }
    }
}