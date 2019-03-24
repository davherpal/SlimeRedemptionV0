using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Collider2D playerisOver;

    public LayerMask player;
    public float circleRadius = 0.2f;
    [Tooltip("Posicion por donde el player tendra que pasar para abatir enemigo")]
    public Transform deadPointTransform;
    [Tooltip("Tiempo para que la velocidad del player baje a 0")]
    public float timeFall = 10;

    private Vector3 deadPoint;

    private void Start()
    {
        deadPoint = deadPointTransform.position;
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerisOver = Physics2D.OverlapCircle(deadPoint, circleRadius, player);

        if (playerisOver)
        {
            GameController.instance.shoot = true;
            Destroy(gameObject);

            if (playerRb.velocity != Vector2.zero)
            {
                playerRb.velocity -= playerRb.velocity * Time.deltaTime * timeFall;
            }

        }
        
    }
}
