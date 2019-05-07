using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVerticalSL : MonoBehaviour
{

    public Rigidbody2D rb2d;
    public Transform position;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb2d.position.y + 5 > rb2d.position.y)
        {
          
            //rb2d = new Vector2.MoveTowards(rb2d.position.x, rb2d.position.y);
        }
      
            
    }
}
