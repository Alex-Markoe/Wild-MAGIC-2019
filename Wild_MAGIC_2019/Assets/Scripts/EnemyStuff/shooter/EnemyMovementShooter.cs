using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementShooter : EnemyMovement
{
    public GameObject bullet;
    public float attackTimer;
    public float attackTimerInterval = 3f;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        attacking = false;
        colliding = false;

        player = FindObjectOfType<Player>();
    }

    public override void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > 1)
            attacking = false;

        if (attackTimer > attackTimerInterval)
        {
            Attack();
            attackTimer = 0;
            attacking = true;
        }

        if (!attacking)
        {
            Move();
            if (!colliding)
                rb.MovePosition(transform.position + (direction * movementSpeed * Time.deltaTime));
        }

        SetAnim();
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

        float angle = (Mathf.Atan2(toTarget.y, toTarget.x)) * Mathf.Rad2Deg;

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
        base.SetAnim();
        if (attacking)
            Debug.Log(direction);

        anim.SetBool("Attacking", attacking);
    }
}