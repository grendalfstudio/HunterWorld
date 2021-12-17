using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.Game;
using Assets.Scripts.LoadingScene;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject aim;
        [SerializeField] private GameObject gun;
        [SerializeField] private GameObject hitPrefab;
        [SerializeField] private AudioClip shootSound;

        [SerializeField] private AnimalsController controller;

        public int BulletsCount { get; private set; }
        public Action OnBulletsCountChanged;
        
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
            OnBulletsCountChanged?.Invoke();
            AudioManager.Instance.Play(shootSound);

            var direction = aim.transform.position - gun.transform.position;
            GameObject hitObj;
            RaycastHit2D hit = Physics2D.Raycast(gun.transform.position,direction,direction.magnitude);
            if (hit.transform != null && hit.transform.root.tag.Equals("Creatures")) {
                hitObj = Instantiate(hitPrefab, hit.transform.position, Quaternion.identity);
                controller.KillTheAnimal(hit.transform.parent.gameObject);
            }
            else
            {
                hitObj = Instantiate(hitPrefab, aim.transform.position, Quaternion.identity);
            }
            
            Destroy(hitObj, 0.1f);
        }
    }
}
