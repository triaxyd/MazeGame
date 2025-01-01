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
    public void PlaySFX(AudioClip audioClip, bool loop)
    {
        if (audioClip == enemyGrowl) // Check if it's the growl sound
        {
            enemySource.clip = audioClip;
            enemySource.loop = loop; // Set the loop flag
            enemySource.Play(); // Start playing the growl
        }
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
