using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashAttack : BossAttack
{
    private Player p;
    private Vector3 dashTarget;
    private Rigidbody2D rb;

    private float dashSpeed = 3f;

    private void Start()
    {
        p = GameObject.FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, dashTarget) > 0.5f)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, dashTarget, dashSpeed * Time.deltaTime));
        } else
        {
            attackComplete = true;
        }
    }

    public override void DoAttack()
    {
        dashTarget = p.transform.position;

        base.DoAttack();
    }
}
