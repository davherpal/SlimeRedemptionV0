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
    [HideInInspector] public bool isRight;
    [HideInInspector] public bool isLeft;
    [HideInInspector] public bool stop;
    [HideInInspector] public bool isIce;

    private Rigidbody2D rb;
    public Transform checkGround;       //gameobjects que detectaran la derecha, izquierda y suelo
    public Transform checkRight;
    public Transform checkLeft;
    public float checkRadius;           // el radio de esos gameobjects a la hora de detectar
    public float checkRadiusGround;     // detectara el radio del suelo  
    public Vector3 jumpVector;          // discta el vector de salto del slime
    public LayerMask whatIsGround;      // las leyermask que definen parez izquierda, derecha,hielo y suelo
    public LayerMask whatIsRight;
    public LayerMask whatIsLeft;
    public LayerMask whatIsIce;
    private int moreJumps;              // variable privada que traduce tus saltos restantes
    public int moreJumpsValue;          // int publica que dicta cuantos saltos extras puedes ahcer
    public float slipMultiplier;        // multiplicador de velocidad al resbalarse por las paredes
    private bool jump;                  // booleano que se activa al saltar            
    public float aceleracion = 2f;      // constante que se va multiplicando a la velocidad al resbalarse
    private float slipMultiplierConstant;       // variable privada, dicta el slip multiplayer
    public float slipIce;               // contante de resbalarse en el hielo
    private float counterJump;          // contador entre saltos
    public float betweenJumps;              
    private bool nextJump;      // discta cuando el slime esta preparado apra el segundo salto
    private bool almostStop;    // booleano dedicado a parar (en proceso)
    private bool counterBetweenJumps;

    // Start is called before the first frame update
    void Start()            //al empezar spot estara en true, sino al tocar las apredes no tendremos el lapso de tiempo en el que estamos quietos
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
        isIce = Physics2D.OverlapCircle(checkGround.position, checkRadius, whatIsIce);

        if (jump)       //al saltar, se le añade una velocidad y fuerza al slime, ademas que se resta un morejumps, asi que tienes un salto menos por hacer hasta que toques una apred o el suelo, el stop se pone en false y el jump tambien para que no se siga añadiendo fuerza
        {
            rb.velocity = Vector2.up * jumpPower;
            rb.AddForce(jumpVector * jumpPower, ForceMode2D.Impulse);
            moreJumps--;
            stop = false;
            jump = false;
        }

        if (almostStop)         // en proceso
        {

        }


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(jump);
        //Debug.Log(counterJump);
        if (!isRight && !isLeft && !isIce)                        // si estas en el aire, stop es true y el contador y la velocidad es igual a 0 y stop sera falso
        {
            stop = true;
            speed = 0;
            counter = 0;
        }

        if (isGround == true)                           // si estas en el suelo, el siguiente salto sera true para poder slatar cuando quieras, se reinicia el slipmultiplayer y se reinicia el morejumps
        {
            nextJump = true;
            slipMultiplierConstant = slipMultiplier;
            moreJumps = moreJumpsValue;
            //Debug.Log("contacto suelo");
        }

        if (isRight == true)                            // si estas en la pared derecha, se cambia la direccion del siguiente salto y se reinician el numero de saltos que tienes
        {
            nextJump = true;
            slipMultiplierConstant = slipMultiplier;
            jumpVector[0] = -3f;
            moreJumps = moreJumpsValue;
            //Debug.Log("contacto derecha");
            if (stop)                                   // si stop es igual a true: el contador empezara a rodar y la velocidad del slime sera 0, haciendo que se resbale my poco a poco
            {
                counter += Time.deltaTime;
                rb.velocity = Vector3.zero;
                if (counter > timeToSlip)       // si el contador se pasa de la variable estimada, el slime dejara de estar quieto
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
            slipMultiplierConstant = slipMultiplier;
            jumpVector[0] = 3f;
            moreJumps = moreJumpsValue;
            //Debug.Log("contacto izquierda");
            if (stop)                               // si stop es igual a true: el contador empezara a rodar y la velocidad del slime sera 0, haciendo que se resbale my poco a poco
            {
                counter += Time.deltaTime;
                rb.velocity = Vector3.zero;
                if (counter > timeToSlip)           // si el contador se pasa de la variable estimada, el slime dejara de estar quieto
                {
                    if (!isGround)                  // si el contador es mayor a timetoslip y no toca el suelo, el slime tendra una velocidad hacia abajo mayor, que se ira incrementando poco a poco
                    {
                        transform.Translate(Vector3.up * Time.deltaTime * -speed * slipMultiplierConstant);
                        speed += aceleracion * Time.deltaTime;
                    }
                }
            }
        }

        if (isIce == true)          // si estas en el hielo
        {
            nextJump = true;           // podras saltar de inmediato
            slipMultiplierConstant = slipIce;       // la constante de slipmultiplayer sera igual a slipice, pues es necesario un valor diferente para recrear una pared mas resbaladiza
            jumpVector[0] = -3f;
            moreJumps = moreJumpsValue;             // se reinician los saltos extra
            //Debug.Log("contacto derecha");
            if (stop)                                   // si stop es igual a true: el contador empezara a rodar y la velocidad del slime sera 0, haciendo que se resbale my poco a poco
            {
                counter += Time.deltaTime;
                rb.velocity = Vector3.zero;
                if (counter > timeToSlip)               // si el contador se pasa de la variable estimada, el slime dejara de estar quieto
                {             // si el contador es mayor a timetoslip y no toca el suelo, el slime tendra una velocidad hacia abajo mayor, que se ira incrementando poco a poco
                    if (!isGround)
                    {
                        transform.Translate(Vector3.up * Time.deltaTime * -speed * slipMultiplierConstant);      // si el contador es mayor a timetoslip y no toca el suelo, el slime tendra una velocidad hacia abajo mayor, que se ira incrementando poco a poco
                        speed += aceleracion * Time.deltaTime;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && moreJumps > 0 && nextJump)       // salto
        {
            jump = true;
            counterBetweenJumps = true;     // se accionan el salto y el contador para el siguiente salto
        }

        if (counterBetweenJumps)        // cuando el contador haya alcanzado el valor deseado, se podra volver a saltar
        {
            counterJump = 0;
            counterJump += Time.deltaTime;
            if (counterJump > betweenJumps)
            {
                nextJump = true;
                counterBetweenJumps = false;
            }
        }
    }
}

