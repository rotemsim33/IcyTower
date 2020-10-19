using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource levelMusic;
    [SerializeField] private AudioSource deathMusic;

    [SerializeField] private bool levelSong = true;
    [SerializeField] private bool deathSong = false;
  
    public void LevelMusic()
    {
        levelSong = true;
        deathSong = false;
        levelMusic.Play();

    }
    public void DeathMusic()
    {
        if(levelMusic.isPlaying)
        {
            levelSong = false;
            levelMusic.Stop();
         }
        if(!deathMusic.isPlaying && deathSong == false)
        {
            deathMusic.Play();
            deathSong = true;
        }

    }
    
}

