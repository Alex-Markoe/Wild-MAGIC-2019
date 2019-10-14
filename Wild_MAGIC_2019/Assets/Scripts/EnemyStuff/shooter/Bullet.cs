using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    public GameObject player;
    private float movementSpeed = 7f;
    public Rigidbody2D rb;
    public GameObject enemy;
    public GameObject enemyHard;
    public GameObject enemyShooter;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), 0);
        direction.Normalize();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * movementSpeed;
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //   deletes on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            if (collision.gameObject.tag == "Player")
                player.GetComponent<Player>().TakeDamage(1);

            Destroy(gameObject);
        }
    }
}
