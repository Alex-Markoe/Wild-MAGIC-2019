using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(player.GetPosition().x - transform.position.x, player.GetPosition().y - transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
