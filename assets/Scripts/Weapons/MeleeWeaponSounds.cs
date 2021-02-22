using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] attackClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomShootSound()
    {
        audioSource.clip = attackClips[UnityEngine.Random.Range(0, attackClips.Length - 1)];
        audioSource.Play();
    }
}
