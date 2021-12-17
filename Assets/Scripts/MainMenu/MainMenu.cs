using System;
using System.Collections;
using System.Collections.Generic;
using LoadingScene;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private Button playButton;
        [SerializeField] private TMP_InputField playerNameIf;
        [SerializeField] private TMP_InputField bulletsIf;
        [SerializeField] private TMP_InputField wolfsIf;
        [SerializeField] private TMP_InputField haresIf;
        [SerializeField] private TMP_InputField deerGroupsIf;
        [SerializeField] private TMP_InputField deerPerGroupsMinIf;
        [SerializeField] private TMP_InputField deerPerGroupsMaxIf;

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
            playerNameIf.text = PlayerProfile.Instance.GameData.PlayerName;
            bulletsIf.text = PlayerProfile.Instance.GameData.BulletsCount.ToString();
            wolfsIf.text = PlayerProfile.Instance.GameData.WolfsCount.ToString();
            haresIf.text = PlayerProfile.Instance.GameData.HaresCount.ToString();
            deerGroupsIf.text = PlayerProfile.Instance.GameData.DeersGroupsCount.ToString();
            deerPerGroupsMinIf.text = PlayerProfile.Instance.GameData.DeersOnGroupCount.Item1.ToString();
            deerPerGroupsMaxIf.text = PlayerProfile.Instance.GameData.DeersOnGroupCount.Item2.ToString();
        }

        private void AddListeners()
        {
            playButton.onClick.AddListener(OnPlayClicked);
            playerNameIf.onEndEdit.AddListener(OnNameChanged);
            bulletsIf.onEndEdit.AddListener(OnBulletsChanged);
            wolfsIf.onEndEdit.AddListener(OnWolfsChanged);
            haresIf.onEndEdit.AddListener(OnHaresChanged);
            deerGroupsIf.onEndEdit.AddListener(OnDeerGroupsChanged);
            deerPerGroupsMinIf.onEndEdit.AddListener(OnDeerOnGroupsMinChanged);
            deerPerGroupsMinIf.onValueChanged.AddListener(OnDeerGroupsMaxUpdate);
            deerPerGroupsMaxIf.onEndEdit.AddListener(OnDeerOnGroupsMaxChanged);
            deerPerGroupsMaxIf.onValueChanged.AddListener(OnDeerGroupsMinUpdate);
        }

        private void RemoveListeners()
        {
            playButton.onClick.RemoveListener(OnPlayClicked);
            playerNameIf.onEndEdit.RemoveListener(OnNameChanged);
            bulletsIf.onEndEdit.RemoveListener(OnBulletsChanged);
            wolfsIf.onEndEdit.RemoveListener(OnWolfsChanged);
            haresIf.onEndEdit.RemoveListener(OnHaresChanged);
            deerGroupsIf.onEndEdit.RemoveListener(OnDeerGroupsChanged);
            deerPerGroupsMinIf.onEndEdit.RemoveListener(OnDeerOnGroupsMinChanged);
            deerPerGroupsMinIf.onValueChanged.RemoveListener(OnDeerGroupsMaxUpdate);
            deerPerGroupsMaxIf.onEndEdit.RemoveListener(OnDeerOnGroupsMaxChanged);
            deerPerGroupsMaxIf.onValueChanged.RemoveListener(OnDeerGroupsMinUpdate);
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
            var count = new Tuple<int, int>(Convert.ToInt32(newCount), 
                PlayerProfile.Instance.GameData.DeersOnGroupCount.Item2);
            PlayerProfile.Instance.GameData.DeersOnGroupCount = count;
        }
        
        private void OnDeerOnGroupsMaxChanged(string newCount)
        {
            var count = new Tuple<int, int>(PlayerProfile.Instance.GameData.DeersOnGroupCount.Item1, 
                Convert.ToInt32(newCount));
            PlayerProfile.Instance.GameData.DeersOnGroupCount = count;
        }
        
        private void OnDeerGroupsMinUpdate(string newCount)
        {
            if (Convert.ToInt32(newCount) < PlayerProfile.Instance.GameData.DeersOnGroupCount.Item1)
            {
                OnDeerOnGroupsMinChanged(newCount);
                deerPerGroupsMinIf.text = newCount;
            }
        }
        
        private void OnDeerGroupsMaxUpdate(string newCount)
        {
            if (Convert.ToInt32(newCount) > PlayerProfile.Instance.GameData.DeersOnGroupCount.Item2)
            {
                OnDeerOnGroupsMaxChanged(newCount);
                deerPerGroupsMaxIf.text = newCount;
            }
        }

        private void OnPlayClicked()
        {
            settingsPanel.SetActive(false);
            sceneLoader.gameObject.SetActive(true);
            sceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

