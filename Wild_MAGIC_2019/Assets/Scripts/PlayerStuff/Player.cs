using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Settings")]
    public float hp = 3f;
    public float movementSpeed = 3f;
    public float attackDamage = 1f;
    public float attackTime = 0.05f;

    [Space(5)]
    [Header("Player's Scripts")]
    public PlayerMovement pMove;

    [Space(5)]
    public GameObject swordObject;

    private float swordTimer;

    // Start is called before the first frame update
    void Start()
    {
        pMove.movementSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (swordTimer > 0)
            swordTimer -= Time.deltaTime;
        else
            swordObject.SetActive(false);

        if(Input.GetKey(KeyCode.Space) && swordTimer <= 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Start the sword timer
        swordTimer = attackTime;

        // Get the direction of the attack based off the player movement
        Vector3 direction = pMove.GetDirection();
        int x = 0, y = 0;

        // So we need to rotate the sword to be in that direction
        swordObject.transform.right = -direction.normalized;

        if(direction.x > 0)
        {
            x = -1;
            y = 0;
        }
        if(direction.x < 0)
        {
            x = 1;
            y = 0;
        }
        if (direction.y > 0)
        {
            x = 0;
            y = -1;
        }
        if (direction.y < 0)
        {
            x = 0;
            y = 1;
        }

        if (direction.x > 0 && direction.y > 0)
        {
            x = -1;
            y = -1;
        }
        if (direction.x < 0 && direction.y < 0)
        {
            x = 1;
            y = 1;
        }
        if (direction.x < 0 && direction.y > 0)
        {
            x = 1;
            y = -1;
        }
        if (direction.x > 0 && direction.y < 0)
        {
            x = -1;
            y = 1;
        }

        swordObject.transform.right = new Vector3(x, y, 0);
        swordObject.SetActive(true);
    }

    public void TakeDamage(float amt)
    {
        hp -= amt;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
