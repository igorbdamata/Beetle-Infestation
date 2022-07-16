using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 750f;
    private Rigidbody2D rig;
    [SerializeField] private AnimationCurve accelerationCurve;
    private float timer;
    public bool isMoving { get => direction != 0; }
    bool canMove = true;
    float direction;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GetComponent<PlayerLife>().isDead) { return; }
        direction = Input.GetAxisRaw("Horizontal");
        if (!canMove) { return; }
        Move();
    }

    private void Move()
    {
        DirectionTimer();
        direction *= accelerationCurve.Evaluate(timer);

        rig.velocity = new Vector2(direction * speed * Time.deltaTime, rig.velocity.y);
    }

    private void DirectionTimer()
    {
        if (Input.GetButton("Horizontal")) { timer += Time.deltaTime; }
        else if (Input.GetButtonUp("Horizontal")) { timer = 0; }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            float distanceX = collision.gameObject.transform.position.x - transform.position.x;
            if (direction < 0 && distanceX < 0 || direction > 0 && distanceX > 0)
            {
                canMove = false;
                rig.velocity = new Vector2(0, rig.velocity.y);
            }
            else
            {
                canMove = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            canMove = true;
        }
    }
}
