using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] spottedSounds;
    public AudioClip[] attackSounds;
    public AudioClip[] deadSounds;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpottedSound()
    {
        PlayRandomClip(spottedSounds);
    }
    public void PlayAttackSound()
    {
        PlayRandomClip(attackSounds);
    }
    public void PlayDeadSound()
    {
        PlayRandomClip(deadSounds);
    }

    private void PlayRandomClip(AudioClip[] audioClips)
    {
        audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length - 1)];
        audioSource.Play();
    }
}
