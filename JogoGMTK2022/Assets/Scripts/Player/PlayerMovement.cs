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
    public bool isMoving { get => rig.velocity.x != 0; }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update() { Move(); }

    private void Move()
    {
        DirectionTimer();
        float direction = Input.GetAxisRaw("Horizontal");
        direction *= accelerationCurve.Evaluate(timer);

        rig.velocity = new Vector2(direction * speed * Time.deltaTime, rig.velocity.y);
    }

    private void DirectionTimer()
    {
        if (Input.GetButton("Horizontal")) { timer += Time.deltaTime; }
        else if (Input.GetButtonUp("Horizontal")) { timer = 0; }
    }
}
