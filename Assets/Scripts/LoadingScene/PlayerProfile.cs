using System;
using System.IO;
using Assets.Scripts.Audio;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.LoadingScene
{
    public class PlayerProfile : MonoBehaviour
    {
        private const string DataKey = "GameData";
        private const string TextsPath = "Resourses/GameTexts.json";
        public GameSettings GameData { get; set; }
        public GameTexts GameTexts { get; set; }
        
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
            GameTexts = LoadTexts();
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

        public GameTexts LoadTexts()
        {
            var sourse=new StreamReader(Application.dataPath+"/" + TextsPath);
            var fileContents=sourse.ReadToEnd();
            sourse.Close();
            return JsonConvert.DeserializeObject<GameTexts>(fileContents);
        }

        private void OnDestroy()
        {
            SaveData();
        }
    }
}