using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //Sources
    public AudioSource soundManagerSource;
    public AudioSource loopSoundSource;
    public AudioSource musicManagerSource;

    private AudioSource[] audioSources;

    public AudioClip Music;

    //Audio Clips
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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Music Methods
    public void PlayMusic()
    {
        musicManagerSource.loop = true;
        musicManagerSource.Play();
    }

    public void StopMusic()
    {
        if (musicManagerSource.isPlaying)
        {
            musicManagerSource.Stop();
        }
    }

    //Loop Sound Methods
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
