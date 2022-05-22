using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundProvider : MonoBehaviour
{
    [SerializeField] private SoundAndSource[] _soundsAndSources;

    private Dictionary<SoundType, AudioSource> _soundAndSourcesDictionary = new Dictionary<SoundType, AudioSource>();

    private void Start()
    {
        foreach (SoundAndSource soundAndSource in _soundsAndSources)
        {
            _soundAndSourcesDictionary[soundAndSource.soundType] = soundAndSource.audioSource;
        }
    }

    public void PlayRandomSound(SoundType soundType, AudioClip[] clips)
    {
        int rand = UnityEngine.Random.Range(0, clips.Length);
        PlaySound(soundType, clips[rand]);
    }

    public void PlaySound(SoundType soundType, AudioClip clip)
    {
        AudioSource source = _soundAndSourcesDictionary[soundType];
        source.clip = clip;
        source.Play();
    }

}

[Serializable]
public struct SoundAndSource
{
    public SoundType soundType;
    public AudioSource audioSource;
}