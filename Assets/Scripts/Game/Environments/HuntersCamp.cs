using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game;
using Assets.Scripts.LoadingScene;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;

public class HuntersCamp : MonoBehaviour
{
    [SerializeField] private GameObject pressObj;
    [SerializeField] private AnimalsController _controller;
    [SerializeField] private PlayerShooting _shooting;

    private bool isTriggered = false;
    private KeyCode expectedKey = KeyCode.R;

    private void Update()
    {
        if (Time.timeScale == 0) return;
        
        if (isTriggered && Input.GetKeyDown(expectedKey))
        {
            _shooting.ReloadGun();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            if (pressObj == null)
            {
                expectedKey = KeyCode.R;
            }
            else
            {
                pressObj.SetActive(true);
            }
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (pressObj != null)
            {
                pressObj.SetActive(false);
            }
            isTriggered = false;
        }
    }
}
