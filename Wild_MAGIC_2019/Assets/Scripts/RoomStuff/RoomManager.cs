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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveRoom(int roomIndex, Vector3 pPos)
    {
        this.roomIndex = roomIndex;
        cam.transform.position = rooms[roomIndex].center;
        rooms[roomIndex].EnableRoom();
        p.transform.position = pPos;
    }
}
