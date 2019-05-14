using UnityEngine;
using UnityEngine.Audio;
using System;

public class audioController : MonoBehaviour
{
    public soundList[] sounds;
    public static audioController instance;

    private void Awake()
    {

        if (instance == null)
            instance = this;

        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (soundList s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.Loop;
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
        if (s == null)
        {
            Debug.Log("sound " + name + "not found");
            return;
        }
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        soundList s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        { Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.source.Stop();
    }
}
