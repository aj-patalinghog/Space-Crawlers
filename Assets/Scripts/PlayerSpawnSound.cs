using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSound : MonoBehaviour
{
    AudioSource audio;
    public AudioClip[] audioClips;

    void Start(){
        audio = GetComponent<AudioSource>();
    }

    void Spawn(){
        audio.PlayOneShot(audioClips[0]);
    }

    void Pass(){
        audio.PlayOneShot(audioClips[1]);
    }

    void Dialog(){
        audio.PlayOneShot(audioClips[2]);
    }

    void Music(){
        audio.Play();
    }

}
