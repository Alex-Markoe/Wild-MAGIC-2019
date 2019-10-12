using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHard : EnemyMovement
{
    public float attackTimer;
    public float attackTimerInterval = 2f;

    public override void Update()
    {
        base.Update();
        if(attackTimer>0)
        {
            attackTimer -= Time.deltaTime;
        }
        if(colliding && attackTimer <= 0)
        {
            player.TakeDamage(1);
            attackTimer = attackTimerInterval;
        }

    }
    //  hard movement
    public override void Move()
    {
        direction = new Vector3(player.GetPosition().x - transform.position.x, player.GetPosition().y - transform.position.y, 0);
        if (direction.magnitude > 1)
            direction = direction.normalized;
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