using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource AS;
    public AudioClip Clip1,Clip2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Method to play Clip1
    public void PlayClipOne()
    {
        if (AS != null && Clip1 != null)
        {
            if (!AS.isPlaying) // Optional: Check if already playing
            {
                AS.clip = Clip1;
                AS.Play();
            }
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
    public void PlayClipTwo()
    {
        if (AS != null && Clip2 != null)
        {
            if (!AS.isPlaying) // Optional: Check if already playing
            {
                AS.clip = Clip2;
                AS.Play();
                Clip2 = null;
            }
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
    // Optional: Method to stop playback
    public void StopAudio()
    {
        if (AS != null && AS.isPlaying)
        {
            AS.Stop();
        }
    }
}
