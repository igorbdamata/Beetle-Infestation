using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector2[] groundDetects;
    [SerializeField] private Vector2[] wallDetects;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed = 500;
    Rigidbody2D rig;
    public int direction { get; private set; } = 1;
    private bool canMove = false;
    EnemyLife eLife;
    EnemyAttack eAttack;

    private IEnumerator Start()
    {
        rig = GetComponent<Rigidbody2D>();
        eLife = GetComponent<EnemyLife>();
        eAttack = GetComponent<EnemyAttack>();
        while (!GameController.gc.finishedAllLevel) { yield return new WaitForEndOfFrame(); }
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove && !eLife.inDamage && (!eAttack || eAttack && !eAttack.isAtacking))
        {
            rig.velocity = new Vector2(speed * Time.deltaTime * direction, rig.velocity.y);
            CheckCollisions();
        }
        else { rig.velocity = Vector2.zero; }
    }

    void CheckCollisions()
    {
        foreach (Vector2 ground in groundDetects)
        {
            if (ground.x > 0 && direction < 0 || ground.x < 0 && direction > 0) { continue; }
            Collider2D col = Physics2D.OverlapCircle(transform.position + (Vector3)ground, 0.1f, groundLayer);
            if (col == null)
            {
                direction *= -1;
                return;
            }
        }

        foreach (Vector2 ground in wallDetects)
        {
            if (ground.x > 0 && direction < 0 || ground.x < 0 && direction > 0) { continue; }
            Collider2D col = Physics2D.OverlapCircle(transform.position + (Vector3)ground, 0.1f, wallLayer);
            if (col != null)
            {
                direction *= -1;
                return;
            }
        }
    }

    public void StopMovement()
    {
        canMove = false;
        rig.velocity = Vector2.zero;
    }

    public void SetMovement()
    {
        canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (Vector2 ground in groundDetects)
        {
            Gizmos.DrawSphere(transform.position + (Vector3)ground, 0.1f);
        }
        Gizmos.color = Color.blue;
        foreach (Vector2 ground in wallDetects)
        {
            Gizmos.DrawSphere(transform.position + (Vector3)ground, 0.1f);
        }
    }
}
