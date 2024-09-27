using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntromusicBGmus : MonoBehaviour
{
    public float songlength = 1f;
    public AudioSource audioSource;
    public AudioClip Intromusic;
    public AudioClip bgMus;
    
    void BgMus()
    {
        audioSource.clip = bgMus;
        audioSource.Play();
    }
    void Start()
    {
        audioSource.clip = Intromusic;
        audioSource.Play();
        Invoke("BgMus", songlength);
    }

}
