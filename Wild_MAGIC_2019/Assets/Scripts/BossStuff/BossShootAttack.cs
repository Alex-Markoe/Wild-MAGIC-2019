using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootAttack : BossAttack
{

    public float bulletSpeed;

    public override void DoAttack()
    {
        GameObject bossBullet = (GameObject)Resources.Load("bossBullet");

        for (int i = 0; i < 8; i++)
        {
            float angle = (((8 - i) / 8) * 360f) * Mathf.Deg2Rad;

            GameObject bBullet = Instantiate(bossBullet, transform.position, Quaternion.FromToRotation(Vector3.right, new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)));

            bBullet.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized * bulletSpeed;
        }

        base.DoAttack();
    }
}
