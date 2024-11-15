using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundManagerSource;
    public AudioSource loopSoundSource;
    public AudioSource musicManagerSource;

    private AudioSource[] audioSources;

    public AudioClip Music;

    public AudioClip Gun;
    public AudioClip Run;
    public AudioClip Walk;

    private void Start()
    {
        loopSoundSource.clip = Walk;

        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSources = GetComponentsInChildren<AudioSource>();
            soundManagerSource = audioSources[0];
            loopSoundSource = audioSources[1];
            musicManagerSource = audioSources[2];

            Debug.Log(loopSoundSource);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlaySound(string name)
    {
        switch (name)
        {
            case "Gun":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(Gun);
                break;
           
        }
    }

    public void PlayMusic()
    {
        musicManagerSource.loop = true;
        musicManagerSource.Play();
    }

    public void PlayLoopSound()
    {
        loopSoundSource.loop = true;

        loopSoundSource.Play();
    }

    public void StopLoopSound()
    {
        if (loopSoundSource.isPlaying)
        {
            loopSoundSource.Stop();
        }

        loopSoundSource.clip = null;
    }

    public void ChangeLoopSound(string name)
    {

        switch (name)
        {
            case "walk":

                if (loopSoundSource.clip != Walk)
                {

                    Debug.Log("CHANGED TO WALK");
                    loopSoundSource.clip = Walk;
                    PlayLoopSound();
                }
           
                break;

            case "run":

                if (loopSoundSource.clip != Run)
                {


                    loopSoundSource.clip = Run;
                    PlayLoopSound();
                }
                break;


        }
    }

}
