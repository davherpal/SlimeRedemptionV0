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
    private bool stop;

    private Rigidbody2D rb;
    public Transform checkGround;
    public Transform checkRight;
    public Transform checkLeft;
    public float checkRadius;
    public float checkRadiusGround;
    public Vector3 jumpVector;
    public Vector3 doubleJumpVector;
    public LayerMask whatIsGround;
    public LayerMask whatIsRight;
    public LayerMask whatIsLeft;
    private int moreJumps;
    public int moreJumpsValue;

    
    private float speed2;
    public float aceleracion = 2f;

    // Start is called before the first frame update
    void Start()
    {
        stop = true;
        moreJumps = moreJumpsValue;
        rb = GetComponent<Rigidbody2D>();

        //speed2 = speed;

    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(checkGround.position, checkRadiusGround, whatIsGround);
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
            //speed = speed2;
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
                /*
                if (!isGround)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * -speed);
                    speed += aceleracion * Time.deltaTime;
                }
                */

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
                /*
                if (!isGround)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * -speed);
                    speed += aceleracion * Time.deltaTime;
                }
                */
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
            stop = false;
            //speed = speed2;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && moreJumps == 0 && isGround == true)
        {
            StopCoroutine(stickToWalls());
            rb.velocity = Vector2.up * jumpPower * 2;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && moreJumps == 0 && isRight == true)
        {
            StopCoroutine(stickToWalls());
            rb.velocity = Vector2.up * jumpPower * 2;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && moreJumps == 0 && isLeft == true)
        {
            StopCoroutine(stickToWalls());
            rb.velocity = Vector2.up * jumpPower * 2;
        }
    }

    IEnumerator stickToWalls()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(2);
        //transform.Translate(Vector3.up * Time.deltaTime * -speed);
        //Debug.Log("fuckoff");
    }
}