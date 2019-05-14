using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ObstaculoCaida : MonoBehaviour
{
    public float time2Destroy = 4f;
    public string soundToPlay;
    public bool notsound;
    private Rigidbody2D rb2d;
    public GameObject go;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = go.GetComponent<Rigidbody2D>();
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
                if (!notsound)
                {
                    FindObjectOfType<audioController>().Play(soundToPlay);
                }
                once = false;
            }
            rb2d.isKinematic = false;
            Destroy(gameObject, time2Destroy);
        }     
    }
}
