using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource MusicPlayer;
    [SerializeField] private AudioSource AmbientPlayer;
    [SerializeField] private AudioSource SoundEffectsPlayer;
    

    [SerializeField] private AudioClip[] Honks;
    [SerializeField] private AudioClip[] WalkingMusic;
    [SerializeField] private AudioClip HardwareMusic;
    [SerializeField] private AudioClip GroceryStoreMusic;
    [SerializeField] private AudioClip AmbientCity;
    [SerializeField] private AudioClip AmbientStore;


    public AudioClip GetRandomHonk()
    {
        return Honks[Random.Range(0, Honks.Length)];
    }
}
