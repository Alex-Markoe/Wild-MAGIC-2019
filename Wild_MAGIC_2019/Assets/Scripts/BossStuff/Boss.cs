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

    public GameManager gameManager;

    public bool moving;
    public float movingTimer;
    public float movingMax = 1;

    public AudioSource source;

    private void Start()
    {
        p = GameObject.FindObjectOfType<Player>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
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
                    if (Vector3.Distance(p.transform.position, transform.position) >= 0.1f)
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

            if (chargeForAttack)
            {
                if (chargeTimer <= 0)
                {
                    source.Play();
                    Attack();
                    chargeForAttack = false;
                }
                if (chargeTimer > 0)
                {
                    chargeTimer -= Time.deltaTime;
                }
            }
        }
        if (movingTimer > movingMax)
        {
            moving = true;
        }
        else
        {
            movingTimer += Time.deltaTime;
        }

        if (timer > 0)
            timer -= Time.deltaTime;

        if(hp <= 0)
        {
            Die();
        }

        SetAnim();

        if(Vector2.Distance(transform.position, p.transform.position) < 1f)
        {
            p.TakeDamage(1);
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
        gameManager.Win();
    }

    private void SetAnim()
    {
        Vector3 direction = p.transform.position - transform.position;
        //Debug.Log(direction);

        int spriteDirection = 1;
        bool lunging = false;
        bool cards = false;

        if (direction.x < animRangeX && direction.x > -animRangeX)
        {
            if (direction.y > 0)
                spriteDirection = 4;
            else if (direction.y < 0)
                spriteDirection = 3;
        }
        else if (direction.x > 0)
            spriteDirection = 1;
        else if (direction.x < 0)
            spriteDirection = 2;

        //Debug.Log(spriteDirection);
        if (currentAttack != null && !currentAttack.attackComplete)
        {
            if (currentAttack.attackType == "Dash")
                lunging = true;
            else if (currentAttack.attackType == "Cards")
                cards = true;
        }

        anim.SetBool("Charging", chargeForAttack);
        anim.SetBool("Lunging", lunging);
        anim.SetBool("Cards", cards);
        anim.SetInteger("Direction", spriteDirection);
    }
}
