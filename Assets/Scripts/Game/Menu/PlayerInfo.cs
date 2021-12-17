using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LoadingScene;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI bulletsCount;
        [SerializeField] private PlayerShooting shootStats;
        
        // Start is called before the first frame update
        void Start()
        {
            playerName.text = PlayerProfile.Instance.GameData.PlayerName;
            bulletsCount.text = shootStats.BulletsCount.ToString();
            AddListeners();
        }

        private void AddListeners()
        {
            shootStats.OnBulletsCountChanged += UpdateStats;
        }

        private void RemoveListeners()
        {
            shootStats.OnBulletsCountChanged -= UpdateStats;
        }

        private void UpdateStats()
        {
            bulletsCount.text = shootStats.BulletsCount.ToString();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }
    }
}

