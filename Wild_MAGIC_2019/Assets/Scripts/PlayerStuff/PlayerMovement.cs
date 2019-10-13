using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3f;
    public bool attacking;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !attacking)
            direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (direction.magnitude > 1)
            direction = direction.normalized;

        SetAnim();
    }

    private void FixedUpdate()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !attacking)
            rb.MovePosition(transform.position + (direction * movementSpeed * Time.deltaTime));
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

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            running = true;
        }
        if (attacking)
        {
            running = false;
        }

        anim.SetBool("Running", running);
        anim.SetBool("Attacking", attacking);
        anim.SetInteger("Direction", spriteDirection);
    }
}
