using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Music,
    Announcement,
    Sfx,
    Ballista,
    Traps
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip soundFile;
    public AudioType type;

    private AudioSource audioSource;

    [Space]
    [Range(0, 1)]
    public float volume = 0.7f;
    public bool randomizeVol;
    [Range(0.4f, 1)]
    public float minVolume = 0.5f;

    [Space]
    [Range(0.5f, 1.5f)]
    public float pitch = 1;
    public bool randomizePitch;
    [Range(0.5f, 1.5f)]
    public float minPitch = 0.5f;

    public void SetAudioSource(AudioSource _audioSource)
    {
        audioSource = _audioSource;
    }

    public void Play()
    {
        float _volume = randomizeVol ? Random.Range(minVolume, volume) : volume;
        audioSource.pitch = randomizePitch ? Random.Range(minPitch, pitch) : pitch;

        audioSource.PlayOneShot(soundFile, _volume);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}

public class AudioManagerScript : MonoBehaviour
{
    [Header("Audio Source References")]
    [Space]
    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource announcementAudioSource, sFXAudioSource, ballistaAudioSource, trapsAudioSource;

    [Header("Sounds")]
    [Space]
    [SerializeField]
    private Sound[] sounds;

    public static AudioManagerScript instance;

    private void Awake()
    {
        instance = this;

        if (instance == this)
        {
            foreach (Sound sound in sounds)
            {
                switch (sound.type)
                {
                    case AudioType.Music:
                        sound.SetAudioSource(musicAudioSource);
                        break;
                    case AudioType.Sfx:
                        sound.SetAudioSource(announcementAudioSource);
                        break;
                    case AudioType.Announcement:
                        sound.SetAudioSource(sFXAudioSource);
                        break;
                    case AudioType.Ballista:
                        sound.SetAudioSource(ballistaAudioSource);
                        break;
                    case AudioType.Traps:
                        sound.SetAudioSource(trapsAudioSource);
                        break;
                    default:
                        Debug.LogError("Type " + sound.type + " not found.");
                        break;
                }
            }
        }
    }

    public void PlaySound(string soundName, string requestAuthor)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == soundName)
            {
                sound.Play();
                return;
            }
        }
        Debug.LogError(soundName + " not found, please name sounds correctly. Source: " + requestAuthor + ".");
    }

    public void StopSound(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == soundName)
            {
                sound.Stop();
                return;
            }
        }
        Debug.LogError(soundName + " not found, please name sounds correctly.");
    }

    public void StopAllSounds()
    {
        musicAudioSource.Stop();
        announcementAudioSource.Stop();
        sFXAudioSource.Stop();
        ballistaAudioSource.Stop();
        trapsAudioSource.Stop();
    }

    public bool CheckMusicPlaying()
    {
        return musicAudioSource.isPlaying;
    }

    public void DisableSound(bool enabled)
    {
        musicAudioSource.mute = enabled;
        announcementAudioSource.mute = enabled;
        sFXAudioSource.mute = enabled;
        ballistaAudioSource.mute = enabled;
        trapsAudioSource.mute = enabled;
    }

    public void UpdateSfxVolume(float newVolume)
    {
        announcementAudioSource.volume = newVolume;
        sFXAudioSource.volume = newVolume;
        ballistaAudioSource.volume = newVolume;
        trapsAudioSource.volume = newVolume;
    }

    public void UpdateMusicVolume(float newVolume)
    {
        musicAudioSource.volume = newVolume;
    }

    //public bool GetAudioState()
    //{
    //    if (musicAudioSource.mute == true) return false;
    //    else return true;
    //}

    public float GetMusicVolume()
    {
        return musicAudioSource.volume;
    }

    public float GetSFxVoluma()
    {
        return sFXAudioSource.volume;
    }
}