using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHard : EnemyMovement
{
    public float attackTimer;
    public float attackTimerInterval = 2f;
    

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
        if (collision.gameObject == player)
        {
            colliding = true;
        }
        GameObject toCollide = collision.gameObject;
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTimerInterval)
        {
            if (toCollide == player)
            {
                //  animate attack
                player.TakeDamage(2);
            }
            attackTimer = 0;
    }
}

}