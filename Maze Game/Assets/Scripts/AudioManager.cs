using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--Audio Source--")]
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource playerSource;

    [SerializeField]
    private AudioSource coinSource;

    [SerializeField]
    private AudioSource enemySource;


    [Header("--Audio Clip--")]
    public AudioClip backgroundMusic;
    public AudioClip playerDeath;
    public AudioClip coinCollect;
    public AudioClip enemyGrowl;
    public AudioClip enemyChase;


    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        coinSource.PlayOneShot(audioClip);
    }

    // Play the growl sound (looped)
    public void PlayLoopingSFX(AudioClip audioClip)
    {
        enemySource.clip = audioClip;
        enemySource.loop = true; 
        enemySource.Play(); 
    }

    public void StopLoopingSFX()
    {
        enemySource.loop = false;
        enemySource.Stop();
    }

    // Stop the growl sound
    public void StopSFX(AudioClip audioClip)
    {
        if (audioClip == enemyGrowl)
        {
            enemySource.Stop(); // Stop playing the growl
        }
    }


}
