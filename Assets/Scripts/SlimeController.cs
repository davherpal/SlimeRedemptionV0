using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float jumpPower;
    private bool isGround;

    private Rigidbody2D rb;
    public Transform checkGround;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int moreJumps;
    public int moreJumpsValue;

    // Start is called before the first frame update
    void Start()
    {
        moreJumps = moreJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsGround);

    }

    // Update is called once per frame
    void Update()
    {
        if (isGround == true)
        {
            moreJumps = moreJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && moreJumps > 0)
        {
            rb.velocity = Vector2.up * jumpPower;
            moreJumps--;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && moreJumps == 0 && isGround == true) {
            rb.velocity = Vector2.up * jumpPower;
        }
    }
}
