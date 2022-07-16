using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{

    [SerializeField] private int totalLife;
    [SerializeField] private int minScore = 1;
    [SerializeField] private int maxScore = 10;
    [SerializeField] private GameObject lifeBarFill;
    public int currentLife { get; private set; }

    public bool isDead { get; private set; }
    public bool inDamage { get; private set; }


    private void Start()
    {
        GetComponentInChildren<Canvas>().worldCamera = GameController.gc.mainCamera;
        currentLife = totalLife;
    }

    private void Update()
    {
        if (currentLife < totalLife)
        {
            lifeBarFill.transform.parent.gameObject.SetActive(true);
            lifeBarFill.GetComponent<Image>().fillAmount = (float)currentLife / (float)totalLife;
        }
        else
        {
            lifeBarFill.transform.parent.gameObject.SetActive(false);
        }
    }
    public void AddDamage(int damage)
    {
        currentLife -= damage;
        if (currentLife <= 0 && !isDead)
        {
            isDead = true;
            GameController.gc.playerScore += Random.Range(minScore, maxScore);
            Destroy(GetComponent<EnemyDamageOnTouch>());
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<EnemyMovement>());
            gameObject.layer = 0;
            FindObjectOfType<EnemySpawner>().OnEnemyDead(transform);
            Destroy(gameObject, 3f);
            return;
        }
        StopCoroutine(DamageCooldown());
        StartCoroutine(DamageCooldown());
    }

    IEnumerator DamageCooldown()
    {
        inDamage = true;
        yield return new WaitForSeconds(1f);
        inDamage = false;
    }
}