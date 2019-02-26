using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeController : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed = 0f;
    //Variables del deslizamiento de pared
    public bool wallSliding;
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        /*
        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f,wallLayerMask);
            if (facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 01f)
            {
                if (wallCheck)
                {
                    HandleWallsSliding();
                }
            }
        }
        */
    }

    void HandleWallsSliding()
    {
        /*
        player.velocity = new Vector2(player.velocity.x, -0.7f);
        wallSliding = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {

            }
            else { }
        }
        */
    }
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, v, 0f);
        player.AddForce(movement * moveSpeed);
    }
}
