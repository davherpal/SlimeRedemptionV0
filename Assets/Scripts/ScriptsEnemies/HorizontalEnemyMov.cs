using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemyMov : MonoBehaviour
{

    public float speed=5;
    public float maxSpeed=5;
    public Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    //Funcion que hace que cada vez que la velocidad sea menor o mayor a 0.01, se cambie el sentido(el signo) del objeto
    private void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * speed);
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if(rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f)
        {
            speed = -speed;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
       
    }
    
}
