using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioClip startingMusic;
    public AudioClip lowLevelMusic;
    public AudioClip highLevelMusic;
    public AudioClip veryHighLevelMusic;
    public AudioClip bossMusic;
    public AudioClip finalBossMusic;
    public AudioClip creditsMusic;

    private AudioSource source;

    void OnEnable()
    {
        Singleton_Service.RegisterSingletonInstance(this);
    }

    void OnDisable()
    {
        Singleton_Service.UnregisterSingletonInstance(this);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayBossMusic()
    {
        StartCoroutine(MusicFadeOut(bossMusic));
    }

    public void PlayFinalBossMusic()
    {
        StartCoroutine(MusicFade(finalBossMusic));
    }

    public void PlayStartingMusic()
    {
        StartCoroutine(MusicFade(startingMusic));
    }

    public void PlayLowLevelMusic()
    {
        StartCoroutine(MusicFade(lowLevelMusic));
    }

    public void PlayHighLevelMusic()
    {
        StartCoroutine(MusicFade(highLevelMusic));
    }

    public void PlayVeryHighLevelMusic()
    {
        StartCoroutine(MusicFade(veryHighLevelMusic));
    }

    public void PlayCreditsMusic()
    {
        StartCoroutine(MusicFade(creditsMusic));
    }

    IEnumerator MusicFade(AudioClip music)
    {
        for (int i = 0; i < 90; i++)
        {
            yield return new WaitForSeconds(0.02f);
            source.volume -= 0.01f;
        }
        source.clip = music;
        source.Play();
        for (int i = 0; i < 90; i++)
        {
            yield return new WaitForSeconds(0.02f);
            source.volume += 0.01f;
        }
    }

    IEnumerator MusicFadeOut(AudioClip music)
    {
        for (int i = 0; i < 90; i++)
        {
            yield return new WaitForSeconds(0.02f);
            source.volume -= 0.01f;
        }
        source.clip = music;
        source.Play();
        source.volume = 1f;
    }
}
