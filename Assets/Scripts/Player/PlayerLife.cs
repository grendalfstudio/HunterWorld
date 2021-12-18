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
        public Action OnPlayerDied;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag.Equals("Wolf"))
            {
                OnPlayerDied?.Invoke();
            }
            if (col.tag.Equals("Obstacle") && IsOutsideTheMap(col.transform))
            {
                OnPlayerDied?.Invoke();
            }
        }

        private bool IsOutsideTheMap(Transform wall)
        {
            var checkByX = wall.position.y == 0 && Math.Abs(transform.position.x) > Math.Abs(wall.position.x-1);
            var checkByY = wall.position.x == 0 && Math.Abs(transform.position.y) > Math.Abs(wall.position.y-1);
            
            return checkByX || checkByY;
        }
    }
}

