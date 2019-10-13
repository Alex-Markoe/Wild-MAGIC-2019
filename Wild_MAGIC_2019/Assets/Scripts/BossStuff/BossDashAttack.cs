using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashAttack : BossAttack
{
    private Player p;
    private Vector3 dashTarget;
    private Rigidbody2D rb;
    private float dashTimer;

    public float dashSpeed = 3f;
    public float attackRange = 2f;
    public float chargeTime = 0.5f;

    private void Start()
    {
        p = GameObject.FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashTimer <= 0)
        {
            if (dashTarget == Vector3.zero)
                dashTarget = p.transform.position;

            if (Vector3.Distance(transform.position, dashTarget) > 0.5f)
            {
                rb.MovePosition(Vector3.Lerp(transform.position, dashTarget, dashSpeed * Time.deltaTime));
            }
            else
            {
                attackComplete = true;
            }
        }

        if (dashTimer > 0)
            dashTimer -= Time.deltaTime;

        if(Vector3.Distance(transform.position, p.transform.position) <= attackRange && !attackComplete)
        {
            p.TakeDamage(attackDamage);
            attackComplete = true;
        }
    }

    public override void DoAttack()
    {
        dashTarget = Vector3.zero;
        dashTimer = chargeTime;

        base.DoAttack();
    }
}
