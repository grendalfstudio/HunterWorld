using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Game
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> gameMusic;
        
        // Start is called before the first frame update
        void Start()
        {
            AudioManager.Instance.PlayMusic(gameMusic[new Random().Next(0, gameMusic.Count - 1)]);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

