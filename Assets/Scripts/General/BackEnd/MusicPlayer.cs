using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> music;
    public AudioSource src;
    System.Random rnd;
    int count;
    void Start()
    {
        count = music.Count;
        rnd = new System.Random();
    }

    void Update()
    {
        if (!src.isPlaying)
        {
            src.PlayOneShot(music[rnd.Next(0, count)]);
        }
    }
}
