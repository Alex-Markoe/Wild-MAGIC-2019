using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementShooter : EnemyMovement
{
    public GameObject bullet;
    public float attackTimer;
    public float attackTimerInterval = 3f;

    private Animator shootyAnim;

    private void Start()
    {
        shootyAnim = GetComponentInChildren<Animator>();
        attacking = false;
    }

    public override void Update()
    {
        base.Update();
        attackTimer += Time.deltaTime;
        attacking = false;

        if (attackTimer > attackTimerInterval)
        {
            Attack();
            //  animate attack
            attackTimer = 0;
            attacking = true;
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
        Vector3 toTarget = player.transform.position - transform.position;
        float angle = (Mathf.Atan2(toTarget.y, toTarget.x)) * (180/Mathf.PI);

        if ((22.5f >= angle && angle >= 0) || (angle < 0 && -22.5f <= angle))
        {
            direction = new Vector3(1, 0, 0);
            angle = 0;
        }
        else if ((67.5 >= angle && angle > 22.5f))
        {
            direction = new Vector3(1, 1, 0);
            angle = 45;
        }
        else if ((112.5f >= angle && angle > 67.5f))
        {
            direction = new Vector3(0, 1, 0);
            angle = 90;
        }
        else if ((157.5 >= angle && angle > 112.5f))
        {
            direction = new Vector3(-1, 1, 0);
            angle = 135;
        }
        else if ((180 >= angle && angle > 157.5f) || (-180 <= angle && angle < -157.5f))
        {
            direction = new Vector3(-1, 0, 0);
            angle = 180;
        }
        else if ((-157.5f <= angle && angle < -112.5f))
        {
            direction = new Vector3(-1, -1, 0);
            angle = 225;
        }
        else if ((-112.5f <= angle && angle < -67.5f))
        {
            direction = new Vector3(0, -1, 0);
            angle = 270;
        }
        else if ((-67.5 <= angle && angle < -22.5f))
        {
            direction = new Vector3(1, -1, 0);
            angle = 315;
        }
        
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));
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

        shootyAnim.SetInteger("Direction", spriteDirection);
        shootyAnim.SetBool("Attacking", attacking);
    }
}