using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
   // Altura como ancho de la pantalla ascesible desde cualquier script
    [HideInInspector] public static float ScreenHeight;
    [HideInInspector] public static float ScreenWidth;

    public GameObject startRoomPosition;
    public Transform player;

    private Vector2 spawnPoint;
    private Vector2 PosRoom;

    //A que altura del nivel se spawneara el siguiente nivel.
    public float distance2SpawnFromPlayer = 5;

    public GameObject[] rooms;
    private int rand;

    // Calculamos Altura como ancho, antes que todo.
    private void Awake()
    {
       // player = GameObject.FindGameObjectWithTag("Player").transform;
        ScreenHeight = 2f * Camera.main.orthographicSize;
        ScreenWidth = ScreenHeight * Camera.main.aspect;
    }

    void Start()
    {
        //Asignamos donde estara el spawnPoint de la siguiente habitacion
        spawnPoint = player.transform.position;
        spawnPoint.y += distance2SpawnFromPlayer;

        PosRoom = startRoomPosition.transform.position;

        rand = Random.Range(0, rooms.Length);
        Instantiate(rooms[rand], PosRoom, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        // Si posicion player mayor que spawnPoint, spawn nueva habitacion encima y movemos SpawnPoint a la posicion correspondiente de la habitacion.
        if(player.position.y >= spawnPoint.y)
        {
            rand = Random.Range(0, rooms.Length);

            PosRoom.y += ScreenHeight;
            Instantiate(rooms[rand], PosRoom, Quaternion.identity);
            spawnPoint.y += ScreenHeight;
        }
    }  
}
