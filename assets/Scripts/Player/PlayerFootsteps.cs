using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    // Audio
    private AudioSource audioSource;
    public AudioClip[] footstepSounds;
    private float minVolume;
    private float maxVolume;
    // Footstep functionality
    [HideInInspector]
    public float minTimeToPlayFootstep;
    private float timePassedBetweenFootsteps = 0f;
    // Object References
    private CharacterController characterController;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayFootsteps();
    }

    private void PlayFootsteps()
    {
        if (!characterController.isGrounded) return;

        // If player is moving
        if (characterController.velocity.sqrMagnitude > 0)
        {
            timePassedBetweenFootsteps += Time.deltaTime;

            if (timePassedBetweenFootsteps >= minTimeToPlayFootstep)
            {
                audioSource.volume = UnityEngine.Random.Range(minVolume, maxVolume);
                audioSource.clip = footstepSounds[UnityEngine.Random.Range(0, footstepSounds.Length)];
                audioSource.Play();

                timePassedBetweenFootsteps = 0f;
            }
            return;
        }

        // If player isn't moving
        timePassedBetweenFootsteps = 0f;
    }

    public void ChangeAudioValues(float minTimeToPlayFootstep, float minVolume, float maxVolume)
    {
        this.minTimeToPlayFootstep = minTimeToPlayFootstep;
        this.minVolume = minVolume;
        this.maxVolume = maxVolume;
    }
}
