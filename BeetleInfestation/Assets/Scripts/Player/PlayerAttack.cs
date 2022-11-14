using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GC;

public class PlayerAttack : MonoBehaviour
{
    private Weapon weapon;
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private float attackRadius;
    [SerializeField] private Vector2 attackPoint;
    [SerializeField] private LayerMask enemiesLayer;
    Animator anim;

    public bool isAtacking { get; private set; }
    bool canAttack = true;
    bool alreadyAttacked = false;
    PlayerMovement pMovement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        pMovement = GetComponent<PlayerMovement>();
        weapon = DATA.d.weapons[DATA.d.currenteWeapon];
        weaponSprite.GetComponent<GenericAnimator>().sprites.AddRange(weapon.sprite);
    }

    private void Update()
    {
        if (GetComponent<PlayerLife>().isDead) { return; }
        if (Input.GetKeyDown(KeyCode.E) && canAttack && !GetComponent<PlayerLife>().isInvencible)
        {
            anim.Play("Attack");
        }
    }

    public void StartAttack()
    {
        isAtacking = true;
        canAttack = false;
        SoundController.sc.PlaySFX(SoundController.sc.attackSFX);
        foreach (Transform t in GameController.gc.enemies)
        {
            t.GetComponent<EnemyMovement>().StopMovement();
        }
        StartCoroutine(AnimationSpeedController());
    }

    public void SetDamage()
    {
        if (alreadyAttacked) { return; }
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position + (Vector3)attackPoint * pMovement.direction, attackRadius, enemiesLayer);
        if (enemies.Length == 0) { return; }
        Collider2D enemyToAttack = nearestEnemyIn(enemies);
        if (enemyToAttack != null)
        {
            enemyToAttack.GetComponent<EnemyLife>().AddDamage(weapon.damage);
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
        isAtacking = false;

        alreadyAttacked = false;
        foreach (Transform t in GameController.gc.enemies)
        {
            t.GetComponent<EnemyMovement>().SetMovement();
        }
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AnimationSpeedController()
    {
        while (isAtacking)
        {
            float timer = 0;
            timer += Time.deltaTime;
            anim.speed = weapon.attackAnimationSpeed.Evaluate(timer);
            yield return new WaitForEndOfFrame();
        }
        anim.speed = 1;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(weapon.attackCooldown);
        canAttack = true;
    }

    public void StopAttack()
    {
        isAtacking = false;
        StopAllCoroutines();
        canAttack = true;
        anim.speed = 1;
        isAtacking = false;
        alreadyAttacked = false;
        foreach (Transform t in GameController.gc.enemies)
        {
            t.GetComponent<EnemyMovement>().SetMovement();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)attackPoint * pMovement.direction, attackRadius);
    }
}
