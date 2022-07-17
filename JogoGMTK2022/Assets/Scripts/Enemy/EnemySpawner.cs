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
    [SerializeField] private int initialEnemies = 5;
    private List<Transform> spawnPointsUseds = new List<Transform>();

    private Transform player;

    private IEnumerator Start()
    {
        while (!GameController.gc.finishedGrounds) { yield return new WaitForEndOfFrame(); }
        if (GameObject.FindGameObjectsWithTag("EnemySpawnPoint").Length <= 1) { UI.ui.RetryLevel(); }
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawnPoint").Select(n => n.transform).ToList());
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < initialEnemies; i++)
        {
            SpawnEnemy();
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnEnemies());
    }

    public void OnEnemyDead(Transform enemy)
    {
        spawnPointsUseds.RemoveAt(GameController.gc.enemies.IndexOf(enemy));
        GameController.gc.enemies.Remove(enemy);
        SpawnEnemy();
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(GameController.gc.finishedAllLevel ? Random.Range(minSpawnCooldown, maxSpawnCooldown) : 0);
            while (GameController.gc.enemies.Count >= maxNumOfEnemies || spawnPointsUseds.Count == spawnPoints.Count)
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
    private void SpawnEnemy()
    {
        Transform spawnPoint = GetSpawnPoint();
        if (!spawnPoint) { return; }
        GameObject enemy = Instantiate(GetEnemyPrefab(), spawnPoint.position, transform.rotation);
        GameController.gc.enemies.Add(enemy.transform);
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
        Transform spawnPoint;
        float distance = 0;
        int securityLock = 0;
        List<Transform> pointsCanSpawn = spawnPoints.Where(n => !spawnPointsUseds.Contains(n)).ToList();
        do
        {
            if (GameController.gc.enemies.Count < 2 || !GameController.gc.finishedAllLevel)
            {
                Transform nearestSpawnPoint = null;
                float lowerDistance = 999;
                foreach (Transform t in pointsCanSpawn)
                {
                    distance = GC.d.GetDistance(t.position, player.position);
                    if (!nearestSpawnPoint || distance <= lowerDistance && distance > minSpawnDistance)
                    {
                        nearestSpawnPoint = t;
                        lowerDistance = distance;
                    }
                }
                spawnPointsUseds.Add(nearestSpawnPoint);
                spawnPoint = nearestSpawnPoint;
                break;
            }
            else
            {
                print("BBB");
                GameController.gc.finishedAllLevel = true;
                Debug.Log(spawnPoints.Count);

                spawnPoint = pointsCanSpawn[Random.Range(0, pointsCanSpawn.Count)];
            }
            distance = GC.d.GetDistance(spawnPoint.position, player.position);
            securityLock++;
            if (securityLock > 100)
            {
                print("Break2");
                break;
            }
        }
        while (distance < minSpawnDistance);
        spawnPointsUseds.Add(spawnPoint);
        return spawnPoint;
    }
}
