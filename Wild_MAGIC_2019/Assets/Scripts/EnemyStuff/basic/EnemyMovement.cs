using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Player player;
    public int rng;
    public Rigidbody2D rb;
    public Vector3 direction;
    public float interval = .25f;
    public float currentTime;
    public float waitTimer;
    public float waitTimerInterval = 1;
    public bool colliding = true;
    public GameObject playerOBJ;

    // Start is called before the first frame update
    void Start()
    {
        rng = Random.Range(1, 9);
        rb = GetComponent<Rigidbody2D>();
        currentTime = Time.time;
        colliding = false;
        playerOBJ = GameObject.FindGameObjectWithTag("Player");
        player = playerOBJ.GetComponent<Player>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        rng = Random.Range(1, 9);
        currentTime += Time.deltaTime;
        if (!colliding)
        {
            rb.MovePosition(transform.position + (direction * movementSpeed * Time.deltaTime));
        }
        if (currentTime > interval)
        {
            Move();
            currentTime = 0;
        }
    }

    //  basic movement
    public virtual void Move()
    {
        switch(rng)
        {
            case 1:
                direction = new Vector3(1, 0, 0);
                break;
            case 2:
                direction = new Vector3(1, 1, 0);
                break;
            case 3:
                direction = new Vector3(0, 1, 0);
                break;
            case 4:
                direction = new Vector3(-1, 0, 0);
                break;
            case 5:
                direction = new Vector3(-1, -1, 0);
                break;
            case 6:
                direction = new Vector3(0, -1, 0);
                break;
            case 7:
                direction = new Vector3(1, -1, 0);
                break;
            case 8:
                direction = new Vector3(-1, 1, 0);
                break;
            case 9:
                direction = new Vector3(0, 0, 0);
                break;

        }
        if (direction.magnitude > 1)
            direction = direction.normalized;
    }

    //  collision with player
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            colliding = true;
            player.TakeDamage(1);
        }
    }

    private void OnCollisionExit2D()
    {
        colliding = false;
    }

    //  keeps track of direction
    public Vector3 GetDirection()
    {
        return direction;
    }

    public virtual void SetAnim()
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
    }
}
