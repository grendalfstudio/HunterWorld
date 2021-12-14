using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LoadingScene;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Game
{
    public class AnimalsController : MonoBehaviour
    {
        [SerializeField] private GameObject wolfPrefab;
        [SerializeField] private GameObject harePrefab;
        [SerializeField] private GameObject deerPrefab;
        [SerializeField] private GameObject deerGroupPrefab;
        [SerializeField] private GameObject animalsParent;
        [SerializeField] private int fieldSizeX;
        [SerializeField] private int fieldSizeY;
        [SerializeField] private int deersRadius;

        public int HaresCount { get; set; }
        public int WolfsCount { get; set; }
        public int DeerGroupsCount { get; set; }
        public int DeersCount { get; set; }

        private List<GameObject> animals = new List<GameObject>();

        private GameSettings Settings => PlayerProfile.Instance.GameData;
        
        void Awake()
        {
            SpawnAnimals();
        }

        private void SpawnAnimals()
        {
            var rnd = new Random();
            for (int i = 0; i < Settings.HaresCount; i++)
            {
                var xCoord = rnd.Next(-fieldSizeX, fieldSizeX);
                var yCoord = rnd.Next(-fieldSizeY, fieldSizeY);
                animals.Add(SpawnHare(new Vector3(xCoord, yCoord, 0)));
                HaresCount++;
            }
            for (int i = 0; i < Settings.WolfsCount; i++)
            {
                var xCoord = rnd.Next(-fieldSizeX, fieldSizeX);
                var yCoord = rnd.Next(-fieldSizeY, fieldSizeY);
                animals.Add(SpawnWolf(new Vector3(xCoord, yCoord, 0)));
                WolfsCount++;
            }
            for (int i = 0; i < Settings.DeersGroupsCount; i++)
            {
                var xCoord = rnd.Next(-fieldSizeX + deersRadius, fieldSizeX - deersRadius);
                var yCoord = rnd.Next(-fieldSizeY + deersRadius, fieldSizeY - deersRadius);
                var newGroup = SpawnDeersGroup(new Vector3(xCoord, yCoord, 0));
                animals.Add(newGroup);
                DeerGroupsCount++;
                var deersCount = rnd.Next(Settings.DeersOnGroupMinCount, Settings.DeersOnGroupMaxCount);
                for (int k = 0; k < deersCount; k++)
                {
                    var xOffset = rnd.Next(-deersRadius, deersRadius);
                    var yOffset = rnd.Next(-deersRadius, deersRadius);
                    animals.Add(SpawnDeerOnGroup(newGroup,new Vector3(xOffset, yOffset, 0)));
                    DeersCount++;
                }
            }
        }

        private GameObject SpawnHare(Vector3 position)
        {
            var newHare = Instantiate(harePrefab, position, Quaternion.identity, animalsParent.transform);
            return newHare;
        }

        private GameObject SpawnWolf(Vector3 position)
        {
            var newWolf = Instantiate(wolfPrefab, position, Quaternion.identity, animalsParent.transform);
            return newWolf;
        }

        private GameObject SpawnDeersGroup(Vector3 position)
        {
            var newGroup = Instantiate(deerGroupPrefab, position, Quaternion.identity, animalsParent.transform);
            return newGroup;
        }

        private GameObject SpawnDeerOnGroup(GameObject deersGroup, Vector3 positionOnGroup)
        {
            var newDeer = Instantiate(deerPrefab, deersGroup.transform.position + positionOnGroup, 
                Quaternion.identity, deersGroup.transform);
            return newDeer;
        }
    }
}
