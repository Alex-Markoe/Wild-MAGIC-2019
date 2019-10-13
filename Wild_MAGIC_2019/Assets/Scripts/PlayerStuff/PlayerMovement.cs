using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3f;
    public bool attacking;
    private Rigidbody2D rb;
    private Animator anim;
    public Vector3 direction;
    public Player player;
    private float dashSpeed = .005f;
    public float dashTimer;
    private float dashMax = .125f;
    public bool dashing = false;
    public bool moving;
    public float movingTimer;
    public float movingMax = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !attacking)
            direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (direction.magnitude > 1)
            direction = direction.normalized;

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            player.Attack();
        }
        else if (dashing)
        {
            dashTimer = dashMax;
            dashing = false;
            dashSpeed = .2f;
        }

        SetAnim();
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !attacking)
            {
                rb.MovePosition(transform.position + (direction * movementSpeed * Time.deltaTime));
            }
        }
        if(movingTimer>movingMax)
        {
            moving = true;
        }
        else
        {
            movingTimer += Time.deltaTime;
        }
        
    }
    public Vector3 GetDirection()
    {
        return direction;
    }

    public void SetAnim()
    {
        bool running = false;
        int spriteDirection = 1;
        int x = 0, y = 0;

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

        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && moving == true)
        {
            running = true;
        }
        if (attacking && !dashing)
        {
            running = false;
        }

        anim.SetBool("Running", running);
        anim.SetBool("Attacking", attacking);
        anim.SetInteger("Direction", spriteDirection);
    }

    public void Dash()
    {
        rb.MovePosition(transform.position + (direction.normalized * dashSpeed));
        dashSpeed *= 1.2f;
    }
}
