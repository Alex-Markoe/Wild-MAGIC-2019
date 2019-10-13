using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHard : EnemyMovement
{
    public float attackTimer;
    public float attackTimerInterval = 2f;

    private void Start()
    {
        rng = Random.Range(1, 9);
        rb = GetComponent<Rigidbody2D>();
        currentTime = Time.time;
        colliding = false;
        playerOBJ = GameObject.FindGameObjectWithTag("Player");
        player = playerOBJ.GetComponent<Player>();
        anim = GetComponentInChildren<Animator>();
        attacking = false;
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
        if (!attacking)
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
        base.SetAnim();

        anim.SetBool("Attacking", attacking);
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