using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{

    // Este Script esta hecho para el 'hazard', que vaya subiendo poco a poco y si el player suba mucho este no se quede atras.

    [Tooltip("Velocidad de subida")]
    public float speed;
    [Tooltip("Numero de habitaciones de diferencia entre player - 'hazard' ")]
    public float roomsDistanceFromPlayer = 2;
    [Tooltip("Numero de habitaciones que suba el 'hazard'")]
    public float addRoomHeight = 0.5f;

    private float posUp;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        roomsDistanceFromPlayer = roomsDistanceFromPlayer * RoomController.ScreenHeight;
        addRoomHeight = addRoomHeight * RoomController.ScreenHeight;       
    }

    // Update is called once per frame
    void Update()
    {
        posUp = Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y + posUp, transform.position.z);


        // Si la habitacion esta por debajo de X habitaciones de distancia del player se movera X distancia hacia arriba. En escala de habitaciones
        if (player.position.y - roomsDistanceFromPlayer > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + addRoomHeight, transform.position.z);
        }
    }
}