using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDeletor : MonoBehaviour
{

    // Este Script elimina la room actual si el player esta muy lejos.

    private Transform player;
    [Tooltip("Numero de habitaciones de diferencia para que se borre")]
    public float roomsToDelete = 2;

    // Start is called before the first frame update
    void Start()
    {
        roomsToDelete = RoomController.ScreenHeight * roomsToDelete;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        // Si la habitacion esta por debajo de X habitaciones de distancia del player se eliminara la actual.
        if (player.position.y - roomsToDelete > transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
