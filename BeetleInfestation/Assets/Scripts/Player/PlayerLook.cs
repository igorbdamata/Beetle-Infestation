using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    PlayerAttack pAttack;
    Rigidbody2D rig;

    void Start()
    {
        pAttack = GetComponent<PlayerAttack>();
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GetComponent<PlayerLife>().isDead) { return; }
        if (!pAttack.isAtacking && GetComponent<PlayerMovement>().direction != 0)
        {
            float directionToLook = GetComponent<PlayerMovement>().direction > 0 ? 0 : 180;
            transform.localEulerAngles = new Vector3(transform.rotation.x, directionToLook, transform.rotation.z);
        }
    }
}
