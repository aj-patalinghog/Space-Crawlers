using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource soundPlayer;

    public void playThisSoundEffect()
    {
        soundPlayer.Play();
    }

    public void pauseSong()
    {
        soundPlayer.Pause();
    }

    public void unpauseSong()
    {
        soundPlayer.UnPause();
    }

    public static class AudioFadeOut
    {

        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

        public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
        {
            float startVolume = 0.2f;

            audioSource.volume = 0;
            audioSource.Play();

            while (audioSource.volume < 1.0f)
            {
                audioSource.volume += startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.volume = 1f;
        }

    }
    public void playSong()
    {
        StartCoroutine(AudioFadeOut.FadeIn(soundPlayer, 1f));
    }

    public void endSong()
    {
        StartCoroutine(AudioFadeOut.FadeOut(soundPlayer, 1f));
    }

}
