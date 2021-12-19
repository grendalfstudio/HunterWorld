using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Game
{
    public class PauseManager : MonoBehaviour 
    {
        [SerializeField] private bool isPaused;

        [SerializeField] private GameObject pauseMenuCanvas;  

        // Update is called once per frame
        void Update () {

            if (isPaused) {
                pauseMenuCanvas.SetActive (true);
                Time.timeScale = 0f;
            } else {
                pauseMenuCanvas.SetActive(false);
                Time.timeScale = 1f;
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
            }
        }
    }
}

