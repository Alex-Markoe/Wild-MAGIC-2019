using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementShooter : EnemyMovement
{
    public GameObject bullet;
    public float attackTimer;
    public float attackTimerInterval = 3f;


    public override void Update()
    {
        base.Update();
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTimerInterval)
        {
            Attack();
            //  animate attack
            attackTimer = 0;
        }
    }

    //  shooter movement
    public override void Move()
    {
        direction = new Vector3(transform.position.x - player.GetPosition().x, transform.position.y - player.GetPosition().y, 0);
        if (direction.magnitude > 1)
            direction = direction.normalized;
    }

    //  shoots a bullet
    public void Attack()
    {
        Instantiate(bullet, transform);
    }
}