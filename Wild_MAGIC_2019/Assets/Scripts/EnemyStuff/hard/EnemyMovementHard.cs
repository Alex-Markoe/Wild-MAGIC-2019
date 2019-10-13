using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHard : EnemyMovement
{
    public float attackTimer;
    public float attackTimerInterval = 2f;
    public bool attacking;

    private Animator animZomb;

    private void Start()
    {
        rng = Random.Range(1, 9);
        rb = GetComponent<Rigidbody2D>();
        currentTime = Time.time;
        colliding = false;
        playerOBJ = GameObject.FindGameObjectWithTag("Player");
        player = playerOBJ.GetComponent<Player>();
        animZomb = GetComponentInChildren<Animator>();
    }

    public override void Update()
    {
        if(attackTimer>0)
        {
            attackTimer -= Time.deltaTime;
            attacking = false;
        }
        if(colliding && attackTimer <= 0)
        {
            player.TakeDamage(1);
            attackTimer = attackTimerInterval;
            attacking = true;
        }
        Move();
        
        base.Update();
    }

    //  hard movement
    public override void Move()
    {
        direction = new Vector3(player.GetPosition().x - transform.position.x, player.GetPosition().y - transform.position.y, 0);
        if (direction.magnitude > 1)
            direction = direction.normalized;
    }

    public override void SetAnim()
    {
        int x = 0;
        int y = 0;
        int spriteDirection = 1;

        if (direction.x < 0)
        {
            x = -1;
        }
        if (direction.x > 0)
        {
            x = 1;
        }

        if (direction.y < 0)
        {
            y = -1;
        }
        if (direction.y > 0)
        {
            y = 1;
        }

        if (x > 0)
        {
            if (y > 0)
                spriteDirection = 7;
            else if (y < 0)
                spriteDirection = 5;
            else
                spriteDirection = 1;
        }
        else if (x < 0)
        {
            if (y > 0)
                spriteDirection = 8;
            else if (y < 0)
                spriteDirection = 6;
            else
                spriteDirection = 2;
        }
        else if (y > 0)
        {
            spriteDirection = 4;
        }
        else if (y < 0)
        {
            spriteDirection = 3;
        }

        animZomb.SetInteger("Direction", spriteDirection);
        animZomb.SetBool("Attacking", attacking);
    }

    //  attacks on collision
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colliding = true;
        }
    }
}