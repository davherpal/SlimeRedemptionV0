using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

    // Este script se encarga de la mecanica de ingerir enemigo si esta encima y gana 1 disparo.

    private Rigidbody2D playerRb;
    private Collider2D playerisOver;
    private Collider2D playerisUnder;

    public LayerMask player;
    public float circleRadius = 0.2f;
    [Tooltip("Posicion por donde el player tendra que pasar para abatir enemigo")]
    public Transform deadPointTransUp;
    public Transform deadPointTransDown;
    [Tooltip("Tiempo para que la velocidad del player baje a 0 despues de comer algo")]
    public float timeFall = 10;

    public int score = 100; 

    private Vector3 deadPointUp;
    private Vector3 deadPointDown;

    private void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        deadPointUp = deadPointTransUp.position;
        deadPointDown = deadPointTransDown.position;

        playerisOver = Physics2D.OverlapCircle(deadPointUp, circleRadius, player);
        playerisUnder = Physics2D.OverlapCircle(deadPointDown, circleRadius, player);

        // Si esta encima se muere y el player gana un disparo
        if (playerisOver || playerisUnder)
        {
            GameController.instance.AddScore(score);
            GameController.instance.setShoot(true);
            
            Destroy(gameObject);

            if (playerRb.velocity != Vector2.zero)
            {
                playerRb.velocity -= playerRb.velocity * Time.deltaTime * timeFall;
            }

        }
        
    }

    // Si recibe un disparo por parte del enemigo, se muere

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet"){

            Destroy(gameObject);
        }
    }
}
