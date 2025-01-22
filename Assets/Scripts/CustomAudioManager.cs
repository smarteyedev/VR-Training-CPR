using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudioManager : MonoBehaviour
{
    public static CustomAudioManager Instance;

    [Header("Audio Libraries")]
    public List<AudioClip> musicClips;
    public List<AudioClip> sfxClips;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private Dictionary<string, AudioClip> musicLibrary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxLibrary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        /* if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } */

        InitializeLibraries();
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

    public void PlayMusic(string clipName)
    {
        PlayMusic(clipName, true);
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
        StartCoroutine(FadeOutMusic(1.0f)); // Set duration of fade-out to 1 second
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

    private IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume; // Reset volume to the original value
    }
}
