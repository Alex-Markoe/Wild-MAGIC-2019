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

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        currentRoom = rooms[0];
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveRoom(int roomIndex, Vector3 pPos)
    {
        source.Play();
        p.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        rooms[currentRoom.roomIndex].firstClear = true;
        this.roomIndex = roomIndex;
        cam.transform.position = rooms[roomIndex].center;
        screenShake.startPos = cam.transform.position;
        p.lightShrink = true;
        rooms[roomIndex].EnableRoom();
        currentRoom = rooms[roomIndex];
        cam.GetComponent<Camera>().orthographicSize = currentRoom.camSize;
        p.transform.position = pPos;
        if(!currentRoom.firstClear)
        {
            p.GetComponent<PlayerMovement>().movingTimer = 0f;
            p.GetComponent<PlayerMovement>().moving = false;
            if (currentRoom.enemies.Length > 0)
            {
                foreach (GameObject enemy in currentRoom.enemies)
                {
                    enemy.GetComponent<EnemyMovement>();
                }
            }
        }
    }
}
