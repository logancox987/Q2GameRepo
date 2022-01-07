﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class AutoManager : MonoBehaviour{

    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

   
    public void Play (string name)
    {
        Sound s =Array.Find(sounds, Sound => Sound.name == name);
        s.source.Play();

    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound; " + name + "not found!");
            return;
        }
        s.source.Play();

    }
}