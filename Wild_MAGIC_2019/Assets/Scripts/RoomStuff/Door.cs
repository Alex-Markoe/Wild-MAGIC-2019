using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int destinationIndex;
    public Vector3 destinationPoint;
    private RoomManager manager;

    public void Start()
    {
        manager = GameObject.FindObjectOfType<RoomManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            manager.MoveRoom(destinationIndex, destinationPoint);
        }
    }
}
