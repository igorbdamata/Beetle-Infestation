using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public static GroundGenerator gg { get; private set; }
    [SerializeField] private GameObject defaultGroundPrefab;
    [SerializeField] private GameObject enemySpawnPointPrefab;
    [SerializeField] private Vector2 minPlatformSize;
    [SerializeField] private Vector2 maxPlatformSize;

    private void Awake()
    {
        if (!gg) { gg = this; FindObjectOfType<LevelGenerator>().GenerateLevel(); return; }
        Destroy(gameObject);
    }

    public Transform GenerateGround(Vector2 position)
    {
        GameObject ground = Instantiate(defaultGroundPrefab, position, transform.rotation);
        Vector2 platformSize = new Vector2(Random.Range(minPlatformSize.x, maxPlatformSize.x), Random.Range(minPlatformSize.y, maxPlatformSize.y));
        ground.GetComponent<SpriteRenderer>().size = platformSize;
        ground.GetComponent<BoxCollider2D>().size = platformSize;

        if (platformSize.x > 5)
        {
            GameObject spawnPoint = Instantiate(enemySpawnPointPrefab, ground.transform);
            spawnPoint.transform.localPosition = new Vector2(0, 0.2f + 0.5f * platformSize.y);
        }
        if (platformSize.x >= 15)
        {
            GameObject spawnPoint = Instantiate(enemySpawnPointPrefab, ground.transform);
            spawnPoint.transform.localPosition = new Vector2(platformSize.x / -2 + 1, 0.2f + 0.5f * platformSize.y);
        }

        return ground.transform;
    }
}
