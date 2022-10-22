using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource MusicPlayer;
    [SerializeField] private AudioSource AmbientPlayer;


    [SerializeField] private AudioClip[] Honks;
    [SerializeField] private AudioClip[] WalkingMusic;
    [SerializeField] private AudioClip FoodMusic;
    [SerializeField] private AudioClip HardwareMusic;
    [SerializeField] private AudioClip GroceryStoreMusic;
    [SerializeField] private AudioClip UberMusic;
    [SerializeField] private AudioClip AmbientCity;
    [SerializeField] private AudioClip AmbientHardwareStore;
    [SerializeField] private AudioClip AmbientGroceryStore;
    [SerializeField] private AudioClip AmbientFood;



    private void Awake()
    {
        YarnInteract.EndConvo += SetSoundEndConvo;
        YarnInteract.StartConvo += SetSoundStartConvo;
    }
    
    private void OnDestroy()
    {
        YarnInteract.EndConvo -= SetSoundEndConvo;
        YarnInteract.StartConvo -= SetSoundStartConvo;
    }

    private void SetSoundEndConvo(string scene)
    {
        switch (scene)
        {
            case "HardwareStore":
                MusicPlayer.clip = WalkingMusic[0];
                AmbientPlayer.clip = AmbientCity;
                MusicPlayer.Play();
                AmbientPlayer.Play();
                break;
            case "Lunch":
                MusicPlayer.clip = WalkingMusic[1];
                AmbientPlayer.clip = AmbientCity;
                MusicPlayer.Play();
                AmbientPlayer.Play();
                break;
            case "GroceryStore":
                MusicPlayer.clip = WalkingMusic[2];
                AmbientPlayer.clip = AmbientCity;
                MusicPlayer.Play();
                AmbientPlayer.Play();
                break;
            case "Rideshare":
                MusicPlayer.clip = WalkingMusic[3];
                AmbientPlayer.clip = AmbientCity;
                MusicPlayer.Play();
                AmbientPlayer.Play();
                break;
           
            
            
        }
    }

    private void SetSoundStartConvo(string scene)
    {
        switch (scene)
        {
            case "GroceryStore":
                AmbientPlayer.clip = AmbientGroceryStore;
                MusicPlayer.clip = GroceryStoreMusic;
                MusicPlayer.Play();
                AmbientPlayer.Play();
                break;
            case "Rideshare":
                MusicPlayer.clip = UberMusic;
                MusicPlayer.Play();
                //AmbientPlayer.Play();
                break;
            case "Lunch":
                MusicPlayer.clip = FoodMusic;
                AmbientPlayer.clip = AmbientFood;
                MusicPlayer.Play();
                AmbientPlayer.Play();
                break;
            case "HardwareStore":
                MusicPlayer.clip = HardwareMusic;
                AmbientPlayer.clip = AmbientHardwareStore;
                MusicPlayer.Play();
                AmbientPlayer.Play();
                break;
            
        }
    }
    

    public AudioClip GetRandomHonk()
    {
        return Honks[Random.Range(0, Honks.Length)];
    }
}
