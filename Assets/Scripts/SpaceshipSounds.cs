using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipSounds : MonoBehaviour
{
    AudioSource audio;
    public AudioClip[] audioClips;

    void Start(){
        audio = GetComponent<AudioSource>();
    }

    void Engine(){
        audio.PlayOneShot(audioClips[0]);
    }

    void Hit(){
        audio.PlayOneShot(audioClips[1]);
    }

    void Crash(){
        audio.PlayOneShot(audioClips[2]);
    }
}
