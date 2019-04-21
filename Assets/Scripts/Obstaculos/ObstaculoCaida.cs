using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoCaida : MonoBehaviour
{
    public float time2Destroy = 4f;
    private Rigidbody2D rb2d;
    public GameObject go;
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
            rb2d.isKinematic = false;
            Destroy(gameObject, time2Destroy);
        }

        
    }

}
