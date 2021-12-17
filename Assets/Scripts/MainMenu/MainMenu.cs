using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LoadingScene;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private Button playButton;
        [SerializeField] private TMP_InputField playerNameIF;
        [SerializeField] private TMP_InputField bulletsIF;
        [SerializeField] private TMP_InputField wolfsIF;
        [SerializeField] private TMP_InputField haresIF;
        [SerializeField] private TMP_InputField deerGroupsIF;
        [SerializeField] private TMP_InputField deerPerGroupsMinIF;
        [SerializeField] private TMP_InputField deerPerGroupsMaxIF;

        private void Start()
        {
            SetSavedValues();
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void SetSavedValues()
        {
            playerNameIF.text = PlayerProfile.Instance.GameData.PlayerName;
            bulletsIF.text = PlayerProfile.Instance.GameData.BulletsCount.ToString();
            wolfsIF.text = PlayerProfile.Instance.GameData.WolfsCount.ToString();
            haresIF.text = PlayerProfile.Instance.GameData.HaresCount.ToString();
            deerGroupsIF.text = PlayerProfile.Instance.GameData.DeersGroupsCount.ToString();
            deerPerGroupsMinIF.text = PlayerProfile.Instance.GameData.DeersOnGroupMinCount.ToString();
            deerPerGroupsMaxIF.text = PlayerProfile.Instance.GameData.DeersOnGroupMaxCount.ToString();
        }

        private void AddListeners()
        {
            playButton.onClick.AddListener(OnPlayClicked);
            playerNameIF.onEndEdit.AddListener(OnNameChanged);
            bulletsIF.onEndEdit.AddListener(OnBulletsChanged);
            wolfsIF.onEndEdit.AddListener(OnWolfsChanged);
            haresIF.onEndEdit.AddListener(OnHaresChanged);
            deerGroupsIF.onEndEdit.AddListener(OnDeerGroupsChanged);
            deerPerGroupsMinIF.onEndEdit.AddListener(OnDeerOnGroupsMinChanged);
            deerPerGroupsMinIF.onValueChanged.AddListener(OnDeerGroupsMaxUpdate);
            deerPerGroupsMaxIF.onEndEdit.AddListener(OnDeerOnGroupsMaxChanged);
            deerPerGroupsMaxIF.onValueChanged.AddListener(OnDeerGroupsMinUpdate);
        }

        private void RemoveListeners()
        {
            playButton.onClick.RemoveListener(OnPlayClicked);
            playerNameIF.onEndEdit.RemoveListener(OnNameChanged);
            bulletsIF.onEndEdit.RemoveListener(OnBulletsChanged);
            wolfsIF.onEndEdit.RemoveListener(OnWolfsChanged);
            haresIF.onEndEdit.RemoveListener(OnHaresChanged);
            deerGroupsIF.onEndEdit.RemoveListener(OnDeerGroupsChanged);
            deerPerGroupsMinIF.onEndEdit.RemoveListener(OnDeerOnGroupsMinChanged);
            deerPerGroupsMinIF.onValueChanged.RemoveListener(OnDeerGroupsMaxUpdate);
            deerPerGroupsMaxIF.onEndEdit.RemoveListener(OnDeerOnGroupsMaxChanged);
            deerPerGroupsMaxIF.onValueChanged.RemoveListener(OnDeerGroupsMinUpdate);
        }

        private void OnNameChanged(string newName)
        {
            PlayerProfile.Instance.GameData.PlayerName = newName;
        }
        
        private void OnBulletsChanged(string newCount)
        {
            PlayerProfile.Instance.GameData.BulletsCount = Convert.ToInt32(newCount);
        }
        
        private void OnWolfsChanged(string newCount)
        {
            PlayerProfile.Instance.GameData.WolfsCount = Convert.ToInt32(newCount);
        }
        
        private void OnHaresChanged(string newCount)
        {
            PlayerProfile.Instance.GameData.HaresCount = Convert.ToInt32(newCount);
        }
        
        private void OnDeerGroupsChanged(string newCount)
        {
            PlayerProfile.Instance.GameData.DeersGroupsCount = Convert.ToInt32(newCount);
        }
        
        private void OnDeerOnGroupsMinChanged(string newCount)
        {
            PlayerProfile.Instance.GameData.DeersOnGroupMinCount = Convert.ToInt32(newCount);
        }
        
        private void OnDeerOnGroupsMaxChanged(string newCount)
        {
            PlayerProfile.Instance.GameData.DeersOnGroupMaxCount = Convert.ToInt32(newCount);
        }
        
        private void OnDeerGroupsMinUpdate(string newCount)
        {
            if (newCount != "" && Convert.ToInt32(newCount) < PlayerProfile.Instance.GameData.DeersOnGroupMinCount)
            {
                OnDeerOnGroupsMinChanged(newCount);
                deerPerGroupsMinIF.text = newCount;
            }
        }
        
        private void OnDeerGroupsMaxUpdate(string newCount)
        {
            if (newCount != "" && Convert.ToInt32(newCount) > PlayerProfile.Instance.GameData.DeersOnGroupMaxCount)
            {
                OnDeerOnGroupsMaxChanged(newCount);
                deerPerGroupsMaxIF.text = newCount;
            }
        }

        private void OnPlayClicked()
        {
            settingsPanel.SetActive(false);
            sceneLoader.gameObject.SetActive(true);
            Cursor.visible = false;
            sceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

