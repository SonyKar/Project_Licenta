using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audios;
    
    [Header("Read only")]
    [SerializeField] private AudioClip currentlyPlaying;

    private int _index;

    private void Start()
    {
        _index = Random.Range(0, audios.Count - 1);
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentlyPlaying = audios[_index];
            _index = (_index + 1) % audios.Count;
            audioSource.clip = currentlyPlaying;
            audioSource.Play();
        }
    }
}