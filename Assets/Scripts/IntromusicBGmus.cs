using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntromusicBGmus : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Intromusic;
    public AudioClip bgMus;
    public float duration = 1f;
    
    void BgMus()
    {
        audioSource.clip = bgMus;
        audioSource.Play();
    }
    void Start()
    {
        audioSource.clip = Intromusic;
        audioSource.Play();
        Invoke("BgMus", duration);
    }

}
