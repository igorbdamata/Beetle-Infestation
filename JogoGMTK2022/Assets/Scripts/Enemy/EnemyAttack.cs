using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRadius;
    [SerializeField] private int damage;
    [SerializeField] private Vector2 attackPoint;
    [SerializeField] private LayerMask playerLayer;
    Animator anim;
    EnemyMovement eMovement;
    public bool isAtacking { get; private set; }
    bool canAttack = true;
    bool alreadyAttacked = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        eMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (GetComponent<EnemyLife>().isDead) { return; }
        Collider2D player = Physics2D.OverlapCircle(transform.position + (Vector3)attackPoint * eMovement.direction, attackRadius, playerLayer);
        if (player != null && canAttack)
        {
            anim.Play("Attack");
        }

    }

    public void StartAttack()
    {
        isAtacking = true;
        canAttack = false;
    }

    public void SetDamage()
    {
        if (alreadyAttacked) { return; }
        Collider2D player = Physics2D.OverlapCircle(transform.position + (Vector3)attackPoint * eMovement.direction, attackRadius, playerLayer);
        if (player != null)
        {
            player.GetComponent<PlayerLife>().AddDamage(damage);
            alreadyAttacked = true;
        }

    }

    Collider2D nearestEnemyIn(Collider2D[] enemies)
    {
        float lowerDistance = 999;
        Collider2D enemyToAttack = null;
        foreach (Collider2D c in enemies)
        {
            if (c == null) { continue; }

            if (enemyToAttack == null) { enemyToAttack = c; continue; }

            float distance = GC.d.GetDistance(transform.position, enemyToAttack.transform.position);
            if (distance <= lowerDistance)
            {
                lowerDistance = distance;
                enemyToAttack = c;
            }
        }
        return enemyToAttack;
    }

    public void EndAttack()
    {
        alreadyAttacked = false;
        isAtacking = false;
        StartCoroutine(AttackCooldown());
    }


    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void StopAttack()
    {
        isAtacking = false;
        StopAllCoroutines();
        canAttack = true;
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)attackPoint * (eMovement ? eMovement.direction : 1), attackRadius);
    }
}
