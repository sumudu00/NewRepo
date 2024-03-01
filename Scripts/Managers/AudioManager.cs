using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource SFX;

    [SerializeField] AudioClip[] MusicClips;
    [SerializeField] AudioClip[] SFXClips;

    Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

    void Awake()
    {
        //load clips to a structure
        foreach (var item in MusicClips)
        {
            musicClips.Add(item.name, item);
        }

        foreach (var item in SFXClips)
        {
            sfxClips.Add(item.name, item);
        }
    }

    void Start()
    {
        PlayMusic("music");
    }

    public void PlayMusic(string name)
    {
        Music.clip = musicClips[name];
        Music.Play();
    }

    public void PlaySFX(string name)
    {
        SFX.PlayOneShot(sfxClips[name]);
    }

}
