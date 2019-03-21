using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public GameObject room;
    public Transform player;

    private Vector2 inPosPlayer;
    private Vector2 PosRoom;
    // Start is called before the first frame update

    void Start()
    {
        inPosPlayer = player.transform.position;
        PosRoom = room.transform.position;
        inPosPlayer.y += 5;

    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.y >= inPosPlayer.y)
        {
            PosRoom.y += 28;
            Instantiate(room, PosRoom, Quaternion.identity);
            inPosPlayer.y += 28;
        }
    }  
}
