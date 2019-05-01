using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ObstaculoCaida : MonoBehaviour
{
    public float time2Destroy = 4f;
    public AudioSource playAudio;
    private Rigidbody2D rb2d;
    public GameObject go;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = go.GetComponent<Rigidbody2D>();
        AudioSource playAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (once)
            {
                playAudio.Play();
                once = false;
            }
            rb2d.isKinematic = false;
            Destroy(gameObject, time2Destroy);
        }     
    }
}
