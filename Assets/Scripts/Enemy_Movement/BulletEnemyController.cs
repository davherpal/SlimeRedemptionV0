using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : MonoBehaviour
{
    public float time2Destroy = 3.5f;
    public float bulletSpeed;
    private Rigidbody2D bulletRB;

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time2Destroy);
        bulletRB.velocity = new Vector2(-bulletSpeed,bulletRB.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }
}
