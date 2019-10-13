using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float attackDamage;
    public bool attackComplete;

    public virtual void DoAttack()
    {
        attackComplete = false;
    }
}
