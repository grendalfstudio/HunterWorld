using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{
    public class PauseManager : MonoBehaviour 
    {
        [SerializeField] private bool isPaused;

        [SerializeField] private GameObject pauseMenuCanvas;  

        // Update is called once per frame
        void Update () {
            if (isPaused) {
                pauseMenuCanvas.SetActive(true);
                Time.timeScale = 0f;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PlayerPrefs.SetInt("PlayIntro", 1);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene(0);
                }
            } else {
                pauseMenuCanvas.SetActive(false);
                Time.timeScale = 1f;
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                isPaused = !isPaused;
            }
        }
    }
}

