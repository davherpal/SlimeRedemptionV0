using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    private float counter;
    public float timeToSlip;
    [HideInInspector] public bool isGround;
    [HideInInspector] public bool isRight;
    [HideInInspector] public bool isLeft;
    [HideInInspector] public bool stop;

    private Rigidbody2D rb;
    public Transform checkGround;
    public Transform checkRight;
    public Transform checkLeft;
    public float checkRadius;
    public float checkRadiusGround;
    public Vector3 jumpVector;
    public LayerMask whatIsGround;
    public LayerMask whatIsRight;
    public LayerMask whatIsLeft;
    private int moreJumps;
    public int moreJumpsValue;
    public float slipMultiplier;
    private bool jump;
    private bool secondjump;
    public float aceleracion = 2f;
    private float slipMultiplierConstant;
    public float slipIce;
    private float counterJump;
    public float betweenJumps;
    private bool isRightOrLeft;
    private bool jumpOnFloor;
    private bool nextJump;

    // Start is called before the first frame update
    void Start()
    {
        stop = true;
        slipMultiplierConstant = slipMultiplier;
        moreJumps = moreJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()      // en fixed update se denominara cuando el slime esta en la pared derecha, en la izquierda o en el suelo
    {
        isGround = Physics2D.OverlapCircle(checkGround.position, checkRadiusGround, whatIsGround);
        isRight = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsRight);
        isLeft = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsLeft);

        if (jump)
        {
            rb.velocity = Vector2.up * jumpPower;
            rb.AddForce(jumpVector * jumpPower, ForceMode2D.Impulse);
            moreJumps--;
            stop = false;
            jump = false;
        }

        if (isRightOrLeft)
        {
            rb.velocity = Vector2.up * jumpPower * 2.5f;
            isRightOrLeft = false;
        }

        if (jumpOnFloor)
        {
            rb.velocity = Vector2.up * jumpPower;
            jumpOnFloor = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(counterJump);
        if (!isRight && !isLeft)                        // si estas en el aire, stop es true y el contador y la velocidad es igual a 0
        {
            stop = true;
            speed = 0;
            counter = 0;
        }

        if (isGround == true)                           // si estas en el suelo
        {
            nextJump = true;
            moreJumps = moreJumpsValue;
            //Debug.Log("contacto suelo");
        }

        if (isRight == true)                            // si estas en la pared derecha, se cambia la direccion del siguiente salto y se reinician el numero de saltos que tienes
        {
            nextJump = true;
            jumpVector[0] = -2.3f;
            moreJumps = moreJumpsValue;
            //Debug.Log("contacto derecha");
            if (stop)                                   // si stop es igual a true: el contador empezara a rodar y la velocidad del slime sera 0, haciendo que se resbale my poco a poco
            {
                counter += Time.deltaTime;
                rb.velocity = Vector3.zero;
                if (counter > timeToSlip)
                {             // si el contador es mayor a timetoslip y no toca el suelo, el slime tendra una velocidad hacia abajo mayor, que se ira incrementando poco a poco
                    if (!isGround)
                    {
                        transform.Translate(Vector3.up * Time.deltaTime * -speed * slipMultiplierConstant);
                        speed += aceleracion * Time.deltaTime;
                    }
                }
            }
        }

        if (isLeft == true)                         // si estas en la pared izquierda, se cambia la direccion del siguiente salto y se reinician el numero de saltos que tienes
        {
            nextJump = true;
            jumpVector[0] = 2.3f;
            moreJumps = moreJumpsValue;
            //Debug.Log("contacto izquierda");
            if (stop)                               // si stop es igual a true: el contador empezara a rodar y la velocidad del slime sera 0, haciendo que se resbale my poco a poco
            {
                counter += Time.deltaTime;
                rb.velocity = Vector3.zero;
                if (counter > timeToSlip)
                {
                    if (!isGround)                  // si el contador es mayor a timetoslip y no toca el suelo, el slime tendra una velocidad hacia abajo mayor, que se ira incrementando poco a poco
                    {
                        transform.Translate(Vector3.up * Time.deltaTime * -speed * slipMultiplierConstant);
                        speed += aceleracion * Time.deltaTime;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && moreJumps > 0 && nextJump)       // salto
        {
            counterJump = 0;
            nextJump = false;
            jump = true;
            counterJump = Time.deltaTime;
            if(counterJump > betweenJumps)
            {
                nextJump=true;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Space) && moreJumps == 0 && isGround == true)     // saltar en el suelo
        {
            jumpOnFloor = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            Debug.Log("ice ice baby");
            slipMultiplierConstant = slipIce;
        }

   
    }
}