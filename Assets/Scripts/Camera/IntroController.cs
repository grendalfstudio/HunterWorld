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
        [SerializeField] private GameObject menu;
        [SerializeField] private AudioClip menuMusic;
    
        // Start is called before the first frame update
        void Start()
        {
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
            menu.SetActive(true);
            AudioManager.Instance.PlayMusic(menuMusic);
        }
    }
}
