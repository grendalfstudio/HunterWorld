using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private PlayerMovement target;
        [SerializeField] private Vector3 offset;

        void Start()
        {
            target.OnPlayerMoved += MoveCamera;
        }

        private void OnDestroy()
        {
            target.OnPlayerMoved -= MoveCamera;
        }

        private void MoveCamera()
        {
            var targetPosition = target.transform.position;
            transform.position = new Vector3 (targetPosition.x + offset.x, targetPosition.y + offset.y, offset.z);
        }
        
        /*void FixedUpdate() 
        {
            transform.position = new Vector3 (target.position.x + offset.x, target.position.y + offset.y, offset.z);
        }*/
    }
}
