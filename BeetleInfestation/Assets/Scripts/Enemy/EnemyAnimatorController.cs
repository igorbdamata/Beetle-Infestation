using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator anim;
    private EnemyAttack eAttack;
    private EnemyLife eLife;
    private PlayerAttack pAtk;

    void Start()
    {
        anim = GetComponent<Animator>();
        eAttack = GetComponent<EnemyAttack>();
        eLife = GetComponent<EnemyLife>();
        pAtk = FindObjectOfType<PlayerAttack>();
    }

    void Update()
    {
        if (eAttack && eAttack.isAtacking && !pAtk.isAtacking) { return; }
        if (eAttack && eAttack.isAtacking) { eAttack.StopAttack(); }

        anim.Play(GetAnimation());
    }

    string GetAnimation()
    {
        if (eLife.isDead) { return "Dead"; }
        if (eLife.inDamage) { return "Damage"; }
        return "Walk";
    }
}
