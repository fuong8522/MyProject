using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource playerEffect;
    public AudioSource playerEffect2;
    public AudioClip basesound;
    public List<AudioClip> clips = new List<AudioClip>();
    void Start()
    {
        playerEffect= GetComponent<AudioSource>();
    }

    //Âm thanh bước chân.
    void OnFootStep()
    {
        AudioClip step = clips[Random.Range(0,clips.Count)];
        playerEffect2.PlayOneShot(step, 1f);
    }
    //Âm thanh vũ khí 1.
    void SoundBaseBall()
    {
        playerEffect.PlayOneShot(basesound, 0.05f);
    }

}
