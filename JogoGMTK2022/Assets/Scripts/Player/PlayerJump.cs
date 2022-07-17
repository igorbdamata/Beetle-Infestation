using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 750f;
    [SerializeField] private Vector2 groundCheckOffset;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float ghostGroundTime = 0.5f;
    public bool isJumping { get; private set; }
    public bool inGround { get; private set; }
    public bool fixedInGround { get; private set; }
    private bool inGhostGround;

    Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGround();

        bool canJump = inGround;
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }
        if (isJumping && Input.GetKeyUp(KeyCode.Space))
        {
            BreakJump();
        }
    }

    void Jump()
    {
        SoundController.sc.PlaySFX(SoundController.sc.jumpSFX);
        rig.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        isJumping = true;
    }

    void BreakJump()
    {
        if (rig.velocity.y > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * 0.2f);
        }

        isJumping = false;
    }

    void CheckGround()
    {
        if (isJumping)
        {
            StopCoroutine(GhostGround());
            inGhostGround = false;
            inGround = false;
            return;
        }
        Collider2D col = Physics2D.OverlapCircle(transform.position + (Vector3)groundCheckOffset, 0.1f, groundLayer);
        if (col != null)
        {
            inGround = true;
            isJumping = false;
            fixedInGround = true;
        }
        else
        {
            fixedInGround = false;
            StartCoroutine(GhostGround());
        }
    }

    IEnumerator GhostGround()
    {
        if (!inGhostGround)
        {
            inGhostGround = true;
            yield return new WaitForSeconds(ghostGroundTime);
            inGround = false;
            inGhostGround = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (Vector3)groundCheckOffset, 0.1f);
    }
}
