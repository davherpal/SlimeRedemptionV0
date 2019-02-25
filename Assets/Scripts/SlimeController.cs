using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    private bool isGround;
    private bool isRight;
    private bool isLeft;
    public bool stop;

    private Rigidbody2D rb;
    public Transform checkGround;
    public Transform checkRight;
    public Transform checkLeft;
    public float checkRadius;
    public Vector3 jumpVector;
    public LayerMask whatIsGround;
    public LayerMask whatIsRight;
    public LayerMask whatIsLeft;
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
        isRight = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsRight);
        isLeft = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsLeft);
    }

    // Update is called once per frame
    void Update()
    {

        if (!isRight && !isLeft)
        {
            stop = true;
        }

        if (isGround == true)
        {
            moreJumps = moreJumpsValue;
            Debug.Log("contacto suelo");
        }

        if (isRight == true)
        {
            jumpVector[0] = -2;
            moreJumps = moreJumpsValue;
            Debug.Log("contacto derecha");
            if (stop)
            {
                StartCoroutine(stickToWalls());
                transform.Translate(Vector3.up * Time.deltaTime * -speed);
            }
        }

        if (isLeft == true)
        {
            jumpVector[0] = 2;
            moreJumps = moreJumpsValue;
            Debug.Log("contacto izquierda");
            if (stop)
            {
                StartCoroutine(stickToWalls());
                transform.Translate(Vector3.up * Time.deltaTime * -speed);
            }
        }

        if (!isRight && !isLeft)
        {
            stop = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && moreJumps > 0)
        {
            rb.AddForce(jumpVector * jumpPower, ForceMode2D.Impulse);
            moreJumps--;
            stop = false; ;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && moreJumps == 0 && isGround == true)
        {
            rb.velocity = Vector2.up * jumpPower;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && moreJumps == 0 && isRight == true)
        {
            rb.velocity = Vector2.up * jumpPower;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && moreJumps == 0 && isLeft == true)
        {
            rb.velocity = Vector2.up * jumpPower;
        }
    }

    IEnumerator stickToWalls()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(2);
    }
}