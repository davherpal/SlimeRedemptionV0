using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed;     //dicta la velocidad del slime
    public float jumpPower;     // fuerza del salto
    private float counter;      //contador para dictar en cuanto tiempo el slime dejara de estar quieto en las paredes
    public float timeToSlip;
    [HideInInspector] public bool isGround;     //booleanos que dictaran si el slime detecta si esta en contacto con el suelo, la izqueirda, la derecha o detenido
    [HideInInspector] public bool stop;
    [HideInInspector] public bool isIce;
    [HideInInspector] public bool isWall;
    [HideInInspector] public bool isStickyWall;
    [HideInInspector] public bool isEnemy;

    private Rigidbody2D rb;
    private SpriteRenderer sp;
    public Transform checkGround;       //gameobjects que detectaran la derecha, izquierda y suelo
    public float checkRadius;           // el radio de esos gameobjects a la hora de detectar
    public Vector3 jumpVector;          // discta el vector de salto del slime
    public LayerMask whatIsGround;      // las leyermask que definen parez izquierda, derecha,hielo y suelo
    public LayerMask whatIsIce;
    public LayerMask whatIsWall;
    public LayerMask whatIsSticky;
    public LayerMask whatIsEnemy;
    //public LayerMask whatIsEnemy;
    [HideInInspector]public int moreJumps;              // variable privada que traduce tus saltos restantes
    public int moreJumpsValue;          // int publica que dicta cuantos saltos extras puedes ahcer
    private bool jump;                  // booleano que se activa al saltar            
    public float speedMultiplier = 2f;      // constante que se va multiplicando a la velocidad al resbalarse
    public float betweenJumps;
    private bool nextJump;      // dicta cuando el slime esta preparado apra el segundo salto
    private bool slip;
    public float timeWall;
    public float timeIce;
    public float timeSticky;
    private float slipMultiplier;        // multiplicador de velocidad al resbalarse por las paredes
    public float slipIce;               // contante de resbalarse en el hielo
    public float slipMultiplierWall;
    public float slipSticky;
    private bool changeDirection;
    public float currentVelocity;
    public CircleCollider2D col;

    private bool jumpButtonPressed;

    // Start is called before the first frame update
    void Start()            //al empezar spot estara en true, sino al tocar las apredes no tendremos el lapso de tiempo en el que estamos quietos
    {
        stop = true;
        moreJumps = moreJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()      // en fixed update se denominara cuando el slime esta en la pared derecha, en la izquierda o en el suelo
    {
        isGround = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsGround);
        isIce = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsIce);
        isWall = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsWall);
        isStickyWall = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsSticky);
        isEnemy = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsEnemy);

        if (jump)       //al saltar, se le añade una velocidad y fuerza al slime, ademas que se resta un morejumps, asi que tienes un salto menos por hacer hasta que toques una apred o el suelo, el stop se pone en false y el jump tambien para que no se siga añadiendo fuerza
        {
            rb.velocity = Vector2.up * jumpPower;
            rb.AddForce(jumpVector * jumpPower, ForceMode2D.Impulse);
            moreJumps--;
            stop = false;
            jump = false;
            slip = false;
            jumpButtonPressed = false;
        }

        if (slip)
        {
            nextJump = true;
            changeDirection = true;
            moreJumps = moreJumpsValue;
            if (stop)                               // si stop es igual a true: el contador empezara a rodar y la velocidad del slime sera 0, haciendo que se resbale my poco a poco
            {
                counter += Time.deltaTime;
                rb.velocity = Vector3.zero;
                if (counter > timeToSlip)           // si el contador se pasa de la variable estimada, el slime dejara de estar quieto
                {
                    if (!isGround)                  // si el contador es mayor a timetoslip y no toca el suelo, el slime tendra una velocidad hacia abajo mayor, que se ira incrementando poco a poco
                    {
                        transform.Translate(Vector3.up * Time.deltaTime * -speed * slipMultiplier);
                        speed += speedMultiplier * Time.deltaTime;
                    }
                }
            }
        }

        if (changeDirection)
        {
            if (currentVelocity > 0)
            {
                jumpVector[0] = -3;
                sp.flipX = false;

            }
            else
            {
                jumpVector[0] = 3;
                sp.flipX = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWall)
        {
            timeToSlip = timeWall;
            col.usedByEffector = true;
            slipMultiplier = slipMultiplierWall;
            slip= true;
        }

        if (isIce)
        {
            timeToSlip = timeIce;
            col.usedByEffector = true;
            slipMultiplier = slipIce;
            slip = true;
        }
        
        if (isStickyWall)
        {
            timeToSlip = timeSticky;
            col.usedByEffector = true;
            slipMultiplier = slipSticky;
            slip = true;
        }

        if (isEnemy)
        {
            Debug.Log("isEnemy");
            changeDirection = true;
            moreJumps = moreJumpsValue;
        }
        
        if (!isIce && !isWall && !isStickyWall && !isEnemy)                        // si estas en el aire, stop es true y el contador y la velocidad es igual a 0 y stop sera falso
        {
            currentVelocity = rb.velocity.x;
            col.usedByEffector = false;
            stop = true;
            speed = 0;
            counter = 0;
            slip = false;
            changeDirection = false;
        }

        if (isGround)                           // si estas en el suelo, el siguiente salto sera true para poder slatar cuando quieras, se reinicia el slipmultiplayer y se reinicia el morejumps
        {
            nextJump = true;
            moreJumps = moreJumpsValue;
            slipMultiplier = slipMultiplierWall;
            //Debug.Log("contacto suelo");
            slip = false;
            stop = true;
        }

        if(isGround && isWall)
        {
            changeDirection = true;
        }

        if (jumpButtonPressed && moreJumps > 0 && nextJump)       // salto
        {
            FindObjectOfType<audioController>().Play("jumpSoundEffect");
            jump = true;
        }
    }

    public void JumpButtonPressed()
    {
        if (moreJumps > 0 && nextJump)
        {
            jumpButtonPressed = true;

        }
    }
}
