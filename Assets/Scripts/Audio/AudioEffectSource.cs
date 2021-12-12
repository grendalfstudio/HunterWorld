using System;
using UnityEngine;

namespace Audio
{
    public class AudioEffectSource : MonoBehaviour
    {
        [SerializeField] public AudioSource source;
        [SerializeField] public GameObject prefab;

        private void Awake()
        {
            Invoke(nameof(DestroySource), 2);
        }

        private void DestroySource()
        {
            Destroy(gameObject);
        }
    }
}