using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
        {
            if(collision.transform.GetComponent<Player>())
            {
                collision.transform.GetComponent<Player>().TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }       
    }
}
