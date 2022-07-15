using UnityEngine;
using System.Collections.Generic;

namespace GC
{
    public class GCMovement : MonoBehaviour
    {
        public void Follow2Direction(float speed, Transform target, Rigidbody2D rig)
        {
            rig.velocity = new Vector2(speed * ((transform.position.x > target.position.x) ? -1 : 1) * Time.fixedDeltaTime, rig.velocity.y);
        }

        public void Follow8Direction(float speed, Transform target, Rigidbody2D rig)
        {
            float x = (transform.position.x > target.position.x) ? -1 : 1;
            float y = (transform.position.y > target.position.y) ? -1 : 1;
            if (Mathf.Abs(transform.position.x - target.position.x) <= 0.1f) { x = 0; }
            if (Mathf.Abs(transform.position.y - target.position.y) <= 0.1f) { y = 0; }
            rig.velocity = new Vector2(x, y) * speed * Time.fixedDeltaTime;
        }

        public static float LookAt2D(Transform looker, Transform target)
        {
            Vector2 playerPos = target.position - looker.position;
            float rotationZ = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
            return rotationZ;
        }
    }
}
