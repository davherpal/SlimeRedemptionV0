using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    // Altura como ancho de la pantalla ascesible desde cualquier script
    [HideInInspector] public static float ScreenHeight;
    [HideInInspector] public static float ScreenWidth;

    public Transform player;

    // A partir de que posicion spawnear nueva habitacion
    private Vector2 spawnPoint;

    // Donde spawnear habitacion
    private Vector2 PosRoom;

    // Posicion spawn primera habitacion
    public GameObject startRoomPosition;

    // Distancia inicial del spawnPoint al player
    public float distance2SpawnFromPlayer = 5;
    // Cada cuentas habitaciones empieza nuevo bioma
    public float numberRoomNextBioma = 5;

    public GameObject[] bioma1;
    public GameObject[] bioma2;
    public GameObject[] bioma3;

    private int rand;
    private int spawnedRooms;

    public enum bioma {bioma1, bioma2, bioma3 }
    private bioma currentBioma;

    // Calculamos Altura como ancho, antes que todo.
    private void Awake()
    {
        ScreenHeight = 2f * Camera.main.orthographicSize;
        ScreenWidth = ScreenHeight * Camera.main.aspect;
    }

    void Start()
    {
        //Asignamos donde estara el spawnPoint de la siguiente habitacion
        spawnPoint = player.transform.position;
        spawnPoint.y += distance2SpawnFromPlayer;

        PosRoom = startRoomPosition.transform.position;

        SpawnRoom(bioma1);

       // numberRoomNextLevel = ScreenHeight * numberRoomNextLevel;
    }

    // Update is called once per frame
    void Update()
    {
        // Si posicion player mayor que spawnPoint, spawn nueva habitacion encima y movemos SpawnPoint a la posicion correspondiente de la habitacion.
        if (player.position.y >= spawnPoint.y)
        {
            PosRoom.y += ScreenHeight;
            spawnPoint.y += ScreenHeight;

            switch (currentBioma) {

                case bioma.bioma1:
                    SpawnRoom(bioma1);
                    spawnedRooms++;
                    changeBiomaEnum(bioma.bioma2);
                    break;

                case bioma.bioma2:
                    SpawnRoom(bioma2);
                    spawnedRooms++;
                    changeBiomaEnum(bioma.bioma3);
                    break;

                case bioma.bioma3:
                    SpawnRoom(bioma3);
                    spawnedRooms++;
                    break;
            }      
        }
    }  


    // Spawnea habitaciones del bioma determinado
    public void SpawnRoom(GameObject[] bioma)
    {
        rand = Random.Range(0, bioma.Length);
        Instantiate(bioma[rand], PosRoom, Quaternion.identity);
    }

    // Cambia enum bioma
    public void changeBiomaEnum(bioma nextBioma)
    {

        if (spawnedRooms == numberRoomNextBioma)
        {
            spawnedRooms = 0;
            currentBioma = nextBioma;
        }
    }
}
