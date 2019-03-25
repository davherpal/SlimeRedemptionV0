using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRUEBA : MonoBehaviour
{
    private Transform player;
    [Tooltip("Numero de habitaciones de diferencia para que se borre")]
    public float roomsToDelete = 2;

    // Start is called before the first frame update
    void Start()
    {
        roomsToDelete = 28 * roomsToDelete;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        // Si la habitacion esta por debajo de dos habitaciones de distancia del player se eliminara la actual.
        if (player.position.y - 56 > transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
