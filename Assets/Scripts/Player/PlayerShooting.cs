using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game;
using Assets.Scripts.LoadingScene;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject aim;
        [SerializeField] private GameObject gun;

        [SerializeField] private AnimalsController controller;

        public int BulletsCount { get; private set; }
        
        // Start is called before the first frame update
        void Awake()
        {
            BulletsCount = PlayerProfile.Instance.GameData.BulletsCount;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (BulletsCount <= 0)
            {
                return;
            }

            BulletsCount--;
            
            var direction = aim.transform.position - gun.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(gun.transform.position,direction,5);
            if (hit.transform != null) {
                controller.KillTheAnimal(hit.transform.gameObject);
            }
        }
    }
}
