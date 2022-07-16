using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator anim;
    private EnemyAttack eAttack;
    private EnemyLife eLife;

    void Start()
    {
        anim = GetComponent<Animator>();
        eAttack = GetComponent<EnemyAttack>();
        eLife = GetComponent<EnemyLife>();
    }

    void Update()
    {
        if (eAttack && eAttack.isAtacking) { return; }
        anim.Play(GetAnimation());
    }

    string GetAnimation()
    {
        if (eLife.isDead) { return "Dead"; }
        if (eLife.inDamage) { return "Damage"; }
        return "Walk";
    }
}
