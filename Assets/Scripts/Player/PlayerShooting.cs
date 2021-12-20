using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public Dictionary<string, int> killedAnimals { get; private set; } = new Dictionary<string, int>();

        private string[] keys;

        // Start is called before the first frame update
        void Awake()
        {
            BulletsCount = PlayerProfile.Instance.GameData.BulletsCount;
            killedAnimals.Add("Hare", 0);
            killedAnimals.Add("Deer", 0);
            killedAnimals.Add("Wolf", 0);
            keys = new string[killedAnimals.Count];
            killedAnimals.Keys.CopyTo(keys, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale == 0) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        public void ReloadGun()
        {
            var animalCoef = 1;
            foreach (var key in keys)
            {
                var neededBulletsCount = PlayerProfile.Instance.GameData.BulletsCount - BulletsCount;
                if (neededBulletsCount == 0)
                {
                    OnBulletsCountChanged.Invoke();
                    break;
                }
                
                var tropheyReward = killedAnimals[key] * animalCoef;
                if (tropheyReward <= neededBulletsCount)
                {
                    killedAnimals[key] = 0;
                    BulletsCount += tropheyReward;
                }
                else
                {
                    killedAnimals[key] -= neededBulletsCount / animalCoef + 1;
                    BulletsCount = PlayerProfile.Instance.GameData.BulletsCount;
                }

                animalCoef++;
            }
        }

        private void Shoot()
        {
            if (BulletsCount <= 0)
            {
                return;
            }
            
            BulletsCount--;
            AudioManager.Instance.Play(shootSound, transform.position);

            var direction = aim.transform.position - gun.transform.position;
            GameObject hitObj;
            RaycastHit2D hit = Physics2D.Raycast(gun.transform.position,direction,direction.magnitude);
            if (hit.transform != null && hit.transform.root.tag.Equals("Creatures")) {
                hitObj = Instantiate(hitPrefab, hit.transform.position, Quaternion.identity);
                controller.KillTheAnimal(hit.transform.gameObject, true);
                killedAnimals[hit.transform.tag] += 1;
            }
            else
            {
                hitObj = Instantiate(hitPrefab, aim.transform.position, Quaternion.identity);
            }
            
            OnBulletsCountChanged?.Invoke();
            
            Destroy(hitObj, 0.1f);
        }
    }
}
