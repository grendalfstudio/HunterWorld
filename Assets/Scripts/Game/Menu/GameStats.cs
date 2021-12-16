using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameStats : MonoBehaviour
    {
        [SerializeField] private AnimalsController controller;
        [SerializeField] private TextMeshProUGUI haresCount;
        [SerializeField] private TextMeshProUGUI deersCount;
        [SerializeField] private TextMeshProUGUI wolfsCount;

        // Start is called before the first frame update
        void Start()
        {
            UpdateCounts();
            AddListeners();
        }

        private void AddListeners()
        {
            controller.OnAnimalCountsUpdated += UpdateCounts;
        }

        private void RemoveListeners()
        {
            controller.OnAnimalCountsUpdated -= UpdateCounts;
        }

        private void UpdateCounts()
        {
            haresCount.text = controller.HaresCount.ToString();
            deersCount.text = controller.DeersCount.ToString();
            wolfsCount.text = controller.WolfsCount.ToString();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }
    }
}
