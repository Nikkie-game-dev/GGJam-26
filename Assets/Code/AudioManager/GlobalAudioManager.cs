using System;
using System.Collections.Generic;
using Code.Service;
using UnityEngine;

[System.Serializable]
public class GlobalAudioManager : IService
{
    [SerializeField]  private AudioSource _SFXAudioSource;
    [SerializeField]  private AudioSource _BGMAudioSource;
    private bool _isPersistance;

    public void PlayAudio(AudioData audioData)
    {
        switch (audioData.Type)
        {
            case AudioType.SFX:
                _SFXAudioSource.Stop();
                _SFXAudioSource.clip = audioData.Clip;
                _SFXAudioSource.Play();
                break;
            case AudioType.Music:
                _BGMAudioSource.Stop();
                _BGMAudioSource.clip = audioData.Clip;
                _BGMAudioSource.Play();
                break;
        }
    }

    bool IService.IsPersistance => _isPersistance;
}

[System.Serializable]
public class AudioData
{
    public string Name;
    public AudioClip Clip;
    public AudioType Type; 
}

public enum AudioType
{
    SFX, Music
}