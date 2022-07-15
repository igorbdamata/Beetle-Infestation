using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GC;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private float attackRadius;
    [SerializeField] private Vector2 attackPoint;
    [SerializeField] private LayerMask enemiesLayer;
    Animator anim;

    public bool isAtacking { get; private set; }
    bool canAttack = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        weaponSprite.sprite = weapon.sprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canAttack)
        {
            anim.Play("Attack");
        }
    }

    public void StartAttack()
    {
        print("Começou atk");
        isAtacking = true;
        canAttack = false;
        StartCoroutine(AnimationSpeedController());
    }

    public void SetDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position + (Vector3)attackPoint, attackRadius, enemiesLayer);
        if (enemies.Length == 0) { return; }
        Collider2D enemyToAttack = nearestEnemyIn(enemies);
        if (enemyToAttack != null)
        {
            print("Atacou " + enemyToAttack.gameObject.name);
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
        print("Terminou ataque");
        isAtacking = false;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)attackPoint, attackRadius);
    }
}
