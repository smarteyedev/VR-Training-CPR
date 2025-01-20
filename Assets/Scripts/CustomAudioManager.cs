using System.Collections.Generic;
using UnityEngine;

public class CustomAudioManager : MonoBehaviour
{
    public static CustomAudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Libraries")]
    public List<AudioClip> musicClips;
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> musicLibrary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxLibrary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeLibraries();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeLibraries()
    {
        // Populate music library
        foreach (AudioClip clip in musicClips)
        {
            if (!musicLibrary.ContainsKey(clip.name))
                musicLibrary.Add(clip.name, clip);
        }

        // Populate SFX library
        foreach (AudioClip clip in sfxClips)
        {
            if (!sfxLibrary.ContainsKey(clip.name))
                sfxLibrary.Add(clip.name, clip);
        }
    }

    public void PlayMusic(string clipName, bool loop = true)
    {
        if (musicLibrary.TryGetValue(clipName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip '{clipName}' not found in the library.");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(string clipName)
    {
        if (sfxLibrary.TryGetValue(clipName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX clip '{clipName}' not found in the library.");
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}
