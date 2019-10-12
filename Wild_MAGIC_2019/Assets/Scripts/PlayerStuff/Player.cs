using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Settings")]
    public float hp = 3f;
    public float movementSpeed = 3f;
    public float attackDamage = 1f;

    [Space(5)]
    [Header("Player's Scripts")]
    public PlayerMovement pMove;

    // Start is called before the first frame update
    void Start()
    {
        pMove.movementSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {

    }

    public void TakeDamage(float amt)
    {
        hp -= amt;
    }
}
