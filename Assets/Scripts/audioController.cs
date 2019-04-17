using UnityEngine;
using UnityEngine.Audio;
using System;

public class audioController : MonoBehaviour
{
    public soundList[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (soundList s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    void Start()
    {
        Play("music1");
    }

    // Update is called once per frame
    public void Play(string name)
    {
        soundList s = Array.Find(sounds,sound => sound.name == name);
        s.source.Play();
        Debug.Log("ok");
    }
}
