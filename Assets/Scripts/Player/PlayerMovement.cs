using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        public Action OnPlayerMoved;

        void Update()
        {
            if (Time.timeScale == 0) return;
            
            ComputeViewDirection();
        }

        private void FixedUpdate()
        {
            if (Time.timeScale == 0) return;
            
            MovePlayer();
        }

        private void ComputeViewDirection()
        {
            var mousePosition = Input.mousePosition;
            if (UnityEngine.Camera.main != null)
                mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(mousePosition);
            var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
            transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);
        }

        private void MovePlayer()
        {
            var pos = transform.position;
 
            if (Input.GetKey(KeyCode.W)) {
                pos.y += speed * Time.deltaTime;
            }
            if (Input.GetKey (KeyCode.S)) {
                pos.y -= speed * Time.deltaTime;
            }
            if (Input.GetKey (KeyCode.D)) {
                pos.x += speed * Time.deltaTime;
            }
            if (Input.GetKey (KeyCode.A)) {
                pos.x -= speed * Time.deltaTime;
            }
        
            transform.position = pos;
            OnPlayerMoved?.Invoke();
        }
    }
}

