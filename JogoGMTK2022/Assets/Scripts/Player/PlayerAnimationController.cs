using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    private PlayerAttack pAttack;
    private PlayerMovement pMovement;
    private PlayerJump pJump;
    private PlayerLife pLife;

    void Start()
    {
        anim = GetComponent<Animator>();
        pAttack = GetComponent<PlayerAttack>();
        pMovement = GetComponent<PlayerMovement>();
        pJump = GetComponent<PlayerJump>();
        pLife = GetComponent<PlayerLife>();
    }

    void Update()
    {
        if (pAttack.isAtacking) { return; }
        anim.Play(GetAnimation());
    }

    string GetAnimation()
    {
        if (pLife.isInvencible) { return "Idle"; }
        if (pLife.isDead) { return "Idle"; }
        if (!pJump.inGround) { return "Jump"; }
        if (pMovement.isMoving) { return "Walk"; }
        return "Idle";
    }

}
