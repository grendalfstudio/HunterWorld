using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.LoadingScene;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Game
{
    public class AnimalsController : MonoBehaviour
    {
        [SerializeField] private int fieldSizeX;
        [SerializeField] private int fieldSizeY;
        [SerializeField] private int deersRadius;
        [SerializeField] private AnimalsSpawner spawner;
        [SerializeField] private List<GameObject> deadPrefabs;
        [SerializeField] private List<AudioClip> deadSounds;
        [SerializeField] private int respawnTriggerCount = 1;

        public int HaresCount { get; set; }
        public int WolfsCount { get; set; }
        public int DeerGroupsCount { get; set; }
        public int DeersCount { get; set; }
        public Action OnAnimalCountsUpdated;

        private List<GameObject> animals = new List<GameObject>();

        private GameSettings Settings => PlayerProfile.Instance.GameData;
        private Random rnd;

        void Awake()
        {
            rnd = new Random();
            SpawnAnimals();
        }

        public void KillTheAnimal(GameObject animal, bool isMurdered = false)
        {
            if (!animals.Contains(animal))
            {
                return;
            }

            var animalId = 0;
            switch (animal.tag)
            {
                case "Hare":
                    HaresCount--;
                    animalId = 0;
                    break;
                case "Deer":
                    DeersCount--;
                    animalId = 1;
                    break;
                case "Wolf":
                    WolfsCount--;
                    animalId = 2;
                    break;
            }

            AudioManager.Instance.Play(deadSounds[animalId], animal.transform.position);
            if (isMurdered)
            {
                Instantiate(deadPrefabs[animalId], animal.transform.position, animal.transform.rotation);
            }

            
            OnAnimalCountsUpdated?.Invoke();
            
            animals.Remove(animal);
            Destroy(animal);
            
            RespawnAnimals();
        }

        private void SpawnAnimals()
        {
            SpawnHares();
            SpawnWolfs();
            SpawnDeersGroups();
        }

        private void SpawnHares()
        {
            for (int i = HaresCount; i < Settings.HaresCount; i++)
            {
                var xCoord = rnd.Next(-fieldSizeX, fieldSizeX);
                var yCoord = rnd.Next(-fieldSizeY, fieldSizeY);
                animals.Add(spawner.SpawnHare(new Vector3(xCoord, yCoord, 0)));
                HaresCount++;
            }
            OnAnimalCountsUpdated?.Invoke();
        }

        private void SpawnWolfs()
        {
            for (int i = WolfsCount; i < Settings.WolfsCount; i++)
            {
                var xCoord = rnd.Next(-fieldSizeX, fieldSizeX);
                var yCoord = rnd.Next(-fieldSizeY, fieldSizeY);
                animals.Add(spawner.SpawnWolf(new Vector3(xCoord, yCoord, 0)));
                WolfsCount++;
            }
            OnAnimalCountsUpdated?.Invoke();
        }

        private void SpawnDeersGroups()
        {
            for (int i = 0; i < Settings.DeersGroupsCount; i++)
            {
                var xCoord = rnd.Next(-fieldSizeX + deersRadius, fieldSizeX - deersRadius);
                var yCoord = rnd.Next(-fieldSizeY + deersRadius, fieldSizeY - deersRadius);
                var newGroup = spawner.SpawnDeersGroup(new Vector3(xCoord, yCoord, 0));
                animals.Add(newGroup);
                DeerGroupsCount++;
                
                SpawnDeersOnGroup(newGroup);
            }
            OnAnimalCountsUpdated?.Invoke();
        }

        private void SpawnDeersOnGroup(GameObject deersGroup)
        {
            var deersCount = rnd.Next(Settings.DeersOnGroupMinCount, Settings.DeersOnGroupMaxCount);
            for (int k = 0; k < deersCount; k++)
            {
                var xOffset = rnd.Next(-deersRadius, deersRadius);
                var yOffset = rnd.Next(-deersRadius, deersRadius);
                animals.Add(spawner.SpawnDeerOnGroup(deersGroup,new Vector3(xOffset, yOffset, 0)));
                DeersCount++;
            }
            OnAnimalCountsUpdated?.Invoke();
        }

        private void RespawnAnimals()
        {
            if (HaresCount <= respawnTriggerCount) SpawnHares();
            if (DeersCount <= respawnTriggerCount) SpawnDeersGroups();
            if (WolfsCount <= respawnTriggerCount) SpawnWolfs();
        }
    }
}
