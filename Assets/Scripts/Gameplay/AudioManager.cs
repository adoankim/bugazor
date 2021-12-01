using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    [SerializeField]
    List<AudioSource> audioSources;

    [SerializeField]
    AudioClip laserSound;

    [SerializeField]
    List<AudioClip> laserImpactSounds;

    [SerializeField]
    AudioClip playerImpactSound;

    [SerializeField]
    AudioClip healthRecoverSound;

    void Start()
    {
        if(_instance != null)
        {
            Destroy(_instance);
        }

        _instance = this;
    }

    public void PlayLaserSound()
    {
        PlaySound(0, laserSound);
    }

    public void PlayLaserImpactSound()
    {
        int impactIdx = Random.Range(0, laserImpactSounds.Count);
        PlaySound(1, laserImpactSounds[impactIdx]);
    }

    public void PlayEnemyExplodeSound()
    {
        int impactIdx = Random.Range(0, laserImpactSounds.Count);
        PlaySound(2, laserImpactSounds[impactIdx]);
    }

    public void PlayPlayerImpactSound()
    {
        PlaySound(3, playerImpactSound);
    }

    public void PlayHealthRecoverSound()
    {
        PlaySound(4, healthRecoverSound);
    }

    private void PlaySound(int i, AudioClip clip)
    {
        audioSources[i].clip = clip;
        audioSources[i].loop = false;
        audioSources[i].Play();
    }
}
