using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class StoryEndManager : MonoBehaviour
    {
        [SerializeField] private GameObject endedMenuCanvas;
        [SerializeField] private GameObject endedGamePauseText;

        public Action OnStoryEnded;
        
        private bool isEnded;
        private bool isPaused;
        private bool canEnd;

        // Update is called once per frame
        void Update () {

            if (isPaused)
            {
                endedMenuCanvas.SetActive(true);
                Time.timeScale = 0f;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    endedMenuCanvas.SetActive(false);
                    Time.timeScale = 1f;
                    isPaused = false;
                    canEnd = false;
                }
            }

            if(!isPaused && isEnded && Input.GetKeyDown(KeyCode.P))
            {
                endedGamePauseText.SetActive(!endedGamePauseText.activeSelf);
                canEnd = !canEnd;
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                endedMenuCanvas.SetActive(false);
                endedGamePauseText.transform.parent.parent.parent.gameObject.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
                canEnd = false;
                OnStoryEnded?.Invoke();
            }
        }

        public void EndStory()
        {
            isPaused = true;
            isEnded = true;
            canEnd = true;
        }
    }
}

