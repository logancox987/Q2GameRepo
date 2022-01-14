using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource[] audioSourceArray;
    public AudioClip[] audioClipArray;

    double nextStartTime;
    int toggle;
    int nextClip;

    // Update is called once per frame
    void Update()
    {
        if(AudioSettings.dspTime > nextStartTime - 1)
        {
            AudioClip clipToPlay = audioClipArray[nextClip];
            //Loads the
        }
    }
}
