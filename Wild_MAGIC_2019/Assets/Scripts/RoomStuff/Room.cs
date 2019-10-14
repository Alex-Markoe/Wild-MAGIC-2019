using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Player player;
    public GameObject[] enemies;
    public GameObject[] doors;
    public bool cleared;
    public bool firstClear;
    public Vector3 center;
    public float camSize;
    public int roomIndex;

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
                firstClear = false;
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
