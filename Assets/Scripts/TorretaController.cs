using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaController : MonoBehaviour
{
    public Transform target;
    public float rotationSpeedDeg = 90f;
    public float shootImpulso = 10;
    public Transform spawnPoint;
    public GameObject laser;
    public float visionRadius;
    public int cadaSegundos = 2;
   

    private GameObject player;
    private Vector3 initialPosition;
    private Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 1, cadaSegundos);
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < visionRadius)
        {

            Vector3 delta = target.position - transform.position;
            float angle = Mathf.Atan2(delta.y, delta.x);
            Quaternion rotZ = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);


            rotZ = Quaternion.RotateTowards(transform.rotation, rotZ, rotationSpeedDeg * Time.deltaTime);

            transform.rotation = rotZ;

            //Vector3 unitario = delta/ delta.magnitude;
            Vector3 unitario = delta.normalized;
            force = unitario * shootImpulso;
        }

    }
    void Shoot()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
          
        if (dist < visionRadius)
        {

            GameObject laserGo = Instantiate(laser);
            laserGo.transform.position = spawnPoint.position;
            laserGo.transform.rotation = spawnPoint.rotation;
            laserGo.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
       
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(initialPosition, visionRadius);
    }
}
