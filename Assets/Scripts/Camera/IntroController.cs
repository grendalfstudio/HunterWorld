using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts.Camera
{
    public class IntroController : MonoBehaviour
    {
        [SerializeField] private VideoPlayer player;
        [SerializeField] private AudioClip menuMusic;
        [SerializeField] private GameObject menu;
        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
            if (!PlayerPrefs.HasKey("PlayIntro") || PlayerPrefs.GetInt("PlayIntro") != 0)
            {
                PlayerPrefs.SetInt("PlayIntro", 0);
                PlayerPrefs.Save();
                Destroy(gameObject);
                EnableMenu();
                return;
            }
            
            Invoke(nameof(EnableMenu), (float)player.length);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                player.Stop();
                EnableMenu();
            }
        }

        private void EnableMenu()
        {
            AudioManager.Instance.PlayMusic(menuMusic);
            menu.SetActive(true);
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }
}
