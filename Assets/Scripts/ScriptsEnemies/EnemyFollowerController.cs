using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowerController : MonoBehaviour
{
    public float visionRadius;
    public float speed;

    private GameObject player;
    private Vector3 initialPosition;
   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 target = initialPosition;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < visionRadius) target = player.transform.position;

        float fixedSpeed = speed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

        Debug.DrawLine(transform.position,target,Color.green);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //size = new Vector3(10f, 1f, 0.0f);
        Gizmos.DrawWireSphere(initialPosition,visionRadius);
    }
}
