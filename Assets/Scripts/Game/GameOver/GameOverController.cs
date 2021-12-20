using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Scripts.Audio;
using Assets.Scripts.LoadingScene;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Assets.Scripts.Game
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private PlayerLife playerLife;
        [SerializeField] private GameObject creatures;
        [SerializeField] private GameObject gameMenu;
        [SerializeField] private VideoPlayer endingVideoPlayer;
        [SerializeField] private VideoClip endingClip;
        [SerializeField] private VideoClip gameOverClip;
        [SerializeField] private GameObject diedPlayerPrefab;
        [SerializeField] private AudioClip playerDied;
        [SerializeField] private EndingTexts texts;
        [SerializeField] private GameObject restartGameCanvas;
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private GameObject background;
    
        private bool gameFinished = false;
        private bool isPlayerDied = false;
        
        // Start is called before the first frame update
        void Awake()
        {
            AddListeners();
        }
    
        private void Update()
        {
            if (Time.timeScale == 0) return;

            if (isPlayerDied)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    isPlayerDied = false;
                    sceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    isPlayerDied = false;
                    PlayGameOver();
                }
            }
            
            if (gameFinished)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    endingVideoPlayer.Stop();
                    OnEndingFinished();
                }
            }
        }
    
        private void AddListeners()
        {
            playerLife.OnPlayerDied += OnPlayerDied;
        }
    
        private void OnPlayerDied()
        {
            Destroy(player);
            Instantiate(diedPlayerPrefab, player.transform.position, Quaternion.identity);
            AudioManager.Instance.Play(playerDied, player.transform.position);
            AudioManager.Instance.StopMusic();
            isPlayerDied = true;
            restartGameCanvas.SetActive(true);
        }
    
        private void PlayGameOver()
        {
            Destroy(creatures);
            Destroy(gameMenu);
            Destroy(background);
            restartGameCanvas.SetActive(false);
            if (gameOverClip != null)
            {
                endingVideoPlayer.clip = gameOverClip;
                endingVideoPlayer.Play();
                Invoke(nameof(PlayEnding), (float)endingVideoPlayer.length);
            }
            else
            {
                PlayEnding();
            }
            
        }
    
        private void PlayEnding()
        {
            endingVideoPlayer.clip = endingClip;
            endingVideoPlayer.Play();
            gameFinished = true;
            texts.PlayTexts(false);
            Invoke(nameof(OnEndingFinished), (float)endingVideoPlayer.length);
        }
    
        private void OnEndingFinished()
        {
            PlayerPrefs.SetInt("PlayIntro", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }
    }

}
