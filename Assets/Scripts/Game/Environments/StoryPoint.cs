using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LoadingScene;
using TMPro;
using UnityEngine;

public class StoryPoint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scroll;
    [SerializeField] private string textKey;
    [SerializeField] private GameObject pressObj;

    private bool isTriggered = false;
    private bool isOpened = false;
    private int counter = 1;
    private KeyCode expectedKey = KeyCode.E;

    private void Update()
    {
        if (Time.timeScale == 0) return;
        
        if (isTriggered && !scroll.transform.parent.gameObject.activeSelf && Input.GetKeyDown(expectedKey))
        {
            if (PlayerProfile.Instance.GameTexts.Texts.ContainsKey(textKey))
            {
                isOpened = true;
                counter = 2;
                scroll.text = PlayerProfile.Instance.GameTexts.Texts[textKey];
                scroll.transform.parent.gameObject.SetActive(true);
            }
        }

        if (isOpened && Input.GetKeyDown(KeyCode.Return))
        {
            if (PlayerProfile.Instance.GameTexts.Texts.ContainsKey(textKey + counter))
            {
                scroll.text = PlayerProfile.Instance.GameTexts.Texts[textKey + counter];
                counter++;
            }
            else
            {
                isOpened = false;
                scroll.transform.parent.gameObject.SetActive(false);
                counter = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            if (pressObj == null)
            {
                expectedKey = KeyCode.F;
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
