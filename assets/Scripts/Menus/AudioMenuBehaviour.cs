using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMenuBehaviour : MonoBehaviour
{
    public AudioMixer mainAudioMixer;

    public void ChangeMasterVolume(float value)
    {
        mainAudioMixer.SetFloat("Master", value);
    }

    public void ChangeAmbienceVolume(float value)
    {
        mainAudioMixer.SetFloat("Ambience", value);
    }

    public void ChangeEnemiesVolume(float value)
    {
        mainAudioMixer.SetFloat("Enemies", value);
    }

    public void ChangeEventsVolume(float value)
    {
        mainAudioMixer.SetFloat("Events", value);
    }

    public void ChangeMusicVolume(float value)
    {
        mainAudioMixer.SetFloat("Music", value);
    }

    public void ChangePlayerVolume(float value)
    {
        mainAudioMixer.SetFloat("Player", value);
    }

    public void ChangeWeaponsVolume(float value)
    {
        mainAudioMixer.SetFloat("Weapons", value);
    }

}
