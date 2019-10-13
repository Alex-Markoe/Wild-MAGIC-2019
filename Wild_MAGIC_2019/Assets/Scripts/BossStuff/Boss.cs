using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossAttack[] attacks;
    public float actionTimer;
    public float hp;
    public float movementSpeed = 2f;
    public float chargeTime = 1f;

    private BossAttack currentAttack;
    private Player p;
    private Rigidbody2D rb;

    private float timer;
    private float chargeTimer;
    private bool chargeForAttack;

    private Animator anim;
    private float animRangeX = .5f;
    private bool lunging;
    private bool cards;

    private void Start()
    {
        p = GameObject.FindObjectOfType<Player>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0 && !chargeForAttack)
        {
            float rng = Random.Range(0, 8);

            if (rng >= 5 && rng <= 8)
            {
                if (currentAttack == null)
                {
                    chargeTimer = chargeTime;
                    chargeForAttack = true;
                }
                else
                {
                    if (currentAttack.attackComplete)
                    {
                        chargeTimer = chargeTime;
                        chargeForAttack = true;
                    }
                }
            }

            timer = actionTimer;
        }

        if (!chargeForAttack)
        {
            if(currentAttack.attackComplete)
            if (currentAttack == null)
            {
                if (Vector3.Distance(p.transform.position, transform.position) >= 2)
                {
                    rb.MovePosition(transform.position + (p.transform.position - transform.position).normalized * movementSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (currentAttack.attackComplete)
                {
                    if (Vector3.Distance(p.transform.position, transform.position) >= 2)
                    {
                        rb.MovePosition(transform.position + (p.transform.position - transform.position).normalized * movementSpeed * Time.deltaTime);
                    }
                }
            }
        }

        if (timer > 0)
            timer -= Time.deltaTime;

        if(hp <= 0)
        {
            Die();
        }

        SetAnim();
        if(chargeForAttack)
        {
            if(chargeTimer <= 0)
            {
                Attack();
                chargeForAttack = false;
            }
            if(chargeTimer > 0)
            {
                chargeTimer -= Time.deltaTime;
            }
        }
    }

    void Attack()
    {
        if (currentAttack != null)
        {
            currentAttack.attackComplete = false;
        }

        int attackIndex = Random.Range(0, attacks.Length);
        currentAttack = attacks[attackIndex];
        attacks[attackIndex].DoAttack();
    }

    public void TakeDamage(float amt)
    {
        hp -= amt;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    private void SetAnim()
    {
        Vector3 direction = p.transform.position - transform.position;
        Debug.Log(direction);
        int spriteDirection = 1;

        if (direction.x < animRangeX && direction.x > -animRangeX)
        {
            if (direction.y > 0)
                spriteDirection = 3;
            else if (direction.y < 0)
                spriteDirection = 4;
        }
        else if (direction.x > 0)
            spriteDirection = 1;
        else if (direction.x < 0)
            spriteDirection = 2;
        Debug.Log(spriteDirection);
        anim.SetInteger("Direction", spriteDirection);
    }
}
