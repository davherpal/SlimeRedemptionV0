using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

    private Collider2D playerisOver;
    private Vector2 deadPoint;
    private float distanceOver = .5f;
    private float circleRadius = 0.2f;
    public LayerMask player;

    // Update is called once per frame
    void Update()
    {

        deadPoint = new Vector2(transform.position.x, transform.position.y + distanceOver);
        playerisOver = Physics2D.OverlapCircle(deadPoint, circleRadius, player);

        if (playerisOver)
        {
            //playerisOver.GetComponent<Rigidbody2D>().gravityScale = 5;
            GameController.instance.shoot = true;
            Destroy(gameObject);
        }
        
    }
}
