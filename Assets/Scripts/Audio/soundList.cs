using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class soundList
{
    public string name;

    [Range(0f,1f)]
    public float volume;

    [Range(0f,3f)]
    public float pitch;
    [Range(0f, 3f)]
    public float volumeVariance;
    [Range(0f, 3f)]
    public float pitchVariance;

    public AudioClip clip;

    public bool Loop;

    [HideInInspector]public AudioSource source;

}
