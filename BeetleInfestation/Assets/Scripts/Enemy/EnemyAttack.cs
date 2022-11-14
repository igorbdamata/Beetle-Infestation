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
    PlayerAttack pAtk;
    public bool isAtacking { get; private set; }
    bool canAttack = true;
    bool alreadyAttacked = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        eMovement = GetComponent<EnemyMovement>();
        pAtk = FindObjectOfType<PlayerAttack>();
    }

    private void Update()
    {
        if (GetComponent<EnemyLife>().isDead|| pAtk.isAtacking) { return; }
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
