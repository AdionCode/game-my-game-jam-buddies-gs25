using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioData> audioList;

    private Dictionary<string, AudioData> audioDict;
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

     public static AudioManager Instance;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // opsional, jika ingin tetap hidup di scene berikutnya
        }
        else
        {
            Destroy(gameObject);
        }

        audioDict = new Dictionary<string, AudioData>();

        foreach (var audio in audioList)
        {
            if (!audioDict.ContainsKey(audio.audioName))
                audioDict.Add(audio.audioName, audio);
            else
                Debug.LogWarning($"Duplicate audio name: {audio.audioName}");
        }
    }

    private void Start()
    {
        PlayBGM("Main");
    }

    public void PlayBGM(string name)
    {
        if (audioDict.TryGetValue(name, out var audioData))
        {
            bgmSource.clip = audioData.clip;
            bgmSource.loop = audioData.loop;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM not found: {name}");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(string name)
    {
        if (audioDict.TryGetValue(name, out var audioData))
        {
            sfxSource.PlayOneShot(audioData.clip, audioData.volume);
        }
        else
        {
            Debug.LogWarning($"SFX not found: {name}");
        }
    }

    public void SetBGMVolume(Slider volume) => bgmSource.volume = volume.value;
    public void SetSFXVolume(Slider volume) => sfxSource.volume = volume.value;
}