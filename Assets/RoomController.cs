using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    //CADA HABITACION TIENE 16 DE ANCHO Y 28 DE ALTO

    public GameObject startRoomPosition;
    public Transform player;

    private Vector2 spawnPoint;
    private Vector2 PosRoom;
    private float heightNewSpawn = 28;

    public float distance2SpawnFromPlayer = 5;

    public GameObject[] rooms;
    private int rand;

    // Start is called before the first frame update

    void Start()
    {
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

            PosRoom.y += heightNewSpawn;
            Instantiate(rooms[rand], PosRoom, Quaternion.identity);
            spawnPoint.y += heightNewSpawn;
        }
    }  
}
