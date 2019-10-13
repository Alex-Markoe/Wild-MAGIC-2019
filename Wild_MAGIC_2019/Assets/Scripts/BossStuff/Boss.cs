using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossMovement bMove;
    public BossAttack[] attacks;
    public float actionTimer;
    public float hp;

    private BossAttack currentAttack;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            float rng = Random.Range(0, 8);

            if (rng >= 5 && rng <= 8)
            {
                if (currentAttack == null)
                {
                    Attack();
                }
                else
                {
                    if (currentAttack.attackComplete)
                        Attack();
                }
            }

            timer = actionTimer;
        }

        if (timer > 0)
            timer -= Time.deltaTime;

        if(hp <= 0)
        {
            Die();
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
}
