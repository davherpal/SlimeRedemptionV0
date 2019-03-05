using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Rigidbody2D prefBullet;
    public float distanceSpawn;
    public float bulletSpeed;
    public float distanceToShoot;

    private bool instanced;
    private Rigidbody2D bulletInstance;
    private Vector3 shootDirection;
    private Vector3 startPosition;
    private Vector3 vecDir;

    private LineRenderer ln;
    private bool drawLn;
    private Vector3 mousePos;
    private bool insideArea;




    // Start is called before the first frame update
    void Start()
    {
        ln = GetComponent<LineRenderer>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            drawLn = true;
            ln.SetPosition(0, startPosition + Vector3.forward * 10);                        // PRIMER PUNTO DE LA LINEA

            Vector3 offSet = startPosition - transform.position;
            float distanceTouch = (Mathf.Sqrt(offSet.x * offSet.x + offSet.y * offSet.y));  // DISTANCIA DEL TOQUE/CLICK AL GAMEOBJECT

            if (distanceTouch < distanceToShoot)                                            // SI LA DISTANCIA ES MENOR PODREMOS DISPARAR
            {
                insideArea = true;
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            drawLn = false;

            if (insideArea)                                                                 //INSTANCIAMOS BALA Y GUARDAMOS LA POSICION DONDE IRA
            {
                shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                vecDir = (shootDirection - startPosition).normalized;

                bulletInstance = Instantiate(prefBullet, transform.position + (vecDir * distanceSpawn), Quaternion.identity) as Rigidbody2D;

                 instanced = true;
                 insideArea = false;
            }
        }

        if (drawLn)                                                                         //DIBUJAMOS EL ULTIMO PUNTO DE LA LINEA
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);         //Se acualiza la linea
            ln.SetPosition(1, mousePos + Vector3.forward * 10);
        }
    }

    private void FixedUpdate()
    {
        if (instanced)                                                                       //LE DAMOS VELOCIDAD A LA BALA HACIA EL PUNTO ELEGIDO
        {
            bulletInstance.velocity = new Vector2(vecDir.x * bulletSpeed, vecDir.y * bulletSpeed);
            instanced = false;
        }
    }
}

