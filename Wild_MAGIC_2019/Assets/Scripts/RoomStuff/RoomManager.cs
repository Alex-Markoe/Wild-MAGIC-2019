using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Room[] rooms;
    public Room currentRoom;
    public int roomIndex;
    public GameObject cam;
    public Player p;
    public ScreenShake screenShake;

    // Start is called before the first frame update
    void Start()
    {
        currentRoom = rooms[roomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveRoom(int roomIndex, Vector3 pPos)
    {
        currentRoom.firstClear = true;
        this.roomIndex = roomIndex;
        cam.transform.position = rooms[roomIndex].center;
        screenShake.startPos = cam.transform.position;
        p.lightShrink = true;
        rooms[roomIndex].EnableRoom();
        currentRoom = rooms[roomIndex];
        p.transform.position = pPos;
    }
}
