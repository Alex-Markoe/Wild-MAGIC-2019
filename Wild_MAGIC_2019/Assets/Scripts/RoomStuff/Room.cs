using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Player player;
    public GameObject[] enemies;
    public GameObject[] doors;
    public bool cleared;
    public Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableRoom()
    {
        player.enabled = true;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }

        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }

        cleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        cleared = true;
        foreach (GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                cleared = false;
            }
        }

        if(cleared)
        {
            foreach(GameObject door in doors)
            {
                door.SetActive(true);
            }
        }
    }
}
