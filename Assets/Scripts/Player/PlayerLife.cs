using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] private Collider2D body;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Wolf"))
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}

