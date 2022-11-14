using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    EnemyMovement eMove;
    private void Start()
    {
        eMove = GetComponent<EnemyMovement>();
    }
    void Update()
    {
        if (eMove.direction != 0)
        {
            transform.localEulerAngles = new Vector3(0, eMove.direction > 0 ? 0 : 180, 0);
        }
    }
}
