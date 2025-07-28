using UnityEngine;

[System.Serializable]
public class AudioData
{
    public string audioName;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool loop = false;
}