using UnityEngine;

public class EnemyDamageOnTouch : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerLife>().AddDamage(damage);
        }
    }
}