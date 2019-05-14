using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemyMov : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 5f;
    public Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        rb2d.AddForce(Vector2.up * speed);
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.y,-maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(rb2d.velocity.x,limitedSpeed );
        
        if (rb2d.velocity.y > -0.01f && rb2d.velocity.y < 0.01f)
        {
            speed = -speed;
            rb2d.velocity = new Vector2(rb2d.velocity.x,speed);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "LimiterEnemy")
        {
            speed = -speed;
            rb2d.velocity = new Vector2(rb2d.velocity.x, speed);
        }
    }

}
