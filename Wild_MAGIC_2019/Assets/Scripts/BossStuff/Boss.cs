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
    private bool charging;
    private bool lunging;
    private bool cards;

    private void Start()
    {
        p = GameObject.FindObjectOfType<Player>();
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

    }
}
