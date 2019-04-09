using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowerController : MonoBehaviour
{
    public float visionRadius;
    public float speed;

    private GameObject player;
    private Vector3 initialPosition;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;

    }


    void Update()
    {

        Vector3 target = initialPosition;
        //dist es un vector.distance que calcula la distancia entre los dos componentes que introduzcas
        float dist = Vector3.Distance(player.transform.position, transform.position);
        //En el caso que la distancia sea menor a la visionradius el gameobject se ira moviendo hacia el objetivo a la velocidad
        //puesta eb fixedSpeed
        if (dist < visionRadius) target = player.transform.position;

        float fixedSpeed = speed * Time.deltaTime;

        //Dibuja una linea entre el gameobject que contenga este escript y el objetivo
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

        Debug.DrawLine(transform.position, target, Color.green);
    }

    //Funcion para dibujar un circulo que sera del mismo tamaño que el rango que le pongamos al gameobject
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(initialPosition, visionRadius);
    }
}
