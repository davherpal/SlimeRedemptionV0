using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDrop : MonoBehaviour
{
    // gameobject es empujado hacia arriab y elimina x segundos despues de su creacion.
    public float time2Destroy = 3.5f;
    public float lavaSpeed = 3f;
    private Rigidbody2D lavaDrop;

    private void Awake()
    {
        lavaDrop = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time2Destroy);
        lavaDrop.velocity = new Vector2(lavaDrop.velocity.x,  lavaSpeed);
    }

}
