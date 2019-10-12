﻿using System.Collections;
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
        player = GameObject.FindGameObjectWithTag("Player");
        direction = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0);
        if (direction.magnitude > 1)
            direction = direction.normalized;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //   deletes on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Wall")
        {
            Destroy(gameObject);
            player.GetComponent<Player>().TakeDamage(1);
        }
    }
}
