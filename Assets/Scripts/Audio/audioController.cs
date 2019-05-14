using UnityEngine;
using UnityEngine.Audio;
using System;

public class audioController : MonoBehaviour
{
    public soundList[] sounds;

    private void Awake()
    {
        foreach (soundList a in sounds)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.loop = a.Loop;
            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
        }
    }

    void Start()
    {
        Play("music1");
    }

    // Update is called once per frame
    public void Play(string name)
    {
        soundList s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
