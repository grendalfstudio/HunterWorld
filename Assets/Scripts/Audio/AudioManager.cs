using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEffectSource effectSource;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private float lowPitchRange = .95f;
    [SerializeField] private float highPitchRange = 1.05f;

    public static AudioManager Instance = null;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        effectSource.source.clip = clip;
        Instantiate(effectSource.prefab);
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        effectSource.source.pitch = randomPitch;
        effectSource.source.clip = clips[randomIndex];
        Instantiate(effectSource.prefab);
    }

    public void OnDestroy()
    {
        Debug.Log("I am bad guy");
    }
}
