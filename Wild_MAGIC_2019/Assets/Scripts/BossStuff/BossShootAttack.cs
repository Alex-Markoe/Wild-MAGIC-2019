using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootAttack : BossAttack
{

    public float bulletSpeed;
    public float cardCount = 8;

    private void Start()
    {
        attackType = "Cards";
    }

    public override void DoAttack()
    {
        GameObject bossBullet = (GameObject)Resources.Load("bossBullet");

        for (int i = 0; i < cardCount; i++)
        {
            float angle = (((cardCount - i) / cardCount) * 360f) * Mathf.Deg2Rad;

            GameObject bBullet = Instantiate(bossBullet, transform.position, Quaternion.FromToRotation(Vector3.right, new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)));
            bBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized * bulletSpeed;
            bBullet.GetComponent<BossBullet>().damage = attackDamage;
        }
        base.DoAttack();

        attackComplete = true;
    }
}
