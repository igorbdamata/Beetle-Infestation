using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> enemiesPrefab = new List<GameObject>();
    [SerializeField] private List<int> enemiesSpawnTax = new List<int>();
    [SerializeField] private int maxNumOfEnemies = 30;

    [SerializeField] private float minSpawnCooldown = 10f;
    [SerializeField] private float maxSpawnCooldown = 30f;

    [SerializeField] private float minSpawnDistance = 10f;
    private List<Transform> initialSpawnedPoints = new List<Transform>();

    private Transform player;

    private IEnumerator Start()
    {
        while (!GameController.gc.finishedGrounds) { yield return new WaitForEndOfFrame(); }
        if (GameObject.FindGameObjectsWithTag("EnemySpawnPoint").Length <= 1) { UI.ui.RetryLevel(); }
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawnPoint").Select(n => n.transform).ToList());
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnEnemies());
    }

    public void OnEnemyDead()
    {
        SpawnEnemy();

    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(GameController.gc.finishedAllLevel ? Random.Range(minSpawnCooldown, maxSpawnCooldown) : 0);
            while (GameController.gc.enemies.Count >= maxNumOfEnemies) { yield return new WaitForSeconds(1f); }
        }
    }
    private void SpawnEnemy()
    {
        Transform spawnPoint = GetSpawnPoint();
        if (!spawnPoint) { return; }
        GameObject enemy = Instantiate(GetEnemyPrefab(), spawnPoint.position, transform.rotation);
    }

    GameObject GetEnemyPrefab()
    {
        int percent = Random.Range(1, 100);
        List<int> possiblePercents = new List<int>(enemiesSpawnTax.Where(n => n >= percent));
        List<int> possibleIDs = possiblePercents.Select(n => enemiesSpawnTax.IndexOf(n)).ToList();
        int ID = possibleIDs[Random.Range(0, possibleIDs.Count)];
        return enemiesPrefab[ID];
    }

    Transform GetSpawnPoint()
    {
        Transform spawnPoint = null;
        float distance = 0;
        int securityLock = 0;
        print(spawnPoints.Count);
        do
        {
            if (GameController.gc.enemies.Count < 2)
            {
                Transform nearestSpawnPoint = spawnPoints[0];
                float lowerDistance = GC.d.GetDistance(nearestSpawnPoint.position, player.position); ;
                foreach (Transform t in spawnPoints)
                {
                    distance = GC.d.GetDistance(t.position, player.position);
                    if (distance <= lowerDistance && distance <= minSpawnDistance && !initialSpawnedPoints.Contains(t))
                    {
                        nearestSpawnPoint = t;
                        lowerDistance = distance;
                    }
                }
                if (initialSpawnedPoints.Contains(nearestSpawnPoint)) 
                {
                    GameController.gc.finishedAllLevel = true;
                    initialSpawnedPoints.Clear();
                    return null; }
                initialSpawnedPoints.Add(nearestSpawnPoint);
                spawnPoint = nearestSpawnPoint;
            }
            else
            {
                GameController.gc.finishedAllLevel = true;
                initialSpawnedPoints.Clear();
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                distance = GC.d.GetDistance(spawnPoint.position, player.position);
            }
            securityLock++;
            if (securityLock > 100)
            {
                print("Break2");
                break;
            }
        }
        while (distance <= minSpawnDistance);
        return spawnPoint;
    }
}
