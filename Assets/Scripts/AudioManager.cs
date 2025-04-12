using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] musicSounds, sfxSounds;
    public AudioSource musicSource;

    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Music Theme");

        
    }

    public void PlayMusic(string name)
    {
        Sounds s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sounds Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void ToggleSFX()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    /*
    public void SFXvolume()
    {
        sfxSource.volume = LightProbeProxyVolume;
    }

    public void PlaySFX()
    {
        Sounds s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sounds Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }*/
    }

