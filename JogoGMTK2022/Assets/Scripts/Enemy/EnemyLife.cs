using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

    [SerializeField] private int totalLife;
    [SerializeField] private int minScore=1;
    [SerializeField] private int maxScore=10;
    public int currentLife { get; private set; }

    public bool isDead { get; private set; }


    private void Start()
    {
        currentLife = totalLife;
    }
    public void AddDamage(int damage)
    {
        currentLife -= damage;
        if (currentLife <= 0 && !isDead)
        {
            isDead = true;
            GameController.gc.playerScore += Random.Range(minScore, maxScore);
            Destroy(gameObject, 3f);
            return;
        }
    }
}
